# Main Body

Request ID `0x750`, response ID `0x758`, address `0x40` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine).

Read each signal with `21 LL`, the local identifier in the **Read** column (older ECU).

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Unlock Key Twice | `21 11` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk Link with Door Lock | `21 11` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto Lock | `21 11` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto Lock/Shift | `21 11` | 9..9 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto Unlock/Shift | `21 11` | 10..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| All Unlock/Open-Close | `21 11` | 11..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Automatic Door Lock Function | `21 11` | 12..13 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Automatic Door Unlock Function | `21 11` | 14..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Lighting Time | `21 12` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| I/L when ACC OFF | `21 12` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| I/L ON W/Door Key Unlock | `21 12` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Room Light when Aprchd | `21 12` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Illumination System | `21 12` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Light Control | `21 12` | 9..9 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Inside Foot Light | `21 12` | 10..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Center Console Spot Light | `21 12` | 11..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Interior Illumination Light | `21 12` | 12..12 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Key P/W Up | `21 13` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Key P/W Down | `21 13` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| P/W Up W/ Transmit | `21 13` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| P/W Down W/ Transmit | `21 13` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| P/W Up W/ Smart | `21 13` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Window Open Warning | `21 13` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Slide Roof  Open Warning | `21 13` | 9..9 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wireless Control | `21 14` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wireless Buzzer Resp | `21 14` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Hazard Answer Back | `21 14` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wireless Buzzer Resp | `21 14` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Open Door Warning | `21 14` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| P/W Wireless Ope Buzz | `21 14` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto Lock Time | `21 14` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Unlock 2 Operation | `21 14` | 9..9 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Panic Function | `21 14` | 10..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wireless Lock Function with Doors Open | `21 14` | 11..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wireless Auto Lock | `21 14` | 13..13 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto Lock Time | `21 14` | 14..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk Lid Operation | `21 14` | 16..17 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk Lid Operation | `21 14` | 21..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Response Time | `21 15` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Sensitivity | `21 15` | 1..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Headlight in Conjunction with Wiper | `21 15` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Disp Ex ON Sen | `21 15` | 8..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Disp Ex OFF Sen | `21 15` | 11..13 | `raw` |  | N | Older ECU: formula and unit not established yet |
| DRL Function | `21 15` | 16..16 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Automatic High Beam | `21 15` | 17..17 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Daylight Function | `21 15` | 18..18 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Light Auto OFF Delay | `21 15` | 24..25 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Extended Headlight Lighting | `21 15` | 26..27 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Light up Clearance Lights at Door Unlock Function | `21 15` | 28..28 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Tail Remind Buzzer Function | `21 15` | 29..29 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto Light Off Timing Setting | `21 15` | 30..30 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Out-Handle-Lgt Approached | `21 17` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Out-Handle-Lgt Unlocked | `21 17` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Out-Handle-Lgt w/Master SW | `21 17` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mirr-Foot-Lgt Approached/Exterior Light Approached | `21 17` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mirr-Foot-Lgt Unlocked/Exterior Light Unlocked | `21 17` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mirr-Foot-Lgt w/Master SW | `21 17` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Exterior Light (Door Open) | `21 17` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rear Shade Delay Time | `21 19` | 0..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| R-pos to Rear Sun Shade COM | `21 19` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Advanced Illumination System Setting | `21 19` | 3..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Start PreA/C | `21 19` | 5..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Security System | `21 1B` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Passive Mode (Security system) | `21 1B` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Glass Breakage Sensor | `21 1B` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Intrusion Sensor Function | `21 1B` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rear Wiper Stop in Back Door/Trunk Lid Open | `21 1F` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rear Wiper Washer Function | `21 1F` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto Rear Wiper in Reverse Setting | `21 1F` | 2..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Timestamp LSB | `21 31` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG ON/OFF Count | `21 32` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Data Acquisition Time | `21 32` | 8..21 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Reset Count | `21 32` | 22..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Lock/Unlock Structure | `21 33` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Lock/Unlock Memory Count | `21 33` | 1..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Lock/Unlock Storage Medium | `21 33` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Lock/Unlock Start Address | `21 33` | 16..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk/Back Door Structure | `21 34` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk/Back Door Memory Count | `21 34` | 1..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk/Back Door Storage Medium | `21 34` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk/Back Door Start Address | `21 34` | 16..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Power Window Structure | `21 3E` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Power Window Memory Count | `21 3E` | 1..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Power Window Storage Medium | `21 3E` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Power Window Start Address | `21 3E` | 16..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Security Alarm Operation Structure | `21 3F` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Security Alarm Operation Memory Count | `21 3F` | 1..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Security Alarm Operation Storage Medium | `21 3F` | 8..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Security Alarm Operation Start Address | `21 3F` | 16..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Close ALL P/W Function | `21 5D` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| User Support Service Function | `21 5D` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mem SW No. w/KeyID1 | `21 6B` | 0..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mem SW No. w/KeyID2 | `21 6B` | 3..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mem SW No. w/KeyID3 | `21 6B` | 8..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mem SW No. w/KeyID4 | `21 6B` | 11..13 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mem SW No. w/KeyID5 | `21 6B` | 16..18 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mem SW No. w/KeyID6 | `21 6B` | 19..21 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mem SW No. w/KeyID7 | `21 6B` | 24..26 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Driver Seat Memory1 | `21 6C` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Driver Seat Memory2 | `21 6C` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Driver Seat Memory3 | `21 6C` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Driver1 Memory (Facial Recognition) | `21 6C` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Driver2 Memory (Facial Recognition) | `21 6C` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Driver3 Memory (Facial Recognition) | `21 6C` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Parking Brake SW | `21 A1` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Stop Light SW | `21 A1` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| D Seat Buckle SW | `21 A1` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rear Washer SW | `21 A1` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| All Power Window UP Switch | `21 A1` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| All Power Window DOWN Switch | `21 A1` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| All Power Window AUTO Switch | `21 A1` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| D Door Courtesy SW | `21 A2` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| D-Door Lock Pos SW | `21 A2` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| FR Door Lock Pos | `21 A2` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| FR Door Courtesy SW | `21 A2` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Lock SW-Lock | `21 A2` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Lock SW-Unlock | `21 A2` | 9..9 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Key SW-Lock | `21 A2` | 10..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Key SW-Unlock | `21 A2` | 11..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| D Door Key SW-UL | `21 A2` | 12..12 | `raw` |  | N | Older ECU: formula and unit not established yet |
| M1 Switch | `21 A2` | 16..16 | `raw` |  | N | Older ECU: formula and unit not established yet |
| M2 Switch | `21 A2` | 17..17 | `raw` |  | N | Older ECU: formula and unit not established yet |
| M3 Switch | `21 A2` | 18..18 | `raw` |  | N | Older ECU: formula and unit not established yet |
| SET Switch | `21 A2` | 19..19 | `raw` |  | N | Older ECU: formula and unit not established yet |
| P Door Courtesy SW | `21 A3` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| P-Door Lock Pos SW | `21 A3` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| FL Door Lock Pos | `21 A3` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| FL Door Courtesy SW | `21 A3` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Reading Light Switch 1 | `21 A3` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Reading Light Switch 2 | `21 A3` | 9..9 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Reading Light Switch 3 | `21 A3` | 10..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Reading Light Switch 4 | `21 A3` | 11..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Trim Illumination Switch (Front) | `21 A3` | 12..12 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Trim Illumination Color Switch (Front) | `21 A3` | 13..13 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Trim Illumination Switch (Rear) | `21 A3` | 14..14 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Door Trim Illumination Color Switch (Rear) | `21 A3` | 15..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| RR Door Courtesy SW | `21 A3` | 16..16 | `raw` |  | N | Older ECU: formula and unit not established yet |
| RR-Door Lock Pos SW | `21 A3` | 17..17 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Luggage Courtesy SW | `21 A4` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Back Door Courtesy SW | `21 A4` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Back Door Open | `21 A4` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Back Door Lock Pos SW | `21 A4` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Hood Courtesy SW | `21 A4` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Glass Hatch Courtesy Switch | `21 A4` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk Lock Position | `21 A4` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk Lid Open Switch (Out Side) | `21 A4` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk Lock/Unlock SW | `21 A4` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Back Door Open Handle SW | `21 A4` | 9..9 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk Cancel Switch | `21 A4` | 10..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Glass Hatch Opener Switch | `21 A4` | 11..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Kick Sensor Connection | `21 A4` | 12..12 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Kick Sensor Detection | `21 A4` | 13..13 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Kick Sensor Error | `21 A4` | 14..14 | `raw` |  | N | Older ECU: formula and unit not established yet |
| IG SW | `21 A5` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| ACC SW | `21 A5` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Key Unlock Warning SW | `21 A5` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk/BDoor Open SW | `21 A6` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Trunk Main SW | `21 A6` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Fuel Lid Opener Switch | `21 A6` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Slide Door RH Close Switch | `21 A6` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Slide Door LH Close Switch | `21 A6` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Slide Door RH Open Switch | `21 A6` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Slide Door LH Open Switch | `21 A6` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Advanced Illumination System | `21 A6` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Back Door Open SW | `21 A6` | 8..8 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rear Right Slide Door Switch | `21 A6` | 9..9 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rear Left Slide Door Switch | `21 A6` | 10..10 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Hazard Switch | `21 A6` | 11..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Cargo Light Switch | `21 A6` | 12..12 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Dome Light DOOR SW | `21 A6` | 13..13 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Dome Light  SW | `21 A6` | 14..14 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Ceiling Light SW | `21 A6` | 15..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mirror Selection SW (R) | `21 A6` | 16..16 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mirror Selection SW (L) | `21 A6` | 17..17 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mirror Position SW (R) | `21 A6` | 18..18 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mirror Position SW (L) | `21 A6` | 19..19 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mirror Position SW (Up) | `21 A6` | 20..20 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Mirror Position SW (Dwn) | `21 A6` | 21..21 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Outer Mirror Fold SW | `21 A6` | 22..22 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Outer Mirror Return SW | `21 A6` | 23..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Dimmer SW | `21 A7` | 0..0 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Passing Light SW | `21 A7` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rear Fog Light SW | `21 A7` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Front Fog Light SW | `21 A7` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto Light SW | `21 A7` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Head Light SW (Head) | `21 A7` | 6..6 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Head Light SW (Tail) | `21 A7` | 7..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| AFS/AHS ECU High Beam Control | `21 AA` | 2..2 | `raw` |  | N | Older ECU: formula and unit not established yet |
| AFS/AHS ECU High Beam Light Status | `21 AA` | 3..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Light Sensor Connection Status | `21 AA` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Headlight Lighting Function | `21 AA` | 5..5 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Insolation Amount of Solar Sensor RH | `21 AA` | 7..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Wiper Motor Cam Switch Information | `21 AA` | 16..16 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Rear Room Light Switch | `21 AA` | 21..21 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Entry and Start System Configuration | `21 AA` | 22..22 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Insolation Amount of Solar Sensor LH | `21 AA` | 23..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Light Sensor Illuminance | `21 AB` | 3..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Hood Courtesy Switch | `21 BB` | 4..4 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Intrusion Sens OFF SW | `21 BB` | 15..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Glass Break Sensor Detection | `21 BB` | 18..18 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Intrusion Sensor | `21 BB` | 19..19 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Tilt Sensor | `21 BB` | 21..21 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Intrusion Sensor Sound Detect | `21 BB` | 23..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto H Beam STS7 | `21 C9` | 0..3 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto H Beam STS6 | `21 C9` | 4..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto H Beam STS5 | `21 C9` | 8..11 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto H Beam STS4 | `21 C9` | 12..15 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto H Beam STS3 | `21 C9` | 16..19 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto H Beam STS2 | `21 C9` | 20..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto H Beam STS1 | `21 C9` | 24..27 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Auto H Beam STS0 | `21 C9` | 28..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| The Number of DTC | `21 E1` | 0..7 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Second) | `21 E1` | 16..23 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Key Cycle | `21 E1` | 16..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Minute) | `21 E1` | 24..31 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Clock Type | `21 E1` | 32..34 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Hour) | `21 E1` | 32..39 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Month) | `21 E1` | 40..47 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Absolute Time (Day) | `21 E1` | 48..55 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Cruise Control | `21 FE` | 1..1 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Tire Prssr Mntr | `21 FE` | 42..42 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication PAD Switch | `21 FE` | 73..73 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Power Integration No.1 | `21 FE` | 74..74 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Power Integration No.2 | `21 FE` | 75..75 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Power Integration No.3 | `21 FE` | 76..76 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Body No.4 | `21 FE` | 79..79 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Body No.5 | `21 FE` | 81..81 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Mayday Gateway | `21 FE` | 90..90 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Overhead Module | `21 FE` | 95..95 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Combi Meter | `21 FE` | 98..98 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Clearance Sonar | `21 FE` | 103..103 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Parking Assist Monitor System3 | `21 FE` | 104..104 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Lighting System ECU | `21 FE` | 112..112 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Automatic High Beam Sensor | `21 FE` | 124..124 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication P-Seat | `21 FE` | 128..128 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication RR Door Quarter Window Sunshade ECU | `21 FE` | 134..134 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication RL Door Quarter Window Sunshade ECU | `21 FE` | 135..135 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication D-Door Motor | `21 FE` | 144..144 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication P-Door Motor | `21 FE` | 145..145 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication RR-Door Motor | `21 FE` | 146..146 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication RL-Door Motor | `21 FE` | 147..147 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Wiper ECU | `21 FE` | 150..150 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Wiper Switch | `21 FE` | 151..151 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Wiper Module | `21 FE` | 152..152 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication ILLUMINATION ECU | `21 FE` | 160..160 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication D-Door/Mirror | `21 FE` | 161..161 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication P-Door | `21 FE` | 162..162 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication RR-Slide Door | `21 FE` | 164..164 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication RL-Slide Door | `21 FE` | 167..167 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Tilt&Tele | `21 FE` | 168..168 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Rear Sliding Sunshade ECU | `21 FE` | 170..170 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication D-Seat | `21 FE` | 171..171 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Sliding Sunshade ECU | `21 FE` | 172..172 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Slide Roof | `21 FE` | 173..173 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Humidity/Rain Sensor | `21 FE` | 176..176 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Combination SW | `21 FE` | 179..179 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Entry&Start/Wireless | `21 FE` | 181..181 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Comm RMT Start | `21 FE` | 182..182 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Comm RMT Start | `21 FE` | 183..183 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Power B-Door | `21 FE` | 184..184 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Back Door | `21 FE` | 185..185 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Accessory GW | `21 FE` | 200..200 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Illumination Module "A" | `21 FE` | 202..202 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Illumination Module "B" | `21 FE` | 203..203 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication D-Seat Switch | `21 FE` | 220..220 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Rear Console SW | `21 FE` | 222..222 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Theft Deterrent | `21 FE` | 224..224 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Mayday | `21 FE` | 225..225 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Double Locking | `21 FE` | 229..229 | `raw` |  | N | Older ECU: formula and unit not established yet |
| Communication Master SW | `21 FE` | 236..236 | `raw` |  | N | Older ECU: formula and unit not established yet |
