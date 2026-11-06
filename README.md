# Toyota RAV4 Hybrid (XA50) diagnostic signals

Live data that the ECUs of a 2023 RAV4 Hybrid (A25A-FXS, AWD) return over the
OBD-II port: 1,751 signals across 9 ECUs. For each one: its name, DID, bit
position, decoding formula, and unit or value names.

Tested on one vehicle. Other model years, markets, and ECU software may differ.

## How to use

1. Find the signal in its ECU's file below, or in [`signals.csv`](signals.csv).
2. Fill the ECU's request and response IDs and the signal's DID into the steps
   in [PROTOCOL.md](PROTOCOL.md).
3. Decode the reply with the signal's **Bits** and **Formula** columns.

## ECUs

| ECU | Request ID | Response ID | Signals | Verified |
|---|---|---|---:|---:|
| [Engine](signals/engine.md) | `0x700` | `0x708` | 530 | 159 |
| [Rear Motor Generator](signals/rear-motor-generator.md) | `0x705` | `0x70D` | 84 | 45 |
| [Motor Generator](signals/motor-generator.md) | `0x724` | `0x72C` | 202 | 87 |
| [Radar Cruise2](signals/radar-cruise2.md) | `0x790` | `0x798` | 30 | 10 |
| [Pre-Collision 2](signals/pre-collision-2.md) | `0x791` | `0x799` | 42 | 1 |
| [Rear Camera](signals/rear-camera.md) | `0x7A2` | `0x7AA` | 13 | 8 |
| [Brake/EPB](signals/brake-epb.md) | `0x7B0` | `0x7B8` | 227 | 58 |
| [Combination Meter](signals/combination-meter.md) | `0x7C0` | `0x7C8` | 229 | 11 |
| [Hybrid Control](signals/hybrid-control.md) | `0x7D2` | `0x7DA` | 394 | 194 |

Engine also answers for Cruise Control, and Hybrid Control also answers for
Cruise Control and EV. Each has its own section in that ECU's file.

## Columns

| Column | Meaning |
|---|---|
| Signal | Name of the value. |
| DID | Data identifier to read with `22 <DID>`. |
| Bits | Position `a..b` of the value in the reply data, as described in [PROTOCOL.md](PROTOCOL.md#5-decode). |
| Formula | Converts `raw`, the number held in those bits, to the value. |
| Unit / Values | The unit, and names for raw values where the signal has them (`0 = OFF; 1 = ON`). |
| Verified | **Y**: read from the vehicle and seen to change. **N**: the ECU answers, but the value was only seen constant, so the formula is unconfirmed. |
| Comment | For N rows, why the value stayed constant, where known. |

[`signals.csv`](signals.csv) holds the same rows, with the ECU name and its
request and response IDs on every row. Its `values` column lists
`raw = text` pairs separated by `;`.

## Read-only

Send only the requests in [PROTOCOL.md](PROTOCOL.md). Never use these readings
to control the vehicle or as a safety warning.
