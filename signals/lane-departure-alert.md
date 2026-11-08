# Lane Departure Alert

Request ID `0x750`, response ID `0x758`, address `0x7A` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Answers; not recorded yet |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Answers; not recorded yet |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Answers; not recorded yet |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Answers; not recorded yet |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw` | km | N | Answers; not recorded yet |
| LDA Control | `22 10 02` | 8..8 | `raw` | 0 = Prohibit; 1 = Permit | N | Answers; not recorded yet |
| Turn Switch (Right) | `22 10 02` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Turn Switch (Left) | `22 10 02` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Lane Departure Alert Buzzer Sound Request | `22 10 02` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Vehicle Information (2WD/4WD) | `22 10 32` | 0..7 | `raw` | 0 = 2WD; 1 = 4WD; 3 = Unknown | N | Answers; not recorded yet |
| Vehicle Information (Conv/HV/EV) | `22 10 33` | 0..7 | `raw` | 0 = Conv; 1 = HV/EV; 3 = Unknown | N | Answers; not recorded yet |
