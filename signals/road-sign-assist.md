# Road Sign Assist

Request ID `0x750`, response ID `0x758`, address `0x79` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Answers; not recorded yet |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Answers; not recorded yet |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Answers; not recorded yet |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Answers; not recorded yet |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw` |  | N | Answers; not recorded yet |
| Traffic Sign Estimated Speed Unit | `22 10 06` | 0..7 | `raw` | 0 = Unconfirmed; 1 = kph; 2 = mph | N | Answers; not recorded yet |
| Traffic Sign Estimated Traffic Division | `22 10 06` | 8..15 | `raw` | 0 = Unconfirmed; 2 = Right-hand traffic; 3 = Left-hand traffic | N | Answers; not recorded yet |
| $100C-1-$80 | `22 10 0C` | 8..8 | `raw` |  | N | Answers; not recorded yet |
| RSA Navigation Fusion Control | `22 10 0C` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
