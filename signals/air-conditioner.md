# Air Conditioner

Request ID `0x7C4`, response ID `0x7CC`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50`), then `10 03` (reply starts `50`; a refusal is fine).

Read each signal with `21 LL`, the local identifier in the **Read** column (older ECU).

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Room Temperature Sensor | `21 21` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Ambient Temp Sensor | `21 22` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Solar Sensor (D Side) | `21 24` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Solar Sensor (P Side) | `21 25` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Engine Coolant Temp | `21 26` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Blower Motor Speed Level | `21 3C` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Adjusted Ambient Temp | `21 3D` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Mix Servo Targ Pulse(D) | `21 41` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Mix Servo Actual Pulse(D) | `21 41` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Mix Servo Targ Pulse(P) | `21 42` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Mix Servo Actual Pulse(P) | `21 42` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Outlet Servo Pulse (D) | `21 43` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Outlet Servo Actu Pulse(D) | `21 43` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Inlet Damper Targ Pulse | `21 44` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Inlet Damper Actual Pulse | `21 44` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Outlet Servo Targ Pulse(P) | `21 46` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Outlet Servo Actu Pulse(P) | `21 46` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Compressor Speed | `21 49` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Compressor Target Speed | `21 4A` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Evaporator Fin Thermistor | `21 4B` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Evaporator Target Temp | `21 4C` | 0..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Regulator Pressure Sensor | `21 53` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Outlet Servo Pulse (R) | `21 70` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Outlet Servo Actual Pls(R) | `21 70` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Pls Servo Test Sts | `21 7C` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Inlet Mode | `21 81` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Compressor Mode | `21 81` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Compressor/DEF Mode | `21 81` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| DEF(PSW/Lock) Mode Setting | `21 81` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Evaporator Control | `21 81` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Button Press Buzzer | `21 81` | 16..16 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Filter Sensor | `21 81` | 17..19 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Add Foot Air in FACE Mode | `21 81` | 20..20 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Foot/DEF Auto Blow Up | `21 81` | 21..21 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Foot/DEF Auto Mode | `21 81` | 23..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Ambient Temperature Shift | `21 82` | 0..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Shift Set Temperature | `21 82` | 8..12 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Emission Gas Sensor Shift | `21 82` | 16..22 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Steering Heater Sensitivity Adjust | `21 84` | 0..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Seat Heater Auto Operation | `21 84` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Air Inlet Damper Mode | `21 84` | 9..9 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Blower Operation Setting (Stop&Start) | `21 84` | 11..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Steering Heater Auto Function | `21 84` | 12..12 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ECO Mode Start Function | `21 84` | 13..13 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto Mode Start Function | `21 84` | 14..14 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Refrigerant Gas Type | `21 86` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Set Temperature (D Side) | `21 91` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Set Temperature (P Side) | `21 92` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ECO Switch | `21 98` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Solar Ventilation Switch | `21 98` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IDLE UP/PWR HEAT Switch | `21 98` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rear FACE Shut Switch | `21 98` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| A/C Cutoff Signal | `21 98` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Compressor Drive Check | `21 98` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Humidity Level | `21 98` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Operation Limitation Control History Count (Level1) | `21 B8` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Operation Limitation Control History Count (Level2) | `21 BA` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Refrigerant High Pressure History | `21 DA` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Number of Trouble Codes | `21 E1` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
