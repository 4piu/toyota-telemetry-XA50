# Front Radar Sensor

Request ID `0x750`, response ID `0x758`, address `0x0F` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Answers; not recorded yet |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Answers; not recorded yet |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Answers; not recorded yet |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Answers; not recorded yet |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw * 625000 / 388357` | km | N | Answers; not recorded yet |
| $0106-0-$FF | `22 01 06` | 0..7 | `raw` |  | N | Answers; not recorded yet |
| Vehicle Setting Value Delta X | `22 01 06` | 8..15 | `raw * 0.01` | m | N | Answers; not recorded yet |
| Vehicle Setting Value Delta Z | `22 01 06` | 16..31 | `raw * 0.01` | m | N | Answers; not recorded yet |
| Vehicle Setting Value H | `22 01 06` | 32..39 | `raw * 0.01` | m | N | Answers; not recorded yet |
| Absolute Value Time (Year) | `22 01 09` | 0..7 | `raw` | Year | N | Answers; not recorded yet |
| Absolute Value Time (Month) | `22 01 09` | 8..15 | `raw` | Month | N | Answers; not recorded yet |
| Absolute Value Time (Day) | `22 01 09` | 16..23 | `raw` | Day | N | Answers; not recorded yet |
| Absolute Value Time (Hour) | `22 01 09` | 24..31 | `raw` | Hour | N | Answers; not recorded yet |
| Absolute Value Time (Minute) | `22 01 09` | 32..39 | `raw` | Minute | N | Answers; not recorded yet |
| Absolute Value Time (Second) | `22 01 09` | 40..47 | `raw` | Second | N | Answers; not recorded yet |
| Radio Wave Irradiation Condition | `22 02 04` | 10..10 | `raw` | 0 = Under Radio Wave Irradiation; 1 = Under Radio Wave Stopping | N | Answers; not recorded yet |
| Optical Axis Adjustment Completed Status | `22 10 02` | 8..8 | `raw` | 0 = Not Complete; 1 = Complete | N | Answers; not recorded yet |
| Acceleration Sensor Calibration Completed Status | `22 10 02` | 9..9 | `raw` | 0 = Not Complete; 1 = Complete | N | Answers; not recorded yet |
| Optical Axis Not Adjusted Status | `22 10 03` | 8..8 | `raw` | 0 = Complete; 1 = Not Complete | N | Answers; not recorded yet |
| Optical Axis Adjusting Low Mount Flag | `22 10 03` | 9..9 | `raw` | 0 = Normal Mount; 1 = Low Mount | N | Answers; not recorded yet |
| Axis Adjustment Error Code | `22 10 05` | 0..4 | `raw` | 0 = Normal; 1 = 31 | N | Answers; not recorded yet |
| Optical Axis Offset Angle Output Completed Status | `22 10 06` | 8..8 | `raw` | 0 = Not Complete; 1 = Complete | N | Answers; not recorded yet |
| Optical Axis Adjustment Angle (Horizontal) (Latest) | `22 10 0B` | 0..7 | `raw * 0.1` | deg | N | Answers; not recorded yet |
| Optical Axis Adjustment Angle (Vertical) (Latest) | `22 10 0B` | 8..15 | `raw * 0.1` | deg | N | Answers; not recorded yet |
| $100B-2-$FFFF | `22 10 0B` | 16..31 | `raw * 0.0078125 - 69.2` | dBm | N | Answers; not recorded yet |
| Optical Axis Adjustment Angle (Horizontal) (2nd Latest) | `22 10 0C` | 0..7 | `raw * 0.1` | deg | N | Answers; not recorded yet |
| Optical Axis Adjustment Angle (Vertical) (2nd Latest) | `22 10 0C` | 8..15 | `raw * 0.1` | deg | N | Answers; not recorded yet |
| $100C-2-$FFFF | `22 10 0C` | 16..31 | `raw * 0.0078125 - 69.2` | dBm | N | Answers; not recorded yet |
| Optical Axis Offset Angle (Horizontal) | `22 10 0D` | 0..7 | `raw * 0.1` | deg | N | Answers; not recorded yet |
| Optical Axis Offset Angle (Vertical) | `22 10 0D` | 8..15 | `raw * 0.1` | deg | N | Answers; not recorded yet |
| Optical Axis Offset Angle (Horizontal) (with On-Line Calibration Angle) | `22 10 0E` | 0..7 | `raw * 0.1` | deg | N | Answers; not recorded yet |
| Optical Axis Offset Angle (Vertical) (with On-Line Calibration Angle) | `22 10 0E` | 8..15 | `raw * 0.1` | deg | N | Answers; not recorded yet |
| Data Flash Malfunction | `22 10 10` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Power Supply Circuit Malfunction | `22 10 10` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Millimeter-wave Circuit Malfunction | `22 10 10` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| eDMA Transfer Completed Interrupt  Malfunction | `22 10 10` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Acceleration Sensor Malfunction | `22 10 10` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Microwave Integrated Circuit to Antenna in Front Radar Sensor Communication Error | `22 10 10` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Antenna to Microwave Integrated Circuit in Front Radar Sensor Communication Error | `22 10 10` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Microwave Integrated Circuit (VCO Circuit) in Front Radar Sensor Internal Electronic Failure | `22 10 10` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Main Microcomputer in Front Radar Sensor Communication Error | `22 10 10` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| IG Voltage Protection | `22 10 11` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Optical Axis Misalignment (Vertical - Acceleration Sensor Method) | `22 10 12` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Optical Axis Misalignment (Horizontal) | `22 10 12` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Optical Axis Misalignment (Vertical - Acceleration Sensor Method and Recognition Method) | `22 10 12` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Optical Axis Misalignment (Vertical) | `22 10 12` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Front Radar Sensor Temporary Unavailable (Horizontal Direction Optical Axis Misalignment) | `22 10 12` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Front Radar Sensor Temporary Unavailable (Vertical Direction Optical Axis Misalignment) | `22 10 12` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Millimeter-wave Circuit Temperature Protection | `22 10 14` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Destination Malfunction | `22 10 15` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| IG Voltage | `22 10 16` | 0..7 | `raw * 0.1` | V | N | Answers; not recorded yet |
| Front Radar Sensor Temperature | `22 10 17` | 0..15 | `raw` | ℃ | N | Answers; not recorded yet |
| Vehicle Speed | `22 10 18` | 0..15 | `raw * 0.00390625 - 30` | km/h | N | Answers; not recorded yet |
| PCS Destination Variation No | `22 10 19` | 0..7 | `raw` |  | N | Answers; not recorded yet |
| Radar Cruise Control System Destination Variation No | `22 10 19` | 8..15 | `raw` |  | N | Answers; not recorded yet |
| TMN Destination Variation No | `22 10 19` | 16..23 | `raw` |  | N | Answers; not recorded yet |
| Front Radar Sensor Destination Variation No | `22 10 19` | 24..31 | `raw` |  | N | Answers; not recorded yet |
| Acceleration Suppression at Low Speed Destination Variation No | `22 10 19` | 32..39 | `raw` |  | N | Answers; not recorded yet |
| Full Blockage (Detection Range Observer) | `22 10 33` | 8..8 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Full Blockage (Detection Time Out Observer) | `22 10 33` | 9..9 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Full Blockage (Detection Object Loss Observer) | `22 10 33` | 10..10 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Partial Blockage (SLL Observer) | `22 10 33` | 11..11 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Partial Blockage (ADE Observer) | `22 10 33` | 12..12 | `raw` | 0 = Not Detected; 1 = Detected | N | Answers; not recorded yet |
| Ambient Temperature | `22 10 35` | 0..7 | `raw * 0.625` | ℃ | N | Answers; not recorded yet |
| Wiper Operation Status | `22 10 36` | 0..7 | `raw` | 0 = Front Wiper Stop; 1 = Front Wiper INT; 2 = Front Wiper LO; 3 = Front Wiper HI; 4 = Front Wiper Storage; 5 = Service Position; 7 = Front Wiper Malfunction | N | Answers; not recorded yet |
| On-Line Calibration Angle (Horizontal) | `22 20 09` | 0..7 | `raw * 0.1` | deg | N | Answers; not recorded yet |
| On-Line Calibration Angle (Vertical) | `22 20 09` | 8..15 | `raw * 0.1` | deg | N | Answers; not recorded yet |
