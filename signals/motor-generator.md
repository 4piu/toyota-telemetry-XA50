# Motor Generator

Request ID `0x724`, response ID `0x72C`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw * 625000 / 388357` | km | Y |  |
| Airbag Collision Detection History | `22 10 38` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Temperature(Upper) | `22 12 01` | 0..7 | `raw` | ℃ | Y |  |
| Boosting Converter Temperature 1(Lower) | `22 12 01` | 8..15 | `raw` | ℃ | Y |  |
| Boosting Converter Temperature Duty(Upper) | `22 12 03` | 0..15 | `raw * 0.1` | % | Y |  |
| Motor Inverter Temperature Duty | `22 12 04` | 0..15 | `raw * 0.1` | % | Y |  |
| Motor Inverter Temperature | `22 12 05` | 0..7 | `raw` | ℃ | Y |  |
| Motor U Current AD Value(Lower Resolution) | `22 12 06` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Motor V Current AD Value(Lower Resolution) | `22 12 06` | 16..31 | `raw * 0.0000762939453125` | V | Y |  |
| Motor W Current AD Value(Lower Resolution) | `22 12 06` | 32..47 | `raw * 0.0000762939453125` | V | Y |  |
| Motor V Current AD Value(Higher Resolution) | `22 12 06` | 48..63 | `raw * 0.0000762939453125` | V | Y |  |
| Motor W Current AD Value(Higher Resolution) | `22 12 06` | 64..79 | `raw * 0.0000762939453125` | V | Y |  |
| Motor U Current(Lower Resolution) | `22 12 07` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| Motor V Current(Lower Resolution) | `22 12 07` | 16..31 | `raw * 0.1 - 3276.8` | A | Y |  |
| Motor W Current(Lower Resolution) | `22 12 07` | 32..47 | `raw * 0.1 - 3276.8` | A | Y |  |
| Generator U Current(Lower Resolution) | `22 12 08` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| Generator V Current(Lower Resolution) | `22 12 08` | 16..31 | `raw * 0.1 - 3276.8` | A | Y |  |
| Generator W Current(Lower Resolution) | `22 12 08` | 32..47 | `raw * 0.1 - 3276.8` | A | Y |  |
| Motor ECU Power Supply (For 31V) | `22 12 09` | 0..7 | `raw * 0.01953125` | V | N | Regulated supply; constant by design |
| Motor ECU Power Supply (For AD0) | `22 12 09` | 8..15 | `raw * 0.01953125` | V | Y |  |
| Motor ECU Power Supply (For 2.5V) | `22 12 09` | 16..23 | `raw * 0.01953125` | V | Y |  |
| Motor ECU Power Supply (For 2.5V Sub) | `22 12 0A` | 0..7 | `raw * 0.01953125` | V | Y |  |
| DC/DC Converter Operation State | `22 12 0B` | 0..7 | `raw * 0.01953125` | V | Y |  |
| DC/DC Converter Output Signal | `22 12 0B` | 8..15 | `raw * 0.01953125` | V | Y |  |
| DC/DC Converter Output Signal Duty | `22 12 0B` | 16..23 | `raw * 0.5` | % | Y |  |
| Motor Revolution | `22 12 0C` | 0..15 | `raw - 32768` | rpm | Y |  |
| Motor Torque | `22 12 0C` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Motor Control Mode | `22 12 0C` | 32..47 | `raw` |  | Y |  |
| Motor Carrier Frequency | `22 12 0C` | 48..55 | `raw` |  | N | Only observed constant |
| Motor Inverter Shut Down Signal | `22 12 0C` | 56..63 | `raw` |  | N | Only observed constant |
| Generator Revolution | `22 12 0D` | 0..15 | `raw - 32768` | rpm | Y |  |
| Generator Torque | `22 12 0D` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Generator Control Mode | `22 12 0D` | 32..47 | `raw` |  | Y |  |
| Generator Carrier Frequency | `22 12 0D` | 48..55 | `raw` |  | Y |  |
| Generator Inverter Shut Down Signal | `22 12 0D` | 56..63 | `raw` |  | Y |  |
| Inverter Coolant Temperature AD Value | `22 12 0E` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Inverter Coolant Temperature | `22 12 0F` | 0..7 | `raw - 40` | ℃ | Y |  |
| Inverter Input Current | `22 12 10` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| Inverter Water Pump Duty | `22 12 11` | 0..7 | `raw` | % | Y |  |
| Inverter Water Pump Revolution | `22 12 11` | 8..23 | `raw` | rpm | Y |  |
| Collinear Graphic Engine Revolution | `22 12 13` | 0..15 | `raw` | rpm | Y |  |
| Motor Resolver After Offset | `22 12 14` | 0..7 | `raw * 1.40625` | deg | Y |  |
| Motor Resolver Offset Value | `22 12 14` | 8..23 | `raw * 0.00762939453125 - 250` | deg | N | Only observed constant |
| Generator Resolver After Offset | `22 12 15` | 0..7 | `raw * 1.40625` | deg | Y |  |
| Generator Resolver Offset Value | `22 12 15` | 8..23 | `raw * 0.00762939453125 - 250` | deg | N | Only observed constant |
| Motor Resolver Supply Voltage | `22 12 16` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| VL Voltage | `22 12 17` | 0..15 | `raw * 0.1` | V | Y |  |
| VH Voltage | `22 12 17` | 16..31 | `raw * 0.1` | V | Y |  |
| Boosting Converter PWM Duty | `22 12 18` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Boosting Converter Control Mode | `22 12 18` | 8..15 | `raw` |  | N | Only observed constant |
| Boosting Converter Carrier Frequency | `22 12 18` | 16..23 | `raw` | 0 = HI; 1 = LO | N | Only observed constant |
| Boosting Converter Shut Down Signal | `22 12 18` | 24..31 | `raw` |  | N | Only observed constant |
| Generate Request Torque | `22 12 19` | 0..15 | `raw * 0.125 - 4096` | Nm | Y |  |
| Shift Position | `22 12 1A` | 0..7 | `raw` | 0 = P; 2 = R; 4 = N; 6 = D; 8 = B | Y |  |
| Motor Temperature | `22 12 1B` | 0..7 | `raw - 40` | ℃ | Y |  |
| Generator Temperature | `22 12 1C` | 0..7 | `raw - 40` | ℃ | Y |  |
| Short Wave Highest Value | `22 12 1D` | 0..7 | `raw * 0.01953125` | V | Y |  |
| Fail Safe Mode | `22 12 1E` | 0..7 | `raw - 128` |  | N | Only observed constant |
| Accelerator Position | `22 12 1F` | 0..7 | `raw * 0.5` | % | Y |  |
| WIN Control Limit Power | `22 12 21` | 0..15 | `raw * 0.01 - 327.68` | kW | Y |  |
| WOUT Control Limit Power | `22 12 21` | 16..31 | `raw * 0.01 - 327.68` | kW | Y |  |
| Engine Control Mode | `22 12 22` | 0..7 | `raw` | 0 = Stop; 1 = Stop Process; 2 = Startup Process; 3 = Running | Y |  |
| MG1 Inverter Estimate Temperature (After IG ON) | `22 12 27` | 0..7 | `raw` | ℃ | Y |  |
| MG1 Inverter Estimate Temperature (Max Value) | `22 12 27` | 8..15 | `raw` | ℃ | N | Only observed constant |
| MG2 Inverter Estimate Temperature (After IG ON) | `22 12 28` | 0..7 | `raw` | ℃ | Y |  |
| MG2 Inverter Estimate Temperature (Max Value) | `22 12 28` | 8..15 | `raw` | ℃ | N | Only observed constant |
| Pressure Converter Temperature (After IG ON) | `22 12 2A` | 0..7 | `raw` | ℃ | Y |  |
| Pressure Converter Temperature (Max Value) | `22 12 2A` | 8..15 | `raw` | ℃ | N | Only observed constant |
| Battery Discharge Control Circuit Monitor Voltage | `22 12 3B` | 0..7 | `raw * 0.01953125` | V | Y |  |
| Battery Discharge Control Circuit Power Switch | `22 12 3B` | 8..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Motor Generate Request Torque | `22 12 41` | 0..15 | `raw * 0.125 - 4096` | Nm | Y |  |
| Generator Inverter Temperature | `22 12 4F` | 0..7 | `raw` | ℃ | Y |  |
| Emergency Shutdown Signal | `22 12 70` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Ready ON Status | `22 12 70` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SMR Status | `22 12 70` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Resolver Offset Complete Status | `22 12 71` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Inverter High Current(GFINV) | `22 12 71` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Inverter Shut Down Status | `22 12 71` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Motor Resolver Offset Complete Status | `22 12 72` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| PTC Heater Activate Permission Status/Prohibition Signal(IDH) | `22 12 72` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| DC/DC Converter Terminate Signal | `22 12 72` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor Inverter High Current(MFINV) | `22 12 72` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor Inverter Shut Down Status | `22 12 72` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Inverter Input High Voltage(OVH) | `22 12 73` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Input High Voltage(OVL) | `22 12 73` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Input High Current(CFINV) | `22 12 73` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Shut Down Status | `22 12 73` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 Vr_CHK | `22 1D F9` | 0..15 | `raw * 0.0001` |  | Y |  |
| MG2 Vr_CHK | `22 1D F9` | 16..31 | `raw * 0.0001` |  | Y |  |
| IL Current Command Value | `22 1D F9` | 32..47 | `raw * 0.06103515625` | A | Y |  |
| Point Trouble Code1 | `22 1D F9` | 48..55 | `raw` |  | N | Only observed constant |
| Point Trouble Code2 | `22 1D F9` | 56..63 | `raw` |  | N | Only observed constant |
| Point Trouble Code3 | `22 1D F9` | 64..71 | `raw` |  | N | Only observed constant |
| MG1 Torque Estimate Value | `22 1D F9` | 72..87 | `raw * 0.125` | Nm | Y |  |
| MG2 Torque Estimate Value | `22 1D F9` | 88..103 | `raw * 0.125` | Nm | Y |  |
| MG2 Emergency Changeover Flag | `22 1D F9` | 104..107 | `raw` |  | N | Only observed constant |
| MG1 Emergency Changeover Flag | `22 1D F9` | 108..111 | `raw` |  | N | Only observed constant |
| MG2 Resolver Correlation Short Circuit Info | `22 1D F9` | 112..115 | `raw` |  | N | Only observed constant |
| MG1 Resolver Correlation Short Circuit Info | `22 1D F9` | 116..119 | `raw` |  | N | Only observed constant |
| OVH Overvoltage Instant Trouble Flag | `22 1D F9` | 120..120 | `raw` | 0 = Normal; 1 = Malfunction | N | Only observed constant |
| OVL Overvoltage  Instant Trouble Flag | `22 1D F9` | 121..121 | `raw` | 0 = Normal; 1 = Malfunction | N | Only observed constant |
| Error Communication Info | `22 1D F9` | 122..125 | `raw` |  | N | Only observed constant |
| OVH CHG Command Value | `22 1D F9` | 126..127 | `raw` |  | N | Only observed constant |
| MG2 Current Sensor Normal State | `22 1D F9` | 128..128 | `raw` | 0 = Not Normal; 1 = Normal | N | Only observed constant |
| MG2 Current Sensor Malfunction State | `22 1D F9` | 129..129 | `raw` | 0 = Not Abnormal; 1 = Abnormal | N | Only observed constant |
| MG1 Current Sensor Normal State | `22 1D F9` | 130..130 | `raw` | 0 = Not Normal; 1 = Normal | N | Only observed constant |
| MG1 Current Sensor  Malfunction State | `22 1D F9` | 131..131 | `raw` | 0 = Not Abnormal; 1 = Abnormal | N | Only observed constant |
| HV Received Data Decompress Start | `22 1D F9` | 132..135 | `raw` |  | N | Only observed constant |
| MG1 Torque Variation Req | `22 1D F9` | 136..136 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 Torque Limited Flag | `22 1D F9` | 137..137 | `raw` | 0 = OFF; 1 = ON | Y |  |
| MG1 Torque Limited Flag | `22 1D F9` | 138..138 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 Slip Judgmental Flag 2 | `22 1D F9` | 139..139 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 Slip Judgmental Flag 1 | `22 1D F9` | 140..140 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 Slip Judgmental Flag 2 | `22 1D F9` | 141..141 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 Slip Judgmental Flag 1 | `22 1D F9` | 142..142 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| +B Low Voltage Condition | `22 1D F9` | 143..143 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 Central Judgmental Flag (Low) | `22 1D F9` | 144..144 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 W-Phase Overcurrent Flag | `22 1D F9` | 145..145 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 W-Phase Overcurrent Flag | `22 1D F9` | 146..146 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 V-Phase Overcurrent Flag | `22 1D F9` | 147..147 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 V-Phase Overcurrent Flag | `22 1D F9` | 148..148 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 U-Phase Overcurrent Flag | `22 1D F9` | 149..149 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 U-Phase Overcurrent Flag | `22 1D F9` | 150..150 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 Torque Variation Req | `22 1D F9` | 151..151 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| BAAB | `22 1D F9` | 152..152 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Battery Power Instant Trouble Flag | `22 1D F9` | 153..153 | `raw` | 0 = Normal; 1 = Malfunction | Y |  |
| Pressure Duty Lower Limit Attainment Flag | `22 1D F9` | 154..154 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Pressure Duty Upper Limit Attainment Flag | `22 1D F9` | 155..155 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| High Loading Rate History Flag 2MG | `22 1D F9` | 156..156 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| High Loading Rate History Flag 1MG | `22 1D F9` | 157..157 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG2 Central Judgmental Flag | `22 1D F9` | 158..158 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MG1 Central Judgmental Flag (High) | `22 1D F9` | 159..159 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Estimated Inverter Coolant Water Temperature | `22 1D F9` | 160..167 | `raw` | ℃ | Y |  |
| Pressure Feedforward duty | `22 1D F9` | 168..175 | `raw / 255` |  | Y |  |
| Communication State | `22 1D F9` | 176..183 | `raw` |  | Y |  |
| Voltage Practice Level Rate Numerator Value | `22 1E 09` | 0..15 | `raw` |  | Y |  |
| Voltage Practice Level Rate Denominator Value | `22 1E 09` | 16..31 | `raw` |  | Y |  |
| MIL | `22 1F 01` | 0..0 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Number of Emission DTC | `22 1F 01` | 1..7 | `raw` |  | N | Only observed constant |
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
| Calculate Load | `22 1F 04` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Coolant Temperature | `22 1F 05` | 0..7 | `raw - 40` | ℃ | Y |  |
| Engine Speed | `22 1F 0C` | 0..15 | `raw * 0.25` | rpm | Y |  |
| Vehicle Speed | `22 1F 0D` | 0..7 | `raw` | km/h | Y |  |
| Intake Air Temperature | `22 1F 0F` | 0..7 | `raw - 40` | ℃ | Y |  |
| Throttle Position Sensor No.1 Voltage % | `22 1F 11` | 0..7 | `raw * 20 / 51` | % | Y |  |
| OBD Requirements | `22 1F 1C` | 0..7 | `raw` | 1 = OBD II (California ARB); 2 = OBD (US Federal EPA); 3 = OBD and OBD II; 4 = OBD I; 5 = Not OBD compliant; 6 = EOBD (Euro OBD); 7 = EOBD and OBD II; 8 = EOBD and OBD; 9 = EOBD, OBD and OBD II; 10 = JOBD (Japan OBD); 11 = JOBD and OBD II; 12 = JOBD and EOBD; 13 = JOBD, EOBD, and OBD II; 14 = EURO 4; 15 = EURO 5; 17 = Engine Manufacturer Diagnostics (EMD); 18 = Engine Manufacturer Diagnostics Enhanced (EMD+); 19 = Heavy Duty On-Board Diagnostics (Child/Partial); 20 = Heavy Duty On-Board Diagnostics; 21 = World Wide Harmonized OBD; 23 = Heavy Duty Euro OBD Stage I without NOx control; 24 = Heavy Duty Euro OBD Stage I with NOx control; 25 = Heavy Duty Euro OBD Stage II without NOx control; 26 = Heavy Duty Euro OBD Stage II with NOx control; 28 = Brazil OBD Phase 1; 29 = Brazil OBD Phase 2; 30 = Korean OBD; 31 = India OBD I; 32 = India OBD II; 33 = Heavy Duty Euro OBD Stage VI; 34 = OBD, OBD II and HD OBD; 35 = Brazil OBD Phase 3; 36 = Motorcycle, Euro OBD-I; 37 = Motorcycle, Euro OBD-II; 38 = Motorcycle, China OBD-I; 39 = Motorcycle, Taiwan OBD-I; 40 = Motorcycle, Japan OBD-I; 41 = China Nationwide Stage 6; 42 = Brazil OBD Diesel | N | Only observed constant |
| Engine Run Time | `22 1F 1F` | 0..15 | `raw` | sec | Y |  |
| MIL ON Run Distance | `22 1F 21` | 0..15 | `raw` | km | N | Only observed constant |
| Warmup Cycle Cleared DTC | `22 1F 30` | 0..7 | `raw` |  | N | Only observed constant |
| Distance from DTC Cleared | `22 1F 31` | 0..15 | `raw` | km | Y |  |
| Component Monitor Result (Current) | `22 1F 41` | 9..9 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Fuel System Monitor Result (Current) | `22 1F 41` | 10..10 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Misfire Monitor Result (Current) | `22 1F 41` | 11..11 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
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
| EGR/VVT Monitor Result (Current) | `22 1F 41` | 24..24 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| O2 Sensor Heater Monitor Result (Current) | `22 1F 41` | 25..25 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| A/F (O2) Sensor Monitor Result (Current) | `22 1F 41` | 26..26 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Secondary Air Injection System Monitor Result (Current) | `22 1F 41` | 28..28 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| EVAP Monitor Result (Current) | `22 1F 41` | 29..29 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Heated Catalyst Monitor Result (Current) | `22 1F 41` | 30..30 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Catalyst Monitor Result (Current) | `22 1F 41` | 31..31 | `raw` | 0 = Compl; 1 = Incmpl | N | Only observed constant |
| Battery Voltage | `22 1F 42` | 0..15 | `raw * 0.001` | V | Y |  |
| Ambient Temperature | `22 1F 46` | 0..7 | `raw - 40` | ℃ | Y |  |
| Running Time from MIL ON | `22 1F 4D` | 0..15 | `raw` | min | N | Only observed constant |
| Time after DTC Cleared | `22 1F 4E` | 0..15 | `raw` | min | Y |  |
| Glow Indicator | `22 1F 65` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Manual Trans Neutral Gear Status | `22 1F 65` | 13..13 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Shift SW Status (N,P Range) | `22 1F 65` | 14..14 | `raw` | 0 = ON; 1 = OFF | Y |  |
| Power Take Off (PTO) Status | `22 1F 65` | 15..15 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Hybrid/EV Charging State | `22 1F 9A` | 15..15 | `raw` | 0 = CSM or Non Support; 1 = CDM | N | Only observed constant |
| Hybrid/EV Battery System Voltage | `22 1F 9A` | 16..31 | `raw * 0.015625` | V | Y |  |
| Hybrid/EV Battery System Current | `22 1F 9A` | 32..47 | `raw * 0.1 - 3276.8` | A | Y |  |
