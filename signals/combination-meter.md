# Combination Meter

Request ID `0x7C0`, response ID `0x7C8`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw * 625000 / 388357` | km | Y |  |
| ODO/TRIP Change Switch | `22 10 01` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Trip Reset Switch | `22 10 01` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Display Select Switch | `22 10 01` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Meter Day Mode Switch | `22 10 01` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Switch (UP) | `22 10 01` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Up/Down Switch | `22 10 01` | 12..13 | `raw` | 0 = OFF; 1 = DOWN; 2 = UP; 3 = UP/DOWN | N | Only observed constant |
| Light Control Switch (DOWN) | `22 10 01` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Switch (Single) | `22 10 01` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Lid Opener Switch | `22 10 01` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch H on Combination Meter | `22 10 01` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch M on Combination Meter | `22 10 01` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch 00 on Combination Meter | `22 10 01` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch on Combination Meter | `22 10 01` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Up) | `22 10 02` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Down) | `22 10 02` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Left) | `22 10 02` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Right) | `22 10 02` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Enter) | `22 10 02` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Back) | `22 10 02` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Menu) | `22 10 02` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| +B Voltage | `22 10 21` | 0..7 | `raw * 0.1` | V; 0 = 0.0; 1 = Unset | Y |  |
| Fuel Input | `22 10 22` | 0..15 | `raw * 0.01` | L; 0 = 0.00; 1 = Unset | Y |  |
| Rheostat value | `22 10 24` | 0..7 | `raw * 50 / 127` | %; 0 = 0.00; 1 = Unset | N | Only observed constant |
| Vehicle Speed Meter | `22 10 41` | 0..7 | `raw` | km/h; 0 = 0; 1 = Unset | Y |  |
| Engine RPM | `22 10 42` | 0..7 | `raw * 50` | r/min; 0 = 0; 1 = Unset | Y |  |
| Nonvolatile Memory Status | `22 10 A4` | 8..8 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| Sub Fuel Sender | `22 10 A6` | 8..8 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Washer Level Warning Switch | `22 11 01` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Driver Buckle Switch | `22 11 01` | 9..9 | `raw` | 0 = Fastened; 1 = Not Fastened | N | Only observed constant |
| Passenger Buckle Switch | `22 11 01` | 10..10 | `raw` | 0 = Fastened; 1 = Not Fastened | N | Only observed constant |
| Passenger Occupant Detection Switch | `22 11 01` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Passenger Buckle / Occupant Detection Warning Switch | `22 11 01` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Right Seat Buckle / Occupant Detection Warning Switch | `22 11 01` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Left Seat Buckle / Occupant Detection Warning Switch | `22 11 01` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Center Seat Buckle / Occupant Detection Warning Switch | `22 11 01` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Right Seat Buckle / Occupant Detection Warning Switch | `22 11 01` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Left Seat Buckle / Occupant Detection Warning Switch | `22 11 01` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Center Seat Buckle / Occupant Detection Warning Switch | `22 11 01` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Turn Signal Switch (Left) | `22 11 01` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Turn Signal Switch (Right) | `22 11 01` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Turn Switch Signal (Full Turn) | `22 11 01` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Hazard Flasher Switch | `22 11 01` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Right Seat Buckle / Occupant Detection Warning Switch | `22 11 01` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Left Seat Buckle / Occupant Detection Warning Switch | `22 11 01` | 40..40 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Center Seat Buckle / Occupant Detection Warning Switch | `22 11 01` | 41..41 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| CTR Seat Buckle Switch | `22 11 01` | 42..42 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| CTR Seat Occupant Detection Warning Switch | `22 11 01` | 43..43 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| CTR Seat Buckle / Occupant Detection Warning Switch | `22 11 01` | 44..44 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Lamp Switch | `22 11 01` | 45..45 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Ambient Temperature (Celsius) | `22 11 41` | 0..7 | `raw * 0.5 - 40` | ℃; 0 = -40.0; 1 = Unset | Y |  |
| Ambient Temperature (Fahrenheit) | `22 11 41` | 8..15 | `raw - 40` | F; 0 = -40; 1 = Unset | Y |  |
| Lighting System | `22 11 A1` | 8..8 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Back Door System | `22 11 A1` | 9..9 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Oil Pressure Switch | `22 12 01` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Oil Level Switch | `22 12 01` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Level Switch | `22 12 01` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Filter Switch | `22 12 01` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Coolant Temperature | `22 12 41` | 0..7 | `raw * 0.5` | ℃; 0 = 0.0; 1 = Unset | Y |  |
| Integrated value for Maintenance | `22 12 A1` | 0..7 | `raw * 100` | mile; 0 = 0; 1 = Unset | N | Only observed constant |
| TPMS System | `22 14 A1` | 8..8 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| VGRS System | `22 14 A1` | 9..9 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| VSC System | `22 14 A1` | 10..10 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| DRS System | `22 14 A1` | 11..11 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Pop Up Hood System | `22 14 A1` | 12..12 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| AVS System | `22 14 A1` | 13..13 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| LDA System | `22 15 A1` | 8..8 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| PCS System | `22 15 A1` | 9..9 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| ICS System | `22 15 A1` | 10..10 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Radar Cruise System | `22 15 A1` | 11..11 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Intuitive P/A System | `22 15 A1` | 12..12 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Road Sign Assist System | `22 15 A1` | 13..13 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| HV/EV System Indicator | `22 16 41` | 0..15 | `raw` | %; 0 = Unset; 1 = 511 | Y |  |
| ODO Display Time After IG OFF Adjust | `22 20 02` | 0..7 | `raw` | 0 = 30s; 1 = 60s; 2 = 600s; 3 = OFF | N | Only observed constant |
| Tail Remind Buzzer Function | `22 20 41` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Driver Seatbelt Warning Buzzer Function | `22 20 41` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Passenger Seatbelt Warning Buzzer Function | `22 20 41` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Front Center Seatbelt Warning Buzzer Function | `22 20 41` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Seatbelt Warning Buzzer Function | `22 20 41` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Right Seatbelt Warning Buzzer Function | `22 20 41` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Left Seatbelt Warning Buzzer Function | `22 20 41` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Center Seatbelt Warning Buzzer Function | `22 20 41` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Right Seatbelt Warning Buzzer Function | `22 20 41` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Left Seatbelt Warning Buzzer Function | `22 20 41` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Center Seatbelt Warning Buzzer Function | `22 20 41` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Right Seatbelt Warning Buzzer Function | `22 20 41` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Left Seatbelt Warning Buzzer Function | `22 20 41` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Center Seatbelt Warning Buzzer Function | `22 20 41` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Lamp Switch Function | `22 20 41` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Lane Change Flashing Times Adjust | `22 20 42` | 0..7 | `raw` | 0 = OFF; 1 = 3; 2 = 4; 3 = 5; 4 = 6; 5 = 7 | N | Only observed constant |
| Flasher Sound Volume Adjust | `22 20 43` | 0..7 | `raw` | 0 = Medium; 1 = Small; 2 = Large | N | Only observed constant |
| Fuel Gauge Operation (Sender E) | `22 28 03` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation | `22 28 03` | 8..15 | `raw` | 0 = OFF; 1 = Sender F; 2 = Full; 4 = 3/4; 8 = 1/2; 16 = 1/4; 32 = Warning; 64 = Empty; 128 = Sender E | N | Only observed constant |
| Fuel Gauge Operation (Empty) | `22 28 03` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (Warning) | `22 28 03` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (1/4) | `22 28 03` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (1/2) | `22 28 03` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (3/4) | `22 28 03` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (Full) | `22 28 03` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (Sender F) | `22 28 03` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (50C,122F) | `22 28 05` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation | `22 28 05` | 8..15 | `raw` | 0 = OFF; 1 = 127; 2 = 120; 4 = 110; 8 = Middle; 16 = 80; 32 = 70; 64 = 60; 128 = 50 | N | Only observed constant |
| Water Temperature Meter Operation (60C,140F) | `22 28 05` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (70C,158F) | `22 28 05` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (80C,176F) | `22 28 05` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (Middle) | `22 28 05` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (110C,230F) | `22 28 05` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (120C,248F) | `22 28 05` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (127C,260F) | `22 28 05` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (MIN) | `22 28 08` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator | `22 28 08` | 8..15 | `raw` | 0 = OFF; 1 = MAX; 2 = 400%; 4 = 300%; 8 = 200%; 16 = 100%; 32 = 0%; 64 = -50%; 128 = MIN | N | Only observed constant |
| HV/EV System Indicator (-50%) | `22 28 08` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (0%) | `22 28 08` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (100%) | `22 28 08` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (200%) | `22 28 08` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (300%) | `22 28 08` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (400%) | `22 28 08` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (MAX) | `22 28 08` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Buzzer Operation (800Hz, Vol L, W/ Damping) | `22 28 21` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Buzzer Operation (800Hz, Vol S, W/ Damping) | `22 28 23` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Buzzer Operation (800Hz, Vol S, W/O Damping) | `22 28 24` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Buzzer Operation (1.6kHz, Vol S, W/O Damping) | `22 28 25` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Buzzer Operation (2.4kHz, Vol L, W/O Damping) | `22 28 27` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Multi Display All (White) | `22 28 31` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Multi Display Default Screen | `22 28 31` | 9..9 | `raw` | 1 = ON | N | Only observed constant |
| Fuel Gauge Indicator | `22 29 01` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Over Speed Indicator | `22 29 01` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Master Warning | `22 29 01` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Stop Warning | `22 29 01` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Go to Service Warning | `22 29 01` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| High Beam Indicator | `22 29 11` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Headlamp Indicator | `22 29 11` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Taillamp Indicator | `22 29 11` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Front Fog Lamp Indicator | `22 29 11` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Fog Lamp Indicator | `22 29 11` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Auto Leveling Headlamp Indicator | `22 29 11` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Automatic High Beam Indicator (Green) | `22 29 11` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Automatic High Beam Indicator (Yellow) | `22 29 11` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| AFS OFF Indicator | `22 29 11` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LED Headlamp Warning | `22 29 11` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Light Indicator | `22 29 11` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Left Turn Signal Indicator | `22 29 11` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Right Turn Signal Indicator | `22 29 11` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Driver Side Seat Belt Warning | `22 29 11` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Passenger Side Seat Belt Warning | `22 29 11` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Seat Belt Warning | `22 29 11` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Washer Warning | `22 29 12` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Door Open Warning | `22 29 12` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Airbag Warning | `22 29 12` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Pedestrian Protection Warning | `22 29 12` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Smart System Indicator | `22 29 12` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Smart System Warning | `22 29 12` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Head Up Display Indicator | `22 29 12` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Auto Air Conditioner Indicator | `22 29 12` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Emergency Exit Warning | `22 29 12` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| RSCA OFF Indicator | `22 29 12` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Weather Light Indicator | `22 29 12` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Brake Warning | `22 29 41` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Park Warning | `22 29 41` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Warning | `22 29 41` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECB Warning | `22 29 41` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| EPB Auto OFF Indicator | `22 29 41` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Slip Warning | `22 29 41` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VSC OFF Indicator | `22 29 41` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VSC TRC Indicator | `22 29 41` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ACTIVE TRC Indicator | `22 29 41` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ACTIVE TRC Warning | `22 29 41` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Brake Hold Indicator | `22 29 41` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Hold Indicator | `22 29 41` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Downhill Assist Control Indicator | `22 29 41` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Downhill Assist Control Warning | `22 29 41` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| CRAWL Indicator | `22 29 41` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| CRAWL Warning | `22 29 41` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS OFF Warning | `22 29 51` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS Warning | `22 29 51` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Radar Cruise Indicator | `22 29 51` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Radar Cruise Indicator (Yellow) | `22 29 51` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Indicator | `22 29 51` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Indicator (Yellow) | `22 29 51` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SET Indicator | `22 29 51` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LDA Steering Control Indicator | `22 29 51` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drift From The Lane Indicator | `22 29 51` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drift From The Lane Warning | `22 29 51` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LDA Indicator | `22 29 51` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LDA Warning | `22 29 51` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Left LDA Indicator | `22 29 51` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Left LDA Warning | `22 29 51` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Right LDA Indicator | `22 29 51` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Right LDA Warning | `22 29 51` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| All Indicator | `22 29 81` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Auxiliary Battery Warning | `22 29 81` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (0) | `22 2A 01` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input | `22 2A 01` | 8..15 | `raw` | 0 = OFF; 1 = 280; 2 = 240; 4 = 200; 8 = 160; 16 = 120; 32 = 80; 64 = 40; 128 = 0 | N | Only observed constant |
| Speed Signal Input (40) | `22 2A 01` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (80) | `22 2A 01` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (120) | `22 2A 01` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (160) | `22 2A 01` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (200) | `22 2A 01` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (240) | `22 2A 01` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (280) | `22 2A 01` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (320) | `22 2A 01` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input 2 | `22 2A 01` | 24..26 | `raw` | 0 = OFF; 1 = 400; 2 = 360; 4 = 320 | N | Only observed constant |
| Speed Signal Input (360) | `22 2A 01` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (400) | `22 2A 01` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ODO/TRIP Change Switch | `22 2A 21` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Trip Reset Switch | `22 2A 21` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Display Select Switch | `22 2A 21` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Meter Day Mode Switch | `22 2A 21` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Switch (UP) | `22 2A 21` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Up/Down Switch | `22 2A 21` | 12..13 | `raw` | 0 = OFF; 1 = DOWN; 2 = UP | N | Only observed constant |
| Light Control Switch (DOWN) | `22 2A 21` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Switch (Single) | `22 2A 21` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch (H) | `22 2A 21` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch | `22 2A 21` | 24..26 | `raw` | 0 = OFF; 1 = 00; 2 = M; 4 = H | N | Only observed constant |
| Timer Switch (M) | `22 2A 21` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch (00) | `22 2A 21` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch (Single) | `22 2A 21` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Up) | `22 2A 22` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch | `22 2A 22` | 8..14 | `raw` | 0 = OFF; 1 = Menu; 2 = Back; 4 = Enter; 8 = Right; 16 = Left; 32 = Down; 64 = Up | N | Only observed constant |
| Multi Switch (Down) | `22 2A 22` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Left) | `22 2A 22` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Right) | `22 2A 22` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Enter) | `22 2A 22` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Back) | `22 2A 22` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Top) | `22 2A 22` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
