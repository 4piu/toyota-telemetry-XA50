# Tire Pressure Monitor

Request ID `0x750`, response ID `0x758`, address `0x2A` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50`), then `10 03` (reply starts `50`; a refusal is fine).

Read each signal with `21 LL`, the local identifier in the **Read** column (older ECU).

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Mode Status | `21 04` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Main Tire | `21 06` | 0..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| 2nd Tire | `21 06` | 8..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Select Switch | `21 09` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Initialization Switch | `21 09` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Sensor ID Status | `21 09` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Vehicle Speed | `21 0A` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 1 Temperature in Tire | `21 16` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 2 Temperature in Tire | `21 16` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 3 Temperature in Tire | `21 16` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 4 Temperature in Tire | `21 16` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 5 Temperature in Tire | `21 16` | 32..39 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Select Switch Info | `21 1B` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Initialization Switch Info | `21 1B` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Sensor ID Switch Function | `21 1B` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Units By Region | `21 21` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 1 Tire Inflation Pressure | `21 30` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 2 Tire Inflation Pressure | `21 30` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 3 Tire Inflation Pressure | `21 30` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 4 Tire Inflation Pressure | `21 30` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 5 Tire Inflation Pressure | `21 30` | 32..39 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Threshold of Low-pressure (Lower Limit) | `21 31` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 1 Initial Threshold of Low-pressure | `21 32` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 2 Initial Threshold of Low-pressure | `21 32` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 3 Initial Threshold of Low-pressure | `21 32` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 4 Initial Threshold of Low-pressure | `21 32` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 5 Initial Threshold of Low-pressure | `21 32` | 32..39 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 1 Tire Position | `21 36` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 2 Tire Position | `21 36` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 3 Tire Position | `21 36` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 4 Tire Position | `21 36` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 5 Tire Position | `21 36` | 32..39 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 1 - FL Wheel Speed Synchronization Count | `21 41` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 1 - FR Wheel Speed Synchronization Count | `21 41` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 1 - RL Wheel Speed Synchronization Count | `21 41` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 1 - RR Wheel Speed Synchronization Count | `21 41` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 2 - FL Wheel Speed Synchronization Count | `21 42` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 2 - FR Wheel Speed Synchronization Count | `21 42` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 2 - RL Wheel Speed Synchronization Count | `21 42` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 2 - RR Wheel Speed Synchronization Count | `21 42` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 3 - FL Wheel Speed Synchronization Count | `21 43` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 3 - FR Wheel Speed Synchronization Count | `21 43` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 3 - RL Wheel Speed Synchronization Count | `21 43` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 3 - RR Wheel Speed Synchronization Count | `21 43` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 4 - FL Wheel Speed Synchronization Count | `21 44` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 4 - FR Wheel Speed Synchronization Count | `21 44` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 4 - RL Wheel Speed Synchronization Count | `21 44` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 4 - RR Wheel Speed Synchronization Count | `21 44` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 5 - FL Wheel Speed Synchronization Count | `21 45` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 5 - FR Wheel Speed Synchronization Count | `21 45` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 5 - RL Wheel Speed Synchronization Count | `21 45` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ID 5 - RR Wheel Speed Synchronization Count | `21 45` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Last Time ID Registered mode | `21 52` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| [FLASH] Auto ID Registered FL Wheel Judgment Status | `21 53` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| [FLASH] Auto ID Registered FR Wheel Judgment Status | `21 54` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| [FLASH] Auto ID Registered RL Wheel Judgment Status | `21 55` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| [FLASH] Auto ID Registered RR Wheel Judgment Status | `21 56` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| [FLASH] Auto ID Registered Spare Wheel Judgment Status | `21 57` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| [RAM] Auto ID Registered FL Wheel Judgment Status | `21 58` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| [RAM] Auto ID Registered FR Wheel Judgment Status | `21 59` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| [RAM] Auto ID Registered RL Wheel Judgment Status | `21 5A` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| [RAM] Auto ID Registered RR Wheel Judgment Status | `21 5B` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| [RAM] Auto ID Registered Spare Wheel Judgment Status | `21 5C` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Number of Trouble Code | `21 E1` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
