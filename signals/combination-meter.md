# Combination Meter

Request ID `0x7C0`, response ID `0x7C8`. Use these as `<REQUEST ID>` and `<RESPONSE ID>` in [PROTOCOL.md](../PROTOCOL.md).

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `0x0103` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw * 625000 / 388357` | km | Y |  |
| ODO/TRIP Change Switch | `0x1001` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Trip Reset Switch | `0x1001` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Display Select Switch | `0x1001` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Meter Day Mode Switch | `0x1001` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Switch (UP) | `0x1001` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Up/Down Switch | `0x1001` | 12..13 | `raw` | 0 = OFF; 1 = DOWN; 2 = UP; 3 = UP/DOWN | N | Only observed constant |
| Light Control Switch (DOWN) | `0x1001` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Switch (Single) | `0x1001` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Lid Opener Switch | `0x1001` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch H on Combination Meter | `0x1001` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch M on Combination Meter | `0x1001` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch 00 on Combination Meter | `0x1001` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch on Combination Meter | `0x1001` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Up) | `0x1002` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Down) | `0x1002` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Left) | `0x1002` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Right) | `0x1002` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Enter) | `0x1002` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Back) | `0x1002` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Menu) | `0x1002` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| +B Voltage | `0x1021` | 0..7 | `raw * 0.1` | V; 0 = 0.0; 1 = Unset | Y |  |
| Fuel Input | `0x1022` | 0..15 | `raw * 0.01` | L; 0 = 0.00; 1 = Unset | Y |  |
| Rheostat value | `0x1024` | 0..7 | `raw * 50 / 127` | %; 0 = 0.00; 1 = Unset | N | Only observed constant |
| Vehicle Speed Meter | `0x1041` | 0..7 | `raw` | km/h; 0 = 0; 1 = Unset | Y |  |
| Engine RPM | `0x1042` | 0..7 | `raw * 50` | r/min; 0 = 0; 1 = Unset | Y |  |
| Nonvolatile Memory Status | `0x10A4` | 8..8 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| Sub Fuel Sender | `0x10A6` | 8..8 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Washer Level Warning Switch | `0x1101` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Driver Buckle Switch | `0x1101` | 9..9 | `raw` | 0 = Fastened; 1 = Not Fastened | N | Only observed constant |
| Passenger Buckle Switch | `0x1101` | 10..10 | `raw` | 0 = Fastened; 1 = Not Fastened | N | Only observed constant |
| Passenger Occupant Detection Switch | `0x1101` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Passenger Buckle / Occupant Detection Warning Switch | `0x1101` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Right Seat Buckle / Occupant Detection Warning Switch | `0x1101` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Left Seat Buckle / Occupant Detection Warning Switch | `0x1101` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Center Seat Buckle / Occupant Detection Warning Switch | `0x1101` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Right Seat Buckle / Occupant Detection Warning Switch | `0x1101` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Left Seat Buckle / Occupant Detection Warning Switch | `0x1101` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Center Seat Buckle / Occupant Detection Warning Switch | `0x1101` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Turn Signal Switch (Left) | `0x1101` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Turn Signal Switch (Right) | `0x1101` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Turn Switch Signal (Full Turn) | `0x1101` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Hazard Flasher Switch | `0x1101` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Right Seat Buckle / Occupant Detection Warning Switch | `0x1101` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Left Seat Buckle / Occupant Detection Warning Switch | `0x1101` | 40..40 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Center Seat Buckle / Occupant Detection Warning Switch | `0x1101` | 41..41 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| CTR Seat Buckle Switch | `0x1101` | 42..42 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| CTR Seat Occupant Detection Warning Switch | `0x1101` | 43..43 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| CTR Seat Buckle / Occupant Detection Warning Switch | `0x1101` | 44..44 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Lamp Switch | `0x1101` | 45..45 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Ambient Temperature (Celsius) | `0x1141` | 0..7 | `raw * 0.5 - 40` | ℃; 0 = -40.0; 1 = Unset | Y |  |
| Ambient Temperature (Fahrenheit) | `0x1141` | 8..15 | `raw - 40` | F; 0 = -40; 1 = Unset | Y |  |
| Lighting System | `0x11A1` | 8..8 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Back Door System | `0x11A1` | 9..9 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Oil Pressure Switch | `0x1201` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Oil Level Switch | `0x1201` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Level Switch | `0x1201` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Filter Switch | `0x1201` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Coolant Temperature | `0x1241` | 0..7 | `raw * 0.5` | ℃; 0 = 0.0; 1 = Unset | Y |  |
| Integrated value for Maintenance | `0x12A1` | 0..7 | `raw * 100` | mile; 0 = 0; 1 = Unset | N | Only observed constant |
| TPMS System | `0x14A1` | 8..8 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| VGRS System | `0x14A1` | 9..9 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| VSC System | `0x14A1` | 10..10 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| DRS System | `0x14A1` | 11..11 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Pop Up Hood System | `0x14A1` | 12..12 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| AVS System | `0x14A1` | 13..13 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| LDA System | `0x15A1` | 8..8 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| PCS System | `0x15A1` | 9..9 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| ICS System | `0x15A1` | 10..10 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Radar Cruise System | `0x15A1` | 11..11 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Intuitive P/A System | `0x15A1` | 12..12 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| Road Sign Assist System | `0x15A1` | 13..13 | `raw` | 0 = Without; 1 = With | N | Only observed constant |
| HV/EV System Indicator | `0x1641` | 0..15 | `raw` | %; 0 = Unset; 1 = 511 | Y |  |
| ODO Display Time After IG OFF Adjust | `0x2002` | 0..7 | `raw` | 0 = 30s; 1 = 60s; 2 = 600s; 3 = OFF | N | Only observed constant |
| Tail Remind Buzzer Function | `0x2041` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Driver Seatbelt Warning Buzzer Function | `0x2041` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Passenger Seatbelt Warning Buzzer Function | `0x2041` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Front Center Seatbelt Warning Buzzer Function | `0x2041` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Seatbelt Warning Buzzer Function | `0x2041` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Right Seatbelt Warning Buzzer Function | `0x2041` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Left Seatbelt Warning Buzzer Function | `0x2041` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Center Seatbelt Warning Buzzer Function | `0x2041` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Right Seatbelt Warning Buzzer Function | `0x2041` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Left Seatbelt Warning Buzzer Function | `0x2041` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 3rd Center Seatbelt Warning Buzzer Function | `0x2041` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Right Seatbelt Warning Buzzer Function | `0x2041` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Left Seatbelt Warning Buzzer Function | `0x2041` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4th Center Seatbelt Warning Buzzer Function | `0x2041` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Lamp Switch Function | `0x2041` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Lane Change Flashing Times Adjust | `0x2042` | 0..7 | `raw` | 0 = OFF; 1 = 3; 2 = 4; 3 = 5; 4 = 6; 5 = 7 | N | Only observed constant |
| Flasher Sound Volume Adjust | `0x2043` | 0..7 | `raw` | 0 = Medium; 1 = Small; 2 = Large | N | Only observed constant |
| Fuel Gauge Operation (Sender E) | `0x2803` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation | `0x2803` | 8..15 | `raw` | 0 = OFF; 1 = Sender F; 2 = Full; 4 = 3/4; 8 = 1/2; 16 = 1/4; 32 = Warning; 64 = Empty; 128 = Sender E | N | Only observed constant |
| Fuel Gauge Operation (Empty) | `0x2803` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (Warning) | `0x2803` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (1/4) | `0x2803` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (1/2) | `0x2803` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (3/4) | `0x2803` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (Full) | `0x2803` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Gauge Operation (Sender F) | `0x2803` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (50C,122F) | `0x2805` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation | `0x2805` | 8..15 | `raw` | 0 = OFF; 1 = 127; 2 = 120; 4 = 110; 8 = Middle; 16 = 80; 32 = 70; 64 = 60; 128 = 50 | N | Only observed constant |
| Water Temperature Meter Operation (60C,140F) | `0x2805` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (70C,158F) | `0x2805` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (80C,176F) | `0x2805` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (Middle) | `0x2805` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (110C,230F) | `0x2805` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (120C,248F) | `0x2805` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Water Temperature Meter Operation (127C,260F) | `0x2805` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (MIN) | `0x2808` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator | `0x2808` | 8..15 | `raw` | 0 = OFF; 1 = MAX; 2 = 400%; 4 = 300%; 8 = 200%; 16 = 100%; 32 = 0%; 64 = -50%; 128 = MIN | N | Only observed constant |
| HV/EV System Indicator (-50%) | `0x2808` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (0%) | `0x2808` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (100%) | `0x2808` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (200%) | `0x2808` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (300%) | `0x2808` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (400%) | `0x2808` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HV/EV System Indicator (MAX) | `0x2808` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Buzzer Operation (800Hz, Vol L, W/ Damping) | `0x2821` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Buzzer Operation (800Hz, Vol S, W/ Damping) | `0x2823` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Buzzer Operation (800Hz, Vol S, W/O Damping) | `0x2824` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Buzzer Operation (1.6kHz, Vol S, W/O Damping) | `0x2825` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Buzzer Operation (2.4kHz, Vol L, W/O Damping) | `0x2827` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Multi Display All (White) | `0x2831` | 8..8 | `raw` | 1 = ON | N | Only observed constant |
| Multi Display Default Screen | `0x2831` | 9..9 | `raw` | 1 = ON | N | Only observed constant |
| Fuel Gauge Indicator | `0x2901` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Over Speed Indicator | `0x2901` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Master Warning | `0x2901` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Stop Warning | `0x2901` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Go to Service Warning | `0x2901` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| High Beam Indicator | `0x2911` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Headlamp Indicator | `0x2911` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Taillamp Indicator | `0x2911` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Front Fog Lamp Indicator | `0x2911` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Fog Lamp Indicator | `0x2911` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Auto Leveling Headlamp Indicator | `0x2911` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Automatic High Beam Indicator (Green) | `0x2911` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Automatic High Beam Indicator (Yellow) | `0x2911` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| AFS OFF Indicator | `0x2911` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LED Headlamp Warning | `0x2911` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Light Indicator | `0x2911` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Left Turn Signal Indicator | `0x2911` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Right Turn Signal Indicator | `0x2911` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Driver Side Seat Belt Warning | `0x2911` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Passenger Side Seat Belt Warning | `0x2911` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Seat Belt Warning | `0x2911` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Washer Warning | `0x2912` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Door Open Warning | `0x2912` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Airbag Warning | `0x2912` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Pedestrian Protection Warning | `0x2912` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Smart System Indicator | `0x2912` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Smart System Warning | `0x2912` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Head Up Display Indicator | `0x2912` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Auto Air Conditioner Indicator | `0x2912` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Emergency Exit Warning | `0x2912` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| RSCA OFF Indicator | `0x2912` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Weather Light Indicator | `0x2912` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Brake Warning | `0x2941` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Park Warning | `0x2941` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Warning | `0x2941` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECB Warning | `0x2941` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| EPB Auto OFF Indicator | `0x2941` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Slip Warning | `0x2941` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VSC OFF Indicator | `0x2941` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VSC TRC Indicator | `0x2941` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ACTIVE TRC Indicator | `0x2941` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ACTIVE TRC Warning | `0x2941` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Brake Hold Indicator | `0x2941` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Hold Indicator | `0x2941` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Downhill Assist Control Indicator | `0x2941` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Downhill Assist Control Warning | `0x2941` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| CRAWL Indicator | `0x2941` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| CRAWL Warning | `0x2941` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS OFF Warning | `0x2951` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PCS Warning | `0x2951` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Radar Cruise Indicator | `0x2951` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Radar Cruise Indicator (Yellow) | `0x2951` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Indicator | `0x2951` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Indicator (Yellow) | `0x2951` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SET Indicator | `0x2951` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LDA Steering Control Indicator | `0x2951` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drift From The Lane Indicator | `0x2951` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drift From The Lane Warning | `0x2951` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LDA Indicator | `0x2951` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LDA Warning | `0x2951` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Left LDA Indicator | `0x2951` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Left LDA Warning | `0x2951` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Right LDA Indicator | `0x2951` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Right LDA Warning | `0x2951` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| All Indicator | `0x2981` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Auxiliary Battery Warning | `0x2981` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (0) | `0x2A01` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input | `0x2A01` | 8..15 | `raw` | 0 = OFF; 1 = 280; 2 = 240; 4 = 200; 8 = 160; 16 = 120; 32 = 80; 64 = 40; 128 = 0 | N | Only observed constant |
| Speed Signal Input (40) | `0x2A01` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (80) | `0x2A01` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (120) | `0x2A01` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (160) | `0x2A01` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (200) | `0x2A01` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (240) | `0x2A01` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (280) | `0x2A01` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (320) | `0x2A01` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input 2 | `0x2A01` | 24..26 | `raw` | 0 = OFF; 1 = 400; 2 = 360; 4 = 320 | N | Only observed constant |
| Speed Signal Input (360) | `0x2A01` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Speed Signal Input (400) | `0x2A01` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ODO/TRIP Change Switch | `0x2A21` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Trip Reset Switch | `0x2A21` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Display Select Switch | `0x2A21` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Meter Day Mode Switch | `0x2A21` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Switch (UP) | `0x2A21` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Up/Down Switch | `0x2A21` | 12..13 | `raw` | 0 = OFF; 1 = DOWN; 2 = UP | N | Only observed constant |
| Light Control Switch (DOWN) | `0x2A21` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Light Control Switch (Single) | `0x2A21` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch (H) | `0x2A21` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch | `0x2A21` | 24..26 | `raw` | 0 = OFF; 1 = 00; 2 = M; 4 = H | N | Only observed constant |
| Timer Switch (M) | `0x2A21` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch (00) | `0x2A21` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Timer Switch (Single) | `0x2A21` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Up) | `0x2A22` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch | `0x2A22` | 8..14 | `raw` | 0 = OFF; 1 = Menu; 2 = Back; 4 = Enter; 8 = Right; 16 = Left; 32 = Down; 64 = Up | N | Only observed constant |
| Multi Switch (Down) | `0x2A22` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Left) | `0x2A22` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Right) | `0x2A22` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Enter) | `0x2A22` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Back) | `0x2A22` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Switch (Top) | `0x2A22` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
