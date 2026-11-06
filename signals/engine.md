# Engine

Request ID `0x700`, response ID `0x708`. Use these as `<REQUEST ID>` and `<RESPONSE ID>` in [PROTOCOL.md](../PROTOCOL.md).

This address answers for Engine (521 signals), Cruise Control (9 signals).

## Engine

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | N | Only observed constant |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | N | Only observed constant |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `0x0103` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw` | km | Y |  |
| Status of Active Grille Air Shutter | `0x0106` | 0..7 | `raw` | 0 = Incomplete; 1 = Existence; 2 = Nonexistence | N | Only observed constant |
| BATT Voltage | `0x1002` | 0..15 | `raw * 0.001220703125` | V | Y |  |
| IG ON Duration Time | `0x1007` | 0..31 | `raw * 8.192` | ms | Y |  |
| IG OFF Duration Time | `0x1007` | 32..63 | `raw * 8.192` | ms | N | Only observed constant |
| Engine Oil Level SW | `0x1011` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Key Unlock Signal | `0x1011` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Electrical Load Signal No.3 | `0x1011` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Electrical Load Signal No.2 | `0x1011` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Electrical Load Signal No.1 | `0x1011` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Starter SW | `0x1011` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light SW | `0x1011` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (N Range) | `0x1011` | 24..24 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (R Range) | `0x1011` | 25..25 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (P Range) | `0x1011` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vehicle Specification Signal No.1 | `0x1011` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Power Steering SW | `0x1011` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| A/C Pressure Normal SW | `0x1011` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Clutch SW | `0x1011` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Neutral Position SW | `0x1011` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Mode Cancel SW | `0x1011` | 40..40 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ECO Mode SW | `0x1011` | 41..41 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Snow Mode SW | `0x1011` | 42..42 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Power Mode SW | `0x1011` | 43..43 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Sport Shift Down SW | `0x1011` | 44..44 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Sport Shift Up SW | `0x1011` | 45..45 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (S Range) | `0x1011` | 46..46 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (D Range) | `0x1011` | 47..47 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Transfer L4 | `0x1011` | 56..56 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| 4WD SW | `0x1011` | 57..57 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.6 | `0x1011` | 58..58 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.5 | `0x1011` | 59..59 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.4 | `0x1011` | 60..60 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.3 | `0x1011` | 61..61 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.2 | `0x1011` | 62..62 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| T/M Pressure SW No.1 | `0x1011` | 63..63 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst Regeneration Switch | `0x1011` | 72..72 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| iMT SW | `0x1011` | 73..73 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Kick Down SW | `0x1011` | 74..74 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Main SW | `0x1011` | 75..75 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ASL SW | `0x1011` | 76..76 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Fuel Lid SW | `0x1011` | 78..78 | `raw` | 0 = Close; 1 = Open | N | Only observed constant |
| Fuel Lid Sensor SW | `0x1011` | 79..79 | `raw` | 0 = Close; 1 = Open | N | Only observed constant |
| Track Mode Switch | `0x1011` | 88..88 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IG2 / IGP | `0x1011` | 89..89 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IGR | `0x1011` | 90..90 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drive Mode Select Switch | `0x1011` | 91..91 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Door Courtesy Switch | `0x1011` | 92..92 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SD Switch | `0x1011` | 93..93 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift SW Status (B Range) | `0x1011` | 94..94 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Neutral Position Switch | `0x1011` | 95..95 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drive Mode Select Switching Switch | `0x1011` | 104..104 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Endless Rotary Switch (Phase B) | `0x1011` | 105..105 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Endless Rotary Switch (Phase A) | `0x1011` | 106..106 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Exhaust Brake Switch | `0x1011` | 107..107 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Heater Idle Up Switch | `0x1011` | 108..108 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drive Mode Select Switch (DMS-) | `0x1011` | 109..109 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Drive Mode Select Switch (DMS+) | `0x1011` | 110..110 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Freezer Signal | `0x1011` | 111..111 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift Sensor Switch DB1 | `0x1011` | 120..120 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift Sensor Switch SPN | `0x1011` | 121..121 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift Sensor Switch SPR | `0x1011` | 122..122 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift Sensor Switch SPP | `0x1011` | 123..123 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Starter Switch Signal | `0x1011` | 124..124 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| TOW HAUL Mode Switching Switch | `0x1011` | 125..125 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IGCT | `0x1011` | 126..126 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Multi Terrain Select Mode Switching Switch | `0x1011` | 127..127 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Total FT Bank 1 | `0x1015` | 0..7 | `raw * 0.00390625 - 0.5` |  | Y |  |
| Injection Volume Cylinder #1 | `0x1017` | 0..15 | `raw * 0.00003125` | ml | Y |  |
| Injector Cylinder #1 (Port) | `0x1018` | 0..15 | `raw` | μs | Y |  |
| Injection Time Cylinder #1 (D4) | `0x1019` | 0..15 | `raw` | μs | Y |  |
| Injection Timing Cylinder #1 (D4) | `0x1019` | 16..31 | `raw * 0.1 - 3276.8` | deg(CA) | Y |  |
| High Pressure Fuel Pump Discharge Rate | `0x101B` | 0..15 | `raw * 0.00003125` | ml | Y |  |
| High Pressure Fuel Pump Duty Ratio (D4) | `0x101B` | 16..23 | `raw * 0.5` | % | Y |  |
| High Fuel Pressure Sensor | `0x101D` | 0..15 | `raw * 0.001953125 - 64` | MPa | Y |  |
| Injection Mode | `0x101F` | 0..7 | `raw` | 0 = Port; 1 = Direct; 2 = Either | Y |  |
| Injection Switching Status | `0x1020` | 15..15 | `raw` | 0 = NG; 1 = OK | N | Only observed constant |
| Vacuum Pump | `0x1024` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VSV for Vent Valve | `0x1024` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| A/F Learn Value Idle Bank 1 | `0x102A` | 0..7 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Learn Value Low Bank 1 | `0x102A` | 8..15 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Learn Value Mid No.1 Bank 1 | `0x102A` | 16..23 | `raw * 0.390625 - 50` | % | N | Only observed constant |
| A/F Learn Value Mid No.2 Bank 1 | `0x102A` | 24..31 | `raw * 0.390625 - 50` | % | N | Only observed constant |
| A/F Learn Value High Bank 1 | `0x102A` | 32..39 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Sensor Heater Current Value B1S2 | `0x1036` | 0..15 | `raw * 0.001` | A | Y |  |
| A/F Sensor Heater Duty B1S2 | `0x1036` | 16..31 | `raw * 0.006103515625` | % | Y |  |
| A/F Sensor Impedance B1S2 | `0x1036` | 32..47 | `raw * 0.32421875` | ohm | Y |  |
| A/F Sensor Determination (Worst Value) Bank 1 | `0x103A` | 0..7 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) Cylinder #1 | `0x103A` | 8..15 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) Cylinder #2 | `0x103A` | 16..23 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) Cylinder #3 | `0x103A` | 24..31 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) Cylinder #4 | `0x103A` | 32..39 | `raw * 0.01` |  | N | Only observed constant |
| A/F Sensor Determination (Worst Value) (Port) Bank 1 | `0x103D` | 0..7 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) (Port) Cylinder #1 | `0x103D` | 8..15 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) (Port) Cylinder #2 | `0x103D` | 16..23 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) (Port) Cylinder #3 | `0x103D` | 24..31 | `raw * 0.01` |  | N | Only observed constant |
| Engine Speed Fluctuation Average (Worst Value) (Port) Cylinder #4 | `0x103D` | 32..39 | `raw * 0.01` |  | N | Only observed constant |
| IG-ON Coolant Temperature | `0x1041` | 0..7 | `raw * 0.625 - 40` | ℃ | N | Only observed constant |
| Initial Engine Coolant Temperature | `0x1041` | 8..15 | `raw * 0.625 - 40` | ℃ | N | Only observed constant |
| IG-ON Intake Air Temperature | `0x1042` | 0..7 | `raw * 0.625 - 40` | ℃ | N | Only observed constant |
| Initial Engine Intake Air Temperature | `0x1042` | 8..15 | `raw * 0.625 - 40` | ℃ | N | Only observed constant |
| Electric Water Pump Target Speed | `0x1043` | 0..7 | `raw * 25` | rpm | Y |  |
| Electric Water Pump Speed | `0x1043` | 8..15 | `raw * 25` | rpm | Y |  |
| Radiator Coolant Temperature | `0x1045` | 0..7 | `raw - 40` | ℃ | Y |  |
| Cooling Fan Duty Ratio | `0x1046` | 0..7 | `raw * 0.5` | % | Y |  |
| Throttle Position Sensor No.1 Voltage | `0x1057` | 0..7 | `raw * 0.01953125` | V | Y |  |
| Throttle Position Sensor No.2 Voltage | `0x1057` | 8..15 | `raw * 0.01953125` | V | Y |  |
| Throttle Position Sensor Fully Closed Learn Value | `0x1057` | 16..23 | `raw * 0.01953125` | V | N | Only observed constant |
| Throttle Position Sensor Open Position No.1 | `0x1057` | 24..31 | `raw * 0.01953125` | V | N | Only observed constant |
| Throttle Position Sensor Open Position No.2 | `0x1057` | 32..39 | `raw * 0.01953125` | V | N | Only observed constant |
| Throttle Request Position | `0x1057` | 40..47 | `raw * 0.01953125` | V | Y |  |
| Throttle Motor Current | `0x1058` | 0..7 | `raw * 0.078125` | A | Y |  |
| +BM Voltage | `0x1058` | 8..23 | `raw * 0.001220703125` | V | Y |  |
| Throttle Position Command | `0x1058` | 24..31 | `raw * 0.01953125` | V | Y |  |
| Throttle Motor Duty Ratio (Open) | `0x1058` | 32..39 | `raw` | % | Y |  |
| Throttle Motor Duty Ratio (Close) | `0x1058` | 40..47 | `raw` | % | Y |  |
| Actuator Power Supply | `0x1058` | 48..55 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Open Side Malfunction | `0x105A` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| System Guard | `0x105A` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Knock Correct Learn Value | `0x105C` | 0..15 | `raw * 0.03125 - 1024` | deg(CA) | Y |  |
| Knock F/B Value | `0x105C` | 16..31 | `raw * 0.03125 - 1024` | deg(CA) | Y |  |
| Engine ECU Internal Temperature | `0x105E` | 0..7 | `raw - 40` | ℃ | Y |  |
| Exhaust VVT OCV Control Duty Ratio Bank 1 | `0x1062` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| Fuel Pump Control Duty Ratio | `0x106A` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| IG OFF Elapsed Time | `0x106B` | 0..15 | `raw * 10` | min | Y |  |
| Soak IC Current Timer Value | `0x106B` | 16..31 | `raw * 9.375` | sec | Y |  |
| Distance Traveled from Last Battery Cable Disconnect | `0x106C` | 0..15 | `raw` | km | Y |  |
| Intake VVT Target Angle Bank 1 | `0x106F` | 0..15 | `raw * 0.009765625` | DegFR | Y |  |
| Exhaust VVT Target Angle Bank 1 | `0x1071` | 0..15 | `raw * 0.009765625` | DegFR | Y |  |
| Precursor Cumulative Dosage (Total) | `0x1073` | 0..15 | `raw * 0.006103515625` | % | N | Only observed constant |
| Oil Maintenance Mileage | `0x1073` | 16..31 | `raw - 32768` | km | Y |  |
| Engine Oil Pressure | `0x1074` | 0..15 | `raw * 0.078125` | kPa | Y |  |
| Idle Spark Advance Control Cylinder #1 | `0x1078` | 0..7 | `raw * 0.0625` | deg(CA) | N | Expected only at engine idle |
| Idle Spark Advance Control Cylinder #2 | `0x1078` | 8..15 | `raw * 0.0625` | deg(CA) | N | Expected only at engine idle |
| Idle Spark Advance Control Cylinder #3 | `0x1078` | 16..23 | `raw * 0.0625` | deg(CA) | N | Expected only at engine idle |
| Idle Spark Advance Control Cylinder #4 | `0x1078` | 24..31 | `raw * 0.0625` | deg(CA) | N | Expected only at engine idle |
| Exhaust VVT Retarded Fail | `0x107C` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Engine Start Hesitation | `0x107C` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Low Revolution for Engine Start | `0x107C` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VVT Advance Fail | `0x107C` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Low Revolution Control | `0x107C` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Engine Stall Control F/B Flow | `0x107D` | 0..15 | `raw * 0.03125 - 1024` | Nm | N | Always 0 on this vehicle; likely not available |
| Throttle Air Flow Learn Value (Area 1) | `0x107F` | 0..15 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Air Flow Learn Value (Area 2) | `0x107F` | 16..31 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Air Flow Learn Value (Area 3) | `0x107F` | 32..47 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Air Flow Learn Value (Calculated Value) | `0x107F` | 48..63 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Air Flow Learn Value (Atmosphere Pressure Offset Value) | `0x107F` | 64..71 | `raw * 0.01` |  | N | Only observed constant |
| Mass Air Flow Circuit | `0x107F` | 72..79 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| Intake VVT Change Angle Bank 1 | `0x1087` | 0..15 | `raw * 0.009765625` | DegFR | Y |  |
| Exhaust VVT Change Angle Bank 1 | `0x1088` | 0..15 | `raw * 0.009765625` | DegFR | Y |  |
| Exhaust VVT Hold Learn Value Bank 1 | `0x1089` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| VVT-iE Duty Ratio Bank 1 | `0x108A` | 0..15 | `raw * 0.006103515625 - 200` | % | Y |  |
| VVT-iE Motor Direction Bank 2 | `0x108C` | 14..14 | `raw` | 0 = Backward; 1 = Forward | N | Only observed constant |
| VVT-iE Motor Direction Bank 1 | `0x108C` | 15..15 | `raw` | 0 = Backward; 1 = Forward | Y |  |
| Intake VVT Timing Most Over-Retarded Learn Value Bank 1 | `0x108D` | 0..15 | `raw * 0.009765625` | deg(CA) | Y |  |
| Exhaust VVT Timing Most Over-Advanced Learn Value Bank 1 | `0x108F` | 0..15 | `raw * 0.009765625` | deg(CA) | Y |  |
| Fuel Pump Target Speed | `0x1097` | 0..15 | `raw - 32768` | rpm | Y |  |
| Fuel Pump F/B Offset | `0x1097` | 16..31 | `raw * 0.01 - 327.68` | L/h | Y |  |
| High Pressure Fuel Pump Internal Temperature | `0x1098` | 0..7 | `raw * 0.625 - 40` | ℃ | Y |  |
| Low Pressure Fuel Delivery Internal Temperature | `0x1098` | 8..15 | `raw * 0.625 - 40` | ℃ | Y |  |
| Cylinder Number 4 | `0x109D` | 0..7 | `raw` |  | N | Only observed constant |
| Average Engine Speed of All Cylinder | `0x109D` | 0..15 | `raw * 0.78125` | rpm | N | Always 0xFFFF (no value) on this vehicle |
| Engine Speed Cylinder #1 | `0x109D` | 16..31 | `raw * 0.78125` | rpm | N | Always 0xFFFF (no value) on this vehicle |
| Engine Speed Cylinder #2 | `0x109D` | 32..47 | `raw * 0.78125` | rpm | N | Always 0xFFFF (no value) on this vehicle |
| Engine Speed Cylinder #3 | `0x109D` | 48..63 | `raw * 0.78125` | rpm | N | Always 0xFFFF (no value) on this vehicle |
| Engine Speed Cylinder #4 | `0x109D` | 64..79 | `raw * 0.78125` | rpm | N | Always 0xFFFF (no value) on this vehicle |
| NT / NIM Sensor Test Results | `0x10A0` | 9..9 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Secondary Air Injection System Test Result | `0x10A0` | 10..10 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Misfire Test Result | `0x10A0` | 11..11 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor B2S1 Test Results | `0x10A0` | 12..12 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor B1S1 Test Results | `0x10A0` | 13..13 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor B2S2 Test Results | `0x10A0` | 14..14 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor B1S2 Test Results | `0x10A0` | 15..15 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F Learn Value Idle (Port) Bank 1 | `0x10A2` | 0..7 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Learn Value Low (Port) Bank 1 | `0x10A2` | 8..15 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Learn Value Mid No.1 (Port) Bank 1 | `0x10A2` | 16..23 | `raw * 0.390625 - 50` | % | Y |  |
| A/F Learn Value Mid No.2 (Port) Bank 1 | `0x10A2` | 24..31 | `raw * 0.390625 - 50` | % | N | Only observed constant |
| A/F Learn Value High (Port) Bank 1 | `0x10A2` | 32..39 | `raw * 0.390625 - 50` | % | N | Only observed constant |
| All Cylinders Misfire Count | `0x10A5` | 0..7 | `raw` |  | Y |  |
| Ignition Trigger Count | `0x10A5` | 8..23 | `raw` |  | Y |  |
| Misfire RPM | `0x10A5` | 24..31 | `raw * 25` | rpm | N | Only observed constant |
| Misfire Margin | `0x10A5` | 32..39 | `raw - 128` | % | Y |  |
| Misfire Count Cylinder #1 | `0x10A7` | 0..7 | `raw` |  | N | Only observed constant |
| Misfire Count Cylinder #2 | `0x10A7` | 8..15 | `raw` |  | N | Only observed constant |
| Misfire Count Cylinder #3 | `0x10A7` | 16..23 | `raw` |  | N | Only observed constant |
| Misfire Count Cylinder #4 | `0x10A7` | 24..31 | `raw` |  | N | Only observed constant |
| Misfire Load | `0x10AA` | 0..7 | `raw * 2` | % | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut History | `0x10AC` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut | `0x10AC` | 15..15 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #6 | `0x10AC` | 26..26 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #5 | `0x10AC` | 27..27 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #4 | `0x10AC` | 28..28 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #3 | `0x10AC` | 29..29 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #2 | `0x10AC` | 30..30 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Catalyst OT Misfire Fuel Cut Cylinder #1 | `0x10AC` | 31..31 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| A/F Sensor Impedance B1S1 | `0x10AD` | 0..15 | `raw * 0.32421875` | ohm | Y |  |
| A/F (O2) Sensor Heater Duty Ratio B1S1 | `0x10AD` | 32..47 | `raw * 0.006103515625` | % | Y |  |
| Vapor Pressure (Calculated) | `0x10B0` | 0..15 | `raw * 0.022 - 720.896` | kPa | Y |  |
| Vapor Pressure Pump | `0x10B0` | 16..31 | `raw * 0.022` | kPa | Y |  |
| Active Test History Flag | `0x10B1` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Active Test Prohibition Flag | `0x10B1` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Active Test Performing Flag | `0x10B1` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Purge Cut VSV Duty | `0x10B2` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| EVAP Purge Flow | `0x10B2` | 16..31 | `raw * 0.006103515625` | % | Y |  |
| EVAP Purge Density Learn Value | `0x10B2` | 32..47 | `raw * 0.006103515625 - 200` |  | N | Only observed constant |
| Tank Pressure | `0x10B3` | 0..15 | `raw * 0.00078125 - 25.6` | kPa | Y |  |
| Requested Engine Torque | `0x10C0` | 0..15 | `raw * 0.25` | kW | Y |  |
| HV Target Engine Speed | `0x10C1` | 0..7 | `raw * 25` | rpm | Y |  |
| Actual Engine Torque | `0x10C2` | 0..15 | `raw - 32768` | Nm | Y |  |
| Engine Driving Time | `0x10C3` | 0..7 | `raw` | sec | Y |  |
| Request Engine Run Time | `0x10C4` | 0..7 | `raw * 0.1` | sec | N | Only observed constant |
| Judge Time Engine Output | `0x10C5` | 0..7 | `raw * 0.1` | sec | N | Only observed constant |
| Judge Time Engine Ignition | `0x10C6` | 0..7 | `raw * 0.1` | sec | N | Only observed constant |
| ISC Learning Value | `0x10C7` | 0..15 | `raw * 0.03125 - 1024` | Nm | N | Only observed constant |
| Fuel Level | `0x10C8` | 9..9 | `raw` | 0 = Empty; 1 = Not Emp | N | Only observed constant |
| ISC Learning | `0x10C8` | 10..10 | `raw` | 0 = Incmpl; 1 = Compl | N | Only observed constant |
| F/C for Engine Stop Req | `0x10C8` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Engine Independent | `0x10C8` | 12..12 | `raw` | 0 = Not Opr; 1 = Operate | N | Only observed constant |
| Racing Operation | `0x10C8` | 13..13 | `raw` | 0 = Not Opr; 1 = Operate | N | Only observed constant |
| Request Warm-up | `0x10C8` | 14..14 | `raw` | 0 = Not Req; 1 = Request | N | Only observed constant |
| Engine Independent Control | `0x10C8` | 15..15 | `raw` | 0 = Not Opr; 1 = Operate | N | Only observed constant |
| Low Fuel Pressure Sensor | `0x10CD` | 0..15 | `raw * 0.1 - 3276.8` | kPa | Y |  |
| Coolant Temperature Sensor Fail Time | `0x10D2` | 0..31 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 0 | `0x10D3` | 0..31 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 1 | `0x10D3` | 32..63 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 2 | `0x10D3` | 64..95 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 3 | `0x10D3` | 96..127 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 4 | `0x10D3` | 128..159 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 5 | `0x10D3` | 160..191 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 6 | `0x10D3` | 192..223 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 7 | `0x10D3` | 224..255 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 8 | `0x10D3` | 256..287 | `raw * 524.228` | ms | N | Only observed constant |
| Coolant Temperature Total Time Area 9 | `0x10D3` | 288..319 | `raw * 524.228` | ms | N | Only observed constant |
| Total Fuel Consumption Integrated Value | `0x10D4` | 0..31 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 0 | `0x10D5` | 0..31 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 1 | `0x10D5` | 32..63 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 2 | `0x10D5` | 64..95 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 3 | `0x10D5` | 96..127 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 4 | `0x10D5` | 128..159 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 5 | `0x10D5` | 160..191 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 6 | `0x10D5` | 192..223 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 7 | `0x10D5` | 224..255 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 8 | `0x10D5` | 256..287 | `raw * 0.064` | ml | N | Only observed constant |
| Total Fuel Consumption Integrated Value Coolant Temperature Area 9 | `0x10D5` | 288..319 | `raw * 0.064` | ml | N | Only observed constant |
| Air Flow Meter Fail Time | `0x10D6` | 0..31 | `raw * 524.228` | ms | N | Only observed constant |
| Total Intake Air Volume Integrated Value | `0x10D7` | 0..31 | `raw` | g | N | Only observed constant |
| Across Initial Position Count Area 0 Bank 1 | `0x10D8` | 0..31 | `raw` |  | Y |  |
| Across Initial Position Count Area 1 Bank 1 | `0x10D8` | 32..63 | `raw` |  | Y |  |
| Across Initial Position Count Area 2 Bank 1 | `0x10D8` | 64..95 | `raw` |  | Y |  |
| Across Initial Position Count Area 3 Bank 1 | `0x10D8` | 96..127 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 4 Bank 1 | `0x10D8` | 128..159 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 0 Bank 2 | `0x10D9` | 0..31 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 1 Bank 2 | `0x10D9` | 32..63 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 2 Bank 2 | `0x10D9` | 64..95 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 3 Bank 2 | `0x10D9` | 96..127 | `raw` |  | N | Only observed constant |
| Across Initial Position Count Area 4 Bank 2 | `0x10D9` | 128..159 | `raw` |  | N | Only observed constant |
| Total IG ON Time | `0x10DA` | 0..31 | `raw * 524.228` | ms | Y |  |
| Precursor Cumulative Dosage (Total) | `0x10DB` | 0..31 | `raw * 3125 / 67108864` | ppm | Y |  |
| Throttle Flow Rate Characteristics Learning Value Area 0 | `0x10DC` | 0..15 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 1 | `0x10DC` | 16..31 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 2 | `0x10DC` | 32..47 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 3 | `0x10DC` | 48..63 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 4 | `0x10DC` | 64..79 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 5 | `0x10DC` | 80..95 | `raw * 0.000030517578125` |  | N | Only observed constant |
| Throttle Flow Rate Characteristics Learning Value Area 6 | `0x10DC` | 96..111 | `raw * 0.000030517578125` |  | N | Only observed constant |
| ISC F/B Learning Value | `0x10DD` | 0..15 | `raw * 0.03125` | Nm | N | Only observed constant |
| A/F Learn Value Area 0 Bank 1 | `0x10DE` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 1 Bank 1 | `0x10DE` | 16..31 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 2 Bank 1 | `0x10DE` | 32..47 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 3 Bank 1 | `0x10DE` | 48..63 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 4 Bank 1 | `0x10DE` | 64..79 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 5 Bank 1 | `0x10DE` | 80..95 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 6 Bank 1 | `0x10DE` | 96..111 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 7 Bank 1 | `0x10DE` | 112..127 | `raw * 0.006103515625` | % | Y |  |
| A/F Learn Value Area 8 Bank 1 | `0x10DE` | 128..143 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 9 Bank 1 | `0x10DE` | 144..159 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 0 Bank 2 | `0x10DF` | 0..15 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 1 Bank 2 | `0x10DF` | 16..31 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 2 Bank 2 | `0x10DF` | 32..47 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 3 Bank 2 | `0x10DF` | 48..63 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 4 Bank 2 | `0x10DF` | 64..79 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 5 Bank 2 | `0x10DF` | 80..95 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 6 Bank 2 | `0x10DF` | 96..111 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 7 Bank 2 | `0x10DF` | 112..127 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 8 Bank 2 | `0x10DF` | 128..143 | `raw * 0.006103515625` | % | N | Only observed constant |
| A/F Learn Value Area 9 Bank 2 | `0x10DF` | 144..159 | `raw * 0.006103515625` | % | N | Only observed constant |
| EGR Valve Initialization Count | `0x10E0` | 0..31 | `raw` |  | Y |  |
| Atmospheric Pressure during EGR OBD | `0x10E1` | 0..7 | `raw * 0.5` | kPa | Y |  |
| Reference Intake Manifold Pressure during EGR OBD | `0x10E1` | 8..15 | `raw * 0.5` | kPa | Y |  |
| Pressure Fluctuation during EGR OBD | `0x10E1` | 16..23 | `raw * 0.15625` | kPa | Y |  |
| Engine Speed during EGR OBD | `0x10E1` | 24..31 | `raw * 12.5` | rpm | N | Only observed constant |
| GPF Differential Pressure Maximum Area 1, 0 | `0x10E2` | 0..7 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 1, 0 | `0x10E2` | 8..15 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 1, 0 | `0x10E2` | 16..23 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 1, 0 | `0x10E2` | 24..31 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 1, 0 | `0x10E2` | 32..39 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 1, 0 | `0x10E2` | 40..47 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 1, 0 | `0x10E2` | 48..55 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 2, 0 | `0x10E2` | 56..63 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 2, 0 | `0x10E2` | 64..71 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 2, 0 | `0x10E2` | 72..79 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 2, 0 | `0x10E2` | 80..87 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 2, 0 | `0x10E2` | 88..95 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 2, 0 | `0x10E2` | 96..103 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 2, 0 | `0x10E2` | 104..111 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 3, 0 | `0x10E2` | 112..119 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 3, 0 | `0x10E2` | 120..127 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 3, 0 | `0x10E2` | 128..135 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 3, 0 | `0x10E2` | 136..143 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 3, 0 | `0x10E2` | 144..151 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 3, 0 | `0x10E2` | 152..159 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 3, 0 | `0x10E2` | 160..167 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 4, 0 | `0x10E2` | 168..175 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 4, 0 | `0x10E2` | 176..183 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 4, 0 | `0x10E2` | 184..191 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 4, 0 | `0x10E2` | 192..199 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 4, 0 | `0x10E2` | 200..207 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 4, 0 | `0x10E2` | 208..215 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 4, 0 | `0x10E2` | 216..223 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 1, 1 | `0x10E3` | 0..7 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 1, 1 | `0x10E3` | 8..15 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 1, 1 | `0x10E3` | 16..23 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 1, 1 | `0x10E3` | 24..31 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 1, 1 | `0x10E3` | 32..39 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 1, 1 | `0x10E3` | 40..47 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 1, 1 | `0x10E3` | 48..55 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 2, 1 | `0x10E3` | 56..63 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 2, 1 | `0x10E3` | 64..71 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 2, 1 | `0x10E3` | 72..79 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 2, 1 | `0x10E3` | 80..87 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 2, 1 | `0x10E3` | 88..95 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 2, 1 | `0x10E3` | 96..103 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 2, 1 | `0x10E3` | 104..111 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 3, 1 | `0x10E3` | 112..119 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 3, 1 | `0x10E3` | 120..127 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 3, 1 | `0x10E3` | 128..135 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 3, 1 | `0x10E3` | 136..143 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 3, 1 | `0x10E3` | 144..151 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 3, 1 | `0x10E3` | 152..159 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 3, 1 | `0x10E3` | 160..167 | `raw` | gm/sec | N | Only observed constant |
| GPF Differential Pressure Maximum Area 4, 1 | `0x10E3` | 168..175 | `raw * 0.625` | kPa | N | Running maximum; rises only on a new peak |
| Intake Air Volume at GPF Differential Pressure Record Area 4, 1 | `0x10E3` | 176..183 | `raw * 2` | gm/sec | N | Only observed constant |
| PM Deposition Value at GPF Differential Pressure Record Area 4, 1 | `0x10E3` | 184..191 | `raw * 0.25` | g | N | Only observed constant |
| Ash Deposition Value at GPF Differential Pressure Record Area 4, 1 | `0x10E3` | 192..199 | `raw * 2` | g | N | Only observed constant |
| GPF Floor Temperature at GPF Differential Pressure Record Area 4, 1 | `0x10E3` | 200..207 | `raw * 5` | ℃ | N | Only observed constant |
| Atmospheric Pressure at GPF Differential Pressure Record Area 4, 1 | `0x10E3` | 208..215 | `raw * 0.5` | kPa | N | Only observed constant |
| Intake Air Volume Differential at GPF Differential Pressure Record Area 4, 1 | `0x10E3` | 216..223 | `raw` | gm/sec | N | Only observed constant |
| Coolant Temperature at IG OFF -> ON | `0x10E4` | 0..7 | `raw` | ℃ | N | Only observed constant |
| Intake Air Temperature at IG OFF -> ON | `0x10E4` | 8..15 | `raw` | ℃ | N | Only observed constant |
| Ambient Temperature at IG OFF -> ON | `0x10E4` | 16..23 | `raw` | ℃ | N | Only observed constant |
| Year at IG OFF -> ON | `0x10E4` | 24..31 | `raw` | year | N | Only observed constant |
| Month at IG OFF -> ON | `0x10E4` | 32..39 | `raw` | month | N | Only observed constant |
| Day at IG OFF -> ON | `0x10E4` | 40..47 | `raw` | day | N | Only observed constant |
| Hour at IG OFF -> ON | `0x10E4` | 48..55 | `raw` | hour | N | Only observed constant |
| Minute at IG OFF -> ON | `0x10E4` | 56..63 | `raw` | minute | N | Only observed constant |
| Second at IG OFF -> ON | `0x10E4` | 64..71 | `raw` | second | N | Only observed constant |
| Coolant Temperature at IG ON -> OFF | `0x10E4` | 72..79 | `raw` | ℃ | N | Only observed constant |
| Intake Air Temperature at IG ON -> OFF | `0x10E4` | 80..87 | `raw` | ℃ | N | Only observed constant |
| Ambient Temperature at IG ON -> OFF | `0x10E4` | 88..95 | `raw` | ℃ | N | Only observed constant |
| Year at IG ON -> OFF | `0x10E4` | 96..103 | `raw` | year | N | Only observed constant |
| Month at IG ON -> OFF | `0x10E4` | 104..111 | `raw` | month | N | Only observed constant |
| Day at IG ON -> OFF | `0x10E4` | 112..119 | `raw` | day | N | Only observed constant |
| Hour at IG ON -> OFF | `0x10E4` | 120..127 | `raw` | hour | N | Only observed constant |
| Minute at IG ON -> OFF | `0x10E4` | 128..135 | `raw` | minute | N | Only observed constant |
| Second at IG ON -> OFF | `0x10E4` | 136..143 | `raw` | second | N | Only observed constant |
| Maximum Coolant Temperature | `0x10E5` | 0..15 | `raw * 0.625` | ℃ | Y |  |
| Minimum Coolant Temperature | `0x10E5` | 16..31 | `raw * 0.625` | ℃ | N | Running minimum; falls only on a new low |
| Target Revolution Deviation Rate at Low | `0x10E5` | 32..47 | `raw * 0.006103515625` | % | Y |  |
| Target Revolution Deviation Rate at Middle | `0x10E5` | 48..63 | `raw * 0.006103515625` | % | Y |  |
| Target Revolution Deviation Rate at High | `0x10E5` | 64..79 | `raw * 0.006103515625` | % | Y |  |
| Average Feed Back Duty at Low | `0x10E5` | 80..95 | `raw * 0.006103515625` | % | Y |  |
| Average Feed Back Duty at Middle | `0x10E5` | 96..111 | `raw * 0.006103515625` | % | Y |  |
| Average Feed Back Duty at High | `0x10E5` | 112..127 | `raw * 0.006103515625` | % | Y |  |
| Data Set File Mode Memory Value | `0x10E6` | 0..15 | `raw` |  | N | Only observed constant |
| Variation Information | `0x10EB` | 0..7 | `raw` |  | N | Only observed constant |
| Soak IC First Start Time | `0x119A` | 0..7 | `raw * 10` | min; 0 = 0; 1 = Unknown | N | Only observed constant |
| Soak Timer Start History | `0x119A` | 8..15 | `raw` | 0 = Unknown; 90 = OFF; 165 = ON | N | Only observed constant |
| Grille Shutter Position | `0x11BB` | 0..7 | `raw` | % | Y |  |
| Grille Shutter Stuck Status | `0x11BD` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Grille Shutter Slipping Status | `0x11BD` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| All Universal Condition History | `0x11E0` | 0..7 | `raw` |  | Y |  |
| Universal Condition 1 Judgment History | `0x11E0` | 8..15 | `raw` |  | Y |  |
| Total Universal Condition 1 Judgment Time | `0x11E0` | 16..31 | `raw * 65.536` | ms | Y |  |
| Universal Condition 2 Judgment History | `0x11E0` | 32..39 | `raw` |  | Y |  |
| Total Universal Condition 2 Judgment Time | `0x11E0` | 40..55 | `raw * 65.536` | ms | Y |  |
| Universal Condition 3 Judgment History | `0x11E0` | 56..63 | `raw` |  | N | Only observed constant |
| Total Universal Condition 3 Judgment Time | `0x11E0` | 64..79 | `raw * 65.536` | ms | Y |  |
| Start Status for PDTC | `0x11E0` | 80..87 | `raw` |  | N | Only observed constant |
| Accelerator Pedal Idle Status for PDTC | `0x11E0` | 88..95 | `raw` |  | Y |  |
| Vehicle Speed for PDTC | `0x11E0` | 96..103 | `raw` | km/h | Y |  |
| Intake Camshaft Position Sensor Voltage | `0x1201` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Exhaust Camshaft Position Sensor Voltage | `0x1203` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Crankshaft Position Sensor Voltage | `0x1207` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| MIL | `0x1F01` | 0..0 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Number of Emission DTC | `0x1F01` | 1..7 | `raw` |  | N | Only observed constant |
| Complete Parts Monitor Result | `0x1F01` | 9..9 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Fuel System Monitor Result | `0x1F01` | 10..10 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Misfire Monitor Result | `0x1F01` | 11..11 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Ignition Monitor | `0x1F01` | 12..12 | `raw` | 0 = Spark Ignition; 1 = Compression Ignition | N | Only observed constant |
| Complete Parts Monitor | `0x1F01` | 13..13 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Fuel System Monitor | `0x1F01` | 14..14 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Misfire Monitor | `0x1F01` | 15..15 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| EGR/VVT Monitor | `0x1F01` | 16..16 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| A/F (O2) Sensor Heater Monitor | `0x1F01` | 17..17 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| A/F (O2) Sensor Monitor | `0x1F01` | 18..18 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Secondary Air Injection System Monitor | `0x1F01` | 20..20 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| EVAP Monitor | `0x1F01` | 21..21 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Heated Catalyst Monitor | `0x1F01` | 22..22 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| Catalyst Monitor | `0x1F01` | 23..23 | `raw` | 0 = Not Avl; 1 = Avail | N | Only observed constant |
| EGR/VVT Monitor Result | `0x1F01` | 24..24 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor Heater Monitor Result | `0x1F01` | 25..25 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor Monitor Result | `0x1F01` | 26..26 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Secondary Air Injection System Monitor Result | `0x1F01` | 28..28 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| EVAP Monitor Result | `0x1F01` | 29..29 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Heated Catalyst Monitor Result | `0x1F01` | 30..30 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Catalyst Monitor Result | `0x1F01` | 31..31 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Fuel System Status Bank 1 | `0x1F03` | 0..7 | `raw` | 0 = Unused; 1 = OL; 2 = CL; 4 = OLDrive; 8 = OLFault; 16 = CLFault | Y |  |
| Fuel System Status Bank 2 | `0x1F03` | 8..15 | `raw` | 0 = Unused; 1 = OL; 2 = CL; 4 = OLDrive; 8 = OLFault; 16 = CLFault | N | Only observed constant |
| Calculate Load | `0x1F04` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Coolant Temperature | `0x1F05` | 0..7 | `raw - 40` | ℃ | Y |  |
| Short FT B1S1 | `0x1F06` | 0..7 | `raw * 0.78125 - 100` | % | Y |  |
| Long FT B1S1 | `0x1F07` | 0..7 | `raw * 0.78125 - 100` | % | Y |  |
| Engine Speed | `0x1F0C` | 0..15 | `raw * 0.25` | rpm | Y |  |
| Vehicle Speed | `0x1F0D` | 0..7 | `raw` | km/h | Y |  |
| Ignition Timing Cylinder #1 | `0x1F0E` | 0..7 | `raw * 0.5 - 64` | deg | Y |  |
| Intake Air Temperature | `0x1F0F` | 0..7 | `raw - 40` | ℃ | Y |  |
| Mass Air Flow Sensor | `0x1F10` | 0..15 | `raw * 0.01` | gm/sec | Y |  |
| Throttle Position Sensor No.1 Voltage % | `0x1F11` | 0..7 | `raw * 20 / 51` | % | Y |  |
| OBD Requirements | `0x1F1C` | 0..7 | `raw` | 1 = OBD II (California ARB); 2 = OBD (US Federal EPA); 3 = OBD and OBD II; 4 = OBD I; 5 = Not OBD compliant; 6 = EOBD (Euro OBD); 7 = EOBD and OBD II; 8 = EOBD and OBD; 9 = EOBD, OBD and OBD II; 10 = JOBD (Japan OBD); 11 = JOBD and OBD II; 12 = JOBD and EOBD; 13 = JOBD, EOBD, and OBD II; 14 = EURO 4; 15 = EURO 5; 17 = Engine Manufacturer Diagnostics (EMD); 18 = Engine Manufacturer Diagnostics Enhanced (EMD+); 19 = Heavy Duty On-Board Diagnostics (Child/Partial); 20 = Heavy Duty On-Board Diagnostics; 21 = World Wide Harmonized OBD; 23 = Heavy Duty Euro OBD Stage I without NOx control; 24 = Heavy Duty Euro OBD Stage I with NOx control; 25 = Heavy Duty Euro OBD Stage II without NOx control; 26 = Heavy Duty Euro OBD Stage II with NOx control; 28 = Brazil OBD Phase 1; 29 = Brazil OBD Phase 2; 30 = Korean OBD; 31 = India OBD I; 32 = India OBD II; 33 = Heavy Duty Euro OBD Stage VI; 34 = OBD, OBD II and HD OBD; 35 = Brazil OBD Phase 3; 36 = Motorcycle, Euro OBD-I; 37 = Motorcycle, Euro OBD-II; 38 = Motorcycle, China OBD-I; 39 = Motorcycle, Taiwan OBD-I; 40 = Motorcycle, Japan OBD-I; 41 = China Nationwide Stage 6; 42 = Brazil OBD Diesel | N | Only observed constant |
| Engine Run Time | `0x1F1F` | 0..15 | `raw` | sec | Y |  |
| MIL ON Run Distance | `0x1F21` | 0..15 | `raw` | km | N | Only observed constant |
| EVAP (Purge) VSV | `0x1F2E` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Warmup Cycle Cleared DTC | `0x1F30` | 0..7 | `raw` |  | N | Only observed constant |
| Distance from DTC Cleared | `0x1F31` | 0..15 | `raw` | km | Y |  |
| Atmospheric Pressure | `0x1F33` | 0..7 | `raw` | kPa | Y |  |
| A/F Bank 1 | `0x1F34` | 0..15 | `raw * 0.000443775` |  | Y |  |
| A/F (O2) Sensor Current B1S1 | `0x1F34` | 16..31 | `raw * 0.00390625 - 128` | mA | Y |  |
| A/F (O2) Sensor Current B1S2 | `0x1F35` | 16..31 | `raw * 0.00390625 - 128` | mA | Y |  |
| Catalyst Temperature B1S1 | `0x1F3C` | 0..15 | `raw * 0.1 - 40` | ℃ | Y |  |
| Catalyst Temperature B1S2 | `0x1F3E` | 0..15 | `raw * 0.1 - 40` | ℃ | Y |  |
| Component Monitor Result (Current) | `0x1F41` | 9..9 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Fuel System Monitor Result (Current) | `0x1F41` | 10..10 | `raw` | 0 = Compl; 1 = Incmpl | Y |  |
| Misfire Monitor Result (Current) | `0x1F41` | 11..11 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Ignition Monitor (Current) | `0x1F41` | 12..12 | `raw` | 0 = Spark Ignition; 1 = Compression Ignition | N | Only observed constant |
| Component Monitor ENA (Current) | `0x1F41` | 13..13 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| Fuel System Monitor ENA (Current) | `0x1F41` | 14..14 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| Misfire Monitor ENA (Current) | `0x1F41` | 15..15 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| EGR/VVT Monitor ENA (Current) | `0x1F41` | 16..16 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| O2 Sensor Heater ENA (Current) | `0x1F41` | 17..17 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| A/F (O2) Sensor Monitor ENA (Current) | `0x1F41` | 18..18 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| Secondary Air Injection System Monitor ENA (Current) | `0x1F41` | 20..20 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| EVAP Monitor ENA (Current) | `0x1F41` | 21..21 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| Heated Catalyst Monitor ENA (Current) | `0x1F41` | 22..22 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| Catalyst Monitor ENA (Current) | `0x1F41` | 23..23 | `raw` | 0 = Unable; 1 = Enable | N | Only observed constant |
| EGR/VVT Monitor Result (Current) | `0x1F41` | 24..24 | `raw` | 0 = Compl; 1 = Incmpl | Y |  |
| O2 Sensor Heater Monitor Result (Current) | `0x1F41` | 25..25 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor Monitor Result (Current) | `0x1F41` | 26..26 | `raw` | 0 = Compl; 1 = Incmpl | Y |  |
| Secondary Air Injection System Monitor Result (Current) | `0x1F41` | 28..28 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| EVAP Monitor Result (Current) | `0x1F41` | 29..29 | `raw` | 0 = Compl; 1 = Incmpl | Y |  |
| Heated Catalyst Monitor Result (Current) | `0x1F41` | 30..30 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Catalyst Monitor Result (Current) | `0x1F41` | 31..31 | `raw` | 0 = Compl; 1 = Incmpl | Y |  |
| Battery Voltage | `0x1F42` | 0..15 | `raw * 0.001` | V | Y |  |
| Vehicle Load | `0x1F43` | 0..15 | `raw * 20 / 51` | % | Y |  |
| Target Air-Fuel Ratio | `0x1F44` | 0..15 | `raw * 2 / 65535` |  | Y |  |
| Throttle Sensor Position | `0x1F45` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Ambient Temperature | `0x1F46` | 0..7 | `raw - 40` | ℃ | Y |  |
| Throttle Position Sensor No.2 Voltage % | `0x1F47` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Throttle Motor Duty Ratio | `0x1F4C` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Running Time from MIL ON | `0x1F4D` | 0..15 | `raw` | min | N | Only observed constant |
| Time after DTC Cleared | `0x1F4E` | 0..15 | `raw` | min | Y |  |
| Current Fuel Type | `0x1F51` | 0..7 | `raw` | 1 = Gasoline/petrol; 2 = Methanol; 3 = Ethanol; 4 = Diesel; 5 = Liquefied Petroleum Gas (LPG) LPG; 6 = Compressed Natural Gas (CNG) CNG; 7 = Propane; 8 = Battery/electric; 9 = Bi-fuel vehicle using gasoline; 10 = Bi-fuel vehicle using methanol; 11 = Bi-fuel vehicle using ethanol; 12 = Bi-fuel vehicle using LPG; 13 = Bi-fuel vehicle using CNG; 14 = Bi-fuel vehicle using propane; 15 = Bi-fuel vehicle using battery; 16 = Bi-fuel vehicle using battery and combustion engine; 17 = Hybrid vehicle using gasoline engine; 18 = Hybrid vehicle using gasoline engine on ethanol; 19 = Hybrid vehicle using diesel engine; 20 = Hybrid vehicle using battery; 21 = Hybrid vehicle using battery and combustion engine; 22 = Hybrid vehicle in regeneration mode; 24 = Bi-fuel vehicle using Natural Gas; 25 = Bi-fuel vehicle using diesel; 26 = Natural Gas (Compressed or Liquefied Natural Gas); 27 = Dual Fuel - Diesel and CNG; 28 = Dual Fuel - Diesel and LNG; 29 = Fuel Cell Utilizing Hydrogen; 30 = Hydrogen Internal Combustion Engine | N | Only observed constant |
| EVAP Vapor Pressure (Wide) | `0x1F54` | 0..15 | `raw * 0.001` | kPa | Y |  |
| Short FT B1S2 | `0x1F55` | 0..7 | `raw * 0.78125 - 100` | % | Y |  |
| Long FT B1S2 | `0x1F56` | 0..7 | `raw * 0.78125 - 100` | % | N | Only observed constant |
| Engine Oil Temperature Sensor | `0x1F5C` | 0..7 | `raw - 40` | ℃ | Y |  |
| Actual Engine Percent Torque | `0x1F62` | 0..7 | `raw - 125` | % | Y |  |
| Engine Reference Torque (Fixed Value) | `0x1F63` | 0..15 | `raw` | Nm | N | Only observed constant |
| Actual EGR Valve Position No.2 Supported | `0x1F69` | 3..3 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target EGR Valve Position No.2 Supported | `0x1F69` | 4..4 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Actual EGR Valve Position No.1 Supported | `0x1F69` | 6..6 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target EGR Valve Position No.1 Supported | `0x1F69` | 7..7 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target EGR Valve Position No.1 | `0x1F69` | 8..15 | `raw * 20 / 51` | % | Y |  |
| Actual EGR Valve Position No.1 | `0x1F69` | 16..23 | `raw * 20 / 51` | % | N | The ECU flags this value as unsupported |
| Target EGR Valve Position No.2 | `0x1F69` | 32..39 | `raw * 20 / 51` | % | N | The ECU flags this value as unsupported |
| Actual EGR Valve Position No.2 | `0x1F69` | 40..47 | `raw * 20 / 51` | % | N | The ECU flags this value as unsupported |
| Fuel Pressure (Low) / Fuel Pressure 2 Supported | `0x1F6D` | 3..3 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target Fuel Pressure (Low) / Target Fuel Pressure 2 Supported | `0x1F6D` | 4..4 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Fuel Pressure (High) Supported | `0x1F6D` | 6..6 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target Fuel Pressure (High) Supported | `0x1F6D` | 7..7 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Target Fuel Pressure (High) | `0x1F6D` | 8..23 | `raw * 10` | kPa | Y |  |
| Fuel Pressure (High) | `0x1F6D` | 24..39 | `raw * 10` | kPa | Y |  |
| Target Fuel Pressure (Low) / Target Fuel Pressure 2 | `0x1F6D` | 48..63 | `raw * 10` | kPa | Y |  |
| Fuel Pressure (Low) / Fuel Pressure 2 | `0x1F6D` | 64..79 | `raw * 10` | kPa | Y |  |
| Intake Manifold Absolute Pressure Supported | `0x1F87` | 7..7 | `raw` | 0 = Unsupp; 1 = Supp | N | Only observed constant |
| Intake Manifold Absolute Pressure | `0x1F87` | 8..23 | `raw * 0.03125` | kPa | Y |  |
| Engine Friction Percent Torque | `0x1F8E` | 0..7 | `raw - 125` | % | Y |  |
| Engine Fuel Rate | `0x1F9D` | 0..15 | `raw * 0.02` | gm/sec | Y |  |
| Vehicle Fuel Rate | `0x1F9D` | 16..31 | `raw * 0.02` | gm/sec | Y |  |
| Engine Exhaust Flow Rate | `0x1F9E` | 0..15 | `raw * 0.2` | kg/h | Y |  |
| Grille Shutter Control Mode | `0x202B` | 0..7 | `raw` | 0 = Normal; 1 = Maintenance | N | Only observed constant |
| EVAP Purge VSV | `0x2801` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vacuum Pump | `0x2802` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Vent Value and Vacuum Pump | `0x2802` | 14..15 | `raw` | 0 = A; 1 = B; 2 = C; 3 = D | N | Only observed constant |
| VSV for Vent Valve | `0x2802` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VVT Ex OCV Target Duty #1 | `0x2805` | 0..7 | `raw - 128` | % | N | Likely an active-test setting; stays at rest while driving |
| EGR Step Position | `0x2807` | 0..7 | `raw` | step | Y |  |
| Fuel Vapor-Containment Valve | `0x2808` | 15..15 | `raw` | 0 = Close; 1 = Open | N | Only observed constant |
| Fuel Pump Duty Ratio | `0x280E` | 0..7 | `raw` | % | N | Likely an active-test setting; stays at rest while driving |
| Fuel Filler Opener | `0x280F` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Target Fuel Pressure Offset | `0x2810` | 0..7 | `raw * 0.1953125 - 25` | % | N | Only observed constant |
| Injection Volume | `0x2811` | 0..7 | `raw * 0.1953125 - 25` | % | N | Likely an active-test setting; stays at rest while driving |
| Engine Cooling Fan | `0x2826` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| Electric Water Pump | `0x2827` | 0..7 | `raw * 40` | rpm | Y |  |
| Coolant Water Route Switching Valve | `0x282C` | 15..15 | `raw` | 0 = Open; 1 = Close | N | Only observed constant |
| VVT-iE Opening Angle Bank 1 | `0x2843` | 0..7 | `raw` | deg | N | Likely an active-test setting; stays at rest while driving |
| Engine Oil Pressure Control Valve | `0x2848` | 0..15 | `raw * 0.0762939453125` | mA | Y |  |
| TC Terminal | `0x2BFF` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |

## Cruise Control

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw * 625000 / 388357` | km | Y |  |
| Cruise Brake Cancel Switch | `0x1011` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light Switch Main-CPU | `0x1011` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Main Switch Operation Condition | `0x1011` | 75..75 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MODE Change Switch | `0x1011` | 77..77 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Number of Emission DTC | `0x1F01` | 1..7 | `raw` |  | N | Only observed constant |
