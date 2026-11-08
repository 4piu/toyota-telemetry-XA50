# Automatic High Beam

Request ID `0x750`, response ID `0x758`, address `0x7C` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Answers; not recorded yet |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Answers; not recorded yet |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Answers; not recorded yet |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Answers; not recorded yet |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw` |  | N | Answers; not recorded yet |
| Absolute Value Time (Year) | `22 01 09` | 0..7 | `raw` | Year | N | Answers; not recorded yet |
| Absolute Value Time (Month) | `22 01 09` | 8..15 | `raw` | Month | N | Answers; not recorded yet |
| Absolute Value Time (Day) | `22 01 09` | 16..23 | `raw` | Day | N | Answers; not recorded yet |
| Absolute Value Time (Hour) | `22 01 09` | 24..31 | `raw` | Hour | N | Answers; not recorded yet |
| Absolute Value Time (Minute) | `22 01 09` | 32..39 | `raw` | Minute | N | Answers; not recorded yet |
| Absolute Value Time (Second) | `22 01 09` | 40..47 | `raw` | Second | N | Answers; not recorded yet |
| AHB/AHS Information | `22 10 01` | 0..7 | `raw` | 0 = Unknown; 1 = AHB; 2 = AHS | N | Answers; not recorded yet |
