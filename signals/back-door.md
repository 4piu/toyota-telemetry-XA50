# Back Door

Request ID `0x750`, response ID `0x758`, address `0xB8` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Answers; not recorded yet |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Answers; not recorded yet |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Answers; not recorded yet |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 0 = -; 1 = km; 2 = mile | N | Answers; not recorded yet |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw` | km | N | Answers; not recorded yet |
| Absolute Value Time (Year) | `22 01 09` | 0..7 | `raw` | Year | N | Answers; not recorded yet |
| Absolute Value Time (Month) | `22 01 09` | 8..15 | `raw` | Month | N | Answers; not recorded yet |
| Absolute Value Time (Day) | `22 01 09` | 16..23 | `raw` | Day | N | Answers; not recorded yet |
| Absolute Value Time (Hour) | `22 01 09` | 24..31 | `raw` | Hour | N | Answers; not recorded yet |
| Absolute Value Time (Minute) | `22 01 09` | 32..39 | `raw` | Minute | N | Answers; not recorded yet |
| Absolute Value Time (Second) | `22 01 09` | 40..47 | `raw` | Second | N | Answers; not recorded yet |
| Back Door Handle Switch | `22 10 10` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Back Door Lock Status | `22 10 10` | 9..9 | `raw` | 0 = Lock; 1 = Unlock | N | Answers; not recorded yet |
| Half Latch Switch | `22 10 10` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Full Latch Switch | `22 10 10` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Closer Position Switch | `22 10 10` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Pawl Switch | `22 10 10` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Courtesy Switch | `22 10 10` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Tail Gate Courtesy Switch RH | `22 10 10` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Tail Gate Courtesy Switch LH | `22 10 10` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Left Outside Open Switch | `22 10 11` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Left Outside Close Switch | `22 10 11` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Right Outside Open Switch | `22 10 11` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Right Outside Close Switch | `22 10 11` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Main Switch | `22 10 11` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Close Switch | `22 10 11` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| IG Switch | `22 10 11` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Kick Sensor Function | `22 10 11` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Close and Lock Switch | `22 10 11` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Door Lock Signal | `22 10 11` | 26..26 | `raw` | 0 = Lock; 1 = Unlock | N | Answers; not recorded yet |
| Status of Kick Sensor Detection | `22 10 11` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Status of Kick Sensor Abnormal | `22 10 11` | 28..28 | `raw` | 0 = Normal; 1 = Abnormal | N | Answers; not recorded yet |
| Kick Sensor Installation | `22 10 11` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Touch Sensor RH | `22 10 12` | 0..7 | `raw` | 0 = OFF; 1 = ON; 2 = Open | N | Answers; not recorded yet |
| PBD Touch Sensor LH | `22 10 12` | 8..15 | `raw` | 0 = OFF; 1 = ON; 2 = Open | N | Answers; not recorded yet |
| Correction of Back Door Position | `22 10 13` | 8..8 | `raw` | 0 = Not Complied With; 1 = Complied With | N | Answers; not recorded yet |
| PBD Close Switch Operation History | `22 10 14` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Close & Lock Switch Operation History | `22 10 14` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Left Outside Open Switch Operation History | `22 10 14` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Left Outside Close Switch Operation History | `22 10 14` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Right Outside Open Switch Operation History | `22 10 14` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Right Outside Close Switch Operation History | `22 10 14` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Kick Sensor Operation History | `22 10 14` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Opened Number of Times | `22 10 17` | 0..31 | `raw` |  | N | Answers; not recorded yet |
| Closed Number of Times | `22 10 17` | 32..63 | `raw` |  | N | Answers; not recorded yet |
| PBD SW Operating Condition Setting | `22 22 02` | 0..7 | `raw` | 0 = Short Press (0.04s); 1 = Long Press (0.8s) | N | Answers; not recorded yet |
| PBD Buzzer Function | `22 22 03` | 0..7 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Assist Close Function | `22 22 04` | 0..7 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Assist Open Function | `22 22 05` | 0..7 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Door Unlock with One Motion Setting | `22 22 08` | 0..7 | `raw` | 0 = Back Door; 1 = All Doors | N | Answers; not recorded yet |
| PBD Wireless Function | `22 22 08` | 8..15 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Wireless One Motion Function | `22 22 08` | 16..23 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| PBD Wireless SW Setting | `22 22 08` | 24..31 | `raw` | 0 = 1 Time Press; 1 = 2 Times Press; 2 = Long Press (0.8s) | N | Answers; not recorded yet |
| PBD Kick-in Beep Sound | `22 22 0D` | 0..7 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
