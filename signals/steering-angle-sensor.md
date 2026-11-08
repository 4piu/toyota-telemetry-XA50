# Steering Angle Sensor

Request ID `0x7B3`, response ID `0x7BB`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50`), then `10 03` (reply starts `50`; a refusal is fine).

Read each signal with `21 LL`, the local identifier in the **Read** column (older ECU).

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Light OFF Switch | `21 03` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto Light Switch | `21 03` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Head Light Switch (Tail) | `21 03` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Head Light Switch (Head) | `21 03` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| High Beam Main Switch | `21 03` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Passing Light Switch | `21 03` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto High Beam Main Switch | `21 03` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Automatic Light Equipped Info | `21 04` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wiper Spec Info | `21 04` | 2..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Bad Weather Mode Equipped Info | `21 04` | 4..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| The Number of DTCs | `21 E1` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
