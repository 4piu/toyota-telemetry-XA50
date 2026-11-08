# Brake Booster

Request ID `0x750`, response ID `0x758`, address `0x29` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Answers; not recorded yet |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Answers; not recorded yet |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Answers; not recorded yet |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Answers; not recorded yet |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw` | km | N | Answers; not recorded yet |
| IG Switch | `22 10 2D` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Downhill Assist Control/Crawl Control SW | `22 10 2D` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Stop Light SW | `22 10 2D` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Parking Brake SW | `22 10 2D` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Brake Pedal Load Sensing SW | `22 10 2D` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Rear Diff Lock Switch | `22 10 2D` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Automatic Disconnect Diff SW | `22 10 2D` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| L4 Switch | `22 10 2D` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Reverse Position Switch | `22 10 2D` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Brake Hold Switch | `22 10 2D` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Stop Light Relay | `22 10 2D` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Inspection Mode | `22 10 2D` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| TRC(TRAC) Control | `22 10 2D` | 29..29 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| TRC(TRAC) Engine Control | `22 10 2D` | 30..30 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| TRC(TRAC) Brake Control | `22 10 2D` | 31..31 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| FR Wheel VSC Ctrl Status | `22 10 2D` | 40..40 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| FL Wheel VSC Ctrl Status | `22 10 2D` | 41..41 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| RR Wheel VSC Ctrl Status | `22 10 2D` | 42..42 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| RL Wheel VSC Ctrl Status | `22 10 2D` | 43..43 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| FR Wheel ABS Ctrl Status | `22 10 2D` | 44..44 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| FL Wheel ABS Ctrl Status | `22 10 2D` | 45..45 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| RR Wheel ABS Ctrl Status | `22 10 2D` | 46..46 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| RL Wheel ABS Ctrl Status | `22 10 2D` | 47..47 | `raw` | 0 = Out of controlling; 1 = Under Controlling | N | Answers; not recorded yet |
| HAB Control | `22 10 2D` | 56..56 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Fail Status | `22 10 2D` | 57..57 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Fail Control | `22 10 2D` | 58..58 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| BA Ctrl Status | `22 10 2D` | 59..59 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBA Ctrl Status | `22 10 2D` | 60..60 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Stop Light Relay State for ECU Control | `22 10 2D` | 61..61 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Engine Stop by Stop and Start System | `22 10 2D` | 62..62 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Solenoid State for ECU Control | `22 10 2D` | 63..63 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Motor State for ECU Control | `22 10 2D` | 72..72 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Operation History of IG OFF while Vehicle is moving | `22 10 2D` | 73..73 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Plumbing information of brake | `22 10 2D` | 74..74 | `raw` | 0 = Anteroposterior plumbing; 1 = X plumbing | N | Answers; not recorded yet |
| Buzzer | `22 10 2D` | 75..75 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Dealer Mode | `22 10 2D` | 76..76 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Zero Point Memory State of Steering Angle Sensor | `22 10 2D` | 77..77 | `raw` | 0 = Zero point is not memorized; 1 = Zero point is memorized | N | Answers; not recorded yet |
| Regenerative Cooperation | `22 10 2D` | 78..78 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| TRC(TRAC)/VSC OFF SW | `22 10 2D` | 88..88 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Reservoir Warning SW | `22 10 2D` | 89..89 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ECB Motor Relay | `22 10 2D` | 90..90 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ECB Motor Relay2 | `22 10 2D` | 91..91 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ECB Solenoid (SGH) | `22 10 2D` | 92..92 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ECB Solenoid (SSA) | `22 10 2D` | 93..93 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| MTT | `22 10 2D` | 94..94 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| IGP_PT1 | `22 10 2D` | 106..106 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| IGP_PT2 | `22 10 2D` | 107..107 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| FR Wheel | `22 10 2D` | 108..108 | `raw` | 0 = Forward; 1 = Back | N | Answers; not recorded yet |
| FL Wheel | `22 10 2D` | 109..109 | `raw` | 0 = Forward; 1 = Back | N | Answers; not recorded yet |
| RR Wheel | `22 10 2D` | 110..110 | `raw` | 0 = Forward; 1 = Back | N | Answers; not recorded yet |
| RL Wheel | `22 10 2D` | 111..111 | `raw` | 0 = Forward; 1 = Back | N | Answers; not recorded yet |
| Brakes Specifications Change by C-BEST | `22 10 2D` | 122..122 | `raw` | 0 = None; 1 = Exist | N | Answers; not recorded yet |
| Zero Point Memory Status of Turning Angle | `22 10 2D` | 123..123 | `raw` | 0 = Zero Point is Not Memorized; 1 = Zero Point is Memorized | N | Answers; not recorded yet |
| SLM2 Refresh Drive Completed Status | `22 10 2D` | 124..124 | `raw` | 0 = Not Complete; 1 = Complete | N | Answers; not recorded yet |
| Neutral Switch | `22 10 2D` | 125..125 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Brake ECU Communication Open History | `22 10 2E` | 72..72 | `raw` | 0 = None; 1 = Exist | N | Answers; not recorded yet |
| Gap Hold Chamber Pressure Sensor Open History | `22 10 2E` | 73..73 | `raw` | 0 = None; 1 = Exist | N | Answers; not recorded yet |
| Vacuum Booster Pressure Sensor Open History | `22 10 2E` | 75..75 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Atmospheric Pressure Sensor Open History | `22 10 2E` | 76..76 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| FR Speed Open | `22 10 2F` | 8..8 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| FL Speed Open | `22 10 2F` | 9..9 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| RR Speed Open | `22 10 2F` | 10..10 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| RL Speed Open | `22 10 2F` | 11..11 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| EFI Communication Open | `22 10 2F` | 12..12 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Yaw Rate Open | `22 10 2F` | 13..13 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Deceleration Open | `22 10 2F` | 14..14 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Steering Open | `22 10 2F` | 15..15 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Master Cylinder Open | `22 10 2F` | 24..24 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| FR Speed Sensor Voltage Open | `22 10 2F` | 25..25 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| FL Speed Sensor Voltage Open | `22 10 2F` | 26..26 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| RR Speed Sensor Voltage Open | `22 10 2F` | 27..27 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| RL Speed Sensor Voltage Open | `22 10 2F` | 28..28 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| M/C Pressure Sensor Noise | `22 10 2F` | 29..29 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Brake Pedal Switch Open | `22 10 2F` | 30..30 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Yaw Rate Sensor Voltage Open | `22 10 2F` | 31..31 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| EFI Communication Invalid | `22 10 2F` | 40..40 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Master Cylinder Pressure Sensor Power Supply Open | `22 10 2F` | 41..41 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Solenoid Power Supply Open | `22 10 2F` | 42..42 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Motor Power Supply Open | `22 10 2F` | 43..43 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Stop Switch Open | `22 10 2F` | 44..44 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| A/C ECU Communication Open | `22 10 2F` | 45..45 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Navigation ECU Communication Open | `22 10 2F` | 46..46 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Stop and Start ECU Communication Open | `22 10 2F` | 47..47 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Air Bag ECU Communication Open | `22 10 2F` | 56..56 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Stroke Open | `22 10 2F` | 57..57 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Stroke2 Open | `22 10 2F` | 58..58 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Accumulator Open | `22 10 2F` | 59..59 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| HV Communication Open | `22 10 2F` | 60..60 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Reaction Force Pressure Sensor Open | `22 10 2F` | 61..61 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Servo Pressure Sensor Open | `22 10 2F` | 62..62 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Body ECU Communication Open | `22 10 2F` | 63..63 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Brake ECU Communication Open | `22 10 2F` | 72..72 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Gap Hold Chamber Pressure Sensor Open | `22 10 2F` | 73..73 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| EPS/Steering Control Actuator ECU Communication Open | `22 10 2F` | 74..74 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Vacuum Booster Pressure Sensor Open | `22 10 2F` | 75..75 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Atmospheric Pressure Sensor Open | `22 10 2F` | 76..76 | `raw` | 0 = Normal; 1 = Under intermittent | N | Answers; not recorded yet |
| Reaction Force Pressure | `22 10 48` | 0..7 | `raw * 0.096` | MPa | N | Answers; not recorded yet |
| Stroke Sensor | `22 10 4A` | 0..7 | `raw / 51` | V | N | Answers; not recorded yet |
| Quantity of Brake Pedal Stroke | `22 10 4C` | 0..7 | `raw` | mm | N | Answers; not recorded yet |
| Servo Pressure | `22 10 4D` | 0..7 | `raw * 0.096` | MPa | N | Answers; not recorded yet |
| Accumulator Pressure | `22 10 4F` | 0..7 | `raw * 0.096` | MPa | N | Answers; not recorded yet |
| Voltage of Stroke Sensor | `22 10 51` | 0..7 | `raw / 51` | V | N | Answers; not recorded yet |
| Brake Pedal Stroke Change Speed | `22 10 58` | 0..7 | `raw * 20 - 2560` | mm/s | N | Answers; not recorded yet |
| Reaction Force Oil Pressure Change Speed | `22 10 59` | 0..7 | `raw - 30` | MPa/s | N | Answers; not recorded yet |
| Target Oil Pressure | `22 10 5A` | 0..7 | `raw * 4 / 51` | MPa | N | Answers; not recorded yet |
| VCSK Voltage Value | `22 10 5C` | 0..7 | `raw * 11 / 510` | V | N | Answers; not recorded yet |
| SLA Solenoid Current | `22 10 5E` | 0..7 | `raw / 170` | A | N | Answers; not recorded yet |
| SLR Solenoid Current | `22 10 5F` | 0..7 | `raw / 170` | A | N | Answers; not recorded yet |
| SGH Solenoid Current | `22 10 60` | 0..7 | `raw / 85` | A | N | Answers; not recorded yet |
| SSA Solenoid Current | `22 10 61` | 0..7 | `raw / 85` | A | N | Answers; not recorded yet |
| IG1 Voltage | `22 10 72` | 0..7 | `raw * 4 / 51` | V | N | Answers; not recorded yet |
| IG2 Voltage | `22 10 72` | 8..15 | `raw * 4 / 51` | V | N | Answers; not recorded yet |
| BI Voltage | `22 10 72` | 16..23 | `raw * 4 / 51` | V | N | Answers; not recorded yet |
| BS Voltage | `22 10 72` | 24..31 | `raw * 4 / 51` | V | N | Answers; not recorded yet |
| ABS Solenoid (SRLR) | `22 28 04` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ABS Solenoid (SRLH) | `22 28 04` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ABS Solenoid (SRRR) | `22 28 04` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ABS Solenoid (SRRH) | `22 28 04` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ABS Solenoid (SFLR) | `22 28 04` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ABS Solenoid (SFLH) | `22 28 04` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ABS Solenoid (SFRR) | `22 28 04` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ABS Solenoid (SFRH) | `22 28 04` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ABS Motor Relay | `22 28 04` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Solenoid Relay | `22 28 04` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ECB Solenoid (SSA) | `22 28 04` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ECB Solenoid (SGH) | `22 28 04` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| ECB Main Relay | `22 28 04` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Brake Booster Motor | `22 28 04` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| SLR Solenoid Current | `22 28 05` | 0..7 | `raw * 0.012` | A | N | Answers; not recorded yet |
| SLA Solenoid Current | `22 28 05` | 8..15 | `raw * 0.012` | A | N | Answers; not recorded yet |
