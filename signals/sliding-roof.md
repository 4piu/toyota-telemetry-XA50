# Sliding Roof

Request ID `0x750`, response ID `0x758`, address `0xAD` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine).

Read each signal with `21 LL`, the local identifier in the **Read** column (older ECU).

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Hall IC1 Status | `21 01` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Hall IC1 Pulse | `21 01` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Hall IC2 Status | `21 01` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Hall IC2 Pulse | `21 01` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Down Switch | `21 01` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Up Switch | `21 01` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Close Switch | `21 01` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Open Switch | `21 01` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wireless Open Operation | `21 01` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wireless Close Operation | `21 01` | 9..9 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Key Open Operation | `21 01` | 10..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Key Close Operation | `21 01` | 11..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Handle Lock Button | `21 01` | 12..12 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Courtesy | `21 01` | 13..13 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Ignition (MPX) | `21 01` | 14..14 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Ignition (Direct Signal) | `21 01` | 15..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Window Lock | `21 01` | 16..16 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Key Off Permission | `21 01` | 17..17 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Vent Switch | `21 01` | 23..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wireless Key Rel Operation | `21 02` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Key Related Operation | `21 02` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Handle Button Close | `21 02` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wireless Key Rel (Close) | `21 02` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wireless Key Rel (Open) | `21 02` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Key Related (Close) | `21 02` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Key Related (Open) | `21 02` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Lock Related Close | `21 02` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Down Switch Failure(Current) | `21 06` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Up Switch Failure(Current) | `21 06` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Close Switch Failure(Current) | `21 06` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Open Switch Failure(Current) | `21 06` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Down Switch Failure(Past) | `21 06` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Up Switch Failure(Past) | `21 06` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Close Switch Failure(Past) | `21 06` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Open Switch Failure(Past) | `21 06` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Close Operation Request | `21 07` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Open Operation Request | `21 07` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Return Operation Request | `21 07` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Suggestion Service Function | `21 07` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rear SW Auto Operation | `21 08` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Front SW Auto Operation | `21 08` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Number of Trouble Codes | `21 E1` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
