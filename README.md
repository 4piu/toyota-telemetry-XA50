# Toyota RAV4 Hybrid (XA50) diagnostic signals

Live data that the ECUs of a 2023 RAV4 Hybrid (A25A-FXS, AWD) return over the
OBD-II port: 2,792 signals across 27 modules. For each one: its name, the
request that reads it, bit position, decoding formula, and unit or value
names.

Tested on one vehicle. Other model years, markets, and ECU software may differ.

## How to use

1. Find the signal in its module's file below, or in [`signals.csv`](signals.csv).
2. Fill the module's request and response IDs, its address if it has one, and
   the signal's **Read** request into the steps in [PROTOCOL.md](PROTOCOL.md).
3. Decode the reply with the signal's **Bits** and **Formula** columns.

## Modules

Modules with an **Address** sit behind the gateway and share request ID
`0x750`; the address byte selects the module. **Read** is the request type:
`22` (read by identifier) or `21` (read by local identifier, older ECUs).

| Module | Request ID | Response ID | Address | Read | Signals | Verified |
|---|---|---|---|---|---:|---:|
| [Engine](signals/engine.md) | `0x700` | `0x708` |  | `22` | 530 | 159 |
| [Rear Motor Generator](signals/rear-motor-generator.md) | `0x705` | `0x70D` |  | `22` | 84 | 45 |
| [Motor Generator](signals/motor-generator.md) | `0x724` | `0x72C` |  | `22` | 202 | 87 |
| [Front Radar Sensor](signals/front-radar-sensor.md) | `0x750` | `0x758` | `0x0F` | `22` | 67 | 0 |
| [Brake Booster](signals/brake-booster.md) | `0x750` | `0x758` | `0x29` | `22` | 135 | 0 |
| [Tire Pressure Monitor](signals/tire-pressure-monitor.md) | `0x750` | `0x758` | `0x2A` | `21` | 64 | 0 |
| [Main Body](signals/main-body.md) | `0x750` | `0x758` | `0x40` | `21` | 270 | 0 |
| [Blind Spot Monitor Master](signals/blind-spot-monitor-master.md) | `0x750` | `0x758` | `0x41` | `21` | 33 | 0 |
| [Blind Spot Monitor Slave](signals/blind-spot-monitor-slave.md) | `0x750` | `0x758` | `0x42` | `21` | 35 | 0 |
| [Front Recognition Camera](signals/front-recognition-camera.md) | `0x750` | `0x758` | `0x6D` | `22` | 35 | 0 |
| [Road Sign Assist](signals/road-sign-assist.md) | `0x750` | `0x758` | `0x79` | `22` | 9 | 0 |
| [Lane Departure Alert](signals/lane-departure-alert.md) | `0x750` | `0x758` | `0x7A` | `22` | 11 | 0 |
| [Automatic High Beam](signals/automatic-high-beam.md) | `0x750` | `0x758` | `0x7C` | `22` | 12 | 0 |
| [Sliding Roof](signals/sliding-roof.md) | `0x750` | `0x758` | `0xAD` | `21` | 42 | 0 |
| [Back Door](signals/back-door.md) | `0x750` | `0x758` | `0xB8` | `22` | 54 | 0 |
| [Telematics](signals/telematics.md) | `0x750` | `0x758` | `0xC7` | `22` | 71 | 0 |
| [Power Source Control](signals/power-source-control.md) | `0x750` | `0x758` | `0xE9` | `21` | 44 | 0 |
| [SRS Airbag](signals/srs-airbag.md) | `0x780` | `0x788` |  | `21` | 16 | 0 |
| [Radar Cruise2](signals/radar-cruise2.md) | `0x790` | `0x798` |  | `22` | 30 | 10 |
| [Pre-Collision 2](signals/pre-collision-2.md) | `0x791` | `0x799` |  | `22` | 42 | 1 |
| [EPS](signals/eps.md) | `0x7A1` | `0x7A9` |  | `21` | 75 | 0 |
| [Rear Camera](signals/rear-camera.md) | `0x7A2` | `0x7AA` |  | `22` | 13 | 8 |
| [Brake/EPB](signals/brake-epb.md) | `0x7B0` | `0x7B8` |  | `22` | 227 | 58 |
| [Steering Angle Sensor](signals/steering-angle-sensor.md) | `0x7B3` | `0x7BB` |  | `21` | 11 | 0 |
| [Combination Meter](signals/combination-meter.md) | `0x7C0` | `0x7C8` |  | `22` | 229 | 11 |
| [Air Conditioner](signals/air-conditioner.md) | `0x7C4` | `0x7CC` |  | `21` | 59 | 0 |
| [Hybrid Control](signals/hybrid-control.md) | `0x7D2` | `0x7DA` |  | `22` | 392 | 194 |

Engine also answers for Cruise Control, and Hybrid Control also answers for
Cruise Control and EV. Each has its own section in that module's file.

## Columns

| Column | Meaning |
|---|---|
| Signal | Name of the value. |
| Read | The request that returns it: `22 HH LL` (a data identifier) or `21 LL` (a local identifier). |
| Bits | Position `a..b` of the value in the reply data, as described in [PROTOCOL.md](PROTOCOL.md#5-decode). |
| Formula | Converts `raw`, the number held in those bits, to the value. |
| Unit / Values | The unit, and names for raw values where the signal has them (`0 = OFF; 1 = ON`). |
| Verified | **Y**: read from the vehicle and seen to change. **N**: the module answers the read, but the value has not been seen to change, so the formula is unconfirmed. |
| Comment | For N rows, why, where known. "Answers; not recorded yet": the read works, but the value has not been logged while driving. "Older ECU: formula and unit not established yet": the position is known, the conversion is not. |

Every signal in the list answered its read on this vehicle; reads the
modules refused are left out.

[`signals.csv`](signals.csv) holds the same rows, with the module name, its
request and response IDs, and its address (empty for most) on every row. Its
`values` column lists `raw = text` pairs separated by `;`.

## Read-only

Send only the requests in [PROTOCOL.md](PROTOCOL.md). Never use these readings
to control the vehicle or as a safety warning.
