# Reading a signal

How to read any signal in the list over the OBD-II port. Everything here is
read-only: send only the requests shown on this page.

## Fill in the blanks

| Blank | Where to find it | Example |
|---|---|---|
| `<REQUEST ID>` | Top of the module's file in [`signals/`](signals/) | `0x7D2` |
| `<RESPONSE ID>` | Top of the module's file | `0x7DA` |
| `<ADDRESS>` | Top of the module's file, for modules behind the gateway only | `0x2A` |
| `<READ>` | The signal's **Read** column | `22 1F 9A` or `21 30` |
| `a..b` | The signal's **Bits** column | `32..47` |

The response ID is always the request ID plus 8. Most modules have no
address; skip everything about `<ADDRESS>` for them.

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

### Behind the gateway

Modules with an `<ADDRESS>` share request ID `0x750` and response ID `0x758`.
The first byte of **every** frame, in both directions, is the module's
address; the frame described above follows it, one byte shorter:

| Frame | Layout |
|---|---|
| Single frame | `<ADDRESS> 0L` + up to 6 bytes of message |
| First frame | `<ADDRESS> 1L LL` + the first 5 bytes |
| Consecutive frame | `<ADDRESS> 2n` + the next 6 bytes |
| Your flow control | `<ADDRESS> 30 00 00 00 00 00 00` |

Other modules may answer on `0x758` at the same time: ignore frames whose first
byte is not your `<ADDRESS>`.

## 3. Open a session

The top of each module's file lists the requests that open its session, in
order. Send them on `<REQUEST ID>` and wait for each reply on `<RESPONSE ID>`
before sending the next. Most modules use these three:

| Send | Reply starts with | Meaning |
|---|---|---|
| `02 3E 00 00 00 00 00 00` | `02 7E 00` | Tester present. A negative reply here is fine. |
| `02 10 01 00 00 00 00 00` | `06 50 01` | Default session. |
| `02 10 03 00 00 00 00 00` | `06 50 03` | Extended session. |

Older modules (those read with `21`) differ: they refuse tester present
(`7F 3E 12`, fine), some answer the default session with a bare `50`, some
refuse the extended session (fine), and some need nothing but tester present.
Follow the module's file.

Reading every few seconds keeps the session open. After a longer pause, or if
reads start getting negative replies, open it again.

## 4. Read

Send `<READ>` on `<REQUEST ID>` as a single frame, with `<ADDRESS>` first for a
module behind the gateway:

| Read | Request frame | Reply message |
|---|---|---|
| `22 HH LL` | `03 22 HH LL 00 00 00 00` | `62 HH LL <data ...>` |
| `21 LL` | `02 21 LL 00 00 00 00 00` | `61 LL <data ...>` |
| `21 LL`, address `2A` | `2A 02 21 LL 00 00 00 00` | `61 LL <data ...>` (in frames starting `2A`) |

`data` is everything after the echo: three bytes (`62 HH LL`) or two (`61 LL`).
Short replies arrive in a single frame; longer ones arrive as first frame plus
consecutive frames (section 2). Modules usually answer within 20 ms. Allow 1
second.

A negative reply is `7F 22 <code>` or `7F 21 <code>`:

| Code | Meaning | What to do |
|---|---|---|
| `78` | Still working | Keep waiting; the real reply follows (allow up to 10 s). |
| `31` | Not available | This module does not have it. |
| `11` | Request type not supported | Check the **Read** column: older modules need `21`. |
| `22`, `7E`, `7F` | Not allowed right now | Open the session again (section 3). |
| `12`, `13` | Wrong request | Check the request bytes. |

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
`62 HH LL 12 34 FF 38`:

- `data` is `12 34 FF 38`; bits 16–31 are bytes 2 and 3, so `raw = 0xFF38`.
- As a signed 16-bit number that is −200.
- −200 × 0.1 = **−20.0 A**.

Signals that share a **Read** request come from the same reply. One read gives
all of them.

## 6. Pace

- One request at a time per module: wait for the reply, or the timeout, before
  sending the next.
- Leave at least 100 ms between the starts of two requests to the same module.
  That gives up to 10 reads per second per module; about 8 is comfortable.
- Different modules can be read in turn on the same bus.

## Stay read-only

Send only `3E 00`, `10 01`, `10 03`, `22 HH LL`, and `21 LL`, plus the
flow-control frame. Other diagnostic services can change how the vehicle
behaves. Never use these readings to control the vehicle or as a safety
warning.
