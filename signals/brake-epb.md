# Brake/EPB

Request ID `0x7B0`, response ID `0x7B8`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Only observed constant |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Only observed constant |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw * 625000 / 388357` | km | N | Only observed constant |
| FR Wheel Speed | `22 10 02` | 0..15 | `raw * 0.1` | km/h | Y |  |
| FL Wheel Speed | `22 10 02` | 16..31 | `raw * 0.1` | km/h | Y |  |
| RR Wheel Speed | `22 10 02` | 32..47 | `raw * 0.1` | km/h | Y |  |
| RL Wheel Speed | `22 10 02` | 48..63 | `raw * 0.1` | km/h | Y |  |
| FR Wheel Acceleration | `22 10 03` | 0..7 | `raw * 1.569064` | m/s2 | Y |  |
| FL Wheel Acceleration | `22 10 03` | 8..15 | `raw * 1.569064` | m/s2 | Y |  |
| RR Wheel Acceleration | `22 10 03` | 16..23 | `raw * 1.569064` | m/s2 | Y |  |
| RL Wheel Acceleration | `22 10 03` | 24..31 | `raw * 1.569064` | m/s2 | Y |  |
| Master Cylinder Sensor 1 | `22 10 04` | 0..7 | `raw * 0.098 - 1` | MPa | Y |  |
| Zero Point of M/C | `22 10 05` | 0..7 | `raw * 0.098` | MPa | N | Only observed constant |
| Master Cylinder Sensor Temperature | `22 10 06` | 0..7 | `raw - 80` | ℃ | Y |  |
| M/C Sensor Grade | `22 10 07` | 0..7 | `raw - 30` | MPa/s | N | Only observed constant |
| Lateral G | `22 10 08` | 0..7 | `raw * 0.196133` | m/s2 | Y |  |
| Forward and Rearward G | `22 10 08` | 8..15 | `raw * 0.196133` | m/s2 | Y |  |
| Zero Point of Decele2 | `22 10 09` | 0..7 | `raw * 0.196133` | m/s2 | N | Only observed constant |
| Zero Point of Decele | `22 10 09` | 8..15 | `raw * 0.196133` | m/s2 | N | Only observed constant |
| Yaw Rate Sensor Value | `22 10 0A` | 0..7 | `raw - 128` | °/s | Y |  |
| Zero Point of Yaw Rate Sensor | `22 10 0B` | 0..7 | `raw - 128` | °/s | N | Only observed constant |
| Steering Angle Value | `22 10 0D` | 0..15 | `raw * 0.1 - 3276.8` | ° | Y |  |
| Zero Point of Steering Angle | `22 10 0E` | 0..15 | `raw * 0.1 - 3276.8` | ° | N | Only observed constant |
| MT Voltage Value | `22 10 10` | 0..7 | `raw * 0.1` | V | Y |  |
| Solenoid Power Supply Voltage | `22 10 11` | 0..7 | `raw * 0.1` | V | Y |  |
| Vehicle Speed | `22 10 12` | 0..15 | `raw * 0.1` | km/h | Y |  |
| Accelerator Opening Angle % | `22 10 16` | 0..7 | `raw * 128 / 255` | % | Y |  |
| Shift Lever Position | `22 10 1A` | 0..7 | `raw` | 0 = fail; 1 = 1st; 2 = 2nd; 3 = 3rd; 4 = 4th; 5 = 5th; 6 = 6th; 10 = B; 11 = D/M; 12 = N; 13 = P; 14 = R; 15 = No input | N | Only observed constant |
| TRC(TRAC)/VSC OFF Mode | `22 10 20` | 0..7 | `raw` | 0 = Normal mode (TRC(TRAC) ON/VSC ON); 1 = TRC(TRAC) OFF mode (TRC(TRAC) OFF/VSC ON); 2 = VSC expert mode(VSC expert mode MID ON); 3 = VSC OFF mode (TRC(TRAC) OFF/VSC OFF) | N | Only observed constant |
| Brake Hold Control Mode | `22 10 21` | 0..7 | `raw` | 0 = Out of control mode; 1 = Pressure hold mode; 2 = Pressure release mode; 3 = EPB lock mode | N | Only observed constant |
| FR Target Oil Pressure | `22 10 22` | 0..7 | `raw * 4 / 51` | MPa | Y |  |
| FL Target Oil Pressure | `22 10 22` | 8..15 | `raw * 4 / 51` | MPa | Y |  |
| RR Target Oil Pressure | `22 10 22` | 16..23 | `raw * 4 / 51` | MPa | Y |  |
| RL Target Oil Pressure | `22 10 22` | 24..31 | `raw * 4 / 51` | MPa | Y |  |
| Vehicle Speed | `22 10 23` | 0..15 | `raw * 0.1` | km/h | Y |  |
| Vehicle Speed Grade | `22 10 24` | 0..7 | `raw * 0.196133` | m/s2 | Y |  |
| Vehicle Stop Time from IG ON | `22 10 25` | 0..7 | `raw * 5` | s | N | Only observed constant |
| Travel Distance from IG ON | `22 10 26` | 0..7 | `raw * 5` | s | N | Only observed constant |
| Actuator ID(for Plant/Dealer service) | `22 10 29` | 0..7 | `raw` | 0 = V3.5; 1 = AHB-Rx; 2 = AHB-R; 3 = AHB-G; 48 = BoschESP; 49 = BoschABS; 50 = Bosch brushless; 80 = Continental | N | Only observed constant |
| IG Switch | `22 10 2D` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Downhill Assist Control/Crawl Control SW | `22 10 2D` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light SW | `22 10 2D` | 10..10 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Parking Brake SW | `22 10 2D` | 11..11 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Brake Pedal Load Sensing SW | `22 10 2D` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Diff Lock Switch | `22 10 2D` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Automatic Disconnect Diff SW | `22 10 2D` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| L4 Switch | `22 10 2D` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Reverse Position Switch | `22 10 2D` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Brake Hold Switch | `22 10 2D` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light Relay | `22 10 2D` | 26..26 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Inspection Mode | `22 10 2D` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| TRC(TRAC) Control | `22 10 2D` | 29..29 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| TRC(TRAC) Engine Control | `22 10 2D` | 30..30 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| TRC(TRAC) Brake Control | `22 10 2D` | 31..31 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| FR Wheel VSC Ctrl Status | `22 10 2D` | 40..40 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| FL Wheel VSC Ctrl Status | `22 10 2D` | 41..41 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| RR Wheel VSC Ctrl Status | `22 10 2D` | 42..42 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| RL Wheel VSC Ctrl Status | `22 10 2D` | 43..43 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| FR Wheel ABS Ctrl Status | `22 10 2D` | 44..44 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| FL Wheel ABS Ctrl Status | `22 10 2D` | 45..45 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| RR Wheel ABS Ctrl Status | `22 10 2D` | 46..46 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| RL Wheel ABS Ctrl Status | `22 10 2D` | 47..47 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Only observed constant |
| HAB Control | `22 10 2D` | 56..56 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fail Status | `22 10 2D` | 57..57 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fail Control | `22 10 2D` | 58..58 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| BA Ctrl Status | `22 10 2D` | 59..59 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PBA Ctrl Status | `22 10 2D` | 60..60 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light Relay State for ECU Control | `22 10 2D` | 61..61 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Engine Stop by Stop and Start System | `22 10 2D` | 62..62 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Solenoid State for ECU Control | `22 10 2D` | 63..63 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor State for ECU Control | `22 10 2D` | 72..72 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Operation History of IG OFF while Vehicle is moving | `22 10 2D` | 73..73 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Plumbing information of brake | `22 10 2D` | 74..74 | `raw` | 0 = Anteroposterior plumbing; 1 = X plumbing | N | Only observed constant |
| Buzzer | `22 10 2D` | 75..75 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Dealer Mode | `22 10 2D` | 76..76 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Zero Point Memory State of Steering Angle Sensor | `22 10 2D` | 77..77 | `raw` | 0 = Zero point is not memorized; 1 = Zero point is memorized | N | Only observed constant |
| Regenerative Cooperation | `22 10 2D` | 78..78 | `raw` | 0 = OFF; 1 = ON | Y |  |
| TRC(TRAC)/VSC OFF SW | `22 10 2D` | 88..88 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Reservoir Warning SW | `22 10 2D` | 89..89 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECB Motor Relay | `22 10 2D` | 90..90 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECB Motor Relay2 | `22 10 2D` | 91..91 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECB Solenoid (SGH) | `22 10 2D` | 92..92 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECB Solenoid (SSA) | `22 10 2D` | 93..93 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MTT | `22 10 2D` | 94..94 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IGP_PT1 | `22 10 2D` | 106..106 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IGP_PT2 | `22 10 2D` | 107..107 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| FR Wheel | `22 10 2D` | 108..108 | `raw` | 0 = Forward; 1 = Back | N | Only observed constant |
| FL Wheel | `22 10 2D` | 109..109 | `raw` | 0 = Forward; 1 = Back | N | Only observed constant |
| RR Wheel | `22 10 2D` | 110..110 | `raw` | 0 = Forward; 1 = Back | N | Only observed constant |
| RL Wheel | `22 10 2D` | 111..111 | `raw` | 0 = Forward; 1 = Back | N | Only observed constant |
| Brakes Specifications Change by C-BEST | `22 10 2D` | 122..122 | `raw` | 0 = None; 1 = Exist | N | Only observed constant |
| Zero Point Memory Status of Turning Angle | `22 10 2D` | 123..123 | `raw` | 0 = Zero Point is Not Memorized; 1 = Zero Point is Memorized | N | Only observed constant |
| SLM2 Refresh Drive Completed Status | `22 10 2D` | 124..124 | `raw` | 0 = Not Complete; 1 = Complete | N | Only observed constant |
| Neutral Switch | `22 10 2D` | 125..125 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Brake ECU Communication Open History | `22 10 2E` | 72..72 | `raw` | 0 = None; 1 = Exist | N | Only observed constant |
| Gap Hold Chamber Pressure Sensor Open History | `22 10 2E` | 73..73 | `raw` | 0 = None; 1 = Exist | N | Only observed constant |
| Vacuum Booster Pressure Sensor Open History | `22 10 2E` | 75..75 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Atmospheric Pressure Sensor Open History | `22 10 2E` | 76..76 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| FR Speed Open | `22 10 2F` | 8..8 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| FL Speed Open | `22 10 2F` | 9..9 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| RR Speed Open | `22 10 2F` | 10..10 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| RL Speed Open | `22 10 2F` | 11..11 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| EFI Communication Open | `22 10 2F` | 12..12 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Yaw Rate Open | `22 10 2F` | 13..13 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Deceleration Open | `22 10 2F` | 14..14 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Steering Open | `22 10 2F` | 15..15 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Master Cylinder Open | `22 10 2F` | 24..24 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| FR Speed Sensor Voltage Open | `22 10 2F` | 25..25 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| FL Speed Sensor Voltage Open | `22 10 2F` | 26..26 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| RR Speed Sensor Voltage Open | `22 10 2F` | 27..27 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| RL Speed Sensor Voltage Open | `22 10 2F` | 28..28 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| M/C Pressure Sensor Noise | `22 10 2F` | 29..29 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Brake Pedal Switch Open | `22 10 2F` | 30..30 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Yaw Rate Sensor Voltage Open | `22 10 2F` | 31..31 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| EFI Communication Invalid | `22 10 2F` | 40..40 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Master Cylinder Pressure Sensor Power Supply Open | `22 10 2F` | 41..41 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Solenoid Power Supply Open | `22 10 2F` | 42..42 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Motor Power Supply Open | `22 10 2F` | 43..43 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Stop Switch Open | `22 10 2F` | 44..44 | `raw` | 0 = Normal; 1 = Under intermittent | Y |  |
| A/C ECU Communication Open | `22 10 2F` | 45..45 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Navigation ECU Communication Open | `22 10 2F` | 46..46 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Stop and Start ECU Communication Open | `22 10 2F` | 47..47 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Air Bag ECU Communication Open | `22 10 2F` | 56..56 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Stroke Open | `22 10 2F` | 57..57 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Stroke2 Open | `22 10 2F` | 58..58 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Accumulator Open | `22 10 2F` | 59..59 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| HV Communication Open | `22 10 2F` | 60..60 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Reaction Force Pressure Sensor Open | `22 10 2F` | 61..61 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Servo Pressure Sensor Open | `22 10 2F` | 62..62 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Body ECU Communication Open | `22 10 2F` | 63..63 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Brake ECU Communication Open | `22 10 2F` | 72..72 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Gap Hold Chamber Pressure Sensor Open | `22 10 2F` | 73..73 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| EPS/Steering Control Actuator ECU Communication Open | `22 10 2F` | 74..74 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Vacuum Booster Pressure Sensor Open | `22 10 2F` | 75..75 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| Atmospheric Pressure Sensor Open | `22 10 2F` | 76..76 | `raw` | 0 = Normal; 1 = Under intermittent | N | Only observed constant |
| ECU +B1 Voltage | `22 10 32` | 0..7 | `raw * 0.1` | V | Y |  |
| RH Actuator Motor Input Voltage | `22 10 34` | 0..7 | `raw * 0.1` | V | Y |  |
| LH Actuator Motor Input Voltage | `22 10 34` | 8..15 | `raw * 0.1` | V | Y |  |
| RH Actuator Motor +Terminal Voltage | `22 10 35` | 0..7 | `raw * 0.1` | V | N | Changes only while the parking brake moves |
| LH Actuator Motor +Terminal Voltage | `22 10 35` | 8..15 | `raw * 0.1` | V | N | Changes only while the parking brake moves |
| RH Actuator Motor -Terminal Voltage | `22 10 36` | 0..7 | `raw * 0.1` | V | N | Changes only while the parking brake moves |
| LH Actuator Motor -Terminal Voltage | `22 10 36` | 8..15 | `raw * 0.1` | V | N | Changes only while the parking brake moves |
| Ambient Temperature | `22 10 38` | 0..7 | `raw - 48` | ℃ | Y |  |
| EPB Switch | `22 10 39` | 6..7 | `raw` | 0 = Neutral; 1 = Apply; 2 = Release; 3 = Unknown | N | Only observed constant |
| Parking Brake Light | `22 10 3A` | 6..7 | `raw` | 0 = OFF; 1 = ON; 2 = Blink | N | Only observed constant |
| RH Actuator Motor Actual Current | `22 10 3C` | 0..7 | `raw * 1114 / 5115 - 5.036` | A | N | Changes only while the parking brake moves |
| LH Actuator Motor Actual Current | `22 10 3C` | 8..15 | `raw * 1114 / 5115 - 5.036` | A | Y |  |
| RH Actuator Motor Adjustment Current | `22 10 3D` | 0..7 | `raw * 557 / 10230` | A | N | Changes only while the parking brake moves |
| LH Actuator Motor Adjustment Current | `22 10 3D` | 8..15 | `raw * 557 / 10230` | A | N | Changes only while the parking brake moves |
| RH Actuator Motor Current Differential | `22 10 41` | 3..7 | `raw * 0.063` | A | Y |  |
| LH Actuator Motor Current Differential | `22 10 41` | 11..15 | `raw * 0.063` | A | Y |  |
| Vehicle Speed (Control Value) | `22 10 42` | 0..15 | `raw * 0.1` | km/h | Y |  |
| EPB Control Cancel History | `22 10 43` | 0..7 | `raw` |  | N | Only observed constant |
| Counter of IG ON After EPB Control Cancel | `22 10 44` | 0..7 | `raw` | Times | N | Only observed constant |
| RH Actuator Status | `22 10 45` | 0..7 | `raw` | 0 = Park Applied; 1 = Hold Applied; 2 = Released; 3 = Applying; 4 = Releasing; 5 = Completely Released; 6 = Unknown | N | Only observed constant |
| LH Actuator Status | `22 10 45` | 8..15 | `raw` | 0 = Park Applied; 1 = Hold Applied; 2 = Released; 3 = Applying; 4 = Releasing; 5 = Completely Released; 6 = Unknown | N | Only observed constant |
| EPB Warning Light | `22 10 46` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Meter ECU Connection Cancel Request | `22 10 46` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Auto Mode Request | `22 10 46` | 11..11 | `raw` | 0 = Manual mode request; 1 = Auto mode request | N | Only observed constant |
| Brake Hold Ready | `22 10 46` | 12..12 | `raw` | 0 = Not in stand-by mode; 1 = Stand-by mode | N | Only observed constant |
| EPB Lock Request | `22 10 46` | 13..13 | `raw` | 0 = Not requested; 1 = requested | N | Only observed constant |
| Auto Mode | `22 10 46` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Dynamic PKB Mode | `22 10 46` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| RH Actuator Motor Relay1 | `22 10 46` | 24..24 | `raw` | 0 = OFF; 1 = ON | Y |  |
| RH Actuator Motor Relay2 | `22 10 46` | 25..25 | `raw` | 0 = OFF; 1 = ON | Y |  |
| RH Actuator Motor Relay3 | `22 10 46` | 26..26 | `raw` | 0 = OFF; 1 = ON | Y |  |
| RH Actuator Motor Relay4 | `22 10 46` | 27..27 | `raw` | 0 = OFF; 1 = ON | Y |  |
| LH Actuator Motor Relay1 | `22 10 46` | 28..28 | `raw` | 0 = OFF; 1 = ON | Y |  |
| LH Actuator Motor Relay2 | `22 10 46` | 29..29 | `raw` | 0 = OFF; 1 = ON | Y |  |
| LH Actuator Motor Relay3 | `22 10 46` | 30..30 | `raw` | 0 = OFF; 1 = ON | Y |  |
| LH Actuator Motor Relay4 | `22 10 46` | 31..31 | `raw` | 0 = OFF; 1 = ON | Y |  |
| FR Speed Signal Status | `22 10 46` | 40..40 | `raw` | 0 = Normal; 1 = Fixed | N | Only observed constant |
| FL Speed Signal Status | `22 10 46` | 41..41 | `raw` | 0 = Normal; 1 = Fixed | N | Only observed constant |
| RR Speed Signal Status | `22 10 46` | 42..42 | `raw` | 0 = Normal; 1 = Fixed | Y |  |
| RL Speed Signal Status | `22 10 46` | 43..43 | `raw` | 0 = Normal; 1 = Fixed | Y |  |
| RH Actuator Current Status | `22 10 46` | 44..44 | `raw` | 0 = Normal; 1 = Invalid | N | Only observed constant |
| LH Actuator Current Status | `22 10 46` | 45..45 | `raw` | 0 = Normal; 1 = Invalid | N | Only observed constant |
| Permission of Interlocking Shift | `22 10 46` | 46..46 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of Interlocking Brake | `22 10 46` | 47..47 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of RH Interlocking PKB Lock | `22 10 46` | 56..56 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of RH Interlocking PKB Release | `22 10 46` | 57..57 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of RH Interlocking Dynamic PKB | `22 10 46` | 58..58 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of RH Interlocking PKB Full Release | `22 10 46` | 59..59 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of LH Interlocking PKB Lock | `22 10 46` | 60..60 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of LH Interlocking PKB Release | `22 10 46` | 61..61 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of LH Interlocking Dynamic PKB | `22 10 46` | 62..62 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Permission of LH Interlocking PKB Full Release | `22 10 46` | 63..63 | `raw` | 0 = Available; 1 = Not available | N | Only observed constant |
| Fade Status | `22 10 46` | 72..72 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| RH Actuator Motor Current High | `22 10 46` | 73..73 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| RH Actuator Motor Driver Operation Status | `22 10 46` | 74..74 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LH Actuator Motor Current High | `22 10 46` | 75..75 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| LH Actuator Motor Driver Operation Status | `22 10 46` | 76..76 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Speed 1 Invalid | `22 10 46` | 77..77 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Speed 2 Invalid | `22 10 46` | 78..78 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Speed 3 Invalid | `22 10 46` | 79..79 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| HVSYS Invalid | `22 10 46` | 88..88 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stroke Sensor2 | `22 10 4B` | 0..7 | `raw / 51` | V | Y |  |
| Quantity of Brake Pedal Stroke | `22 10 4C` | 0..7 | `raw` | mm | Y |  |
| Voltage of Stroke Sensor2 | `22 10 52` | 0..7 | `raw / 51` | V | Y |  |
| FR Regenerative Request | `22 10 54` | 0..15 | `raw * 16` | Nm | Y |  |
| FR Regenerative Operation | `22 10 55` | 0..15 | `raw * 16` | Nm | Y |  |
| Brake Pedal Stroke Change Speed | `22 10 58` | 0..7 | `raw * 20 - 2560` | mm/s | Y |  |
| VCSK Voltage Value | `22 10 5C` | 0..7 | `raw * 11 / 510` | V | N | Regulated supply; constant by design |
| IG1 Voltage | `22 10 73` | 0..7 | `raw * 4 / 51` | V | Y |  |
| IG2 Voltage | `22 10 73` | 8..15 | `raw * 4 / 51` | V | Y |  |
| BS Voltage | `22 10 73` | 16..23 | `raw * 4 / 51` | V | Y |  |
| BM Voltage | `22 10 73` | 24..31 | `raw * 4 / 51` | V | Y |  |
| Voltage Difference between Motor Input-Output Terminal | `22 10 76` | 0..15 | `raw * 0.01953125` | V | N | Changes only while the parking brake moves |
| Drive Mode Select Customize Item EPS Available | `22 10 B6` | 8..8 | `raw` | 0 = Not Applicable; 1 = Applicable | N | Only observed constant |
| Drive Mode Select Customize Item Adaptive Variable Suspension System Available | `22 10 B6` | 9..9 | `raw` | 0 = Not Applicable; 1 = Applicable | N | Only observed constant |
| Drive Mode Select Customize Item Brake Available | `22 10 B6` | 10..10 | `raw` | 0 = Not Applicable; 1 = Applicable | N | Only observed constant |
| ABS Solenoid (SRLR) | `22 28 01` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SRLH) | `22 28 01` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SRRR) | `22 28 01` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SRRH) | `22 28 01` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SFLR) | `22 28 01` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SFLH) | `22 28 01` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SFRR) | `22 28 01` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Solenoid (SFRH) | `22 28 01` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| TRC(TRAC)/VSC Solenoid (SM2) | `22 28 01` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| TRC(TRAC)/VSC Solenoid (SM1) | `22 28 01` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ABS Motor Relay | `22 28 01` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Solenoid Relay | `22 28 01` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| STPO | `22 28 03` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| EXO | `22 28 03` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
