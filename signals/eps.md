# EPS

Request ID `0x7A1`, response ID `0x7A9`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50`), then `10 03` (reply starts `50`; a refusal is fine).

Read each signal with `21 LL`, the local identifier in the **Read** column (older ECU).

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Meter Vehicle Velocity | `21 03` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wheel Speed Right | `21 04` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wheel Speed Left | `21 05` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Engine/Power Management ECU Pseudo Engine Revolution | `21 06` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor Actual Current | `21 07` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Command Value Current | `21 08` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Steering Angle Velocity | `21 09` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Thermistor Temperature | `21 0A` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG Power Supply | `21 0C` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Steering Angle Sens Sig | `21 0E` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Steering Angle | `21 0F` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Steering Wheel Torque | `21 15` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor Rotation Angle | `21 16` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Command Val Current 2 | `21 17` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor Voltage | `21 19` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| TRQ1 Zero Point Value | `21 24` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| TRQ2 Zero Point Value | `21 25` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Control Mode | `21 29` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG ON/OFF Times | `21 41` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor Overheat Record | `21 42` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor Lo Power Record | `21 44` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Eng Rev/System Start Inter Record | `21 45` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Str Angle Inter Record | `21 46` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Spd Sig Invalid Record | `21 47` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Battery Voltage Lo Record | `21 48` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Battery Control Count (Body ECU) | `21 4A` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| PS Assist Signal | `21 57` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Ready Status | `21 58` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor Actual Current 2 | `21 5A` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Torque Sensor 1 Output | `21 5D` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Torque Sensor 2 Output | `21 5D` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Torque Sensor 1 Power Supply | `21 5D` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Torque Sensor 2 Power Supply | `21 5D` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rotation Angle Sensor Output 1 | `21 5D` | 32..39 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rotation Angle Sensor Output 2 | `21 5D` | 40..47 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rotation Angle Sensor Output 3 | `21 5D` | 48..55 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rotation Angle Sensor Output 4 | `21 5D` | 56..63 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 1 Terminal Current (U) | `21 5E` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 1 Terminal Current (V) | `21 5E` | 16..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 1 Terminal Current (W) | `21 5E` | 32..47 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 2 Terminal Current (U) | `21 5E` | 48..63 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 2 Terminal Current (V) | `21 5E` | 64..79 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 2 Terminal Current (W) | `21 5E` | 80..95 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 1 U Phase Duty | `21 5E` | 96..111 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 1 V Phase Duty | `21 5E` | 112..127 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 1 W Phase Duty | `21 5E` | 128..143 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 2 U Phase Duty | `21 5E` | 144..159 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 2 V Phase Duty | `21 5E` | 160..175 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 2 W Phase Duty | `21 5E` | 176..191 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 1 Terminal Volt (U) | `21 5E` | 192..207 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 1 Terminal Volt (V) | `21 5E` | 208..223 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 1 Terminal Volt (W) | `21 5E` | 224..239 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 2 Terminal Volt (U) | `21 5E` | 240..255 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 2 Terminal Volt (V) | `21 5E` | 256..271 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 2 Terminal Volt (W) | `21 5E` | 272..287 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 1 PIG Power Supply | `21 5E` | 288..303 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor 2 PIG Power Supply | `21 5E` | 304..319 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor Overheat Limit Cnt(Normal) | `21 98` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Assist Limit Cnt(Power Voltage Low) | `21 98` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Power Vol Low Prevention Limit Cnt | `21 98` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Eng Stall/Ready OFF Cnt | `21 98` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Motor Overheat Limit Cnt(Motor Overload) | `21 98` | 32..39 | `raw` |  | N | Older ECU: formula and unit not established yet |
| DCDC Current Limit Cnt(DCDC Malf) | `21 98` | 40..47 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Assist Limit Cnt(Vehicle Spd Sig Malf) | `21 98` | 48..55 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Battery Voltage Keep Limit Cnt | `21 98` | 56..63 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Assist Limit Record 1 | `21 98` | 64..71 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Record 1 Key Cycle | `21 98` | 72..87 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Record 1 Key Cycle Elapsed Time | `21 98` | 91..119 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Assist Limit Record 2 | `21 98` | 136..143 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Record 2 Key Cycle | `21 98` | 144..159 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Record 2 Key Cycle Elapsed Time | `21 98` | 163..191 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Assist Limit Record 3 | `21 98` | 208..215 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Record 3 Key Cycle | `21 98` | 216..231 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Record 3 Key Cycle Elapsed Time | `21 98` | 235..263 | `raw` |  | N | Older ECU: formula and unit not established yet |
| The Number of DTCs | `21 E1` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
