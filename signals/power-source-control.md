# Power Source Control

Request ID `0x750`, response ID `0x758`, address `0xE9` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50`), then `10 03` (reply starts `50`; a refusal is fine).

Read each signal with `21 LL`, the local identifier in the **Read** column (older ECU).

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Start Switch1 | `21 11` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Start Switch2 | `21 11` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Stop Light Switch1 | `21 11` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Steering Unlock Switch | `21 11` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Shift P Signal | `21 11` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG2 Relay Monitor(Outside) | `21 12` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG1 Relay Monitor(Outside) | `21 12` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ACC Relay Monitor | `21 12` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG2 Relay Monitor(Inside) | `21 12` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG1 Relay Monitor(Inside) | `21 12` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Latch Circuit | `21 12` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Starter Request Signal | `21 12` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Shift P Signal Pulse | `21 14` | 1..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Vehicle Speed Signal | `21 14` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Engine/EV System Condition | `21 14` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Ready Signal | `21 14` | 6..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Power Supply Condition | `21 15` | 0..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IGP Status | `21 15` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IGB Request | `21 15` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Remote A/C Power Supply Request | `21 15` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG1 Circuit | `21 16` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG2 Circuit | `21 16` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Starter SW Sig Mismatch | `21 16` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Park Signal Mismatch | `21 16` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Str Lock/Unlock Wait T-Out | `21 16` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Key Certif Wait T-Out | `21 16` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG Circuit | `21 16` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Timestamp LSB | `21 21` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG ON/OFF Count | `21 22` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Data Acquisition Time | `21 22` | 8..21 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Reset Count | `21 22` | 22..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Leave IG Status Structure | `21 29` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Leave IG Status Memory Count | `21 29` | 1..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Leave IG Status Storage Medium | `21 29` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Leave IG Status Start Address | `21 29` | 16..47 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Cause of Startability Malfunction Structure | `21 2B` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Cause of Startability Malfunction Memory Count | `21 2B` | 1..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Cause of Startability Malfunction Storage Medium | `21 2B` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Cause of Startability Malfunction Start Address | `21 2B` | 16..47 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Cause of Engine Stop IG ON Structure | `21 2C` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Cause of Engine Stop IG ON Memory Count | `21 2C` | 1..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Cause of Engine Stop IG ON Storage Medium | `21 2C` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Cause of Engine Stop IG ON Start Address | `21 2C` | 16..47 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Number of Diagnosis Code | `21 E1` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
