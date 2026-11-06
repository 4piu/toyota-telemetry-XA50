# Pre-Collision 2

Request ID `0x791`, response ID `0x799`. Use these as `<REQUEST ID>` and `<RESPONSE ID>` in [PROTOCOL.md](../PROTOCOL.md).

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | N | Only observed constant |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | N | Only observed constant |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `0x0103` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw` | km | Y |  |
| Absolute Value Time (Year) | `0x0109` | 0..7 | `raw` | Year | N | Only observed constant |
| Absolute Value Time (Month) | `0x0109` | 8..15 | `raw` | Month | N | Only observed constant |
| Absolute Value Time (Day) | `0x0109` | 16..23 | `raw` | Day | N | Only observed constant |
| Absolute Value Time (Hour) | `0x0109` | 24..31 | `raw` | Hour | N | Only observed constant |
| Absolute Value Time (Minute) | `0x0109` | 32..39 | `raw` | Minute | N | Only observed constant |
| Absolute Value Time (Second) | `0x0109` | 40..47 | `raw` | Second | N | Only observed constant |
| PCS Function ON/OFF State | `0x1004` | 8..8 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| PBA Request | `0x1008` | 0..7 | `raw` | 0 = Not PBA Request; 1 = PBA Standby Request; 2 = PBA Request Level 1; 3 = PBA Request Level 2 | N | Only observed constant |
| VGRS Activation Request | `0x1008` | 8..15 | `raw` | 0 = Not Request; 1 = VGRS 1 Request; 2 = VGRS 2 Request; 3 = VGRS 3 Request | N | Only observed constant |
| Warning Brake Request | `0x1009` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| FPB Request | `0x1009` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PB Request | `0x1009` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| AVS Control Request | `0x1009` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Prefill | `0x1009` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PBR Request | `0x1009` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PBH Request | `0x1009` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LPB Request | `0x1009` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS PBA Invalid | `0x100B` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS Warning Buzzer Invalid | `0x100B` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS PB Invalid | `0x100B` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Steering Combined PCS Invalid | `0x100B` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS Stereo Camera Invalid | `0x100B` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Front Side PCS PBA Invalid | `0x100B` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Front Side PCS Warning Buzzer Invalid | `0x100B` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| FCTA Invalid | `0x100B` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Acceleration Suppression at Low Speed (A Control) Invalid | `0x100B` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Emergency Steering Assist Invalid 1 | `0x100B` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Emergency Steering Assist Invalid 2 | `0x100B` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS Warning Display Request | `0x100E` | 0..7 | `raw` | 0 = Not Request; 1 = PCS Warning Display 1 Request; 2 = PCS Warning Display 2 Request; 3 = PCS Warning Display 3 Request | N | Only observed constant |
| PCS Warning Buzzer Request | `0x1014` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Information (Conv/HV/EV) | `0x1015` | 0..7 | `raw` | 0 = Unknown; 1 = Conv; 2 = HV/EV | N | Only observed constant |
| Vehicle Information (Powertrain Type) | `0x101C` | 0..7 | `raw` | 0 = Unknown; 1 = Conv; 2 = S&S; 3 = HV; 4 = PHV; 5 = EV; 6 = 1Motor HV; 7 = FCV | N | Only observed constant |
| Vehicle Information (Transmission Type) | `0x101D` | 0..7 | `raw` | 0 = MT; 1 = AT; 2 = CVT; 3 = MMT; 4 = SMT; 5 = HV/EV - AT; 15 = Unknown | N | Only observed constant |
| PCS Function ON/OFF State Stored | `0x2001` | 4..4 | `raw` | 0 = Not Stored; 1 = Stored | N | Only observed constant |
| PCS Warning Timing State Stored 2 | `0x2001` | 5..5 | `raw` | 0 = Not Stored; 1 = Stored | N | Only observed constant |
| PCS Warning Timing State Stored 3 | `0x2001` | 6..6 | `raw` | 0 = Not Stored; 1 = Stored | N | Only observed constant |
| PCS Warning Timing State Stored 1 | `0x2001` | 7..7 | `raw` | 0 = Not Stored; 1 = Stored | N | Only observed constant |
