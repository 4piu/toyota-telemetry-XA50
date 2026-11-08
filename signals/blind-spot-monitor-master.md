# Blind Spot Monitor Master

Request ID `0x750`, response ID `0x758`, address `0x41` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50`), then `10 03` (reply starts `50`; a refusal is fine).

Read each signal with `21 LL`, the local identifier in the **Read** column (older ECU).

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Main Switch | `21 61` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Master Side Turn Switch | `21 61` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| RCTA Switch Status | `21 61` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Power Supply Voltage | `21 61` | 14..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Turn Switch State | `21 62` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Master Side Turn Signal | `21 62` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Reverse (Only for AT/CVT) | `21 62` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Indicator Light Control State | `21 62` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Vehicle Speed | `21 62` | 8..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Steering Angle | `21 62` | 28..39 | `raw` |  | N | Older ECU: formula and unit not established yet |
| VGRS Relative Angle | `21 62` | 42..55 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Warning Indicator Turning on(Master) | `21 69` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Warning Indicator Flashing(Master) | `21 69` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Main Switch Indicator Status | `21 69` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Warning Indicator Input Circuit Voltage | `21 69` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Warning Indicator Input Circuit Current | `21 69` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| HV/EV Flag | `21 6A` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| VGRS Flag | `21 6A` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| RCTA Equipped Information | `21 6A` | 32..32 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mounting FHL Function | `21 6A` | 33..33 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Outer Mirror Type | `21 6A` | 34..35 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Main SW Inspection Completeness Flag | `21 6B` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Beam Axis Inspection Completeness Flag | `21 6B` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Destination Undefined Flag | `21 6B` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Master Side RCTA Function | `21 70` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| FHL Function | `21 70` | 10..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| BSM Function | `21 70` | 11..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Setting of Warning Indicator Current (Dim OFF) | `21 72` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Setting of Warning Indicator Current (Dim ON) | `21 73` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mirror LED Duty Ratio (Blinking) | `21 74` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Master Side BSM Approaching Vehicle Detection Timing Setting | `21 7D` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| BSM Mirror Indicator Brightness | `21 7E` | 1..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Number of DTC | `21 E1` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
