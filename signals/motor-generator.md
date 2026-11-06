# Motor Generator

Request ID `0x724`, response ID `0x72C`. Use these as `<REQUEST ID>` and `<RESPONSE ID>` in [PROTOCOL.md](../PROTOCOL.md).

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `0x0103` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw * 625000 / 388357` | km | Y |  |
| Airbag Collision Detection History | `0x1038` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Temperature(Upper) | `0x1201` | 0..7 | `raw` | ℃ | Y |  |
| Boosting Converter Temperature 1(Lower) | `0x1201` | 8..15 | `raw` | ℃ | Y |  |
| Boosting Converter Temperature Duty(Upper) | `0x1203` | 0..15 | `raw * 0.1` | % | Y |  |
| Motor Inverter Temperature Duty | `0x1204` | 0..15 | `raw * 0.1` | % | Y |  |
| Motor Inverter Temperature | `0x1205` | 0..7 | `raw` | ℃ | Y |  |
| Motor U Current AD Value(Lower Resolution) | `0x1206` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Motor V Current AD Value(Lower Resolution) | `0x1206` | 16..31 | `raw * 0.0000762939453125` | V | Y |  |
| Motor W Current AD Value(Lower Resolution) | `0x1206` | 32..47 | `raw * 0.0000762939453125` | V | Y |  |
| Motor V Current AD Value(Higher Resolution) | `0x1206` | 48..63 | `raw * 0.0000762939453125` | V | Y |  |
| Motor W Current AD Value(Higher Resolution) | `0x1206` | 64..79 | `raw * 0.0000762939453125` | V | Y |  |
| Motor U Current(Lower Resolution) | `0x1207` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| Motor V Current(Lower Resolution) | `0x1207` | 16..31 | `raw * 0.1 - 3276.8` | A | Y |  |
| Motor W Current(Lower Resolution) | `0x1207` | 32..47 | `raw * 0.1 - 3276.8` | A | Y |  |
| Generator U Current(Lower Resolution) | `0x1208` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| Generator V Current(Lower Resolution) | `0x1208` | 16..31 | `raw * 0.1 - 3276.8` | A | Y |  |
| Generator W Current(Lower Resolution) | `0x1208` | 32..47 | `raw * 0.1 - 3276.8` | A | Y |  |
| Motor ECU Power Supply (For 31V) | `0x1209` | 0..7 | `raw * 0.01953125` | V | N | Regulated supply; constant by design |
| Motor ECU Power Supply (For AD0) | `0x1209` | 8..15 | `raw * 0.01953125` | V | Y |  |
| Motor ECU Power Supply (For 2.5V) | `0x1209` | 16..23 | `raw * 0.01953125` | V | Y |  |
| Motor ECU Power Supply (For 2.5V Sub) | `0x120A` | 0..7 | `raw * 0.01953125` | V | Y |  |
| DC/DC Converter Operation State | `0x120B` | 0..7 | `raw * 0.01953125` | V | Y |  |
| DC/DC Converter Output Signal | `0x120B` | 8..15 | `raw * 0.01953125` | V | Y |  |
| DC/DC Converter Output Signal Duty | `0x120B` | 16..23 | `raw * 0.5` | % | Y |  |
| Motor Revolution | `0x120C` | 0..15 | `raw - 32768` | rpm | Y |  |
| Motor Torque | `0x120C` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Motor Control Mode | `0x120C` | 32..47 | `raw` |  | Y |  |
| Motor Carrier Frequency | `0x120C` | 48..55 | `raw` |  | N | Only observed constant |
| Motor Inverter Shut Down Signal | `0x120C` | 56..63 | `raw` |  | N | Only observed constant |
| Generator Revolution | `0x120D` | 0..15 | `raw - 32768` | rpm | Y |  |
| Generator Torque | `0x120D` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Generator Control Mode | `0x120D` | 32..47 | `raw` |  | Y |  |
| Generator Carrier Frequency | `0x120D` | 48..55 | `raw` |  | Y |  |
| Generator Inverter Shut Down Signal | `0x120D` | 56..63 | `raw` |  | Y |  |
| Inverter Coolant Temperature AD Value | `0x120E` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Inverter Coolant Temperature | `0x120F` | 0..7 | `raw - 40` | ℃ | Y |  |
| Inverter Input Current | `0x1210` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| Inverter Water Pump Duty | `0x1211` | 0..7 | `raw` | % | Y |  |
| Inverter Water Pump Revolution | `0x1211` | 8..23 | `raw` | rpm | Y |  |
| Collinear Graphic Engine Revolution | `0x1213` | 0..15 | `raw` | rpm | Y |  |
| Motor Resolver After Offset | `0x1214` | 0..7 | `raw * 1.40625` | deg | Y |  |
| Motor Resolver Offset Value | `0x1214` | 8..23 | `raw * 0.00762939453125 - 250` | deg | N | Only observed constant |
| Generator Resolver After Offset | `0x1215` | 0..7 | `raw * 1.40625` | deg | Y |  |
| Generator Resolver Offset Value | `0x1215` | 8..23 | `raw * 0.00762939453125 - 250` | deg | N | Only observed constant |
| Motor Resolver Supply Voltage | `0x1216` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| VL Voltage | `0x1217` | 0..15 | `raw * 0.1` | V | Y |  |
| VH Voltage | `0x1217` | 16..31 | `raw * 0.1` | V | Y |  |
| Boosting Converter PWM Duty | `0x1218` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Boosting Converter Control Mode | `0x1218` | 8..15 | `raw` |  | N | Only observed constant |
| Boosting Converter Carrier Frequency | `0x1218` | 16..23 | `raw` | 0 = HI; 1 = LO | N | Only observed constant |
| Boosting Converter Shut Down Signal | `0x1218` | 24..31 | `raw` |  | N | Only observed constant |
| Generate Request Torque | `0x1219` | 0..15 | `raw * 0.125 - 4096` | Nm | Y |  |
| Shift Position | `0x121A` | 0..7 | `raw` | 0 = P; 2 = R; 4 = N; 6 = D; 8 = B | Y |  |
| Motor Temperature | `0x121B` | 0..7 | `raw - 40` | ℃ | Y |  |
| Generator Temperature | `0x121C` | 0..7 | `raw - 40` | ℃ | Y |  |
| Short Wave Highest Value | `0x121D` | 0..7 | `raw * 0.01953125` | V | Y |  |
| Fail Safe Mode | `0x121E` | 0..7 | `raw - 128` |  | N | Only observed constant |
| Accelerator Position | `0x121F` | 0..7 | `raw * 0.5` | % | Y |  |
| WIN Control Limit Power | `0x1221` | 0..15 | `raw * 0.01 - 327.68` | kW | Y |  |
| WOUT Control Limit Power | `0x1221` | 16..31 | `raw * 0.01 - 327.68` | kW | Y |  |
| Engine Control Mode | `0x1222` | 0..7 | `raw` | 0 = Stop; 1 = Stop Process; 2 = Startup Process; 3 = Running | Y |  |
| MG1 Inverter Estimate Temperature (After IG ON) | `0x1227` | 0..7 | `raw` | ℃ | Y |  |
| MG1 Inverter Estimate Temperature (Max Value) | `0x1227` | 8..15 | `raw` | ℃ | N | Only observed constant |
| MG2 Inverter Estimate Temperature (After IG ON) | `0x1228` | 0..7 | `raw` | ℃ | Y |  |
| MG2 Inverter Estimate Temperature (Max Value) | `0x1228` | 8..15 | `raw` | ℃ | N | Only observed constant |
| Pressure Converter Temperature (After IG ON) | `0x122A` | 0..7 | `raw` | ℃ | Y |  |
| Pressure Converter Temperature (Max Value) | `0x122A` | 8..15 | `raw` | ℃ | N | Only observed constant |
| Battery Discharge Control Circuit Monitor Voltage | `0x123B` | 0..7 | `raw * 0.01953125` | V | Y |  |
| Battery Discharge Control Circuit Power Switch | `0x123B` | 8..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Motor Generate Request Torque | `0x1241` | 0..15 | `raw * 0.125 - 4096` | Nm | Y |  |
| Generator Inverter Temperature | `0x124F` | 0..7 | `raw` | ℃ | Y |  |
| Emergency Shutdown Signal | `0x1270` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Ready ON Status | `0x1270` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SMR Status | `0x1270` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Resolver Offset Complete Status | `0x1271` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Inverter High Current(GFINV) | `0x1271` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Inverter Shut Down Status | `0x1271` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Motor Resolver Offset Complete Status | `0x1272` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PTC Heater Activate Permission Status/Prohibition Signal(IDH) | `0x1272` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| DC/DC Converter Terminate Signal | `0x1272` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor Inverter High Current(MFINV) | `0x1272` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor Inverter Shut Down Status | `0x1272` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Inverter Input High Voltage(OVH) | `0x1273` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Input High Voltage(OVL) | `0x1273` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Input High Current(CFINV) | `0x1273` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Shut Down Status | `0x1273` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 Vr_CHK | `0x1DF9` | 0..15 | `raw * 0.0001` |  | Y |  |
| MG2 Vr_CHK | `0x1DF9` | 16..31 | `raw * 0.0001` |  | Y |  |
| IL Current Command Value | `0x1DF9` | 32..47 | `raw * 0.06103515625` | A | Y |  |
| Point Trouble Code1 | `0x1DF9` | 48..55 | `raw` |  | N | Only observed constant |
| Point Trouble Code2 | `0x1DF9` | 56..63 | `raw` |  | N | Only observed constant |
| Point Trouble Code3 | `0x1DF9` | 64..71 | `raw` |  | N | Only observed constant |
| MG1 Torque Estimate Value | `0x1DF9` | 72..87 | `raw * 0.125` | Nm | Y |  |
| MG2 Torque Estimate Value | `0x1DF9` | 88..103 | `raw * 0.125` | Nm | Y |  |
| MG2 Emergency Changeover Flag | `0x1DF9` | 104..107 | `raw` |  | N | Only observed constant |
| MG1 Emergency Changeover Flag | `0x1DF9` | 108..111 | `raw` |  | N | Only observed constant |
| MG2 Resolver Correlation Short Circuit Info | `0x1DF9` | 112..115 | `raw` |  | N | Only observed constant |
| MG1 Resolver Correlation Short Circuit Info | `0x1DF9` | 116..119 | `raw` |  | N | Only observed constant |
| OVH Overvoltage Instant Trouble Flag | `0x1DF9` | 120..120 | `raw` | 0 = Normal; 1 = Malfunction | N | Only observed constant |
| OVL Overvoltage  Instant Trouble Flag | `0x1DF9` | 121..121 | `raw` | 0 = Normal; 1 = Malfunction | N | Only observed constant |
| Error Communication Info | `0x1DF9` | 122..125 | `raw` |  | N | Only observed constant |
| OVH CHG Command Value | `0x1DF9` | 126..127 | `raw` |  | N | Only observed constant |
| MG2 Current Sensor Normal State | `0x1DF9` | 128..128 | `raw` | 0 = Not Normal; 1 = Normal | N | Only observed constant |
| MG2 Current Sensor Malfunction State | `0x1DF9` | 129..129 | `raw` | 0 = Not Abnormal; 1 = Abnormal | N | Only observed constant |
| MG1 Current Sensor Normal State | `0x1DF9` | 130..130 | `raw` | 0 = Not Normal; 1 = Normal | N | Only observed constant |
| MG1 Current Sensor  Malfunction State | `0x1DF9` | 131..131 | `raw` | 0 = Not Abnormal; 1 = Abnormal | N | Only observed constant |
| HV Received Data Decompress Start | `0x1DF9` | 132..135 | `raw` |  | N | Only observed constant |
| MG1 Torque Variation Req | `0x1DF9` | 136..136 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 Torque Limited Flag | `0x1DF9` | 137..137 | `raw` | 0 = OFF; 1 = ON | Y |  |
| MG1 Torque Limited Flag | `0x1DF9` | 138..138 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 Slip Judgmental Flag 2 | `0x1DF9` | 139..139 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 Slip Judgmental Flag 1 | `0x1DF9` | 140..140 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 Slip Judgmental Flag 2 | `0x1DF9` | 141..141 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 Slip Judgmental Flag 1 | `0x1DF9` | 142..142 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| +B Low Voltage Condition | `0x1DF9` | 143..143 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 Central Judgmental Flag (Low) | `0x1DF9` | 144..144 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 W-Phase Overcurrent Flag | `0x1DF9` | 145..145 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 W-Phase Overcurrent Flag | `0x1DF9` | 146..146 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 V-Phase Overcurrent Flag | `0x1DF9` | 147..147 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 V-Phase Overcurrent Flag | `0x1DF9` | 148..148 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 U-Phase Overcurrent Flag | `0x1DF9` | 149..149 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 U-Phase Overcurrent Flag | `0x1DF9` | 150..150 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 Torque Variation Req | `0x1DF9` | 151..151 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| BAAB | `0x1DF9` | 152..152 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Battery Power Instant Trouble Flag | `0x1DF9` | 153..153 | `raw` | 0 = Normal; 1 = Malfunction | Y |  |
| Pressure Duty Lower Limit Attainment Flag | `0x1DF9` | 154..154 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Pressure Duty Upper Limit Attainment Flag | `0x1DF9` | 155..155 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| High Loading Rate History Flag 2MG | `0x1DF9` | 156..156 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| High Loading Rate History Flag 1MG | `0x1DF9` | 157..157 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 Central Judgmental Flag | `0x1DF9` | 158..158 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 Central Judgmental Flag (High) | `0x1DF9` | 159..159 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Estimated Inverter Coolant Water Temperature | `0x1DF9` | 160..167 | `raw` | ℃ | Y |  |
| Pressure Feedforward duty | `0x1DF9` | 168..175 | `raw / 255` |  | Y |  |
| Communication State | `0x1DF9` | 176..183 | `raw` |  | Y |  |
| Voltage Practice Level Rate Numerator Value | `0x1E09` | 0..15 | `raw` |  | Y |  |
| Voltage Practice Level Rate Denominator Value | `0x1E09` | 16..31 | `raw` |  | Y |  |
| MIL | `0x1F01` | 0..0 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Number of Emission DTC | `0x1F01` | 1..7 | `raw` |  | N | Only observed constant |
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
| Calculate Load | `0x1F04` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Coolant Temperature | `0x1F05` | 0..7 | `raw - 40` | ℃ | Y |  |
| Engine Speed | `0x1F0C` | 0..15 | `raw * 0.25` | rpm | Y |  |
| Vehicle Speed | `0x1F0D` | 0..7 | `raw` | km/h | Y |  |
| Intake Air Temperature | `0x1F0F` | 0..7 | `raw - 40` | ℃ | Y |  |
| Throttle Position Sensor No.1 Voltage % | `0x1F11` | 0..7 | `raw * 20 / 51` | % | Y |  |
| OBD Requirements | `0x1F1C` | 0..7 | `raw` | 1 = OBD II (California ARB); 2 = OBD (US Federal EPA); 3 = OBD and OBD II; 4 = OBD I; 5 = Not OBD compliant; 6 = EOBD (Euro OBD); 7 = EOBD and OBD II; 8 = EOBD and OBD; 9 = EOBD, OBD and OBD II; 10 = JOBD (Japan OBD); 11 = JOBD and OBD II; 12 = JOBD and EOBD; 13 = JOBD, EOBD, and OBD II; 14 = EURO 4; 15 = EURO 5; 17 = Engine Manufacturer Diagnostics (EMD); 18 = Engine Manufacturer Diagnostics Enhanced (EMD+); 19 = Heavy Duty On-Board Diagnostics (Child/Partial); 20 = Heavy Duty On-Board Diagnostics; 21 = World Wide Harmonized OBD; 23 = Heavy Duty Euro OBD Stage I without NOx control; 24 = Heavy Duty Euro OBD Stage I with NOx control; 25 = Heavy Duty Euro OBD Stage II without NOx control; 26 = Heavy Duty Euro OBD Stage II with NOx control; 28 = Brazil OBD Phase 1; 29 = Brazil OBD Phase 2; 30 = Korean OBD; 31 = India OBD I; 32 = India OBD II; 33 = Heavy Duty Euro OBD Stage VI; 34 = OBD, OBD II and HD OBD; 35 = Brazil OBD Phase 3; 36 = Motorcycle, Euro OBD-I; 37 = Motorcycle, Euro OBD-II; 38 = Motorcycle, China OBD-I; 39 = Motorcycle, Taiwan OBD-I; 40 = Motorcycle, Japan OBD-I; 41 = China Nationwide Stage 6; 42 = Brazil OBD Diesel | N | Only observed constant |
| Engine Run Time | `0x1F1F` | 0..15 | `raw` | sec | Y |  |
| MIL ON Run Distance | `0x1F21` | 0..15 | `raw` | km | N | Only observed constant |
| Warmup Cycle Cleared DTC | `0x1F30` | 0..7 | `raw` |  | N | Only observed constant |
| Distance from DTC Cleared | `0x1F31` | 0..15 | `raw` | km | Y |  |
| Component Monitor Result (Current) | `0x1F41` | 9..9 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Fuel System Monitor Result (Current) | `0x1F41` | 10..10 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Misfire Monitor Result (Current) | `0x1F41` | 11..11 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
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
| EGR/VVT Monitor Result (Current) | `0x1F41` | 24..24 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| O2 Sensor Heater Monitor Result (Current) | `0x1F41` | 25..25 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor Monitor Result (Current) | `0x1F41` | 26..26 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Secondary Air Injection System Monitor Result (Current) | `0x1F41` | 28..28 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| EVAP Monitor Result (Current) | `0x1F41` | 29..29 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Heated Catalyst Monitor Result (Current) | `0x1F41` | 30..30 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Catalyst Monitor Result (Current) | `0x1F41` | 31..31 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Battery Voltage | `0x1F42` | 0..15 | `raw * 0.001` | V | Y |  |
| Ambient Temperature | `0x1F46` | 0..7 | `raw - 40` | ℃ | Y |  |
| Running Time from MIL ON | `0x1F4D` | 0..15 | `raw` | min | N | Only observed constant |
| Time after DTC Cleared | `0x1F4E` | 0..15 | `raw` | min | Y |  |
| Glow Indicator | `0x1F65` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Manual Trans Neutral Gear Status | `0x1F65` | 13..13 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Shift SW Status (N,P Range) | `0x1F65` | 14..14 | `raw` | 0 = ON; 1 = OFF | Y |  |
| Power Take Off (PTO) Status | `0x1F65` | 15..15 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Hybrid/EV Charging State | `0x1F9A` | 15..15 | `raw` | 0 = CSM or Non Support; 1 = CDM | N | Only observed constant |
| Hybrid/EV Battery System Voltage | `0x1F9A` | 16..31 | `raw * 0.015625` | V | Y |  |
| Hybrid/EV Battery System Current | `0x1F9A` | 32..47 | `raw * 0.1 - 3276.8` | A | Y |  |
