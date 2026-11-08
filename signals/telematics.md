# Telematics

Request ID `0x750`, response ID `0x758`, address `0xC7` (behind the gateway: start every frame with it). Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Answers; not recorded yet |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Answers; not recorded yet |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Answers; not recorded yet |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 0 = -; 1 = km; 2 = mile | N | Answers; not recorded yet |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw * 625000 / 388357` | km | N | Answers; not recorded yet |
| Absolute Value Time (Year) | `22 01 09` | 0..7 | `raw` | Year | N | Answers; not recorded yet |
| Absolute Value Time (Month) | `22 01 09` | 8..15 | `raw` | Month | N | Answers; not recorded yet |
| Absolute Value Time (Day) | `22 01 09` | 16..23 | `raw` | Day | N | Answers; not recorded yet |
| Absolute Value Time (Hour) | `22 01 09` | 24..31 | `raw` | Hour | N | Answers; not recorded yet |
| Absolute Value Time (Minute) | `22 01 09` | 32..39 | `raw` | Minute | N | Answers; not recorded yet |
| Absolute Value Time (Second) | `22 01 09` | 40..47 | `raw` | Second | N | Answers; not recorded yet |
| IMEI | `22 10 03` | 8..127 | `raw` |  | N | Answers; not recorded yet |
| Software Version High | `22 10 04` | 0..79 | `raw` |  | N | Answers; not recorded yet |
| Software Version Low | `22 10 04` | 80..159 | `raw` |  | N | Answers; not recorded yet |
| MSISDN | `22 10 06` | 0..127 | `raw` |  | N | Answers; not recorded yet |
| PLMN(MCC) | `22 10 17` | 0..23 | `raw` |  | N | Answers; not recorded yet |
| PLMN(MNC) | `22 10 17` | 24..47 | `raw` |  | N | Answers; not recorded yet |
| ICCID(High) | `22 10 18` | 0..79 | `raw` |  | N | Answers; not recorded yet |
| ICCID(Low) | `22 10 18` | 80..159 | `raw` |  | N | Answers; not recorded yet |
| Communication Status with TSC | `22 10 1B` | 0..7 | `raw` | 0 = Incomplete; 1 = Complete | N | Answers; not recorded yet |
| Telematics Activation Status | `22 10 1E` | 0..7 | `raw` | 0 = Shipping; 1 = Active | N | Answers; not recorded yet |
| Remote Vehicle Tracking | `22 10 1F` | 0..0 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Brand TOYOTA | `22 10 1F` | 1..1 | `raw` | 0 = No; 1 = Yes | N | Answers; not recorded yet |
| OTA Reprogramming | `22 10 1F` | 2..2 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Dormant Flag | `22 10 1F` | 3..3 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| New Energy Vehicle | `22 10 1F` | 4..4 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| Collision Management Function | `22 10 1F` | 6..6 | `raw` | 0 = OFF; 1 = ON | N | Answers; not recorded yet |
| HELPNET Function | `22 10 1F` | 7..7 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Information Collection via Ethernet | `22 10 1F` | 8..8 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Information Collection via CAN | `22 10 1F` | 9..9 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Service Function | `22 10 1F` | 10..10 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Vehicle State Notification | `22 10 1F` | 11..11 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Stolen Vehicle Tracking | `22 10 1F` | 12..12 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| IP Remote Detected DTC | `22 10 1F` | 13..13 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| IP Remote FFD | `22 10 1F` | 14..14 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| IP Remote Monitoring | `22 10 1F` | 15..15 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Warning | `22 10 1F` | 16..16 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Detected DTC | `22 10 1F` | 17..17 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote FFD | `22 10 1F` | 18..18 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Detect DTC | `22 10 1F` | 19..19 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Confirmation | `22 10 1F` | 20..20 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Precaution | `22 10 1F` | 21..21 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Monitoring | `22 10 1F` | 22..22 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Diagnostics Recorder | `22 10 1F` | 23..23 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Operation | `22 10 1F` | 24..24 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Alarm Notification | `22 10 1F` | 25..25 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Immobilizer | `22 10 1F` | 26..26 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Vehicle Location | `22 10 1F` | 27..27 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Navigation Display | `22 10 1F` | 28..28 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Vehicle Control History | `22 10 1F` | 29..29 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Maintenance Message | `22 10 1F` | 30..30 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote A/C by Smart Key | `22 10 1F` | 31..31 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| eConnect State Notification | `22 10 1F` | 32..32 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| eConnect Event Notification | `22 10 1F` | 33..33 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| eConnect State Change Notification | `22 10 1F` | 34..34 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| eConnect Remote Operation | `22 10 1F` | 35..35 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Driving Trip Data Notification | `22 10 1F` | 36..36 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| eConnect Remote Vehicle Location | `22 10 1F` | 37..37 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Automatic Collision Notification Function | `22 10 21` | 0..0 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Manual Emergency Call Function | `22 10 21` | 1..1 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Wireless Health Check Function | `22 10 21` | 2..2 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Stolen Vehicle Tracking Function | `22 10 21` | 3..3 | `raw` | 0 = Disable; 1 = Enable | N | Answers; not recorded yet |
| Remote Engine Starter Registration Status | `22 10 81` | 0..7 | `raw` | 0 = Not Registered; 1 = Registered | N | Answers; not recorded yet |
| Telematics Microphone Volume Adjust | `22 20 05` | 0..7 | `raw` | 1 = 1(MIN); 2 = 2; 3 = 3; 4 = 4; 5 = 5; 6 = 6; 7 = 7; 8 = 8; 9 = 9; 10 = 10; 11 = 11(MAX) | N | Answers; not recorded yet |
| Telematics Speaker Volume Adjust | `22 20 06` | 0..7 | `raw` | 1 = 1(MIN); 2 = 2; 3 = 3; 4 = 4; 5 = 5; 6 = 6; 7 = 7; 8 = 8; 9 = 9; 10 = 10; 11 = 11; 12 = 12; 13 = 13; 14 = 14(MAX) | N | Answers; not recorded yet |
| Remote Door Lock Function | `22 20 40` | 0..7 | `raw` | 0 = Not Registered; 1 = Registered; 3 = Registration Not Required | N | Answers; not recorded yet |
| Communication Remote Engine Starter | `22 20 41` | 0..7 | `raw` | 0 = Not Available; 1 = Available | N | Answers; not recorded yet |
| Communication Pausing State | `22 20 43` | 0..7 | `raw` | 0 = Cancel Pausing; 1 = Pausing | N | Answers; not recorded yet |
| Green Indicator | `22 28 01` | 0..7 | `raw` | 0 = Lighting OFF; 1 = Nighttime Brightness-Low; 2 = Nighttime Brightness-Middle; 3 = Nighttime Brightness-High; 4 = Daytime Brightness | N | Answers; not recorded yet |
| Red Indicator | `22 28 02` | 0..7 | `raw` | 0 = Lighting OFF; 1 = Nighttime Brightness-Low; 2 = Nighttime Brightness-Middle; 3 = Nighttime Brightness-High; 4 = Daytime Brightness | N | Answers; not recorded yet |
| Beep | `22 28 03` | 0..15 | `raw` | 0 = OFF; 1 = OFF; 2 = OFF; 3 = OFF; 4 = OFF; 5 = OFF; 6 = OFF; 7 = OFF; 8 = OFF; 9 = OFF; 10 = OFF; 11 = OFF; 12 = OFF; 13 = OFF; 14 = OFF; 4353 = Vol01; 4354 = Vol02; 4355 = Vol03; 4356 = Vol04; 4357 = Vol05; 4358 = Vol06; 4359 = Vol07; 4360 = Vol08; 4361 = Vol09; 4362 = Vol10; 4363 = Vol11; 4364 = Vol12; 4365 = Vol13; 4366 = Vol14 | N | Answers; not recorded yet |
