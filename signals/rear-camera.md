# Rear Camera

Request ID `0x7A2`, response ID `0x7AA`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw * 625000 / 388357` | km | Y |  |
| Absolute Value Time (Year) | `22 01 09` | 0..7 | `raw` | Year | N | Only observed constant |
| Absolute Value Time (Month) | `22 01 09` | 8..15 | `raw` | Month | N | Only observed constant |
| Absolute Value Time (Day) | `22 01 09` | 16..23 | `raw` | Day | Y |  |
| Absolute Value Time (Hour) | `22 01 09` | 24..31 | `raw` | Hour | N | Only observed constant |
| Absolute Value Time (Minute) | `22 01 09` | 32..39 | `raw` | Minute | Y |  |
| Absolute Value Time (Second) | `22 01 09` | 40..47 | `raw` | Second | Y |  |
| Steering Angle | `22 10 01` | 0..15 | `raw * 0.1` | deg | Y |  |
| Vehicle Speed | `22 10 05` | 0..15 | `raw * 0.01` | km/h | Y |  |
