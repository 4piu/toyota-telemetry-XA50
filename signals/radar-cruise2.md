# Radar Cruise2

Request ID `0x790`, response ID `0x798`. Use these as `<REQUEST ID>` and `<RESPONSE ID>` in [PROTOCOL.md](../PROTOCOL.md).

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `0x0103` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw` | km | Y |  |
| Absolute Value Time (Year) | `0x0109` | 0..7 | `raw` | Year | N | Only observed constant |
| Absolute Value Time (Month) | `0x0109` | 8..15 | `raw` | Month | N | Only observed constant |
| Absolute Value Time (Day) | `0x0109` | 16..23 | `raw` | Day | N | Only observed constant |
| Absolute Value Time (Hour) | `0x0109` | 24..31 | `raw` | Hour | N | Only observed constant |
| Absolute Value Time (Minute) | `0x0109` | 32..39 | `raw` | Minute | N | Only observed constant |
| Absolute Value Time (Second) | `0x0109` | 40..47 | `raw` | Second | N | Only observed constant |
| Cruise Control Main Switch | `0x1008` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle-to-vehicle Distance Control Switch | `0x1008` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Control Mode | `0x1009` | 0..7 | `raw` | 1 = Vehicle-to-vehicle Distance Control (All Speed); 2 = Vehicle-to-vehicle Distance Control (Hi Speed); 3 = Constant Speed Control | N | Only observed constant |
| Vehicle Speed | `0x100A` | 0..15 | `raw * 0.01` | km/h | Y |  |
| Yaw Rate Sensor Signal | `0x100C` | 6..15 | `raw * 0.244 - 125` | deg/s | Y |  |
| Steering Angle Signal | `0x100D` | 4..15 | `raw * 1.5` | deg | Y |  |
| Estimated Vehicle Acceleration | `0x100F` | 0..7 | `raw * 0.04` | m/s2 | Y |  |
| Accelerator Pedal Idle | `0x1010` | 8..8 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Cruise Warning Request | `0x1012` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Radar Transmitting | `0x1018` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Information (Conv/HV/EV) | `0x101B` | 0..7 | `raw` | 0 = Unknown; 1 = Conv; 2 = HV/EV | N | Only observed constant |
| Vehicle Information (Steering Wheel Position) | `0x101C` | 0..7 | `raw` | 0 = Unknown; 1 = Left; 2 = Right | N | Only observed constant |
| Key Cycle | `0x101E` | 0..15 | `raw` | trip | Y |  |
| Accelerator Over Ride | `0x101E` | 8..8 | `raw` | 0 = ON; 1 = OFF | Y |  |
| Vehicle Information (Powertrain Type) | `0x1023` | 0..7 | `raw` | 0 = Unknown; 1 = Conv; 2 = S&S; 3 = HV; 4 = PHV; 5 = EV; 6 = 1Motor HV; 7 = FCV | N | Only observed constant |
| Front Radar Sensor High Temperature | `0x122B` | 8..8 | `raw` | 0 = Normal; 1 = High Temperature | N | Only observed constant |
| Front Radar Sensor Low Voltage | `0x122D` | 8..8 | `raw` | 0 = Normal; 1 = Low Voltage | N | Only observed constant |
| Radar Cruise Control System Malfunction | `0x13FD` | 8..8 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| Front Radar Sensor Destination Information | `0x13FF` | 0..7 | `raw` | 0 = 0; 1 = Unknown | N | Only observed constant |
