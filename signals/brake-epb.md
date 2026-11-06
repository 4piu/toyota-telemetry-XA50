# Brake/EPB

Request ID `0x7B0`, response ID `0x7B8`. Use these as `<REQUEST ID>` and `<RESPONSE ID>` in [PROTOCOL.md](../PROTOCOL.md).

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | N | Only observed constant |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | N | Only observed constant |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `0x0103` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw * 625000 / 388357` | km | N | Only observed constant |
| FR Wheel Speed | `0x1002` | 0..15 | `raw * 0.1` | km/h | Y |  |
| FL Wheel Speed | `0x1002` | 16..31 | `raw * 0.1` | km/h | Y |  |
| RR Wheel Speed | `0x1002` | 32..47 | `raw * 0.1` | km/h | Y |  |
| RL Wheel Speed | `0x1002` | 48..63 | `raw * 0.1` | km/h | Y |  |
| FR Wheel Acceleration | `0x1003` | 0..7 | `raw * 1.569064` | m/s2 | Y |  |
| FL Wheel Acceleration | `0x1003` | 8..15 | `raw * 1.569064` | m/s2 | Y |  |
| RR Wheel Acceleration | `0x1003` | 16..23 | `raw * 1.569064` | m/s2 | Y |  |
| RL Wheel Acceleration | `0x1003` | 24..31 | `raw * 1.569064` | m/s2 | Y |  |
| Master Cylinder Sensor 1 | `0x1004` | 0..7 | `raw * 0.098 - 1` | MPa | Y |  |
| Zero Point of M/C | `0x1005` | 0..7 | `raw * 0.098` | MPa | N | Only observed constant |
| Master Cylinder Sensor Temperature | `0x1006` | 0..7 | `raw - 80` | ℃ | Y |  |
| M/C Sensor Grade | `0x1007` | 0..7 | `raw - 30` | MPa/s | N | Only observed constant |
| Lateral G | `0x1008` | 0..7 | `raw * 0.196133` | m/s2 | Y |  |
| Forward and Rearward G | `0x1008` | 8..15 | `raw * 0.196133` | m/s2 | Y |  |
| Zero Point of Decele2 | `0x1009` | 0..7 | `raw * 0.196133` | m/s2 | N | Only observed constant |
| Zero Point of Decele | `0x1009` | 8..15 | `raw * 0.196133` | m/s2 | N | Only observed constant |
| Yaw Rate Sensor Value | `0x100A` | 0..7 | `raw - 128` | °/s | Y |  |
| Zero Point of Yaw Rate Sensor | `0x100B` | 0..7 | `raw - 128` | °/s | N | Only observed constant |
| Steering Angle Value | `0x100D` | 0..15 | `raw * 0.1 - 3276.8` | ° | Y |  |
| Zero Point of Steering Angle | `0x100E` | 0..15 | `raw * 0.1 - 3276.8` | ° | N | Only observed constant |
| MT Voltage Value | `0x1010` | 0..7 | `raw * 0.1` | V | Y |  |
| Solenoid Power Supply Voltage | `0x1011` | 0..7 | `raw * 0.1` | V | Y |  |
| Vehicle Speed | `0x1012` | 0..15 | `raw * 0.1` | km/h | Y |  |
| Accelerator Opening Angle % | `0x1016` | 0..7 | `raw * 128 / 255` | % | Y |  |
| Shift Lever Position | `0x101A` | 0..7 | `raw` | 0 = fail; 1 = 1st; 2 = 2nd; 3 = 3rd; 4 = 4th; 5 = 5th; 6 = 6th; 10 = B; 11 = D/M; 12 = N; 13 = P; 14 = R; 15 = No input | N | Only observed constant |
| TRC(TRAC)/VSC OFF Mode | `0x1020` | 0..7 | `raw` | 0 = Normal mode (TRC(TRAC) ON/VSC ON); 1 = TRC(TRAC) OFF mode (TRC(TRAC) OFF/VSC ON); 2 = VSC expert mode(VSC expert mode MID ON); 3 = VSC OFF mode (TRC(TRAC) OFF/VSC OFF) | N | Only observed constant |
| Brake Hold Control Mode | `0x1021` | 0..7 | `raw` | 0 = Out of control mode; 1 = Pressure hold mode; 2 = Pressure release mode; 3 = EPB lock mode | N | Only observed constant |
| FR Target Oil Pressure | `0x1022` | 0..7 | `raw * 4 / 51` | MPa | Y |  |
| FL Target Oil Pressure | `0x1022` | 8..15 | `raw * 4 / 51` | MPa | Y |  |
| RR Target Oil Pressure | `0x1022` | 16..23 | `raw * 4 / 51` | MPa | Y |  |
| RL Target Oil Pressure | `0x1022` | 24..31 | `raw * 4 / 51` | MPa | Y |  |
| Vehicle Speed | `0x1023` | 0..15 | `raw * 0.1` | km/h | Y |  |
| Vehicle Speed Grade | `0x1024` | 0..7 | `raw * 0.196133` | m/s2 | Y |  |
| Vehicle Stop Time from IG ON | `0x1025` | 0..7 | `raw * 5` | s | N | Only observed constant |
| Travel Distance from IG ON | `0x1026` | 0..7 | `raw * 5` | s | N | Only observed constant |
| Actuator ID(for Plant/Dealer service) | `0x1029` | 0..7 | `raw` | 0 = V3.5; 1 = AHB-Rx; 2 = AHB-R; 3 = AHB-G; 48 = BoschESP; 49 = BoschABS; 50 = Bosch brushless; 80 = Continental | N | Only observed constant |
| IG Switch | `0x102D` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Downhill Assist Control/Crawl Control SW | `0x102D` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light SW | `0x102D` | 10..10 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Parking Brake SW | `0x102D` | 11..11 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Brake Pedal Load Sensing SW | `0x102D` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Diff Lock Switch | `0x102D` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Automatic Disconnect Diff SW | `0x102D` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| L4 Switch | `0x102D` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Reverse Position Switch | `0x102D` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Brake Hold Switch | `0x102D` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light Relay | `0x102D` | 26..26 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Inspection Mode | `0x102D` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| TRC(TRAC) Control | `0x102D` | 29..29 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| TRC(TRAC) Engine Control | `0x102D` | 30..30 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| TRC(TRAC) Brake Control | `0x102D` | 31..31 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| FR Wheel VSC Ctrl Status | `0x102D` | 40..40 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| FL Wheel VSC Ctrl Status | `0x102D` | 41..41 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| RR Wheel VSC Ctrl Status | `0x102D` | 42..42 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| RL Wheel VSC Ctrl Status | `0x102D` | 43..43 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| FR Wheel ABS Ctrl Status | `0x102D` | 44..44 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| FL Wheel ABS Ctrl Status | `0x102D` | 45..45 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| RR Wheel ABS Ctrl Status | `0x102D` | 46..46 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| RL Wheel ABS Ctrl Status | `0x102D` | 47..47 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| HAB Control | `0x102D` | 56..56 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fail Status | `0x102D` | 57..57 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fail Control | `0x102D` | 58..58 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| BA Ctrl Status | `0x102D` | 59..59 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PBA Ctrl Status | `0x102D` | 60..60 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light Relay State for ECU Control | `0x102D` | 61..61 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Engine Stop by Stop and Start System | `0x102D` | 62..62 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Solenoid State for ECU Control | `0x102D` | 63..63 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor State for ECU Control | `0x102D` | 72..72 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Operation History of IG OFF while Vehicle is moving | `0x102D` | 73..73 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Plumbing information of brake | `0x102D` | 74..74 | `raw` | 0 = Anteroposterior plumbing; 1 = X plumbing | N | Only observed constant |
| Buzzer | `0x102D` | 75..75 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Dealer Mode | `0x102D` | 76..76 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Zero Point Memory State of Steering Angle Sensor | `0x102D` | 77..77 | `raw` | 0 = Zero point is not memorized; 1 = Zero point is memorized | N | Only observed constant |
| Regenerative Cooperation | `0x102D` | 78..78 | `raw` | 0 = OFF; 1 = ON | Y |  |
| TRC(TRAC)/VSC OFF SW | `0x102D` | 88..88 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Reservoir Warning SW | `0x102D` | 89..89 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECB Motor Relay | `0x102D` | 90..90 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECB Motor Relay2 | `0x102D` | 91..91 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECB Solenoid (SGH) | `0x102D` | 92..92 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECB Solenoid (SSA) | `0x102D` | 93..93 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MTT | `0x102D` | 94..94 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IGP_PT1 | `0x102D` | 106..106 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IGP_PT2 | `0x102D` | 107..107 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| FR Wheel | `0x102D` | 108..108 | `raw` | 0 = Forward; 1 = Back | N | Only observed constant |
| FL Wheel | `0x102D` | 109..109 | `raw` | 0 = Forward; 1 = Back | N | Only observed constant |
| RR Wheel | `0x102D` | 110..110 | `raw` | 0 = Forward; 1 = Back | N | Only observed constant |
| RL Wheel | `0x102D` | 111..111 | `raw` | 0 = Forward; 1 = Back | N | Only observed constant |
| Brakes Specifications Change by C-BEST | `0x102D` | 122..122 | `raw` | 0 = None; 1 = Exist | N | Only observed constant |
| Zero Point Memory Status of Turning Angle | `0x102D` | 123..123 | `raw` | 0 = Zero Point is Not Memorized; 1 = Zero Point is Memorized | N | Only observed constant |
| SLM2 Refresh Drive Completed Status | `0x102D` | 124..124 | `raw` | 0 = Not Complete; 1 = Complete | N | Only observed constant |
| Neutral Switch | `0x102D` | 125..125 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Brake ECU Communication Open History | `0x102E` | 72..72 | `raw` | 0 = None; 1 = Exist | N | Only observed constant |
| Gap Hold Chamber Pressure Sensor Open History | `0x102E` | 73..73 | `raw` | 0 = None; 1 = Exist | N | Only observed constant |
| Vacuum Booster Pressure Sensor Open History | `0x102E` | 75..75 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Atmospheric Pressure Sensor Open History | `0x102E` | 76..76 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| FR Speed Open | `0x102F` | 8..8 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| FL Speed Open | `0x102F` | 9..9 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| RR Speed Open | `0x102F` | 10..10 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| RL Speed Open | `0x102F` | 11..11 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| EFI Communication Open | `0x102F` | 12..12 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Yaw Rate Open | `0x102F` | 13..13 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Deceleration Open | `0x102F` | 14..14 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Steering Open | `0x102F` | 15..15 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Master Cylinder Open | `0x102F` | 24..24 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| FR Speed Sensor Voltage Open | `0x102F` | 25..25 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| FL Speed Sensor Voltage Open | `0x102F` | 26..26 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| RR Speed Sensor Voltage Open | `0x102F` | 27..27 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| RL Speed Sensor Voltage Open | `0x102F` | 28..28 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| M/C Pressure Sensor Noise | `0x102F` | 29..29 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Brake Pedal Switch Open | `0x102F` | 30..30 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Yaw Rate Sensor Voltage Open | `0x102F` | 31..31 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| EFI Communication Invalid | `0x102F` | 40..40 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Master Cylinder Pressure Sensor Power Supply Open | `0x102F` | 41..41 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Solenoid Power Supply Open | `0x102F` | 42..42 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Motor Power Supply Open | `0x102F` | 43..43 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Stop Switch Open | `0x102F` | 44..44 | `raw` | 0 = Normal; 1 = Under intermittent | Y |  |
| A/C ECU Communication Open | `0x102F` | 45..45 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Navigation ECU Communication Open | `0x102F` | 46..46 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Stop and Start ECU Communication Open | `0x102F` | 47..47 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Air Bag ECU Communication Open | `0x102F` | 56..56 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Stroke Open | `0x102F` | 57..57 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Stroke2 Open | `0x102F` | 58..58 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Accumulator Open | `0x102F` | 59..59 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| HV Communication Open | `0x102F` | 60..60 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Reaction Force Pressure Sensor Open | `0x102F` | 61..61 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Servo Pressure Sensor Open | `0x102F` | 62..62 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Body ECU Communication Open | `0x102F` | 63..63 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Brake ECU Communication Open | `0x102F` | 72..72 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Gap Hold Chamber Pressure Sensor Open | `0x102F` | 73..73 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| EPS/Steering Control Actuator ECU Communication Open | `0x102F` | 74..74 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Vacuum Booster Pressure Sensor Open | `0x102F` | 75..75 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Atmospheric Pressure Sensor Open | `0x102F` | 76..76 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| ECU +B1 Voltage | `0x1032` | 0..7 | `raw * 0.1` | V | Y |  |
| RH Actuator Motor Input Voltage | `0x1034` | 0..7 | `raw * 0.1` | V | Y |  |
| LH Actuator Motor Input Voltage | `0x1034` | 8..15 | `raw * 0.1` | V | Y |  |
| RH Actuator Motor +Terminal Voltage | `0x1035` | 0..7 | `raw * 0.1` | V | N | Changes only while the parking brake moves |
| LH Actuator Motor +Terminal Voltage | `0x1035` | 8..15 | `raw * 0.1` | V | N | Changes only while the parking brake moves |
| RH Actuator Motor -Terminal Voltage | `0x1036` | 0..7 | `raw * 0.1` | V | N | Changes only while the parking brake moves |
| LH Actuator Motor -Terminal Voltage | `0x1036` | 8..15 | `raw * 0.1` | V | N | Changes only while the parking brake moves |
| Ambient Temperature | `0x1038` | 0..7 | `raw - 48` | ℃ | Y |  |
| EPB Switch | `0x1039` | 6..7 | `raw` | 0 = Neutral; 1 = Apply; 2 = Release; 3 = Unknown | N | Only observed constant |
| Parking Brake Light | `0x103A` | 6..7 | `raw` | 0 = OFF; 1 = ON; 2 = Blink | N | Only observed constant |
| RH Actuator Motor Actual Current | `0x103C` | 0..7 | `raw * 1114 / 5115 - 5.036` | A | N | Changes only while the parking brake moves |
| LH Actuator Motor Actual Current | `0x103C` | 8..15 | `raw * 1114 / 5115 - 5.036` | A | Y |  |
| RH Actuator Motor Adjustment Current | `0x103D` | 0..7 | `raw * 557 / 10230` | A | N | Changes only while the parking brake moves |
| LH Actuator Motor Adjustment Current | `0x103D` | 8..15 | `raw * 557 / 10230` | A | N | Changes only while the parking brake moves |
| RH Actuator Motor Current Differential | `0x1041` | 3..7 | `raw * 0.063` | A | Y |  |
| LH Actuator Motor Current Differential | `0x1041` | 11..15 | `raw * 0.063` | A | Y |  |
| Vehicle Speed (Control Value) | `0x1042` | 0..15 | `raw * 0.1` | km/h | Y |  |
| EPB Control Cancel History | `0x1043` | 0..7 | `raw` |  | N | Only observed constant |
| Counter of IG ON After EPB Control Cancel | `0x1044` | 0..7 | `raw` | Times | N | Only observed constant |
| RH Actuator Status | `0x1045` | 0..7 | `raw` | 0 = Park Applied; 1 = Hold Applied; 2 = Released; 3 = Applying; 4 = Releasing; 5 = Completely Released; 6 = Unknown | N | Only observed constant |
| LH Actuator Status | `0x1045` | 8..15 | `raw` | 0 = Park Applied; 1 = Hold Applied; 2 = Released; 3 = Applying; 4 = Releasing; 5 = Completely Released; 6 = Unknown | N | Only observed constant |
| EPB Warning Light | `0x1046` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Meter ECU Connection Cancel Request | `0x1046` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Auto Mode Request | `0x1046` | 11..11 | `raw` | 0 = Manual mode request; 1 = Auto mode request | N | Only observed constant |
| Brake Hold Ready | `0x1046` | 12..12 | `raw` | 0 = Not in stand-by mode; 1 = Stand-by mode | N | Only observed constant |
| EPB Lock Request | `0x1046` | 13..13 | `raw` | 0 = Not requested; 1 = requested | N | Only observed constant |
| Auto Mode | `0x1046` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Dynamic PKB Mode | `0x1046` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| RH Actuator Motor Relay1 | `0x1046` | 24..24 | `raw` | 0 = OFF; 1 = ON | Y |  |
| RH Actuator Motor Relay2 | `0x1046` | 25..25 | `raw` | 0 = OFF; 1 = ON | Y |  |
| RH Actuator Motor Relay3 | `0x1046` | 26..26 | `raw` | 0 = OFF; 1 = ON | Y |  |
| RH Actuator Motor Relay4 | `0x1046` | 27..27 | `raw` | 0 = OFF; 1 = ON | Y |  |
| LH Actuator Motor Relay1 | `0x1046` | 28..28 | `raw` | 0 = OFF; 1 = ON | Y |  |
| LH Actuator Motor Relay2 | `0x1046` | 29..29 | `raw` | 0 = OFF; 1 = ON | Y |  |
| LH Actuator Motor Relay3 | `0x1046` | 30..30 | `raw` | 0 = OFF; 1 = ON | Y |  |
| LH Actuator Motor Relay4 | `0x1046` | 31..31 | `raw` | 0 = OFF; 1 = ON | Y |  |
| FR Speed Signal Status | `0x1046` | 40..40 | `raw` | 0 = Normal; 1 = Fixed | N | Only observed constant |
| FL Speed Signal Status | `0x1046` | 41..41 | `raw` | 0 = Normal; 1 = Fixed | N | Only observed constant |
| RR Speed Signal Status | `0x1046` | 42..42 | `raw` | 0 = Normal; 1 = Fixed | Y |  |
| RL Speed Signal Status | `0x1046` | 43..43 | `raw` | 0 = Normal; 1 = Fixed | Y |  |
| RH Actuator Current Status | `0x1046` | 44..44 | `raw` | 0 = Normal; 1 = Invalid | N | Only observed constant |
| LH Actuator Current Status | `0x1046` | 45..45 | `raw` | 0 = Normal; 1 = Invalid | N | Only observed constant |
| Permission of Interlocking Shift | `0x1046` | 46..46 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of Interlocking Brake | `0x1046` | 47..47 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of RH Interlocking PKB Lock | `0x1046` | 56..56 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of RH Interlocking PKB Release | `0x1046` | 57..57 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of RH Interlocking Dynamic PKB | `0x1046` | 58..58 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of RH Interlocking PKB Full Release | `0x1046` | 59..59 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of LH Interlocking PKB Lock | `0x1046` | 60..60 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of LH Interlocking PKB Release | `0x1046` | 61..61 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of LH Interlocking Dynamic PKB | `0x1046` | 62..62 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of LH Interlocking PKB Full Release | `0x1046` | 63..63 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Fade Status | `0x1046` | 72..72 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| RH Actuator Motor Current High | `0x1046` | 73..73 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| RH Actuator Motor Driver Operation Status | `0x1046` | 74..74 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LH Actuator Motor Current High | `0x1046` | 75..75 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LH Actuator Motor Driver Operation Status | `0x1046` | 76..76 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Speed 1 Invalid | `0x1046` | 77..77 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Speed 2 Invalid | `0x1046` | 78..78 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Speed 3 Invalid | `0x1046` | 79..79 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HVSYS Invalid | `0x1046` | 88..88 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stroke Sensor2 | `0x104B` | 0..7 | `raw / 51` | V | Y |  |
| Quantity of Brake Pedal Stroke | `0x104C` | 0..7 | `raw` | mm | Y |  |
| Voltage of Stroke Sensor2 | `0x1052` | 0..7 | `raw / 51` | V | Y |  |
| FR Regenerative Request | `0x1054` | 0..15 | `raw * 16` | Nm | Y |  |
| FR Regenerative Operation | `0x1055` | 0..15 | `raw * 16` | Nm | Y |  |
| Brake Pedal Stroke Change Speed | `0x1058` | 0..7 | `raw * 20 - 2560` | mm/s | Y |  |
| VCSK Voltage Value | `0x105C` | 0..7 | `raw * 11 / 510` | V | N | Regulated supply; constant by design |
| IG1 Voltage | `0x1073` | 0..7 | `raw * 4 / 51` | V | Y |  |
| IG2 Voltage | `0x1073` | 8..15 | `raw * 4 / 51` | V | Y |  |
| BS Voltage | `0x1073` | 16..23 | `raw * 4 / 51` | V | Y |  |
| BM Voltage | `0x1073` | 24..31 | `raw * 4 / 51` | V | Y |  |
| Voltage Difference between Motor Input-Output Terminal | `0x1076` | 0..15 | `raw * 0.01953125` | V | N | Changes only while the parking brake moves |
| Drive Mode Select Customize Item EPS Available | `0x10B6` | 8..8 | `raw` | 0 = Not Applicable; 1 = Applicable | N | Only observed constant |
| Drive Mode Select Customize Item Adaptive Variable Suspension System Available | `0x10B6` | 9..9 | `raw` | 0 = Not Applicable; 1 = Applicable | N | Only observed constant |
| Drive Mode Select Customize Item Brake Available | `0x10B6` | 10..10 | `raw` | 0 = Not Applicable; 1 = Applicable | N | Only observed constant |
| ABS Solenoid (SRLR) | `0x2801` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SRLH) | `0x2801` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SRRR) | `0x2801` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SRRH) | `0x2801` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SFLR) | `0x2801` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SFLH) | `0x2801` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SFRR) | `0x2801` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SFRH) | `0x2801` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| TRC(TRAC)/VSC Solenoid (SM2) | `0x2801` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| TRC(TRAC)/VSC Solenoid (SM1) | `0x2801` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Motor Relay | `0x2801` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Solenoid Relay | `0x2801` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| STPO | `0x2803` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| EXO | `0x2803` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
