# Pre-Collision 2

Request ID `0x791`, response ID `0x799`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Only observed constant |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Only observed constant |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw` | km | Y |  |
| Absolute Value Time (Year) | `22 01 09` | 0..7 | `raw` | Year | N | Only observed constant |
| Absolute Value Time (Month) | `22 01 09` | 8..15 | `raw` | Month | N | Only observed constant |
| Absolute Value Time (Day) | `22 01 09` | 16..23 | `raw` | Day | N | Only observed constant |
| Absolute Value Time (Hour) | `22 01 09` | 24..31 | `raw` | Hour | N | Only observed constant |
| Absolute Value Time (Minute) | `22 01 09` | 32..39 | `raw` | Minute | N | Only observed constant |
| Absolute Value Time (Second) | `22 01 09` | 40..47 | `raw` | Second | N | Only observed constant |
| PCS Function ON/OFF State | `22 10 04` | 8..8 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| PBA Request | `22 10 08` | 0..7 | `raw` | 0 = Not PBA Request; 1 = PBA Standby Request; 2 = PBA Request Level 1; 3 = PBA Request Level 2 | N | Only observed constant |
| VGRS Activation Request | `22 10 08` | 8..15 | `raw` | 0 = Not Request; 1 = VGRS 1 Request; 2 = VGRS 2 Request; 3 = VGRS 3 Request | N | Only observed constant |
| Warning Brake Request | `22 10 09` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| FPB Request | `22 10 09` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PB Request | `22 10 09` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| AVS Control Request | `22 10 09` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Prefill | `22 10 09` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PBR Request | `22 10 09` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PBH Request | `22 10 09` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LPB Request | `22 10 09` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS PBA Invalid | `22 10 0B` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS Warning Buzzer Invalid | `22 10 0B` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS PB Invalid | `22 10 0B` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Steering Combined PCS Invalid | `22 10 0B` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS Stereo Camera Invalid | `22 10 0B` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Front Side PCS PBA Invalid | `22 10 0B` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Front Side PCS Warning Buzzer Invalid | `22 10 0B` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| FCTA Invalid | `22 10 0B` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Acceleration Suppression at Low Speed (A Control) Invalid | `22 10 0B` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Emergency Steering Assist Invalid 1 | `22 10 0B` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Emergency Steering Assist Invalid 2 | `22 10 0B` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS Warning Display Request | `22 10 0E` | 0..7 | `raw` | 0 = Not Request; 1 = PCS Warning Display 1 Request; 2 = PCS Warning Display 2 Request; 3 = PCS Warning Display 3 Request | N | Only observed constant |
| PCS Warning Buzzer Request | `22 10 14` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Information (Conv/HV/EV) | `22 10 15` | 0..7 | `raw` | 0 = Unknown; 1 = Conv; 2 = HV/EV | N | Only observed constant |
| Vehicle Information (Powertrain Type) | `22 10 1C` | 0..7 | `raw` | 0 = Unknown; 1 = Conv; 2 = S&S; 3 = HV; 4 = PHV; 5 = EV; 6 = 1Motor HV; 7 = FCV | N | Only observed constant |
| Vehicle Information (Transmission Type) | `22 10 1D` | 0..7 | `raw` | 0 = MT; 1 = AT; 2 = CVT; 3 = MMT; 4 = SMT; 5 = HV/EV - AT; 15 = Unknown | N | Only observed constant |
| PCS Function ON/OFF State Stored | `22 20 01` | 4..4 | `raw` | 0 = Not Stored; 1 = Stored | N | Only observed constant |
| PCS Warning Timing State Stored 2 | `22 20 01` | 5..5 | `raw` | 0 = Not Stored; 1 = Stored | N | Only observed constant |
| PCS Warning Timing State Stored 3 | `22 20 01` | 6..6 | `raw` | 0 = Not Stored; 1 = Stored | N | Only observed constant |
| PCS Warning Timing State Stored 1 | `22 20 01` | 7..7 | `raw` | 0 = Not Stored; 1 = Stored | N | Only observed constant |
