# Reading a signal

How to read any signal in the list over the OBD-II port. Everything here is
read-only: send only the requests shown on this page.

## Fill in the blanks

| Blank | Where to find it | Example |
|---|---|---|
| `<REQUEST ID>` | Top of the ECU's file in [`signals/`](signals/) | `0x7D2` |
| `<RESPONSE ID>` | Top of the ECU's file | `0x7DA` |
| `<DID HI> <DID LO>` | The signal's **DID** column, split into two bytes | `0x1F9A` → `1F 9A` |
| `a..b` | The signal's **Bits** column | `32..47` |

The response ID is always the request ID plus 8.

## 1. Connect

| OBD-II pin | Signal |
|---:|---|
| 6 | CAN-H |
| 14 | CAN-L |
| 5 | Signal ground |

- Classical CAN, **500 kbit/s**, 11-bit identifiers, 8-byte data frames.
- The adapter must be in normal mode, because it has to acknowledge frames.
  Listen-only shows nothing: the port carries no traffic until you ask.
- Do not add a terminating resistor. Do not power the adapter from pin 16.
- Ignition on (IG ON or READY).

To check the wiring, send `02 01 00 00 00 00 00 00` on `0x7DF`. An ECU answers
on `0x7E8`–`0x7EF` with `06 41 00 ...` within a few milliseconds.

## 2. Frames

Every message travels in 8-byte CAN frames (ISO-TP). Pad unused bytes with
`00`.

- **Single frame**, for messages of up to 7 bytes: first byte `0L`, where `L` is
  the message length, then the message. All requests on this page are single
  frames.
- **Long replies** arrive in several frames:
  1. The ECU sends a first frame: `1L LL` (12-bit message length) followed by
     the first 6 bytes of the message.
  2. You answer on `<REQUEST ID>` with flow control `30 00 00 00 00 00 00 00`
     (send the rest, no pauses).
  3. The ECU sends consecutive frames `21`, `22`, … `2F`, `20`, `21`, … each
     with the next 7 bytes, until the length is reached.

## 3. Open a session

Once per ECU, send these three on `<REQUEST ID>`. Wait for each reply on
`<RESPONSE ID>` before sending the next.

| Send | Reply starts with | Meaning |
|---|---|---|
| `02 3E 00 00 00 00 00 00` | `02 7E 00` | Tester present. A negative reply here is fine. |
| `02 10 01 00 00 00 00 00` | `06 50 01` | Default session. |
| `02 10 03 00 00 00 00 00` | `06 50 03` | Extended session. |

Reading every few seconds keeps the session open. After a longer pause, or if
reads start getting negative replies, send the three again.

## 4. Read

Send on `<REQUEST ID>`:

```text
03 22 <DID HI> <DID LO> 00 00 00 00
```

The reply on `<RESPONSE ID>` is the message:

```text
62 <DID HI> <DID LO> <data ...>
```

`data` is everything after those three bytes. Short replies arrive in a single
frame; longer ones arrive as first frame plus consecutive frames (section 2).
The ECU usually answers within 20 ms. Allow 1 second.

A negative reply is `7F 22 <code>`:

| Code | Meaning | What to do |
|---|---|---|
| `78` | Still working | Keep waiting; the real reply follows (allow up to 10 s). |
| `31` | DID not available | This ECU does not have it. |
| `22`, `7E`, `7F` | Not allowed right now | Open the session again (section 3). |
| `13` | Wrong length | Check the request bytes. |

## 5. Decode

1. **Take the bits.** Number the bits of `data` from 0, starting at the most
   significant bit of the first data byte: byte 0 holds bits 0–7, byte 1 holds
   bits 8–15, and so on. `raw` is bits `a` through `b` read as one unsigned
   big-endian number. In code:

   ```python
   n = int.from_bytes(data, "big")
   raw = (n >> (len(data) * 8 - 1 - b)) & ((1 << (b - a + 1)) - 1)
   ```

2. **Look up named values.** If the **Unit / Values** column lists `raw = text`
   pairs and `raw` is one of them, the reading is that text. Some numeric
   signals name only a few special values; for anything else, go on to step 3.
3. **Apply the formula.** Put `raw` into the **Formula** column.
   `signed(raw)` means `raw` as a two's-complement number over its own bit
   width. The result is in the **Unit** shown.

**Example.** Bits `16..31`, formula `signed(raw) * 0.1`, unit `A`, and the reply
`62 <DID HI> <DID LO> 12 34 FF 38`:

- `data` is `12 34 FF 38`; bits 16–31 are bytes 2 and 3, so `raw = 0xFF38`.
- As a signed 16-bit number that is −200.
- −200 × 0.1 = **−20.0 A**.

Signals that share a DID come from the same reply. One read gives all of them.

## 6. Pace

- One request at a time per ECU: wait for the reply, or the timeout, before
  sending the next.
- Leave at least 100 ms between the starts of two requests to the same ECU.
  That gives up to 10 reads per second per ECU; about 8 is comfortable.
- Different ECUs can be read in turn on the same bus.

## Stay read-only

Send only `3E 00`, `10 01`, `10 03`, and `22 <DID>`, plus the flow-control
frame. Other diagnostic services can change how the vehicle behaves. Never use
these readings to control the vehicle or as a safety warning.
