# Engine

Request ID `0x700`, response ID `0x708`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

This address answers for Engine (521 signals), Cruise Control (9 signals).

## Engine

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | N | Only observed constant |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | N | Only observed constant |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw` | km | Y |  |
| Status of Active Grille Air Shutter | `22 01 06` | 0..7 | `raw` | 0 = Incomplete; 1 = Existence; 2 = Nonexistence | N | Only observed constant |
| BATT Voltage | `22 10 02` | 0..15 | `raw * 0.001220703125` | V | Y |  |
| IG ON Duration Time | `22 10 07` | 0..31 | `raw * 8.192` | ms | Y |  |
| IG OFF Duration Time | `22 10 07` | 32..63 | `raw * 8.192` | ms | N | Only observed constant |
| Engine Oil Level SW | `22 10 11` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Key Unlock Signal | `22 10 11` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Electrical Load Signal No.3 | `22 10 11` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Electrical Load Signal No.2 | `22 10 11` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Electrical Load Signal No.1 | `22 10 11` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Starter SW | `22 10 11` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light SW | `22 10 11` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (N Range) | `22 10 11` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (R Range) | `22 10 11` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (P Range) | `22 10 11` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Specification Signal No.1 | `22 10 11` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Power Steering SW | `22 10 11` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| A/C Pressure Normal SW | `22 10 11` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Clutch SW | `22 10 11` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Neutral Position SW | `22 10 11` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Mode Cancel SW | `22 10 11` | 40..40 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECO Mode SW | `22 10 11` | 41..41 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Snow Mode SW | `22 10 11` | 42..42 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Power Mode SW | `22 10 11` | 43..43 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Sport Shift Down SW | `22 10 11` | 44..44 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Sport Shift Up SW | `22 10 11` | 45..45 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (S Range) | `22 10 11` | 46..46 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (D Range) | `22 10 11` | 47..47 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Transfer L4 | `22 10 11` | 56..56 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4WD SW | `22 10 11` | 57..57 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.6 | `22 10 11` | 58..58 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.5 | `22 10 11` | 59..59 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.4 | `22 10 11` | 60..60 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.3 | `22 10 11` | 61..61 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.2 | `22 10 11` | 62..62 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.1 | `22 10 11` | 63..63 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst Regeneration Switch | `22 10 11` | 72..72 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| iMT SW | `22 10 11` | 73..73 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Kick Down SW | `22 10 11` | 74..74 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Main SW | `22 10 11` | 75..75 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ASL SW | `22 10 11` | 76..76 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Lid SW | `22 10 11` | 78..78 | `raw` | 0 = Close; 1 = Open | N | Only observed constant |
| Fuel Lid Sensor SW | `22 10 11` | 79..79 | `raw` | 0 = Close; 1 = Open | N | Only observed constant |
| Track Mode Switch | `22 10 11` | 88..88 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IG2 / IGP | `22 10 11` | 89..89 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IGR | `22 10 11` | 90..90 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drive Mode Select Switch | `22 10 11` | 91..91 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Door Courtesy Switch | `22 10 11` | 92..92 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SD Switch | `22 10 11` | 93..93 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (B Range) | `22 10 11` | 94..94 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Neutral Position Switch | `22 10 11` | 95..95 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drive Mode Select Switching Switch | `22 10 11` | 104..104 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Endless Rotary Switch (Phase B) | `22 10 11` | 105..105 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Endless Rotary Switch (Phase A) | `22 10 11` | 106..106 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Exhaust Brake Switch | `22 10 11` | 107..107 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Heater Idle Up Switch | `22 10 11` | 108..108 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drive Mode Select Switch (DMS-) | `22 10 11` | 109..109 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drive Mode Select Switch (DMS+) | `22 10 11` | 110..110 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Freezer Signal | `22 10 11` | 111..111 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift Sensor Switch DB1 | `22 10 11` | 120..120 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift Sensor Switch SPN | `22 10 11` | 121..121 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift Sensor Switch SPR | `22 10 11` | 122..122 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift Sensor Switch SPP | `22 10 11` | 123..123 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Starter Switch Signal | `22 10 11` | 124..124 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| TOW HAUL Mode Switching Switch | `22 10 11` | 125..125 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IGCT | `22 10 11` | 126..126 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Terrain Select Mode Switching Switch | `22 10 11` | 127..127 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Total FT Bank 1 | `22 10 15` | 0..7 | `raw * 0.00390625 - 0.5` |  | Y |  |
| Injection Volume Cylinder #1 | `22 10 17` | 0..15 | `raw * 0.00003125` | ml | Y |  |
| Injector Cylinder #1 (Port) | `22 10 18` | 0..15 | `raw` | μs | Y |  |
| Injection Time Cylinder #1 (D4) | `22 10 19` | 0..15 | `raw` | μs | Y |  |
| Injection Timing Cylinder #1 (D4) | `22 10 19` | 16..31 | `raw * 0.1 - 3276.8` | deg(CA) | Y |  |
| High Pressure Fuel Pump Discharge Rate | `22 10 1B` | 0..15 | `raw * 0.00003125` | ml | Y |  |
| High Pressure Fuel Pump Duty Ratio (D4) | `22 10 1B` | 16..23 | `raw * 0.5` | % | Y |  |
| High Fuel Pressure Sensor | `22 10 1D` | 0..15 | `raw * 0.001953125 - 64` | MPa | Y |  |
| Injection Mode | `22 10 1F` | 0..7 | `raw` | 0 = Port; 1 = Direct; 2 = Either | Y |  |
| Injection Switching Status | `22 10 20` | 15..15 | `raw` | 0 = NG; 1 = OK | N | Only observed constant |
| Vacuum Pump | `22 10 24` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VSV for Vent Valve | `22 10 24` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| A/F Learn Value Idle Bank 1 | `22 10 2A` | 0..7 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Learn Value Low Bank 1 | `22 10 2A` | 8..15 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Learn Value Mid No.1 Bank 1 | `22 10 2A` | 16..23 | `raw * 0.390625 - 50` | % | N | Only observed constant |
| A/F Learn Value Mid No.2 Bank 1 | `22 10 2A` | 24..31 | `raw * 0.390625 - 50` | % | N | Only observed constant |
| A/F Learn Value High Bank 1 | `22 10 2A` | 32..39 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Sensor Heater Current Value B1S2 | `22 10 36` | 0..15 | `raw * 0.001` | A | Y |  |
| A/F Sensor Heater Duty B1S2 | `22 10 36` | 16..31 | `raw * 0.006103515625` | % | Y |  |
| A/F Sensor Impedance B1S2 | `22 10 36` | 32..47 | `raw * 0.32421875` | ohm | Y |  |
| A/F Sensor Determination (Worst Value) Bank 1 | `22 10 3A` | 0..7 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) Cylinder #1 | `22 10 3A` | 8..15 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) Cylinder #2 | `22 10 3A` | 16..23 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) Cylinder #3 | `22 10 3A` | 24..31 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) Cylinder #4 | `22 10 3A` | 32..39 | `raw * 0.01` |  | N | Only observed constant |
| A/F Sensor Determination (Worst Value) (Port) Bank 1 | `22 10 3D` | 0..7 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) (Port) Cylinder #1 | `22 10 3D` | 8..15 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) (Port) Cylinder #2 | `22 10 3D` | 16..23 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) (Port) Cylinder #3 | `22 10 3D` | 24..31 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) (Port) Cylinder #4 | `22 10 3D` | 32..39 | `raw * 0.01` |  | N | Only observed constant |
| IG-ON Coolant Temperature | `22 10 41` | 0..7 | `raw * 0.625 - 40` | ℃ | N | Only observed constant |
| Initial Engine Coolant Temperature | `22 10 41` | 8..15 | `raw * 0.625 - 40` | ℃ | N | Only observed constant |
| IG-ON Intake Air Temperature | `22 10 42` | 0..7 | `raw * 0.625 - 40` | ℃ | N | Only observed constant |
| Initial Engine Intake Air Temperature | `22 10 42` | 8..15 | `raw * 0.625 - 40` | ℃ | N | Only observed constant |
| Electric Water Pump Target Speed | `22 10 43` | 0..7 | `raw * 25` | rpm | Y |  |
| Electric Water Pump Speed | `22 10 43` | 8..15 | `raw * 25` | rpm | Y |  |
| Radiator Coolant Temperature | `22 10 45` | 0..7 | `raw - 40` | ℃ | Y |  |
| Cooling Fan Duty Ratio | `22 10 46` | 0..7 | `raw * 0.5` | % | Y |  |
| Throttle Position Sensor No.1 Voltage | `22 10 57` | 0..7 | `raw * 0.01953125` | V | Y |  |
| Throttle Position Sensor No.2 Voltage | `22 10 57` | 8..15 | `raw * 0.01953125` | V | Y |  |
| Throttle Position Sensor Fully Closed Learn Value | `22 10 57` | 16..23 | `raw * 0.01953125` | V | N | Only observed constant |
| Throttle Position Sensor Open Position No.1 | `22 10 57` | 24..31 | `raw * 0.01953125` | V | N | Only observed constant |
| Throttle Position Sensor Open Position No.2 | `22 10 57` | 32..39 | `raw * 0.01953125` | V | N | Only observed constant |
| Throttle Request Position | `22 10 57` | 40..47 | `raw * 0.01953125` | V | Y |  |
| Throttle Motor Current | `22 10 58` | 0..7 | `raw * 0.078125` | A | Y |  |
| +BM Voltage | `22 10 58` | 8..23 | `raw * 0.001220703125` | V | Y |  |
| Throttle Position Command | `22 10 58` | 24..31 | `raw * 0.01953125` | V | Y |  |
| Throttle Motor Duty Ratio (Open) | `22 10 58` | 32..39 | `raw` | % | Y |  |
| Throttle Motor Duty Ratio (Close) | `22 10 58` | 40..47 | `raw` | % | Y |  |
| Actuator Power Supply | `22 10 58` | 48..55 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Open Side Malfunction | `22 10 5A` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| System Guard | `22 10 5A` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Knock Correct Learn Value | `22 10 5C` | 0..15 | `raw * 0.03125 - 1024` | deg(CA) | Y |  |
| Knock F/B Value | `22 10 5C` | 16..31 | `raw * 0.03125 - 1024` | deg(CA) | Y |  |
| Engine ECU Internal Temperature | `22 10 5E` | 0..7 | `raw - 40` | ℃ | Y |  |
| Exhaust VVT OCV Control Duty Ratio Bank 1 | `22 10 62` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| Fuel Pump Control Duty Ratio | `22 10 6A` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| IG OFF Elapsed Time | `22 10 6B` | 0..15 | `raw * 10` | min | Y |  |
| Soak IC Current Timer Value | `22 10 6B` | 16..31 | `raw * 9.375` | sec | Y |  |
| Distance Traveled from Last Battery Cable Disconnect | `22 10 6C` | 0..15 | `raw` | km | Y |  |
| Intake VVT Target Angle Bank 1 | `22 10 6F` | 0..15 | `raw * 0.009765625` | DegFR | Y |  |
| Exhaust VVT Target Angle Bank 1 | `22 10 71` | 0..15 | `raw * 0.009765625` | DegFR | Y |  |
| Precursor Cumulative Dosage (Total) | `22 10 73` | 0..15 | `raw * 0.006103515625` | % | N | Only observed constant |
| Oil Maintenance Mileage | `22 10 73` | 16..31 | `raw - 32768` | km | Y |  |
| Engine Oil Pressure | `22 10 74` | 0..15 | `raw * 0.078125` | kPa | Y |  |
| Idle Spark Advance Control Cylinder #1 | `22 10 78` | 0..7 | `raw * 0.0625` | deg(CA) | N | Expected only at engine idle |
| Idle Spark Advance Control Cylinder #2 | `22 10 78` | 8..15 | `raw * 0.0625` | deg(CA) | N | Expected only at engine idle |
| Idle Spark Advance Control Cylinder #3 | `22 10 78` | 16..23 | `raw * 0.0625` | deg(CA) | N | Expected only at engine idle |
| Idle Spark Advance Control Cylinder #4 | `22 10 78` | 24..31 | `raw * 0.0625` | deg(CA) | N | Expected only at engine idle |
| Exhaust VVT Retarded Fail | `22 10 7C` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Engine Start Hesitation | `22 10 7C` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Low Revolution for Engine Start | `22 10 7C` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VVT Advance Fail | `22 10 7C` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Low Revolution Control | `22 10 7C` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Engine Stall Control F/B Flow | `22 10 7D` | 0..15 | `raw * 0.03125 - 1024` | Nm | N | Always 0 on this vehicle; likely not available |
| Throttle Air Flow Learn Value (Area 1) | `22 10 7F` | 0..15 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Air Flow Learn Value (Area 2) | `22 10 7F` | 16..31 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Air Flow Learn Value (Area 3) | `22 10 7F` | 32..47 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Air Flow Learn Value (Calculated Value) | `22 10 7F` | 48..63 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Air Flow Learn Value (Atmosphere Pressure Offset Value) | `22 10 7F` | 64..71 | `raw * 0.01` |  | N | Only observed constant |
| Mass Air Flow Circuit | `22 10 7F` | 72..79 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| Intake VVT Change Angle Bank 1 | `22 10 87` | 0..15 | `raw * 0.009765625` | DegFR | Y |  |
| Exhaust VVT Change Angle Bank 1 | `22 10 88` | 0..15 | `raw * 0.009765625` | DegFR | Y |  |
| Exhaust VVT Hold Learn Value Bank 1 | `22 10 89` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| VVT-iE Duty Ratio Bank 1 | `22 10 8A` | 0..15 | `raw * 0.006103515625 - 200` | % | Y |  |
| VVT-iE Motor Direction Bank 2 | `22 10 8C` | 14..14 | `raw` | 0 = Backward; 1 = Forward | N | Only observed constant |
| VVT-iE Motor Direction Bank 1 | `22 10 8C` | 15..15 | `raw` | 0 = Backward; 1 = Forward | Y |  |
| Intake VVT Timing Most Over-Retarded Learn Value Bank 1 | `22 10 8D` | 0..15 | `raw * 0.009765625` | deg(CA) | Y |  |
| Exhaust VVT Timing Most Over-Advanced Learn Value Bank 1 | `22 10 8F` | 0..15 | `raw * 0.009765625` | deg(CA) | Y |  |
| Fuel Pump Target Speed | `22 10 97` | 0..15 | `raw - 32768` | rpm | Y |  |
| Fuel Pump F/B Offset | `22 10 97` | 16..31 | `raw * 0.01 - 327.68` | L/h | Y |  |
| High Pressure Fuel Pump Internal Temperature | `22 10 98` | 0..7 | `raw * 0.625 - 40` | ℃ | Y |  |
| Low Pressure Fuel Delivery Internal Temperature | `22 10 98` | 8..15 | `raw * 0.625 - 40` | ℃ | Y |  |
| Cylinder Number 4 | `22 10 9D` | 0..7 | `raw` |  | N | Only observed constant |
| Average Engine Speed of All Cylinder | `22 10 9D` | 0..15 | `raw * 0.78125` | rpm | N | Always 0xFFFF (no value) on this vehicle |
| Engine Speed Cylinder #1 | `22 10 9D` | 16..31 | `raw * 0.78125` | rpm | N | Always 0xFFFF (no value) on this vehicle |
| Engine Speed Cylinder #2 | `22 10 9D` | 32..47 | `raw * 0.78125` | rpm | N | Always 0xFFFF (no value) on this vehicle |
| Engine Speed Cylinder #3 | `22 10 9D` | 48..63 | `raw * 0.78125` | rpm | N | Always 0xFFFF (no value) on this vehicle |
| Engine Speed Cylinder #4 | `22 10 9D` | 64..79 | `raw * 0.78125` | rpm | N | Always 0xFFFF (no value) on this vehicle |
| NT / NIM Sensor Test Results | `22 10 A0` | 9..9 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Secondary Air Injection System Test Result | `22 10 A0` | 10..10 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Misfire Test Result | `22 10 A0` | 11..11 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor B2S1 Test Results | `22 10 A0` | 12..12 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor B1S1 Test Results | `22 10 A0` | 13..13 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor B2S2 Test Results | `22 10 A0` | 14..14 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor B1S2 Test Results | `22 10 A0` | 15..15 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F Learn Value Idle (Port) Bank 1 | `22 10 A2` | 0..7 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Learn Value Low (Port) Bank 1 | `22 10 A2` | 8..15 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Learn Value Mid No.1 (Port) Bank 1 | `22 10 A2` | 16..23 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Learn Value Mid No.2 (Port) Bank 1 | `22 10 A2` | 24..31 | `raw * 0.390625 - 50` | % | N | Only observed constant |
| A/F Learn Value High (Port) Bank 1 | `22 10 A2` | 32..39 | `raw * 0.390625 - 50` | % | N | Only observed constant |
| All Cylinders Misfire Count | `22 10 A5` | 0..7 | `raw` |  | Y |  |
| Ignition Trigger Count | `22 10 A5` | 8..23 | `raw` |  | Y |  |
| Misfire RPM | `22 10 A5` | 24..31 | `raw * 25` | rpm | N | Only observed constant |
| Misfire Margin | `22 10 A5` | 32..39 | `raw - 128` | % | Y |  |
| Misfire Count Cylinder #1 | `22 10 A7` | 0..7 | `raw` |  | N | Only observed constant |
| Misfire Count Cylinder #2 | `22 10 A7` | 8..15 | `raw` |  | N | Only observed constant |
| Misfire Count Cylinder #3 | `22 10 A7` | 16..23 | `raw` |  | N | Only observed constant |
| Misfire Count Cylinder #4 | `22 10 A7` | 24..31 | `raw` |  | N | Only observed constant |
| Misfire Load | `22 10 AA` | 0..7 | `raw * 2` | % | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut History | `22 10 AC` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut | `22 10 AC` | 15..15 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #6 | `22 10 AC` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #5 | `22 10 AC` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #4 | `22 10 AC` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #3 | `22 10 AC` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #2 | `22 10 AC` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #1 | `22 10 AC` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| A/F Sensor Impedance B1S1 | `22 10 AD` | 0..15 | `raw * 0.32421875` | ohm | Y |  |
| A/F (O2) Sensor Heater Duty Ratio B1S1 | `22 10 AD` | 32..47 | `raw * 0.006103515625` | % | Y |  |
| Vapor Pressure (Calculated) | `22 10 B0` | 0..15 | `raw * 0.022 - 720.896` | kPa | Y |  |
| Vapor Pressure Pump | `22 10 B0` | 16..31 | `raw * 0.022` | kPa | Y |  |
| Active Test History Flag | `22 10 B1` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Active Test Prohibition Flag | `22 10 B1` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Active Test Performing Flag | `22 10 B1` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Purge Cut VSV Duty | `22 10 B2` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| EVAP Purge Flow | `22 10 B2` | 16..31 | `raw * 0.006103515625` | % | Y |  |
| EVAP Purge Density Learn Value | `22 10 B2` | 32..47 | `raw * 0.006103515625 - 200` |  | N | Only observed constant |
| Tank Pressure | `22 10 B3` | 0..15 | `raw * 0.00078125 - 25.6` | kPa | Y |  |
| Requested Engine Torque | `22 10 C0` | 0..15 | `raw * 0.25` | kW | Y |  |
| HV Target Engine Speed | `22 10 C1` | 0..7 | `raw * 25` | rpm | Y |  |
| Actual Engine Torque | `22 10 C2` | 0..15 | `raw - 32768` | Nm | Y |  |
| Engine Driving Time | `22 10 C3` | 0..7 | `raw` | sec | Y |  |
| Request Engine Run Time | `22 10 C4` | 0..7 | `raw * 0.1` | sec | N | Only observed constant |
| Judge Time Engine Output | `22 10 C5` | 0..7 | `raw * 0.1` | sec | N | Only observed constant |
| Judge Time Engine Ignition | `22 10 C6` | 0..7 | `raw * 0.1` | sec | N | Only observed constant |
| ISC Learning Value | `22 10 C7` | 0..15 | `raw * 0.03125 - 1024` | Nm | N | Only observed constant |
| Fuel Level | `22 10 C8` | 9..9 | `raw` | 0 = Empty; 1 = Not Emp | N | Only observed constant |
| ISC Learning | `22 10 C8` | 10..10 | `raw` | 0 = Incmpl; 1 = Compl | N | Only observed constant |
| F/C for Engine Stop Req | `22 10 C8` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Engine Independent | `22 10 C8` | 12..12 | `raw` | 0 = Not Opr; 1 = Operate | N | Only observed constant |
| Racing Operation | `22 10 C8` | 13..13 | `raw` | 0 = Not Opr; 1 = Operate | N | Only observed constant |
| Request Warm-up | `22 10 C8` | 14..14 | `raw` | 0 = Not Req; 1 = Request | N | Only observed constant |
| Engine Independent Control | `22 10 C8` | 15..15 | `raw` | 0 = Not Opr; 1 = Operate | N | Only observed constant |
| Low Fuel Pressure Sensor | `22 10 CD` | 0..15 | `raw * 0.1 - 3276.8` | kPa | Y |  |
| Coolant Temperature Sensor Fail Time | `22 10 D2` | 0..31 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 0 | `22 10 D3` | 0..31 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 1 | `22 10 D3` | 32..63 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 2 | `22 10 D3` | 64..95 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 3 | `22 10 D3` | 96..127 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 4 | `22 10 D3` | 128..159 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 5 | `22 10 D3` | 160..191 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 6 | `22 10 D3` | 192..223 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 7 | `22 10 D3` | 224..255 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 8 | `22 10 D3` | 256..287 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 9 | `22 10 D3` | 288..319 | `raw * 524.228` | ms | N | Only observed constant |
| Total Fuel Consumption Integrated Value | `22 10 D4` | 0..31 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 0 | `22 10 D5` | 0..31 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 1 | `22 10 D5` | 32..63 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 2 | `22 10 D5` | 64..95 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 3 | `22 10 D5` | 96..127 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 4 | `22 10 D5` | 128..159 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 5 | `22 10 D5` | 160..191 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 6 | `22 10 D5` | 192..223 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 7 | `22 10 D5` | 224..255 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 8 | `22 10 D5` | 256..287 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 9 | `22 10 D5` | 288..319 | `raw * 0.064` | ml | N | Only observed constant |
| Air Flow Meter Fail Time | `22 10 D6` | 0..31 | `raw * 524.228` | ms | N | Only observed constant |
| Total Intake Air Volume Integrated Value | `22 10 D7` | 0..31 | `raw` | g | N | Only observed constant |
| Across Initial Position Count Area 0 Bank 1 | `22 10 D8` | 0..31 | `raw` |  | Y |  |
| Across Initial Position Count Area 1 Bank 1 | `22 10 D8` | 32..63 | `raw` |  | Y |  |
| Across Initial Position Count Area 2 Bank 1 | `22 10 D8` | 64..95 | `raw` |  | Y |  |
| Across Initial Position Count Area 3 Bank 1 | `22 10 D8` | 96..127 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 4 Bank 1 | `22 10 D8` | 128..159 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 0 Bank 2 | `22 10 D9` | 0..31 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 1 Bank 2 | `22 10 D9` | 32..63 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 2 Bank 2 | `22 10 D9` | 64..95 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 3 Bank 2 | `22 10 D9` | 96..127 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 4 Bank 2 | `22 10 D9` | 128..159 | `raw` |  | N | Only observed constant |
| Total IG ON Time | `22 10 DA` | 0..31 | `raw * 524.228` | ms | Y |  |
| Precursor Cumulative Dosage (Total) | `22 10 DB` | 0..31 | `raw * 3125 / 67108864` | ppm | Y |  |
| Throttle Flow Rate Characteristics Learning Value Area 0 | `22 10 DC` | 0..15 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 1 | `22 10 DC` | 16..31 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 2 | `22 10 DC` | 32..47 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 3 | `22 10 DC` | 48..63 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 4 | `22 10 DC` | 64..79 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 5 | `22 10 DC` | 80..95 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 6 | `22 10 DC` | 96..111 | `raw * 0.000030517578125` |  | N | Only observed constant |
| ISC F/B Learning Value | `22 10 DD` | 0..15 | `raw * 0.03125` | Nm | N | Only observed constant |
| A/F Learn Value Area 0 Bank 1 | `22 10 DE` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 1 Bank 1 | `22 10 DE` | 16..31 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 2 Bank 1 | `22 10 DE` | 32..47 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 3 Bank 1 | `22 10 DE` | 48..63 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 4 Bank 1 | `22 10 DE` | 64..79 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 5 Bank 1 | `22 10 DE` | 80..95 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 6 Bank 1 | `22 10 DE` | 96..111 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 7 Bank 1 | `22 10 DE` | 112..127 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 8 Bank 1 | `22 10 DE` | 128..143 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 9 Bank 1 | `22 10 DE` | 144..159 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 0 Bank 2 | `22 10 DF` | 0..15 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 1 Bank 2 | `22 10 DF` | 16..31 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 2 Bank 2 | `22 10 DF` | 32..47 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 3 Bank 2 | `22 10 DF` | 48..63 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 4 Bank 2 | `22 10 DF` | 64..79 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 5 Bank 2 | `22 10 DF` | 80..95 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 6 Bank 2 | `22 10 DF` | 96..111 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 7 Bank 2 | `22 10 DF` | 112..127 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 8 Bank 2 | `22 10 DF` | 128..143 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 9 Bank 2 | `22 10 DF` | 144..159 | `raw * 0.006103515625` | % | N | Only observed constant |
| EGR Valve Initialization Count | `22 10 E0` | 0..31 | `raw` |  | Y |  |
| Atmospheric Pressure during EGR OBD | `22 10 E1` | 0..7 | `raw * 0.5` | kPa | Y |  |
| Reference Intake Manifold Pressure during EGR OBD | `22 10 E1` | 8..15 | `raw * 0.5` | kPa | Y |  |
| Pressure Fluctuation during EGR OBD | `22 10 E1` | 16..23 | `raw * 0.15625` | kPa | Y |  |
| Engine Speed during EGR OBD | `22 10 E1` | 24..31 | `raw * 12.5` | rpm | N | Only observed constant |
| GPF Differential Pressure Maximum Area 1, 0 | `22 10 E2` | 0..7 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 1, 0 | `22 10 E2` | 8..15 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 1, 0 | `22 10 E2` | 16..23 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 1, 0 | `22 10 E2` | 24..31 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 1, 0 | `22 10 E2` | 32..39 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 1, 0 | `22 10 E2` | 40..47 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 1, 0 | `22 10 E2` | 48..55 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 2, 0 | `22 10 E2` | 56..63 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 2, 0 | `22 10 E2` | 64..71 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 2, 0 | `22 10 E2` | 72..79 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 2, 0 | `22 10 E2` | 80..87 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 2, 0 | `22 10 E2` | 88..95 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 2, 0 | `22 10 E2` | 96..103 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 2, 0 | `22 10 E2` | 104..111 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 3, 0 | `22 10 E2` | 112..119 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 3, 0 | `22 10 E2` | 120..127 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 3, 0 | `22 10 E2` | 128..135 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 3, 0 | `22 10 E2` | 136..143 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 3, 0 | `22 10 E2` | 144..151 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 3, 0 | `22 10 E2` | 152..159 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 3, 0 | `22 10 E2` | 160..167 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 4, 0 | `22 10 E2` | 168..175 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 4, 0 | `22 10 E2` | 176..183 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 4, 0 | `22 10 E2` | 184..191 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 4, 0 | `22 10 E2` | 192..199 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 4, 0 | `22 10 E2` | 200..207 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 4, 0 | `22 10 E2` | 208..215 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 4, 0 | `22 10 E2` | 216..223 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 1, 1 | `22 10 E3` | 0..7 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 1, 1 | `22 10 E3` | 8..15 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 1, 1 | `22 10 E3` | 16..23 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 1, 1 | `22 10 E3` | 24..31 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 1, 1 | `22 10 E3` | 32..39 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 1, 1 | `22 10 E3` | 40..47 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 1, 1 | `22 10 E3` | 48..55 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 2, 1 | `22 10 E3` | 56..63 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 2, 1 | `22 10 E3` | 64..71 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 2, 1 | `22 10 E3` | 72..79 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 2, 1 | `22 10 E3` | 80..87 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 2, 1 | `22 10 E3` | 88..95 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 2, 1 | `22 10 E3` | 96..103 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 2, 1 | `22 10 E3` | 104..111 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 3, 1 | `22 10 E3` | 112..119 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 3, 1 | `22 10 E3` | 120..127 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 3, 1 | `22 10 E3` | 128..135 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 3, 1 | `22 10 E3` | 136..143 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 3, 1 | `22 10 E3` | 144..151 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 3, 1 | `22 10 E3` | 152..159 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 3, 1 | `22 10 E3` | 160..167 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 4, 1 | `22 10 E3` | 168..175 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 4, 1 | `22 10 E3` | 176..183 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 4, 1 | `22 10 E3` | 184..191 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 4, 1 | `22 10 E3` | 192..199 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 4, 1 | `22 10 E3` | 200..207 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 4, 1 | `22 10 E3` | 208..215 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 4, 1 | `22 10 E3` | 216..223 | `raw` | gm/sec | N | Only observed constant |
| Coolant Temperature at IG OFF -> ON | `22 10 E4` | 0..7 | `raw` | ℃ | N | Only observed constant |
| Intake Air Temperature at IG OFF -> ON | `22 10 E4` | 8..15 | `raw` | ℃ | N | Only observed constant |
| Ambient Temperature at IG OFF -> ON | `22 10 E4` | 16..23 | `raw` | ℃ | N | Only observed constant |
| Year at IG OFF -> ON | `22 10 E4` | 24..31 | `raw` | year | N | Only observed constant |
| Month at IG OFF -> ON | `22 10 E4` | 32..39 | `raw` | month | N | Only observed constant |
| Day at IG OFF -> ON | `22 10 E4` | 40..47 | `raw` | day | N | Only observed constant |
| Hour at IG OFF -> ON | `22 10 E4` | 48..55 | `raw` | hour | N | Only observed constant |
| Minute at IG OFF -> ON | `22 10 E4` | 56..63 | `raw` | minute | N | Only observed constant |
| Second at IG OFF -> ON | `22 10 E4` | 64..71 | `raw` | second | N | Only observed constant |
| Coolant Temperature at IG ON -> OFF | `22 10 E4` | 72..79 | `raw` | ℃ | N | Only observed constant |
| Intake Air Temperature at IG ON -> OFF | `22 10 E4` | 80..87 | `raw` | ℃ | N | Only observed constant |
| Ambient Temperature at IG ON -> OFF | `22 10 E4` | 88..95 | `raw` | ℃ | N | Only observed constant |
| Year at IG ON -> OFF | `22 10 E4` | 96..103 | `raw` | year | N | Only observed constant |
| Month at IG ON -> OFF | `22 10 E4` | 104..111 | `raw` | month | N | Only observed constant |
| Day at IG ON -> OFF | `22 10 E4` | 112..119 | `raw` | day | N | Only observed constant |
| Hour at IG ON -> OFF | `22 10 E4` | 120..127 | `raw` | hour | N | Only observed constant |
| Minute at IG ON -> OFF | `22 10 E4` | 128..135 | `raw` | minute | N | Only observed constant |
| Second at IG ON -> OFF | `22 10 E4` | 136..143 | `raw` | second | N | Only observed constant |
| Maximum Coolant Temperature | `22 10 E5` | 0..15 | `raw * 0.625` | ℃ | Y |  |
| Minimum Coolant Temperature | `22 10 E5` | 16..31 | `raw * 0.625` | ℃ | N | Running minimum; falls only on a new low |
| Target Revolution Deviation Rate at Low | `22 10 E5` | 32..47 | `raw * 0.006103515625` | % | Y |  |
| Target Revolution Deviation Rate at Middle | `22 10 E5` | 48..63 | `raw * 0.006103515625` | % | Y |  |
| Target Revolution Deviation Rate at High | `22 10 E5` | 64..79 | `raw * 0.006103515625` | % | Y |  |
| Average Feed Back Duty at Low | `22 10 E5` | 80..95 | `raw * 0.006103515625` | % | Y |  |
| Average Feed Back Duty at Middle | `22 10 E5` | 96..111 | `raw * 0.006103515625` | % | Y |  |
| Average Feed Back Duty at High | `22 10 E5` | 112..127 | `raw * 0.006103515625` | % | Y |  |
| Data Set File Mode Memory Value | `22 10 E6` | 0..15 | `raw` |  | N | Only observed constant |
| Variation Information | `22 10 EB` | 0..7 | `raw` |  | N | Only observed constant |
| Soak IC First Start Time | `22 11 9A` | 0..7 | `raw * 10` | min; 0 = 0; 1 = Unknown | N | Only observed constant |
| Soak Timer Start History | `22 11 9A` | 8..15 | `raw` | 0 = Unknown; 90 = OFF; 165 = ON | N | Only observed constant |
| Grille Shutter Position | `22 11 BB` | 0..7 | `raw` | % | Y |  |
| Grille Shutter Stuck Status | `22 11 BD` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Grille Shutter Slipping Status | `22 11 BD` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| All Universal Condition History | `22 11 E0` | 0..7 | `raw` |  | Y |  |
| Universal Condition 1 Judgment History | `22 11 E0` | 8..15 | `raw` |  | Y |  |
| Total Universal Condition 1 Judgment Time | `22 11 E0` | 16..31 | `raw * 65.536` | ms | Y |  |
| Universal Condition 2 Judgment History | `22 11 E0` | 32..39 | `raw` |  | Y |  |
| Total Universal Condition 2 Judgment Time | `22 11 E0` | 40..55 | `raw * 65.536` | ms | Y |  |
| Universal Condition 3 Judgment History | `22 11 E0` | 56..63 | `raw` |  | N | Only observed constant |
| Total Universal Condition 3 Judgment Time | `22 11 E0` | 64..79 | `raw * 65.536` | ms | Y |  |
| Start Status for PDTC | `22 11 E0` | 80..87 | `raw` |  | N | Only observed constant |
| Accelerator Pedal Idle Status for PDTC | `22 11 E0` | 88..95 | `raw` |  | Y |  |
| Vehicle Speed for PDTC | `22 11 E0` | 96..103 | `raw` | km/h | Y |  |
| Intake Camshaft Position Sensor Voltage | `22 12 01` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Exhaust Camshaft Position Sensor Voltage | `22 12 03` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Crankshaft Position Sensor Voltage | `22 12 07` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| MIL | `22 1F 01` | 0..0 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Number of Emission DTC | `22 1F 01` | 1..7 | `raw` |  | N | Only observed constant |
| Complete Parts Monitor Result | `22 1F 01` | 9..9 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Fuel System Monitor Result | `22 1F 01` | 10..10 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Misfire Monitor Result | `22 1F 01` | 11..11 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Ignition Monitor | `22 1F 01` | 12..12 | `raw` | 0 = Spark Ignition; 1 = Compression Ignition | N | Only observed constant |
| Complete Parts Monitor | `22 1F 01` | 13..13 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Fuel System Monitor | `22 1F 01` | 14..14 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Misfire Monitor | `22 1F 01` | 15..15 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| EGR/VVT Monitor | `22 1F 01` | 16..16 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| A/F (O2) Sensor Heater Monitor | `22 1F 01` | 17..17 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| A/F (O2) Sensor Monitor | `22 1F 01` | 18..18 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Secondary Air Injection System Monitor | `22 1F 01` | 20..20 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| EVAP Monitor | `22 1F 01` | 21..21 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Heated Catalyst Monitor | `22 1F 01` | 22..22 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Catalyst Monitor | `22 1F 01` | 23..23 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| EGR/VVT Monitor Result | `22 1F 01` | 24..24 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor Heater Monitor Result | `22 1F 01` | 25..25 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor Monitor Result | `22 1F 01` | 26..26 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Secondary Air Injection System Monitor Result | `22 1F 01` | 28..28 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| EVAP Monitor Result | `22 1F 01` | 29..29 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Heated Catalyst Monitor Result | `22 1F 01` | 30..30 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Catalyst Monitor Result | `22 1F 01` | 31..31 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Fuel System Status Bank 1 | `22 1F 03` | 0..7 | `raw` | 0 = Unused; 1 = OL; 2 = CL; 4 = OLDrive; 8 = OLFault; 16 = CLFault | Y |  |
| Fuel System Status Bank 2 | `22 1F 03` | 8..15 | `raw` | 0 = Unused; 1 = OL; 2 = CL; 4 = OLDrive; 8 = OLFault; 16 = CLFault | N | Only observed constant |
| Calculate Load | `22 1F 04` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Coolant Temperature | `22 1F 05` | 0..7 | `raw - 40` | ℃ | Y |  |
| Short FT B1S1 | `22 1F 06` | 0..7 | `raw * 0.78125 - 100` | % | Y |  |
| Long FT B1S1 | `22 1F 07` | 0..7 | `raw * 0.78125 - 100` | % | Y |  |
| Engine Speed | `22 1F 0C` | 0..15 | `raw * 0.25` | rpm | Y |  |
| Vehicle Speed | `22 1F 0D` | 0..7 | `raw` | km/h | Y |  |
| Ignition Timing Cylinder #1 | `22 1F 0E` | 0..7 | `raw * 0.5 - 64` | deg | Y |  |
| Intake Air Temperature | `22 1F 0F` | 0..7 | `raw - 40` | ℃ | Y |  |
| Mass Air Flow Sensor | `22 1F 10` | 0..15 | `raw * 0.01` | gm/sec | Y |  |
| Throttle Position Sensor No.1 Voltage % | `22 1F 11` | 0..7 | `raw * 20 / 51` | % | Y |  |
| OBD Requirements | `22 1F 1C` | 0..7 | `raw` | 1 = OBD II (California ARB); 2 = OBD (US Federal EPA); 3 = OBD and OBD II; 4 = OBD I; 5 = Not OBD compliant; 6 = EOBD (Euro OBD); 7 = EOBD and OBD II; 8 = EOBD and OBD; 9 = EOBD, OBD and OBD II; 10 = JOBD (Japan OBD); 11 = JOBD and OBD II; 12 = JOBD and EOBD; 13 = JOBD, EOBD, and OBD II; 14 = EURO 4; 15 = EURO 5; 17 = Engine Manufacturer Diagnostics (EMD); 18 = Engine Manufacturer Diagnostics Enhanced (EMD+); 19 = Heavy Duty On-Board Diagnostics (Child/Partial); 20 = Heavy Duty On-Board Diagnostics; 21 = World Wide Harmonized OBD; 23 = Heavy Duty Euro OBD Stage I without NOx control; 24 = Heavy Duty Euro OBD Stage I with NOx control; 25 = Heavy Duty Euro OBD Stage II without NOx control; 26 = Heavy Duty Euro OBD Stage II with NOx control; 28 = Brazil OBD Phase 1; 29 = Brazil OBD Phase 2; 30 = Korean OBD; 31 = India OBD I; 32 = India OBD II; 33 = Heavy Duty Euro OBD Stage VI; 34 = OBD, OBD II and HD OBD; 35 = Brazil OBD Phase 3; 36 = Motorcycle, Euro OBD-I; 37 = Motorcycle, Euro OBD-II; 38 = Motorcycle, China OBD-I; 39 = Motorcycle, Taiwan OBD-I; 40 = Motorcycle, Japan OBD-I; 41 = China Nationwide Stage 6; 42 = Brazil OBD Diesel | N | Only observed constant |
| Engine Run Time | `22 1F 1F` | 0..15 | `raw` | sec | Y |  |
| MIL ON Run Distance | `22 1F 21` | 0..15 | `raw` | km | N | Only observed constant |
| EVAP (Purge) VSV | `22 1F 2E` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Warmup Cycle Cleared DTC | `22 1F 30` | 0..7 | `raw` |  | N | Only observed constant |
| Distance from DTC Cleared | `22 1F 31` | 0..15 | `raw` | km | Y |  |
| Atmospheric Pressure | `22 1F 33` | 0..7 | `raw` | kPa | Y |  |
| A/F Bank 1 | `22 1F 34` | 0..15 | `raw * 0.000443775` |  | Y |  |
| A/F (O2) Sensor Current B1S1 | `22 1F 34` | 16..31 | `raw * 0.00390625 - 128` | mA | Y |  |
| A/F (O2) Sensor Current B1S2 | `22 1F 35` | 16..31 | `raw * 0.00390625 - 128` | mA | Y |  |
| Catalyst Temperature B1S1 | `22 1F 3C` | 0..15 | `raw * 0.1 - 40` | ℃ | Y |  |
| Catalyst Temperature B1S2 | `22 1F 3E` | 0..15 | `raw * 0.1 - 40` | ℃ | Y |  |
| Component Monitor Result (Current) | `22 1F 41` | 9..9 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Fuel System Monitor Result (Current) | `22 1F 41` | 10..10 | `raw` | 0 = Compl; 1 = Incmpl | Y |  |
| Misfire Monitor Result (Current) | `22 1F 41` | 11..11 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Ignition Monitor (Current) | `22 1F 41` | 12..12 | `raw` | 0 = Spark Ignition; 1 = Compression Ignition | N | Only observed constant |
| Component Monitor ENA (Current) | `22 1F 41` | 13..13 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| Fuel System Monitor ENA (Current) | `22 1F 41` | 14..14 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| Misfire Monitor ENA (Current) | `22 1F 41` | 15..15 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| EGR/VVT Monitor ENA (Current) | `22 1F 41` | 16..16 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| O2 Sensor Heater ENA (Current) | `22 1F 41` | 17..17 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| A/F (O2) Sensor Monitor ENA (Current) | `22 1F 41` | 18..18 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| Secondary Air Injection System Monitor ENA (Current) | `22 1F 41` | 20..20 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| EVAP Monitor ENA (Current) | `22 1F 41` | 21..21 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| Heated Catalyst Monitor ENA (Current) | `22 1F 41` | 22..22 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| Catalyst Monitor ENA (Current) | `22 1F 41` | 23..23 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| EGR/VVT Monitor Result (Current) | `22 1F 41` | 24..24 | `raw` | 0 = Compl; 1 = Incmpl | Y |  |
| O2 Sensor Heater Monitor Result (Current) | `22 1F 41` | 25..25 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor Monitor Result (Current) | `22 1F 41` | 26..26 | `raw` | 0 = Compl; 1 = Incmpl | Y |  |
| Secondary Air Injection System Monitor Result (Current) | `22 1F 41` | 28..28 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| EVAP Monitor Result (Current) | `22 1F 41` | 29..29 | `raw` | 0 = Compl; 1 = Incmpl | Y |  |
| Heated Catalyst Monitor Result (Current) | `22 1F 41` | 30..30 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Catalyst Monitor Result (Current) | `22 1F 41` | 31..31 | `raw` | 0 = Compl; 1 = Incmpl | Y |  |
| Battery Voltage | `22 1F 42` | 0..15 | `raw * 0.001` | V | Y |  |
| Vehicle Load | `22 1F 43` | 0..15 | `raw * 20 / 51` | % | Y |  |
| Target Air-Fuel Ratio | `22 1F 44` | 0..15 | `raw * 2 / 65535` |  | Y |  |
| Throttle Sensor Position | `22 1F 45` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Ambient Temperature | `22 1F 46` | 0..7 | `raw - 40` | ℃ | Y |  |
| Throttle Position Sensor No.2 Voltage % | `22 1F 47` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Throttle Motor Duty Ratio | `22 1F 4C` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Running Time from MIL ON | `22 1F 4D` | 0..15 | `raw` | min | N | Only observed constant |
| Time after DTC Cleared | `22 1F 4E` | 0..15 | `raw` | min | Y |  |
| Current Fuel Type | `22 1F 51` | 0..7 | `raw` | 1 = Gasoline/petrol; 2 = Methanol; 3 = Ethanol; 4 = Diesel; 5 = Liquefied Petroleum Gas (LPG) LPG; 6 = Compressed Natural Gas (CNG) CNG; 7 = Propane; 8 = Battery/electric; 9 = Bi-fuel vehicle using gasoline; 10 = Bi-fuel vehicle using methanol; 11 = Bi-fuel vehicle using ethanol; 12 = Bi-fuel vehicle using LPG; 13 = Bi-fuel vehicle using CNG; 14 = Bi-fuel vehicle using propane; 15 = Bi-fuel vehicle using battery; 16 = Bi-fuel vehicle using battery and combustion engine; 17 = Hybrid vehicle using gasoline engine; 18 = Hybrid vehicle using gasoline engine on ethanol; 19 = Hybrid vehicle using diesel engine; 20 = Hybrid vehicle using battery; 21 = Hybrid vehicle using battery and combustion engine; 22 = Hybrid vehicle in regeneration mode; 24 = Bi-fuel vehicle using Natural Gas; 25 = Bi-fuel vehicle using diesel; 26 = Natural Gas (Compressed or Liquefied Natural Gas); 27 = Dual Fuel - Diesel and CNG; 28 = Dual Fuel - Diesel and LNG; 29 = Fuel Cell Utilizing Hydrogen; 30 = Hydrogen Internal Combustion Engine | N | Only observed constant |
| EVAP Vapor Pressure (Wide) | `22 1F 54` | 0..15 | `raw * 0.001` | kPa | Y |  |
| Short FT B1S2 | `22 1F 55` | 0..7 | `raw * 0.78125 - 100` | % | Y |  |
| Long FT B1S2 | `22 1F 56` | 0..7 | `raw * 0.78125 - 100` | % | N | Only observed constant |
| Engine Oil Temperature Sensor | `22 1F 5C` | 0..7 | `raw - 40` | ℃ | Y |  |
| Actual Engine Percent Torque | `22 1F 62` | 0..7 | `raw - 125` | % | Y |  |
| Engine Reference Torque (Fixed Value) | `22 1F 63` | 0..15 | `raw` | Nm | N | Only observed constant |
| Actual EGR Valve Position No.2 Supported | `22 1F 69` | 3..3 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target EGR Valve Position No.2 Supported | `22 1F 69` | 4..4 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Actual EGR Valve Position No.1 Supported | `22 1F 69` | 6..6 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target EGR Valve Position No.1 Supported | `22 1F 69` | 7..7 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target EGR Valve Position No.1 | `22 1F 69` | 8..15 | `raw * 20 / 51` | % | Y |  |
| Actual EGR Valve Position No.1 | `22 1F 69` | 16..23 | `raw * 20 / 51` | % | N | The ECU flags this value as unsupported |
| Target EGR Valve Position No.2 | `22 1F 69` | 32..39 | `raw * 20 / 51` | % | N | The ECU flags this value as unsupported |
| Actual EGR Valve Position No.2 | `22 1F 69` | 40..47 | `raw * 20 / 51` | % | N | The ECU flags this value as unsupported |
| Fuel Pressure (Low) / Fuel Pressure 2 Supported | `22 1F 6D` | 3..3 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target Fuel Pressure (Low) / Target Fuel Pressure 2 Supported | `22 1F 6D` | 4..4 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Fuel Pressure (High) Supported | `22 1F 6D` | 6..6 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target Fuel Pressure (High) Supported | `22 1F 6D` | 7..7 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target Fuel Pressure (High) | `22 1F 6D` | 8..23 | `raw * 10` | kPa | Y |  |
| Fuel Pressure (High) | `22 1F 6D` | 24..39 | `raw * 10` | kPa | Y |  |
| Target Fuel Pressure (Low) / Target Fuel Pressure 2 | `22 1F 6D` | 48..63 | `raw * 10` | kPa | Y |  |
| Fuel Pressure (Low) / Fuel Pressure 2 | `22 1F 6D` | 64..79 | `raw * 10` | kPa | Y |  |
| Intake Manifold Absolute Pressure Supported | `22 1F 87` | 7..7 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Intake Manifold Absolute Pressure | `22 1F 87` | 8..23 | `raw * 0.03125` | kPa | Y |  |
| Engine Friction Percent Torque | `22 1F 8E` | 0..7 | `raw - 125` | % | Y |  |
| Engine Fuel Rate | `22 1F 9D` | 0..15 | `raw * 0.02` | gm/sec | Y |  |
| Vehicle Fuel Rate | `22 1F 9D` | 16..31 | `raw * 0.02` | gm/sec | Y |  |
| Engine Exhaust Flow Rate | `22 1F 9E` | 0..15 | `raw * 0.2` | kg/h | Y |  |
| Grille Shutter Control Mode | `22 20 2B` | 0..7 | `raw` | 0 = Normal; 1 = Maintenance | N | Only observed constant |
| EVAP Purge VSV | `22 28 01` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vacuum Pump | `22 28 02` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vent Value and Vacuum Pump | `22 28 02` | 14..15 | `raw` | 0 = A; 1 = B; 2 = C; 3 = D | N | Only observed constant |
| VSV for Vent Valve | `22 28 02` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VVT Ex OCV Target Duty #1 | `22 28 05` | 0..7 | `raw - 128` | % | N | Likely an active-test setting; stays at rest while driving |
| EGR Step Position | `22 28 07` | 0..7 | `raw` | step | Y |  |
| Fuel Vapor-Containment Valve | `22 28 08` | 15..15 | `raw` | 0 = Close; 1 = Open | N | Only observed constant |
| Fuel Pump Duty Ratio | `22 28 0E` | 0..7 | `raw` | % | N | Likely an active-test setting; stays at rest while driving |
| Fuel Filler Opener | `22 28 0F` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Target Fuel Pressure Offset | `22 28 10` | 0..7 | `raw * 0.1953125 - 25` | % | N | Only observed constant |
| Injection Volume | `22 28 11` | 0..7 | `raw * 0.1953125 - 25` | % | N | Likely an active-test setting; stays at rest while driving |
| Engine Cooling Fan | `22 28 26` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| Electric Water Pump | `22 28 27` | 0..7 | `raw * 40` | rpm | Y |  |
| Coolant Water Route Switching Valve | `22 28 2C` | 15..15 | `raw` | 0 = Open; 1 = Close | N | Only observed constant |
| VVT-iE Opening Angle Bank 1 | `22 28 43` | 0..7 | `raw` | deg | N | Likely an active-test setting; stays at rest while driving |
| Engine Oil Pressure Control Valve | `22 28 48` | 0..15 | `raw * 0.0762939453125` | mA | Y |  |
| TC Terminal | `22 2B FF` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |

## Cruise Control

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw * 625000 / 388357` | km | Y |  |
| Cruise Brake Cancel Switch | `22 10 11` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light Switch Main-CPU | `22 10 11` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Main Switch Operation Condition | `22 10 11` | 75..75 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MODE Change Switch | `22 10 11` | 77..77 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Number of Emission DTC | `22 1F 01` | 1..7 | `raw` |  | N | Only observed constant |
