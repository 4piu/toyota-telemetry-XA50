# Rear Camera

Request ID `0x7A2`, response ID `0x7AA`. Use these as `<REQUEST ID>` and `<RESPONSE ID>` in [PROTOCOL.md](../PROTOCOL.md).

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `0x0103` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw * 625000 / 388357` | km | Y |  |
| Absolute Value Time (Year) | `0x0109` | 0..7 | `raw` | Year | N | Only observed constant |
| Absolute Value Time (Month) | `0x0109` | 8..15 | `raw` | Month | N | Only observed constant |
| Absolute Value Time (Day) | `0x0109` | 16..23 | `raw` | Day | Y |  |
| Absolute Value Time (Hour) | `0x0109` | 24..31 | `raw` | Hour | N | Only observed constant |
| Absolute Value Time (Minute) | `0x0109` | 32..39 | `raw` | Minute | Y |  |
| Absolute Value Time (Second) | `0x0109` | 40..47 | `raw` | Second | Y |  |
| Steering Angle | `0x1001` | 0..15 | `raw * 0.1` | deg | Y |  |
| Vehicle Speed | `0x1005` | 0..15 | `raw * 0.01` | km/h | Y |  |
