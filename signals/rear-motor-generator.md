# Rear Motor Generator

Request ID `0x705`, response ID `0x70D`. Use these as `<REQUEST ID>` and `<RESPONSE ID>` in [PROTOCOL.md](../PROTOCOL.md).

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `0x0103` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw` | km | Y |  |
| Motor Revolution | `0x120C` | 0..15 | `raw - 32768` | rpm | Y |  |
| Motor Torque | `0x120C` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Motor Control Mode | `0x120C` | 32..47 | `raw` |  | Y |  |
| Generator Revolution | `0x120D` | 0..15 | `raw - 32768` | rpm | Y |  |
| Generator Torque | `0x120D` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Generator Control Mode | `0x120D` | 32..47 | `raw` |  | Y |  |
| VL Voltage | `0x1217` | 0..15 | `raw * 0.1` | V | Y |  |
| VH Voltage | `0x1217` | 16..31 | `raw * 0.1` | V | Y |  |
| Boosting Converter PWM Duty | `0x1218` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Boosting Converter Control Mode | `0x1218` | 8..15 | `raw` |  | N | Only observed constant |
| Short Wave Highest Value | `0x121D` | 0..7 | `raw * 0.01953125` | V | Y |  |
| Fail Safe Mode | `0x121E` | 0..7 | `raw - 128` |  | N | Only observed constant |
| Accelerator Position | `0x121F` | 0..7 | `raw * 0.5` | % | Y |  |
| WIN Control Limit Power | `0x1221` | 0..15 | `raw * 0.01 - 327.68` | kW | Y |  |
| WOUT Control Limit Power | `0x1221` | 16..31 | `raw * 0.01 - 327.68` | kW | Y |  |
| Rear Motor Resolver After Offset | `0x1224` | 0..7 | `raw * 1.40625` | deg | Y |  |
| Rear Motor Resolver Offset Value | `0x1224` | 8..23 | `raw * 0.00762939453125 - 250` | deg | N | Only observed constant |
| Rear Motor Revolution | `0x1226` | 0..15 | `raw - 32768` | rpm | Y |  |
| Rear Motor Torque | `0x1226` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Rear Motor Control Mode | `0x1226` | 32..47 | `raw` |  | Y |  |
| Rear Motor Carrier Frequency | `0x1226` | 48..55 | `raw` |  | N | Only observed constant |
| Rear Motor Inverter Shut Down Signal | `0x1226` | 56..63 | `raw` |  | Y |  |
| Rear Motor Inverter Temperature | `0x122B` | 0..7 | `raw` | ℃ | Y |  |
| Rear Motor Temperature | `0x122C` | 0..7 | `raw - 40` | ℃ | Y |  |
| Rear Motor Inverter Temperature Duty | `0x1230` | 0..15 | `raw * 0.1` | % | Y |  |
| Rear Motor U Current (Lower Resolution) | `0x123C` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| Rear Motor V Current (Lower Resolution) | `0x123C` | 16..31 | `raw * 0.1 - 3276.8` | A | Y |  |
| Rear Motor W Current (Lower Resolution) | `0x123C` | 32..47 | `raw * 0.1 - 3276.8` | A | Y |  |
| Emergency Shutdown Signal | `0x1270` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Ready ON Status | `0x1270` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SMR Status | `0x1270` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Inverter High Current(GFINV) | `0x1271` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Inverter Shut Down Status | `0x1271` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Motor Inverter High Current(MFINV) | `0x1272` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor Inverter Shut Down Status | `0x1272` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Input High Voltage | `0x1273` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Input High Current(CFINV) | `0x1273` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Shut Down Status | `0x1273` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Motor Resolver Offset Complete Status | `0x1274` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Inverter Input High Voltage | `0x1274` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Inverter Input High Current (RFINV) | `0x1274` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Inverter Shut Down Status | `0x1274` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| IL Current Command Value | `0x1DFA` | 0..15 | `raw` |  | Y |  |
| Vr_CHK (Modulation Degree) | `0x1DFA` | 16..31 | `raw` |  | Y |  |
| Torque Distribution Ratio | `0x1DFA` | 32..47 | `raw` |  | Y |  |
| Develop Driving Torque (TPr) | `0x1DFA` | 48..63 | `raw * 0.125` | Nm | Y |  |
| IL Current Value | `0x1DFA` | 64..79 | `raw` |  | Y |  |
| Torque Estimate Value | `0x1DFA` | 80..95 | `raw * 0.125` | Nm | Y |  |
| Point Trouble Code1 | `0x1DFA` | 96..103 | `raw` |  | N | Only observed constant |
| Point Trouble Code2 | `0x1DFA` | 104..111 | `raw` |  | N | Only observed constant |
| Point Trouble Code3 | `0x1DFA` | 112..119 | `raw` |  | N | Only observed constant |
| Resolver Correlation Short Circuit Info | `0x1DFA` | 124..127 | `raw` |  | N | Only observed constant |
| +B Low Voltage Condition | `0x1DFA` | 128..128 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VL Reliability Info | `0x1DFA` | 129..129 | `raw` | 0 = Dependability; 1 = Not Dependability | N | Only observed constant |
| Slip Judgmental Flag 2 | `0x1DFA` | 130..130 | `raw` | 0 = Normal; 1 = Sudden Change | N | Only observed constant |
| Slip Judgmental Flag 1 | `0x1DFA` | 131..131 | `raw` | 0 = Normal; 1 = Sudden Change | N | Only observed constant |
| MG2 Slip Judgmental Flag 2 | `0x1DFA` | 132..132 | `raw` | 0 = Normal; 1 = Sudden Change | N | Only observed constant |
| MG2 Slip Judgmental Flag 1 | `0x1DFA` | 133..133 | `raw` | 0 = Normal; 1 = Sudden Change | N | Only observed constant |
| Torque Limited Flag | `0x1DFA` | 134..135 | `raw` | 0 = No Limit; 1 = Win Limit; 2 = Wout  Limit | N | Only observed constant |
| Inverter Temperature Soft Trimming Offset Value Abnormal Determination Flag | `0x1DFA` | 136..136 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| Current Sensor Normal State | `0x1DFA` | 137..137 | `raw` | 0 = Not Normal; 1 = Normal | N | Only observed constant |
| Current Sensor  Malfunction State | `0x1DFA` | 138..138 | `raw` | 0 = Not Abnormal; 1 = Abnormal | N | Only observed constant |
| VL-R Overvoltage Abnormal Status | `0x1DFA` | 139..139 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| Central Judgmental Flag | `0x1DFA` | 140..140 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| W-Phase Overcurrent Flag | `0x1DFA` | 141..141 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| V-Phase Overcurrent Flag | `0x1DFA` | 142..142 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| High Loading Rate History Flag | `0x1DFA` | 143..143 | `raw` | 0 = Not Failure; 1 = Failure | N | Only observed constant |
| Pressure Feedforward duty | `0x1DFA` | 144..151 | `raw / 255` |  | Y |  |
| Engine Speed | `0x1F0C` | 0..15 | `raw * 0.25` | rpm | Y |  |
| Vehicle Speed | `0x1F0D` | 0..7 | `raw` | km/h | Y |  |
| Engine Run Time | `0x1F1F` | 0..15 | `raw` | sec | Y |  |
| Warmup Cycle Cleared DTC | `0x1F30` | 0..7 | `raw` | Times | N | Only observed constant |
| Distance from DTC Cleared | `0x1F31` | 0..15 | `raw` | km | Y |  |
| Battery Voltage | `0x1F42` | 0..15 | `raw * 0.001` | V | Y |  |
| Ambient Temperature | `0x1F46` | 0..7 | `raw - 40` | ℃ | Y |  |
| Time after DTC Cleared | `0x1F4E` | 0..15 | `raw` | min | Y |  |
| Hybrid/EV Charging State | `0x1F9A` | 15..15 | `raw` | 0 = CSM or Non Support; 1 = CDM | N | Only observed constant |
| Hybrid/EV Battery System Voltage | `0x1F9A` | 16..31 | `raw * 0.015625` | V | Y |  |
| Hybrid/EV Battery System Current | `0x1F9A` | 32..47 | `raw * 0.1 - 3276.8` | A | Y |  |
