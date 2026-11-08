# Rear Motor Generator

Request ID `0x705`, response ID `0x70D`. Use these in [PROTOCOL.md](../PROTOCOL.md).

Open the session with `3E 00` (reply starts `7E 00`; a refusal is fine), then `10 01` (reply starts `50 01`), then `10 03` (reply starts `50 03`).

Read each signal with the request in the **Read** column.

| Signal | Read | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `22 01 02` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `22 01 02` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `22 01 02` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `22 01 03` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `22 01 03` | 8..31 | `raw` | km | Y |  |
| Motor Revolution | `22 12 0C` | 0..15 | `raw - 32768` | rpm | Y |  |
| Motor Torque | `22 12 0C` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Motor Control Mode | `22 12 0C` | 32..47 | `raw` |  | Y |  |
| Generator Revolution | `22 12 0D` | 0..15 | `raw - 32768` | rpm | Y |  |
| Generator Torque | `22 12 0D` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Generator Control Mode | `22 12 0D` | 32..47 | `raw` |  | Y |  |
| VL Voltage | `22 12 17` | 0..15 | `raw * 0.1` | V | Y |  |
| VH Voltage | `22 12 17` | 16..31 | `raw * 0.1` | V | Y |  |
| Boosting Converter PWM Duty | `22 12 18` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Boosting Converter Control Mode | `22 12 18` | 8..15 | `raw` |  | N | Only observed constant |
| Short Wave Highest Value | `22 12 1D` | 0..7 | `raw * 0.01953125` | V | Y |  |
| Fail Safe Mode | `22 12 1E` | 0..7 | `raw - 128` |  | N | Only observed constant |
| Accelerator Position | `22 12 1F` | 0..7 | `raw * 0.5` | % | Y |  |
| WIN Control Limit Power | `22 12 21` | 0..15 | `raw * 0.01 - 327.68` | kW | Y |  |
| WOUT Control Limit Power | `22 12 21` | 16..31 | `raw * 0.01 - 327.68` | kW | Y |  |
| Rear Motor Resolver After Offset | `22 12 24` | 0..7 | `raw * 1.40625` | deg | Y |  |
| Rear Motor Resolver Offset Value | `22 12 24` | 8..23 | `raw * 0.00762939453125 - 250` | deg | N | Only observed constant |
| Rear Motor Revolution | `22 12 26` | 0..15 | `raw - 32768` | rpm | Y |  |
| Rear Motor Torque | `22 12 26` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Rear Motor Control Mode | `22 12 26` | 32..47 | `raw` |  | Y |  |
| Rear Motor Carrier Frequency | `22 12 26` | 48..55 | `raw` |  | N | Only observed constant |
| Rear Motor Inverter Shut Down Signal | `22 12 26` | 56..63 | `raw` |  | Y |  |
| Rear Motor Inverter Temperature | `22 12 2B` | 0..7 | `raw` | ℃ | Y |  |
| Rear Motor Temperature | `22 12 2C` | 0..7 | `raw - 40` | ℃ | Y |  |
| Rear Motor Inverter Temperature Duty | `22 12 30` | 0..15 | `raw * 0.1` | % | Y |  |
| Rear Motor U Current (Lower Resolution) | `22 12 3C` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| Rear Motor V Current (Lower Resolution) | `22 12 3C` | 16..31 | `raw * 0.1 - 3276.8` | A | Y |  |
| Rear Motor W Current (Lower Resolution) | `22 12 3C` | 32..47 | `raw * 0.1 - 3276.8` | A | Y |  |
| Emergency Shutdown Signal | `22 12 70` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Ready ON Status | `22 12 70` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SMR Status | `22 12 70` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Inverter High Current(GFINV) | `22 12 71` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Inverter Shut Down Status | `22 12 71` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Motor Inverter High Current(MFINV) | `22 12 72` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor Inverter Shut Down Status | `22 12 72` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Input High Voltage | `22 12 73` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Input High Current(CFINV) | `22 12 73` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Shut Down Status | `22 12 73` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Motor Resolver Offset Complete Status | `22 12 74` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Inverter Input High Voltage | `22 12 74` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Inverter Input High Current (RFINV) | `22 12 74` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Inverter Shut Down Status | `22 12 74` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| IL Current Command Value | `22 1D FA` | 0..15 | `raw` |  | Y |  |
| Vr_CHK (Modulation Degree) | `22 1D FA` | 16..31 | `raw` |  | Y |  |
| Torque Distribution Ratio | `22 1D FA` | 32..47 | `raw` |  | Y |  |
| Develop Driving Torque (TPr) | `22 1D FA` | 48..63 | `raw * 0.125` | Nm | Y |  |
| IL Current Value | `22 1D FA` | 64..79 | `raw` |  | Y |  |
| Torque Estimate Value | `22 1D FA` | 80..95 | `raw * 0.125` | Nm | Y |  |
| Point Trouble Code1 | `22 1D FA` | 96..103 | `raw` |  | N | Only observed constant |
| Point Trouble Code2 | `22 1D FA` | 104..111 | `raw` |  | N | Only observed constant |
| Point Trouble Code3 | `22 1D FA` | 112..119 | `raw` |  | N | Only observed constant |
| Resolver Correlation Short Circuit Info | `22 1D FA` | 124..127 | `raw` |  | N | Only observed constant |
| +B Low Voltage Condition | `22 1D FA` | 128..128 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VL Reliability Info | `22 1D FA` | 129..129 | `raw` | 0 = Dependability; 1 = Not Dependability | N | Only observed constant |
| Slip Judgmental Flag 2 | `22 1D FA` | 130..130 | `raw` | 0 = Normal; 1 = Sudden Change | N | Only observed constant |
| Slip Judgmental Flag 1 | `22 1D FA` | 131..131 | `raw` | 0 = Normal; 1 = Sudden Change | N | Only observed constant |
| MG2 Slip Judgmental Flag 2 | `22 1D FA` | 132..132 | `raw` | 0 = Normal; 1 = Sudden Change | N | Only observed constant |
| MG2 Slip Judgmental Flag 1 | `22 1D FA` | 133..133 | `raw` | 0 = Normal; 1 = Sudden Change | N | Only observed constant |
| Torque Limited Flag | `22 1D FA` | 134..135 | `raw` | 0 = No Limit; 1 = Win Limit; 2 = Wout  Limit | N | Only observed constant |
| Inverter Temperature Soft Trimming Offset Value Abnormal Determination Flag | `22 1D FA` | 136..136 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| Current Sensor Normal State | `22 1D FA` | 137..137 | `raw` | 0 = Not Normal; 1 = Normal | N | Only observed constant |
| Current Sensor  Malfunction State | `22 1D FA` | 138..138 | `raw` | 0 = Not Abnormal; 1 = Abnormal | N | Only observed constant |
| VL-R Overvoltage Abnormal Status | `22 1D FA` | 139..139 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| Central Judgmental Flag | `22 1D FA` | 140..140 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| W-Phase Overcurrent Flag | `22 1D FA` | 141..141 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| V-Phase Overcurrent Flag | `22 1D FA` | 142..142 | `raw` | 0 = Normal; 1 = Abnormal | N | Only observed constant |
| High Loading Rate History Flag | `22 1D FA` | 143..143 | `raw` | 0 = Not Failure; 1 = Failure | N | Only observed constant |
| Pressure Feedforward duty | `22 1D FA` | 144..151 | `raw / 255` |  | Y |  |
| Engine Speed | `22 1F 0C` | 0..15 | `raw * 0.25` | rpm | Y |  |
| Vehicle Speed | `22 1F 0D` | 0..7 | `raw` | km/h | Y |  |
| Engine Run Time | `22 1F 1F` | 0..15 | `raw` | sec | Y |  |
| Warmup Cycle Cleared DTC | `22 1F 30` | 0..7 | `raw` | Times | N | Only observed constant |
| Distance from DTC Cleared | `22 1F 31` | 0..15 | `raw` | km | Y |  |
| Battery Voltage | `22 1F 42` | 0..15 | `raw * 0.001` | V | Y |  |
| Ambient Temperature | `22 1F 46` | 0..7 | `raw - 40` | ℃ | Y |  |
| Time after DTC Cleared | `22 1F 4E` | 0..15 | `raw` | min | Y |  |
| Hybrid/EV Charging State | `22 1F 9A` | 15..15 | `raw` | 0 = CSM or Non Support; 1 = CDM | N | Only observed constant |
| Hybrid/EV Battery System Voltage | `22 1F 9A` | 16..31 | `raw * 0.015625` | V | Y |  |
| Hybrid/EV Battery System Current | `22 1F 9A` | 32..47 | `raw * 0.1 - 3276.8` | A | Y |  |
