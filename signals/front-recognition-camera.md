# Front Recognition Camera

Request ID `0x750`, response ID `0x758`, address `0x6D` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Answers; not recorded yet |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Answers; not recorded yet |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Answers; not recorded yet |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Answers; not recorded yet |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw` | km | N | Answers; not recorded yet |
| $1005-1-$80 | `22 10 05` | 8..8 | `raw` | 0 = Incomplete; 1 = Complete | N | Answers; not recorded yet |
| $1005-1-$40 | `22 10 05` | 9..9 | `raw` | 0 = Incomplete; 1 = Complete | N | Answers; not recorded yet |
| Vehicle Information (Steering Wheel Position) | `22 10 15` | 0..7 | `raw` | 0 = Right; 1 = Left; 2 = Right | N | Answers; not recorded yet |
| HV/EV Equipped Information | `22 10 1C` | 8..8 | `raw` | 0 = Non-HV/EV; 1 = HV/EV | N | Answers; not recorded yet |
| Key Cycle | `22 10 1E` | 0..15 | `raw` | trip | N | Answers; not recorded yet |
| Buzzer (Line) Equipped Information | `22 10 1E` | 8..8 | `raw` | 0 = Absent; 1 = Present | N | Answers; not recorded yet |
| Online Calibration Angle (Horizontal) | `22 10 2B` | 0..15 | `raw * 0.01 - 327.68` | deg | N | Answers; not recorded yet |
| Online Calibration Angle (Vertical) | `22 10 2B` | 16..31 | `raw * 0.01 - 327.68` | deg | N | Answers; not recorded yet |
| Online Calibration Angle (Roll Angle) | `22 10 2B` | 32..47 | `raw * 0.01 - 327.68` | deg | N | Answers; not recorded yet |
| Occurrence Times of Front Recognition Camera Optical Axis Misalignment | `22 10 D1` | 0..7 | `raw` |  | N | Answers; not recorded yet |
| Weather Condition (Heavy Fog) | `22 10 D2` | 8..8 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Weather Condition (Heavy Snow / Heavy Rain) | `22 10 D2` | 9..9 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Vehicle Speed | `22 10 D3` | 0..7 | `raw` | km/h | N | Answers; not recorded yet |
| Front Recognition Camera Axis Adjusted Status | `22 11 01` | 8..8 | `raw` | 0 = Complete; 1 = Not Complete | N | Answers; not recorded yet |
| Front Recognition Camera Low Voltage | `22 11 08` | 8..8 | `raw` | 0 = Not Low Voltage; 1 = Low Voltage | N | Answers; not recorded yet |
| Front Recognition Camera High Voltage | `22 11 08` | 9..9 | `raw` | 0 = Not High Voltage; 1 = High Voltage | N | Answers; not recorded yet |
| Front Recognition Camera Low Temperature | `22 11 09` | 8..8 | `raw` | 0 = Not Low Temperature; 1 = Low Temperature | N | Answers; not recorded yet |
| Front Recognition Camera High Temperature 1 | `22 11 09` | 9..9 | `raw` | 0 = Not High Temperature; 1 = High Temperature | N | Answers; not recorded yet |
| Front Recognition Camera High Temperature 2 | `22 11 09` | 10..10 | `raw` | 0 = Not High Temperature; 1 = High Temperature | N | Answers; not recorded yet |
| Front Recognition Camera Optical Axis Misalignment (X) | `22 11 0A` | 9..9 | `raw` | 0 = Not Axis Misalignment; 1 = Axis Misalignment | N | Answers; not recorded yet |
| Front Recognition Camera Optical Axis Misalignment (Y) | `22 11 0A` | 10..10 | `raw` | 0 = Not Axis Misalignment; 1 = Axis Misalignment | N | Answers; not recorded yet |
| Front Recognition Camera Roll Angle Optical Axis Misalignment | `22 11 0A` | 11..11 | `raw` | 0 = Not Axis Misalignment; 1 = Axis Misalignment | N | Answers; not recorded yet |
| Online Axis Alignment Error Code 31 | `22 11 18` | 8..8 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Online Axis Alignment Error Code 32 | `22 11 18` | 9..9 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Online Axis Alignment Error Code 33 | `22 11 18` | 10..10 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Online Axis Alignment Error Code 34 | `22 11 18` | 11..11 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Online Axis Alignment Error Code 35 | `22 11 18` | 12..12 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Heater Available Information | `22 13 01` | 8..8 | `raw` | 0 = Absence; 1 = Presence | N | Answers; not recorded yet |
| Heater Control Permit | `22 13 02` | 8..8 | `raw` | 0 = Not Allow; 1 = Allow | N | Answers; not recorded yet |
| Heater Control | `22 13 0A` | 8..8 | `raw` | 0 = Not Control; 1 = Under Control | N | Answers; not recorded yet |
