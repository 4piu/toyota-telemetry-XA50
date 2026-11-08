# Radar Cruise2

Request ID `0x790`, response ID `0x798`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw` | km | Y |  |
| Absolute Value Time (Year) | `22 01 09` | 0..7 | `raw` | Year | N | Only observed constant |
| Absolute Value Time (Month) | `22 01 09` | 8..15 | `raw` | Month | N | Only observed constant |
| Absolute Value Time (Day) | `22 01 09` | 16..23 | `raw` | Day | N | Only observed constant |
| Absolute Value Time (Hour) | `22 01 09` | 24..31 | `raw` | Hour | N | Only observed constant |
| Absolute Value Time (Minute) | `22 01 09` | 32..39 | `raw` | Minute | N | Only observed constant |
| Absolute Value Time (Second) | `22 01 09` | 40..47 | `raw` | Second | N | Only observed constant |
| Cruise Control Main Switch | `22 10 08` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle-to-vehicle Distance Control Switch | `22 10 08` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Control Mode | `22 10 09` | 0..7 | `raw` | 1 = Vehicle-to-vehicle Distance Control (All Speed); 2 = Vehicle-to-vehicle Distance Control (Hi Speed); 3 = Constant Speed Control | N | Only observed constant |
| Vehicle Speed | `22 10 0A` | 0..15 | `raw * 0.01` | km/h | Y |  |
| Yaw Rate Sensor Signal | `22 10 0C` | 6..15 | `raw * 0.244 - 125` | deg/s | Y |  |
| Steering Angle Signal | `22 10 0D` | 4..15 | `raw * 1.5` | deg | Y |  |
| Estimated Vehicle Acceleration | `22 10 0F` | 0..7 | `raw * 0.04` | m/s2 | Y |  |
| Accelerator Pedal Idle | `22 10 10` | 8..8 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Cruise Warning Request | `22 10 12` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Radar Transmitting | `22 10 18` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Information (Conv/HV/EV) | `22 10 1B` | 0..7 | `raw` | 0 = Unknown; 1 = Conv; 2 = HV/EV | N | Only observed constant |
| Vehicle Information (Steering Wheel Position) | `22 10 1C` | 0..7 | `raw` | 0 = Unknown; 1 = Left; 2 = Right | N | Only observed constant |
| Key Cycle | `22 10 1E` | 0..15 | `raw` | trip | Y |  |
| Accelerator Over Ride | `22 10 1E` | 8..8 | `raw` | 0 = ON; 1 = OFF | Y |  |
| Vehicle Information (Powertrain Type) | `22 10 23` | 0..7 | `raw` | 0 = Unknown; 1 = Conv; 2 = S&S; 3 = HV; 4 = PHV; 5 = EV; 6 = 1Motor HV; 7 = FCV | N | Only observed constant |
| Front Radar Sensor High Temperature | `22 12 2B` | 8..8 | `raw` | 0 = Normal; 1 = High Temperature | N | Only observed constant |
| Front Radar Sensor Low Voltage | `22 12 2D` | 8..8 | `raw` | 0 = Normal; 1 = Low Voltage | N | Only observed constant |
| Radar Cruise Control System Malfunction | `22 13 FD` | 8..8 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| Front Radar Sensor Destination Information | `22 13 FF` | 0..7 | `raw` | 0 = 0; 1 = Unknown | N | Only observed constant |
