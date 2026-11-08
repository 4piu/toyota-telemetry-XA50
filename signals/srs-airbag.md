# SRS Airbag

Request ID `0x780`, response ID `0x788`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50`), then `10 03` (reply starts `50`; a refusal is fine).

Read each signal with `21 LL`, the local identifier in the **Read** column (older ECU).

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Driver Seat Position | `21 04` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Passenger Classification | `21 04` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Passenger Detection | `21 04` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Right side Buckle SW | `21 04` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Display Type Information | `21 05` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Number of Past DTC | `21 E1` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Second) | `21 E1` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Key Cycle | `21 E1` | 16..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Minute) | `21 E1` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Clock Type | `21 E1` | 32..34 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Hour) | `21 E1` | 32..39 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG-ON Elapsed Time for Independent | `21 E1` | 35..63 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Month) | `21 E1` | 40..47 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG-ON Elapsed Time | `21 E1` | 40..63 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Day) | `21 E1` | 48..55 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Year) | `21 E1` | 56..63 | `raw` |  | N | Older ECU: formula and unit not established yet |
