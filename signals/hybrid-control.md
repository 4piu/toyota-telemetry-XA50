# Hybrid Control

Request ID `0x7D2`, response ID `0x7DA`. Use these as `<REQUEST ID>` and `<RESPONSE ID>` in [PROTOCOL.md](../PROTOCOL.md).

This address answers for Hybrid Control (325 signals), Cruise Control (43 signals), EV (26 signals).

## Hybrid Control

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | N | Only observed constant |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled - Unit | `0x0103` | 0..7 | `raw` | 1 = km; 2 = mile | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw` | km | Y |  |
| Absolute Value Time (Year) | `0x0109` | 0..7 | `raw` | Year | N | Only observed constant |
| Absolute Value Time (Month) | `0x0109` | 8..15 | `raw` | Month | N | Only observed constant |
| Absolute Value Time (Day) | `0x0109` | 16..23 | `raw` | Day | N | Only observed constant |
| Absolute Value Time (Hour) | `0x0109` | 24..31 | `raw` | Hour | N | Only observed constant |
| Absolute Value Time (Minute) | `0x0109` | 32..39 | `raw` | Minute | Y |  |
| Absolute Value Time (Second) | `0x0109` | 40..47 | `raw` | Second | Y |  |
| BATT Voltage | `0x1002` | 0..15 | `raw * 0.001220703125` | V | Y |  |
| IG ON Duration Time | `0x1007` | 0..31 | `raw * 8.192` | ms | Y |  |
| IG OFF Duration Time | `0x1007` | 32..63 | `raw * 8.192` | ms | N | Only observed constant |
| Rock and Dirt Switch History | `0x1010` | 73..73 | `raw` | 0 = OFF; 1 = ON | N | Not read yet |
| Mud and Sand Switch History | `0x1010` | 74..74 | `raw` | 0 = OFF; 1 = ON | N | Not read yet |
| Interlock Switch (DC/DC Converter) | `0x1031` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Inter Lock Switch (MG) | `0x1031` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Inter Lock Switch | `0x1031` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Motor Emergency Shutdown (Sub CPU) | `0x1032` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Motor Emergency Shutdown (Main CPU) | `0x1032` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor Emergency Shutdown (Sub CPU) | `0x1032` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor Emergency Shutdown (Main CPU) | `0x1032` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Inverter Emergency Shutdown (Sub CPU) | `0x1032` | 14..14 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Inverter Emergency Shutdown (Main CPU) | `0x1032` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SMRG Status | `0x1033` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SMRG Control Status | `0x1033` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SMRB Status | `0x1033` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SMRB Control Status | `0x1033` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SMRP Status | `0x1033` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| SMRP Control Status | `0x1033` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Back Up Light Relay | `0x1035` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Inverter Water Pump Duty Ratio | `0x1039` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| Inverter Water Pump Revolution | `0x1039` | 16..31 | `raw` | rpm | Y |  |
| Generator Temperature Sensor Voltage | `0x103A` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Generator Temperature | `0x103A` | 16..23 | `raw - 40` | ℃ | Y |  |
| Generator Temperature just after IG ON | `0x103A` | 24..31 | `raw - 40` | ℃ | N | Snapshot taken at ignition on; fixed during a drive |
| Generator Maximum Temperature | `0x103A` | 32..39 | `raw - 40` | ℃ | N | Running maximum; rises only on a new peak |
| Motor Temperature Sensor Voltage | `0x103B` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Motor Temperature | `0x103B` | 16..23 | `raw - 40` | ℃ | Y |  |
| Motor Temperature just after IG ON | `0x103B` | 24..31 | `raw - 40` | ℃ | N | Snapshot taken at ignition on; fixed during a drive |
| Motor Maximum Temperature | `0x103B` | 32..39 | `raw - 40` | ℃ | N | Running maximum; rises only on a new peak |
| Rear Motor Temperature Sensor Voltage | `0x103C` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Rear Motor Temperature | `0x103C` | 16..23 | `raw - 40` | ℃ | Y |  |
| Rear Motor Temperature just after IG ON | `0x103C` | 24..31 | `raw - 40` | ℃ | N | Snapshot taken at ignition on; fixed during a drive |
| Rear Motor Maximum Temperature | `0x103C` | 32..39 | `raw - 40` | ℃ | N | Running maximum; rises only on a new peak |
| Transaxle Oil Temperature Sensor Voltage | `0x1040` | 0..15 | `raw * 0.0000762939453125` | V | Y |  |
| Transaxle Oil Temperature | `0x1040` | 16..23 | `raw - 40` | ℃ | Y |  |
| Brake Cancel Switch | `0x1043` | 14..14 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Stop Light Switch | `0x1043` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| IGB Keeping Status | `0x1044` | 8..8 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| +B3M Signal Status | `0x1044` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| MRL2 Signal Status | `0x1044` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| ACC Signal | `0x1044` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IG Switch (CAN) | `0x1044` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IG Switch (LIN) | `0x1044` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IGB Signal Status | `0x1044` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| IG2 Signal Status | `0x1044` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| EV Mode | `0x1045` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| EV Mode Switch | `0x1045` | 15..15 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Starter Switch Signal | `0x1046` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| M/S/B Shift Position Signal | `0x104B` | 13..13 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Sports Shift DOWN Signal | `0x104B` | 14..14 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Sports Shift UP Signal | `0x104B` | 15..15 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Shift Position Sensor (PNB) | `0x104C` | 9..9 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Shift Position Sensor (PR) | `0x104C` | 10..10 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Shift Position Sensor (DB2) | `0x104C` | 11..11 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Shift Position Sensor (DB1) | `0x104C` | 12..12 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Shift Position Sensor (N) | `0x104C` | 13..13 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Shift Position Sensor (R) | `0x104C` | 14..14 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Shift Position Sensor (P) | `0x104C` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Sport Mode Switch | `0x104F` | 15..15 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Normal Mode Switch | `0x1050` | 15..15 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Shift Lock Release Request | `0x1052` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Auto LSD Switch | `0x1057` | 15..15 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Accelerator Position | `0x1060` | 0..7 | `raw * 0.5` | % | Y |  |
| Shift Position | `0x1061` | 0..7 | `raw` | 0 = P; 2 = R; 4 = N; 6 = D; 8 = B | Y |  |
| Shift Position (Meter) | `0x1061` | 8..15 | `raw` | 0 = Not Displayed; 1 = P; 2 = R; 4 = N; 8 = D; 16 = B | Y |  |
| Sports Shift Position | `0x1062` | 0..7 | `raw` |  | N | Only observed constant |
| Engine Mode | `0x1063` | 0..7 | `raw` | 0 = Stop; 1 = Stop Process; 2 = Startup Process; 3 = Running | N | Only observed constant |
| Target Engine Revolution | `0x1064` | 0..15 | `raw` | rpm | Y |  |
| Target Engine Power | `0x1065` | 0..15 | `raw * 10` | W | Y |  |
| Execute Engine Power | `0x1065` | 16..31 | `raw * 10` | W | Y |  |
| Generate Torque (Request from DSS) | `0x1066` | 0..15 | `raw * 0.125 - 4096` | Nm | Y |  |
| DSS Control Status | `0x1066` | 16..23 | `raw` | 0 = Not Control; 1 = Available; 3 = Unavailable; 7 = Disable | N | Only observed constant |
| Master Cylinder Control Torque | `0x1067` | 0..15 | `raw * 0.125 - 4096` | Nm | Y |  |
| Rear Motor Torque Ratio | `0x1068` | 0..15 | `raw * 0.0009765625 - 32` | Times | Y |  |
| Request Motor Regenerative Brake Torque | `0x1069` | 0..15 | `raw * 0.125 - 4096` | Nm | Y |  |
| Motor Regenerate Brake Execution Torque | `0x1069` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Request Rear Motor Regenerative Brake Torque | `0x106A` | 0..15 | `raw * 0.125 - 4096` | Nm | N | Stayed 0 in Eco mode on dry roads; expected when the rear motor drives or regenerates |
| Rear Motor Regenerate Brake Execution Torque | `0x106A` | 16..31 | `raw * 0.125 - 4096` | Nm | N | Stayed 0 in Eco mode on dry roads; expected when the rear motor drives or regenerates |
| HV/EV Activate Condition | `0x106B` | 0..7 | `raw` | 0 = Normal; 1 = Remote Air Control System; 2 = Remote | N | Only observed constant |
| Hybrid/EV Battery SOC of Immediately after IG ON | `0x106C` | 0..7 | `raw * 0.5` | % | N | Snapshot taken at ignition on; fixed during a drive |
| Hybrid/EV Battery Maximum SOC | `0x106C` | 8..15 | `raw * 0.5` | % | Y |  |
| Hybrid/EV Battery Minimum SOC | `0x106C` | 16..23 | `raw * 0.5` | % | N | Running minimum; falls only on a new low |
| Crank Position | `0x106D` | 0..7 | `raw - 128` | deg(CA) | Y |  |
| A/C Consumption Power | `0x106E` | 0..7 | `raw * 0.05` | kW | Y |  |
| Primary Driving Force Adjustment Result | `0x106F` | 0..7 | `raw` | 0 = Accelerator; 2 = DSS | N | Only observed constant |
| Engine Start Request (A/C) | `0x1070` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Engine Start Request (Engine Warm-up) | `0x1070` | 11..11 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Lack of Fuel | `0x1070` | 12..12 | `raw` | 0 = ON; 1 = OFF | Y |  |
| Engine Idling Request | `0x1070` | 13..13 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Engine Stop F/C Status | `0x1070` | 14..14 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Engine Stop Request | `0x1070` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Airbag Status (Collision) (CAN) | `0x1071` | 0..7 | `raw` | 0 = Normal; 1 = Collision from Back; 2 = Airbag Circuit Abnormal; 3 = Safing; 5 = Collision from Front or Side | N | Only observed constant |
| Insulation Resistance Division Check Completion using Rear Motor Inv | `0x1072` | 8..8 | `raw` | 0 = Not Complete; 1 = Complete | N | Only observed constant |
| Insulation Resistance Division Check Completion using MG Inv | `0x1072` | 9..9 | `raw` | 0 = Not Complete; 1 = Complete | N | Only observed constant |
| Insulation Resistance Division Check Completion using A/C Inv | `0x1072` | 10..10 | `raw` | 0 = Not Complete; 1 = Complete | N | Only observed constant |
| Insulation Resistance Division Check Completion using SMR | `0x1072` | 11..11 | `raw` | 0 = Not Complete; 1 = Complete | N | Only observed constant |
| Short Wave Highest Value Availability just after Rear Motor Inv On/Off | `0x1072` | 12..12 | `raw` | 0 = Yes; 1 = No | Y |  |
| Short Wave Highest Value Availability just after MG Inv On/Off | `0x1072` | 13..13 | `raw` | 0 = Yes; 1 = No | N | Only observed constant |
| Short Wave Highest Value Availability just after A/C Inv On/Off | `0x1072` | 14..14 | `raw` | 0 = Yes; 1 = No | Y |  |
| Short Wave Highest Value Availability just after SMR On/Off | `0x1072` | 15..15 | `raw` | 0 = Yes; 1 = No | N | Only observed constant |
| FR Wheel Speed | `0x1075` | 0..15 | `raw * 0.01 - 327.68` | km/h | Y |  |
| FL Wheel Speed | `0x1075` | 16..31 | `raw * 0.01 - 327.68` | km/h | Y |  |
| RR Wheel Speed | `0x1075` | 32..47 | `raw * 0.01 - 327.68` | km/h | Y |  |
| RL Wheel Speed | `0x1075` | 48..63 | `raw * 0.01 - 327.68` | km/h | Y |  |
| Rear MG Activate Condition | `0x1076` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Engine Start Request (Hybrid/EV Battery Charging) | `0x1076` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Accelerator Pedal Status | `0x1076` | 13..13 | `raw` | 0 = OFF; 1 = ON | Y |  |
| MG Activate Condition | `0x1076` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Ready Signal | `0x1076` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Permit Start by Immobiliser | `0x1077` | 0..7 | `raw` | 0 = No Judgment; 1 = OK; 2 = NG; 128 = No Judgment; 129 = OK; 130 = NG | N | Only observed constant |
| Request Torque for Rear Side | `0x1078` | 0..15 | `raw - 32768` | Nm | N | Stayed 0 in Eco mode on dry roads; expected when the rear motor drives or regenerates |
| Hybrid/EV Battery Cooling Fan Low Speed Request | `0x1079` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| VSC/TRC OFF Switch | `0x107A` | 0..7 | `raw` | 0 = OFF; 1 = TRC OFF Mode; 3 = TRC/VSC OFF Mode | N | Only observed constant |
| Short Wave Highest Value Level | `0x107B` | 0..7 | `raw` | 0 = Not Judge; 1 = Normal; 2 = Insulation Lower LV2; 3 = Insulation Lower LV3 | Y |  |
| Motor/Generator Reactor Current before SMR Precharge | `0x107D` | 0..15 | `raw * 0.1 - 3276.8` | A | N | Only observed constant |
| Motor/Generator Reactor Maximum Current during SMR Precharge | `0x107D` | 16..31 | `raw * 0.1 - 3276.8` | A | N | Running maximum; rises only on a new peak |
| Auxiliary Battery Voltage just before SMR Precharge | `0x107E` | 0..15 | `raw * 0.001220703125` | V | N | Only observed constant |
| Voltage Deviation between before Boosting and after Boosting during SMR Precharge | `0x107F` | 0..15 | `raw * 0.1 - 3276.8` | V | N | Only observed constant |
| Motor/Generator Reactor Current-Carrying Status during SMR Precharge | `0x1080` | 14..14 | `raw` | 0 = Indefinite; 1 = Definite | N | Only observed constant |
| Motor/Generator Reactor Noncurrent-Carrying Status during SMR Precharge | `0x1080` | 15..15 | `raw` | 0 = Indefinite; 1 = Definite | N | Only observed constant |
| Steering Angle | `0x1090` | 0..15 | `raw * 0.1 - 3276.8` | deg | Y |  |
| Forward and Rearward G | `0x1091` | 0..15 | `raw * 0.0004 - 13.107` | m/s2 | Y |  |
| Lateral G | `0x1092` | 0..15 | `raw * 0.0004 - 13.107` | m/s2 | Y |  |
| Yaw Rate Value | `0x1093` | 0..15 | `raw * 0.00390625 - 128` | deg/s | Y |  |
| Generator Revolution (MG1) | `0x10A0` | 0..15 | `raw - 32768` | rpm | Y |  |
| Target Generator Torque | `0x10A0` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Generator Carrier Frequency | `0x10A0` | 32..39 | `raw` |  | Y |  |
| Generator Control Mode | `0x10A0` | 40..47 | `raw` | 0 = Sine Wave; 1 = Overmodulation; 2 = Square Wave; 3 = Electrical Power Loss Reduction Mode | Y |  |
| Generator Torque | `0x10A0` | 48..63 | `raw * 0.125 - 4096` | Nm | Y |  |
| Generator Inverter Operation Request | `0x10A0` | 64..71 | `raw` | 0 = Shutdown; 1 = 3 Phase ON; 2 = Discharge; 3 = Insulation Resistance Measurement; 4 = Output Torque; 5 = Emergency Shutdown; 6 = Shutdown during Insulation Resistance Measurement | N | Only observed constant |
| Motor Revolution (MG2) | `0x10A1` | 0..15 | `raw - 32768` | rpm | Y |  |
| Target Motor Torque | `0x10A1` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Motor Carrier Frequency | `0x10A1` | 32..39 | `raw` |  | Y |  |
| Motor Control Mode | `0x10A1` | 40..47 | `raw` | 0 = Sine Wave; 1 = Overmodulation; 2 = Square Wave; 3 = Electrical Power Loss Reduction Mode | Y |  |
| Motor Torque | `0x10A1` | 48..63 | `raw * 0.125 - 4096` | Nm | Y |  |
| Motor Inverter Operation Request | `0x10A1` | 64..71 | `raw` | 0 = Shutdown; 1 = 3 Phase ON; 2 = Discharge; 3 = Insulation Resistance Measurement; 4 = Output Torque; 5 = Emergency Shutdown; 6 = Shutdown during Insulation Resistance Measurement | N | Only observed constant |
| Rear Motor Revolution | `0x10A2` | 0..15 | `raw - 32768` | rpm | Y |  |
| Target Rear Motor Torque | `0x10A2` | 16..31 | `raw * 0.125 - 4096` | Nm | Y |  |
| Rear Motor Carrier Frequency | `0x10A2` | 32..39 | `raw` |  | Y |  |
| Rear Motor Control Mode | `0x10A2` | 40..47 | `raw` | 0 = Sine Wave; 1 = Overmodulation; 2 = Square Wave; 3 = Electrical Power Loss Reduction Mode | N | Only observed constant |
| Rear Motor Torque | `0x10A2` | 48..63 | `raw * 0.125 - 4096` | Nm | Y |  |
| Rear Motor Inverter Operation Request | `0x10A2` | 64..71 | `raw` | 0 = Shutdown; 1 = 3 Phase ON; 2 = Discharge; 3 = Insulation Resistance Measurement; 4 = Output Torque; 5 = Emergency Shutdown; 6 = Shutdown during Insulation Resistance Measurement | Y |  |
| Boosting Converter Carrier Frequency | `0x10A3` | 0..7 | `raw` | 0 = HI; 1 = LO | N | Only observed constant |
| Boost Ratio | `0x10A3` | 8..15 | `raw * 20 / 51` | % | Y |  |
| Boosting Converter Operation Request | `0x10A3` | 16..23 | `raw` | 0 = Normal; 1 = Boosting Stop; 2 = Upper Arm ON; 3 = Maximum Boosting; 4 = VL Constant; 5 = Upper Arm Lowering; 6 = VH Constant; 7 = Discharge | N | Only observed constant |
| Motor Inverter Temperature | `0x10A5` | 0..7 | `raw - 40` | ℃ | Y |  |
| Motor Inverter Temperature just after IG ON | `0x10A5` | 8..15 | `raw - 40` | ℃ | N | Snapshot taken at ignition on; fixed during a drive |
| Motor Inverter Maximum Temperature | `0x10A5` | 16..23 | `raw - 40` | ℃ | N | Running maximum; rises only on a new peak |
| Rear Motor Inverter Temperature | `0x10A6` | 0..7 | `raw - 40` | ℃ | Y |  |
| Rear Motor Inverter Temperature just after IG ON | `0x10A6` | 8..15 | `raw - 40` | ℃ | Y |  |
| Rear Motor Inverter Maximum Temperature | `0x10A6` | 16..23 | `raw - 40` | ℃ | Y |  |
| Boosting Converter Temperature (Upper) | `0x10A7` | 0..7 | `raw - 40` | ℃ | Y |  |
| Boosting Converter Temperature (Lower) | `0x10A7` | 8..15 | `raw - 40` | ℃ | Y |  |
| Boosting Converter Temperature just after IG ON | `0x10A7` | 16..23 | `raw - 40` | ℃ | Y |  |
| Boosting Converter Maximum Temperature | `0x10A7` | 24..31 | `raw - 40` | ℃ | Y |  |
| Generator Inverter Temperature | `0x10A8` | 0..7 | `raw - 40` | ℃ | Y |  |
| Generator Inverter Temperature just after IG ON | `0x10A8` | 8..15 | `raw - 40` | ℃ | Y |  |
| Generator Inverter Maximum Temperature | `0x10A8` | 16..23 | `raw - 40` | ℃ | Y |  |
| Generator Inverter Fail | `0x10AC` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Generator Inverter Shutdown Status | `0x10AC` | 15..15 | `raw` | 0 = Awake; 1 = Shutdown | Y |  |
| Motor Inverter Fail | `0x10AD` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Motor Inverter Shutdown Status | `0x10AD` | 15..15 | `raw` | 0 = Awake; 1 = Shutdown | N | Only observed constant |
| Rear Motor Inverter Fail | `0x10AE` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Rear Motor Inverter Shutdown Status | `0x10AE` | 15..15 | `raw` | 0 = Awake; 1 = Shutdown | Y |  |
| Overvoltage Input to Inverter | `0x10AF` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Overvoltage Input to Boosting Converter | `0x10AF` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Fail | `0x10AF` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Boosting Converter Shutdown Status | `0x10AF` | 15..15 | `raw` | 0 = Awake; 1 = Shutdown | N | Only observed constant |
| VL-Voltage before Boosting | `0x10B0` | 0..15 | `raw * 0.5` | V | Y |  |
| VH-Voltage after Boosting | `0x10B0` | 16..31 | `raw * 0.5` | V | Y |  |
| Inverter Coolant Water Temperature | `0x10B2` | 0..7 | `raw - 40` | ℃ | Y |  |
| Rear Motor Inverter Temperature before Operation | `0x10B4` | 0..7 | `raw - 40` | ℃ | N | Only observed constant |
| VL-Voltage before Boosting for Rear Motor | `0x10B5` | 0..15 | `raw * 0.5` | V | N | Only observed constant |
| V Phase Generator Current | `0x10B6` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| W Phase Generator Current | `0x10B6` | 16..31 | `raw * 0.1 - 3276.8` | A | Y |  |
| V Phase Motor Current | `0x10B7` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| W Phase Motor Current | `0x10B7` | 16..31 | `raw * 0.1 - 3276.8` | A | Y |  |
| V Phase Rear Motor Current | `0x10B8` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| W Phase Rear Motor Current | `0x10B8` | 16..31 | `raw * 0.1 - 3276.8` | A | Y |  |
| Inverter Input Current | `0x1210` | 0..15 | `raw * 0.1 - 3276.8` | A | Y |  |
| Auxiliary Battery Charging Rate Accuracy | `0x15E4` | 0..7 | `raw` | 0 = Low; 1 = Middle; 2 = High; 3 = Disable | N | Only observed constant |
| Auxiliary Battery Status of Full Charge | `0x15E5` | 0..7 | `raw * 0.5` | Ah | N | Only observed constant |
| Auxiliary Battery Dark Current | `0x15E7` | 0..15 | `raw` | mA | N | Only observed constant |
| Auxiliary Battery Charging Integrated Current | `0x15E8` | 0..31 | `raw * 0.1` | Ah | N | Only observed constant |
| Auxiliary Battery Discharging Integrated Current | `0x15E8` | 32..63 | `raw * 0.1` | Ah | N | Only observed constant |
| Auxiliary Battery Capacity after IG ON | `0x15E8` | 64..71 | `raw - 128` | Ah | N | Snapshot taken at ignition on; fixed during a drive |
| Auxiliary Battery Capacity after IG OFF | `0x15E8` | 72..79 | `raw - 128` | Ah | N | Only observed constant |
| Integrated Ready ON Time | `0x15E8` | 80..95 | `raw` | hour | N | Only observed constant |
| Number of Long Term Leaving with IG OFF | `0x15E8` | 96..103 | `raw` |  | N | Only observed constant |
| Auxiliary Battery Integrated Thermal Load | `0x15E8` | 104..135 | `raw` |  | N | Only observed constant |
| Total Distance Indicated after Long Term Leaving with IG OFF (1st) | `0x15E9` | 0..15 | `raw * 2` | km | N | Only observed constant |
| Total Distance Indicated after Long Term Leaving with IG OFF (2nd) | `0x15E9` | 16..31 | `raw * 2` | km | N | Only observed constant |
| Total Distance Indicated after Long Term Leaving with IG OFF (3rd) | `0x15E9` | 32..47 | `raw * 2` | km | N | Only observed constant |
| Time of Long Term Leaving with IG OFF (1st) | `0x15E9` | 48..55 | `raw` | day | N | Only observed constant |
| Time of Long Term Leaving with IG OFF (2nd) | `0x15E9` | 56..63 | `raw` | day | N | Only observed constant |
| Time of Long Term Leaving with IG OFF (3rd) | `0x15E9` | 64..71 | `raw` | day | N | Only observed constant |
| Auxiliary Battery Average Current during IG OFF 1 Trip before | `0x15EA` | 0..15 | `raw * 0.001 - 32.768` | A | N | Only observed constant |
| Auxiliary Battery Average Current during IG OFF 2 Trip before | `0x15EA` | 16..31 | `raw * 0.001 - 32.768` | A | N | Only observed constant |
| Auxiliary Battery Average Current during IG OFF 3 Trip before | `0x15EA` | 32..47 | `raw * 0.001 - 32.768` | A | N | Only observed constant |
| Auxiliary Battery Average Current during IG OFF 4 Trip before | `0x15EA` | 48..63 | `raw * 0.001 - 32.768` | A | N | Only observed constant |
| Auxiliary Battery Average Current during IG OFF 5 Trip before | `0x15EA` | 64..79 | `raw * 0.001 - 32.768` | A | N | Only observed constant |
| Total Distance Up to 1 Trip before | `0x15EA` | 80..95 | `raw * 2` | km | N | Only observed constant |
| Total Distance Up to 2 Trip before | `0x15EA` | 96..111 | `raw * 2` | km | N | Only observed constant |
| Total Distance Up to 3 Trip before | `0x15EA` | 112..127 | `raw * 2` | km | N | Only observed constant |
| Total Distance Up to 4 Trip before | `0x15EA` | 128..143 | `raw * 2` | km | N | Only observed constant |
| Total Distance Up to 5 Trip before | `0x15EA` | 144..159 | `raw * 2` | km | N | Only observed constant |
| IG OFF Time before 1 trip | `0x15EA` | 160..167 | `raw` | day | N | Only observed constant |
| IG OFF Time before 2 trip | `0x15EA` | 168..175 | `raw` | day | N | Only observed constant |
| IG OFF Time before 3 trip | `0x15EA` | 176..183 | `raw` | day | N | Only observed constant |
| IG OFF Time before 4 trip | `0x15EA` | 184..191 | `raw` | day | N | Only observed constant |
| IG OFF Time before 5 trip | `0x15EA` | 192..199 | `raw` | day | N | Only observed constant |
| IG ON Time Up to 1 trip before | `0x15EA` | 200..207 | `raw * 2` | min | N | Only observed constant |
| IG ON Time Up to 2 trip before | `0x15EA` | 208..215 | `raw * 2` | min | N | Only observed constant |
| IG ON Time Up to 3 trip before | `0x15EA` | 216..223 | `raw * 2` | min | N | Only observed constant |
| IG ON Time Up to 4 trip before | `0x15EA` | 224..231 | `raw * 2` | min | N | Only observed constant |
| IG ON Time Up to 5 trip before | `0x15EA` | 232..239 | `raw * 2` | min | N | Only observed constant |
| Ready ON Time Up to 1 trip before | `0x15EA` | 240..247 | `raw * 2` | min | N | Only observed constant |
| Ready ON Time Up to 2 trip before | `0x15EA` | 248..255 | `raw * 2` | min | N | Only observed constant |
| Ready ON Time Up to 3 trip before | `0x15EA` | 256..263 | `raw * 2` | min | N | Only observed constant |
| Ready ON Time Up to 4 trip before | `0x15EA` | 264..271 | `raw * 2` | min | N | Only observed constant |
| Ready ON Time Up to 5 trip before | `0x15EA` | 272..279 | `raw * 2` | min | N | Only observed constant |
| Auxiliary Battery Voltage | `0x15EE` | 0..15 | `raw * 0.001220703125` | V | Y |  |
| Auxiliary Battery Voltage Low Times | `0x15EF` | 0..7 | `raw` |  | N | Only observed constant |
| Auxiliary Battery Voltage at Low Voltage Checking Initiation | `0x15EF` | 8..23 | `raw * 0.001220703125` | V | N | Only observed constant |
| Auxiliary Battery Sensor Sleep Time | `0x15F0` | 0..7 | `raw` | h | Y |  |
| Auxiliary Battery Current | `0x15F7` | 0..15 | `raw * 125 / 32768 - 125` | A | Y |  |
| Smoothed Value of Auxiliary Battery Temperature | `0x15F8` | 0..15 | `raw * 0.1 - 40` | ℃ | Y |  |
| Immobiliser Communication | `0x15FF` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Hybrid/EV Battery Cooling Fan 1 Drive Request | `0x1802` | 0..7 | `raw * 0.5` | % | Y |  |
| Hybrid/EV Battery Cooling Fan 1 Frequency | `0x1802` | 8..23 | `raw * 0.1` | Hz | Y |  |
| Hybrid/EV Battery Cooling Fan 1 Drive Status | `0x1802` | 24..31 | `raw` | 0 = 0; 1 = 1; 2 = 2; 3 = 3; 4 = 4; 5 = 5; 6 = 6; 7 = 7; 8 = 8; 9 = 9; 10 = 10; 11 = 11; 12 = 12; 13 = 13; 14 = 14; 15 = 15; 16 = 16; 17 = 17; 18 = 18; 19 = 19; 20 = 20; 21 = 21; 22 = 22; 23 = 23; 24 = 24; 25 = 25; 26 = 26; 27 = 27; 28 = 28; 29 = 29; 30 = 30; 31 = 31; 32 = 32; 33 = 33; 34 = 34; 35 = 35; 36 = 36; 37 = 37; 38 = 38; 39 = 39; 40 = -; 41 = 41; 42 = 42; 43 = 43; 44 = 44; 45 = 45; 46 = 46; 47 = 47; 48 = 48; 49 = 49; 50 = 50; 51 = 51; 52 = 52; 53 = 53; 54 = 54; 55 = 55; 56 = 56; 57 = 57; 58 = 58; 59 = 59; 60 = 60; 61 = 61; 62 = 62; 63 = 63; 64 = 64; 65 = 65; 66 = 66; 67 = 67; 68 = 68; 69 = 69; 70 = 70; 71 = 71; 72 = 72; 73 = 73; 74 = 74; 75 = 75; 76 = 76; 77 = 77; 78 = 78; 79 = 79; 80 = 80; 81 = 81; 82 = 82; 83 = 83; 84 = 84; 85 = 85; 86 = 86; 87 = 87; 88 = 88; 89 = 89; 90 = 90; 91 = 91; 92 = 92; 93 = 93; 94 = 94; 95 = 95; 96 = 96; 97 = 97; 98 = 98; 99 = 99; 100 = 100; 101 = 101; 102 = 102; 103 = 103; 104 = 104; 105 = 105; 106 = 106; 107 = 107; 108 = 108; 109 = 109; 110 = 110; 111 = 111; 112 = 112; 113 = 113; 114 = 114; 115 = 115; 116 = 116; 117 = 117; 118 = 118; 119 = 119; 120 = 120; 121 = 121; 122 = 122; 123 = 123; 124 = 124; 125 = 125; 126 = 126; 127 = 127; 128 = 128; 129 = 129; 130 = 130; 131 = 131; 132 = 132; 133 = 133; 134 = 134; 135 = 135; 136 = 136; 137 = 137; 138 = 138; 139 = 139; 140 = 140; 141 = 141; 142 = 142; 143 = 143; 144 = 144; 145 = 145; 146 = 146; 147 = 147; 148 = 148; 149 = 149; 150 = 150; 151 = 151; 152 = 152; 153 = 153; 154 = 154; 155 = 155; 156 = 156; 157 = 157; 158 = 158; 159 = 159; 160 = 160; 161 = 161; 162 = 162; 163 = 163; 164 = 164; 165 = 165; 166 = 166; 167 = 167; 168 = 168; 169 = 169; 170 = 170; 171 = 171; 172 = 172; 173 = 173; 174 = 174; 175 = 175; 176 = 176; 177 = 177; 178 = 178; 179 = 179; 180 = 180; 181 = 181; 182 = 182; 183 = 183; 184 = 184; 185 = 185; 186 = 186; 187 = 187; 188 = 188; 189 = 189; 190 = 190; 191 = 191; 192 = 192; 193 = 193; 194 = 194; 195 = 195; 196 = 196; 197 = 197; 198 = 198; 199 = 199; 200 = 200; 201 = 201; 202 = 202; 203 = 203; 204 = 204; 205 = 205; 206 = 206; 207 = 207; 208 = 208; 209 = 209; 210 = 210; 211 = 211; 212 = 212; 213 = 213; 214 = 214; 215 = 215; 216 = 216; 217 = 217; 218 = 218; 219 = 219; 220 = 220; 221 = 221; 222 = 222; 223 = 223; 224 = 224; 225 = 225; 226 = 226; 227 = 227; 228 = 228; 229 = 229; 230 = 230; 231 = 231; 232 = 232; 233 = 233; 234 = 234; 235 = 235; 236 = 236; 237 = 237; 238 = 238; 239 = 239; 240 = 240; 241 = 241; 242 = 242; 243 = 243; 244 = 244; 245 = 245; 246 = 246; 247 = 247; 248 = 248; 249 = 249; 250 = 250; 251 = 251; 252 = 252; 253 = 253; 254 = 254; 255 = 255 | Y |  |
| Hybrid/EV Battery Sensor Module Power Supply Voltage | `0x1805` | 0..15 | `raw * 0.001220703125 - 40` | V | Y |  |
| WIN Control Limit Power | `0x1807` | 0..15 | `raw * 0.01 - 327.68` | kW | Y |  |
| WOUT Control Limit Power | `0x1807` | 16..31 | `raw * 0.01 - 327.68` | kW | N | Held at its limit; drops with a cold, hot or low battery |
| Hybrid/EV Battery Block 1 Voltage | `0x1809` | 0..15 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Block 2 Voltage | `0x1809` | 16..31 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Block 3 Voltage | `0x1809` | 32..47 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Block 4 Voltage | `0x1809` | 48..63 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Block 5 Voltage | `0x1809` | 64..79 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Block 6 Voltage | `0x1809` | 80..95 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Block 7 Voltage | `0x1809` | 96..111 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Block 8 Voltage | `0x1809` | 112..127 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Block 9 Voltage | `0x1809` | 128..143 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Block 10 Voltage | `0x1809` | 144..159 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Block 11 Voltage | `0x1809` | 160..175 | `raw * 0.001220703125` | V | Y |  |
| Number of Hybrid/EV Battery Block | `0x180A` | 0..7 | `raw` |  | N | Only observed constant |
| Hybrid/EV Battery Block Ch (Minimum Voltage) | `0x180A` | 8..15 | `raw` | ch | Y |  |
| Hybrid/EV Battery Block Ch (Maximum Voltage) | `0x180A` | 16..23 | `raw` | ch | Y |  |
| Minimum Voltage of Hybrid/EV Battery Block | `0x180A` | 24..39 | `raw * 0.001220703125` | V | Y |  |
| Maximum Voltage of Hybrid/EV Battery Block | `0x180A` | 40..55 | `raw * 0.001220703125` | V | Y |  |
| Hybrid/EV Battery Cooling Fan Intake Air Temperature 1 | `0x180B` | 0..15 | `raw * 0.00390625 - 50` | ℃ | Y |  |
| Delta SOC | `0x180D` | 0..7 | `raw * 0.5` | % | N | 0 while the battery cells are balanced |
| Hybrid/EV Battery Control Mode | `0x180F` | 0..7 | `raw` | 0 = Driving Control Mode; 1 = Current Sensor Offset Calibration Mode; 2 = Hybrid/EV Battery External Charging Control Mode; 3 = ECU Shutdown Mode | N | Only observed constant |
| Internal Resistance 1 | `0x1813` | 0..7 | `raw * 0.001` | ohm | Y |  |
| Internal Resistance 2 | `0x1813` | 8..15 | `raw * 0.001` | ohm | Y |  |
| Internal Resistance 3 | `0x1813` | 16..23 | `raw * 0.001` | ohm | Y |  |
| Internal Resistance 4 | `0x1813` | 24..31 | `raw * 0.001` | ohm | Y |  |
| Internal Resistance 5 | `0x1813` | 32..39 | `raw * 0.001` | ohm | Y |  |
| Internal Resistance 6 | `0x1813` | 40..47 | `raw * 0.001` | ohm | Y |  |
| Internal Resistance 7 | `0x1813` | 48..55 | `raw * 0.001` | ohm | Y |  |
| Internal Resistance 8 | `0x1813` | 56..63 | `raw * 0.001` | ohm | Y |  |
| Internal Resistance 9 | `0x1813` | 64..71 | `raw * 0.001` | ohm | Y |  |
| Internal Resistance 10 | `0x1813` | 72..79 | `raw * 0.001` | ohm | Y |  |
| Internal Resistance 11 | `0x1813` | 80..87 | `raw * 0.001` | ohm | Y |  |
| Hybrid/EV Battery Temperature 1 | `0x1814` | 0..15 | `raw * 0.00390625 - 50` | ℃ | Y |  |
| Hybrid/EV Battery Temperature 2 | `0x1814` | 16..31 | `raw * 0.00390625 - 50` | ℃ | Y |  |
| Hybrid/EV Battery Temperature 3 | `0x1814` | 32..47 | `raw * 0.00390625 - 50` | ℃ | Y |  |
| Hybrid/EV Battery Current for Hybrid/EV Battery Control | `0x1817` | 0..15 | `raw * 0.01 - 327.68` | A | Y |  |
| Hybrid/EV Battery Current Sensor Power Supply Voltage | `0x1817` | 16..23 | `raw * 0.1` | V | N | Only observed constant |
| Hybrid/EV Battery Current Sensor Offset Learning Value | `0x1817` | 24..39 | `raw * 0.01 - 327.68` | A | N | Only observed constant |
| Hybrid/EV Battery Current Sensor Offset (High) | `0x1817` | 40..55 | `raw * 0.01 - 327.68` | A | N | Only observed constant |
| Hybrid/EV Battery Current for Driving Control | `0x181D` | 0..15 | `raw * 0.01 - 327.68` | A | Y |  |
| Number of Hybrid/EV Battery Current Sensor Characteristics Determination | `0x181F` | 0..7 | `raw` |  | N | Only observed constant |
| Hybrid/EV Battery Monitoring IC 1 Voltage 1 | `0x1828` | 0..15 | `raw * 0.01 - 327.68` | V | Y |  |
| Hybrid/EV Battery Monitoring IC 1 Voltage 2 | `0x1828` | 16..31 | `raw * 0.01 - 327.68` | V | Y |  |
| Hybrid/EV Battery Monitoring IC 2 Voltage 1 | `0x1828` | 32..47 | `raw * 0.01 - 327.68` | V | Y |  |
| Hybrid/EV Battery Monitoring IC 2 Voltage 2 | `0x1828` | 48..63 | `raw * 0.01 - 327.68` | V | Y |  |
| Hybrid/EV Battery Monitoring IC 3 Voltage 1 | `0x1828` | 64..79 | `raw * 0.01 - 327.68` | V | Y |  |
| Hybrid/EV Battery Monitoring IC 3 Voltage 2 | `0x1828` | 80..95 | `raw * 0.01 - 327.68` | V | Y |  |
| MIL | `0x1F01` | 0..0 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Number of Emission DTC | `0x1F01` | 1..7 | `raw` |  | N | Only observed constant |
| Calculate Load | `0x1F04` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Coolant Temperature | `0x1F05` | 0..7 | `raw - 40` | ℃ | Y |  |
| Engine Speed | `0x1F0C` | 0..15 | `raw * 0.25` | rpm | Y |  |
| Vehicle Speed | `0x1F0D` | 0..7 | `raw` | km/h | Y |  |
| Intake Air Temperature | `0x1F0F` | 0..7 | `raw - 40` | ℃ | Y |  |
| Throttle Position Sensor No.1 Voltage % | `0x1F11` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Engine Run Time | `0x1F1F` | 0..15 | `raw` | sec | Y |  |
| MIL ON Run Distance | `0x1F21` | 0..15 | `raw` | km | N | Only observed constant |
| Warmup Cycle Cleared DTC | `0x1F30` | 0..7 | `raw` |  | N | Only observed constant |
| Distance from DTC Cleared | `0x1F31` | 0..15 | `raw` | km | Y |  |
| Atmospheric Pressure | `0x1F33` | 0..7 | `raw` | kPa | Y |  |
| Smoothed Value of BATT Voltage | `0x1F42` | 0..15 | `raw * 0.001` | V | Y |  |
| Ambient Temperature | `0x1F46` | 0..7 | `raw - 40` | ℃ | Y |  |
| Accelerator Position Sensor No.1 Voltage % | `0x1F49` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Accelerator Position Sensor No.2 Voltage % | `0x1F4A` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Running Time from MIL ON | `0x1F4D` | 0..15 | `raw` | min | N | Only observed constant |
| Time after DTC Cleared | `0x1F4E` | 0..15 | `raw` | min | Y |  |
| Hybrid/EV Battery SOC | `0x1F5B` | 0..7 | `raw * 20 / 51` | % | Y |  |
| Recommended Gear for Current Vehicle Conditions | `0x1F65` | 8..11 | `raw` | 1 = 1st; 2 = 2nd; 3 = 3rd; 4 = 4th; 5 = 5th; 6 = 6th; 7 = 7th; 8 = 8th; 9 = 9th; 10 = 10th; 11 = 11th; 12 = 12th; 13 = 13th; 14 = 14th; 15 = 15th | N | Only observed constant |
| Glow Indicator | `0x1F65` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Shift Switch Status (Neutral) | `0x1F65` | 13..13 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Shift Switch Status (N,P Position) | `0x1F65` | 14..14 | `raw` | 0 = ON; 1 = OFF | Y |  |
| Power Take Off Status | `0x1F65` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Intake Manifold Absolute Pressure | `0x1F87` | 8..23 | `raw * 0.03125` | kPa | Y |  |
| Power Feeding Electrical Using Status | `0x1F9A` | 13..14 | `raw` | 0 = Charge Sustaining Mode/Not PHV; 1 = Charge Depleting Mode; 2 = Charge Increasing Mode; 3 = Ready OFF | N | Only observed constant |
| Hybrid/EV Battery Voltage | `0x1F9A` | 16..31 | `raw * 0.015625` | V | Y |  |
| Hybrid/EV Battery Current | `0x1F9A` | 32..47 | `signed(raw) * 0.1` | A | Y |  |
| Body Control with Torque Demand | `0x23FF` | 0..7 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Inverter Water Pump | `0x2801` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Hybrid/EV Battery Cooling Fan | `0x2802` | 0..7 | `raw` | 0 = 0; 1 = 1; 2 = 2; 3 = 3; 4 = 4; 5 = 5; 6 = 6; 7 = 7; 8 = 8; 9 = 9; 10 = 10; 11 = 11; 12 = 12; 13 = 13; 14 = 14; 15 = 15; 16 = 16; 17 = 17; 18 = 18; 19 = 19; 20 = 20; 21 = 21; 22 = 22; 23 = 23; 24 = 24; 25 = 25; 26 = 26; 27 = 27; 28 = 28; 29 = 29; 30 = 30; 31 = 31; 32 = 32; 33 = 33; 34 = 34; 35 = 35; 36 = 36; 37 = 37; 38 = 38; 39 = 39; 40 = -; 41 = 41; 42 = 42; 43 = 43; 44 = 44; 45 = 45; 46 = 46; 47 = 47; 48 = 48; 49 = 49; 50 = 50; 51 = 51; 52 = 52; 53 = 53; 54 = 54; 55 = 55; 56 = 56; 57 = 57; 58 = 58; 59 = 59; 60 = 60; 61 = 61; 62 = 62; 63 = 63; 64 = 64; 65 = 65; 66 = 66; 67 = 67; 68 = 68; 69 = 69; 70 = 70; 71 = 71; 72 = 72; 73 = 73; 74 = 74; 75 = 75; 76 = 76; 77 = 77; 78 = 78; 79 = 79; 80 = 80; 81 = 81; 82 = 82; 83 = 83; 84 = 84; 85 = 85; 86 = 86; 87 = 87; 88 = 88; 89 = 89; 90 = 90; 91 = 91; 92 = 92; 93 = 93; 94 = 94; 95 = 95; 96 = 96; 97 = 97; 98 = 98; 99 = 99; 100 = 100; 101 = 101; 102 = 102; 103 = 103; 104 = 104; 105 = 105; 106 = 106; 107 = 107; 108 = 108; 109 = 109; 110 = 110; 111 = 111; 112 = 112; 113 = 113; 114 = 114; 115 = 115; 116 = 116; 117 = 117; 118 = 118; 119 = 119; 120 = 120; 121 = 121; 122 = 122; 123 = 123; 124 = 124; 125 = 125; 126 = 126; 127 = 127; 128 = 128; 129 = 129; 130 = 130; 131 = 131; 132 = 132; 133 = 133; 134 = 134; 135 = 135; 136 = 136; 137 = 137; 138 = 138; 139 = 139; 140 = 140; 141 = 141; 142 = 142; 143 = 143; 144 = 144; 145 = 145; 146 = 146; 147 = 147; 148 = 148; 149 = 149; 150 = 150; 151 = 151; 152 = 152; 153 = 153; 154 = 154; 155 = 155; 156 = 156; 157 = 157; 158 = 158; 159 = 159; 160 = 160; 161 = 161; 162 = 162; 163 = 163; 164 = 164; 165 = 165; 166 = 166; 167 = 167; 168 = 168; 169 = 169; 170 = 170; 171 = 171; 172 = 172; 173 = 173; 174 = 174; 175 = 175; 176 = 176; 177 = 177; 178 = 178; 179 = 179; 180 = 180; 181 = 181; 182 = 182; 183 = 183; 184 = 184; 185 = 185; 186 = 186; 187 = 187; 188 = 188; 189 = 189; 190 = 190; 191 = 191; 192 = 192; 193 = 193; 194 = 194; 195 = 195; 196 = 196; 197 = 197; 198 = 198; 199 = 199; 200 = 200; 201 = 201; 202 = 202; 203 = 203; 204 = 204; 205 = 205; 206 = 206; 207 = 207; 208 = 208; 209 = 209; 210 = 210; 211 = 211; 212 = 212; 213 = 213; 214 = 214; 215 = 215; 216 = 216; 217 = 217; 218 = 218; 219 = 219; 220 = 220; 221 = 221; 222 = 222; 223 = 223; 224 = 224; 225 = 225; 226 = 226; 227 = 227; 228 = 228; 229 = 229; 230 = 230; 231 = 231; 232 = 232; 233 = 233; 234 = 234; 235 = 235; 236 = 236; 237 = 237; 238 = 238; 239 = 239; 240 = 240; 241 = 241; 242 = 242; 243 = 243; 244 = 244; 245 = 245; 246 = 246; 247 = 247; 248 = 248; 249 = 249; 250 = 250; 251 = 251; 252 = 252; 253 = 253; 254 = 254; 255 = 255 | Y |  |
| TC Terminal | `0x2BFF` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |

## Cruise Control

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | Y |  |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | Y |  |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw` | km | Y |  |
| Absolute Value Time (Year) | `0x0109` | 0..7 | `raw` | Year | N | Only observed constant |
| Absolute Value Time (Month) | `0x0109` | 8..15 | `raw` | Month | N | Only observed constant |
| Absolute Value Time (Day) | `0x0109` | 16..23 | `raw` | Day | Y |  |
| Absolute Value Time (Hour) | `0x0109` | 24..31 | `raw` | Hour | Y |  |
| Absolute Value Time (Minute) | `0x0109` | 32..39 | `raw` | Minute | Y |  |
| Absolute Value Time (Second) | `0x0109` | 40..47 | `raw` | Second | Y |  |
| Cruise Main Switch Operation Condition | `0x1042` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Brake Cancel Switch | `0x1043` | 14..14 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Stop Light Switch Main-CPU | `0x1043` | 15..15 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Vehicle Acceleration | `0x15EC` | 0..15 | `raw * 0.0004 - 13.1` | m/s2 | Y |  |
| Gradient of Road Surface | `0x15EC` | 16..31 | `raw * 0.001 - 32.76` | m/s2 | Y |  |
| Output Driving Force | `0x15EC` | 32..39 | `raw * 0.078125 - 10` | kN | Y |  |
| Cruise Acceleration and Deceleration Operation Condition | `0x15EC` | 40..47 | `raw` | 0 = No Control; 1 = Constant Speed Control Mode - Standby; 2 = Constant Speed Control Mode - RES SW Control; 3 = Constant Speed Control Mode - ACC SW Control; 4 = Constant Speed Control Mode - COAST SW Control; 5 = Constant Speed Control Mode - Tap Down Control; 6 = Constant Speed Control Mode - Tap Up Control; 7 = Vehicle Distance Control Mode - Vehicle Stop; 8 = Vehicle Distance Control Mode - Standby; 9 = Vehicle Distance Control Mode - Acceleration Operation; 10 = Vehicle Distance Control Mode - Deceleration Operation | N | Only observed constant |
| Cruise Control Mode | `0x15EC` | 48..55 | `raw` | 1 = Vehicle Distance Control Mode (All Speed); 2 = Vehicle Distance Control Mode; 3 = Constant Speed Control Mode | N | Only observed constant |
| Cruise Request Gear Position | `0x15EC` | 56..63 | `raw` | Gear; 0 = None; 1 = 255 | N | Stayed 0 with cruise control on; likely unused on this hybrid |
| Cruise Vehicle Speed | `0x15ED` | 0..7 | `raw` | km/h | Y |  |
| Cruise Memory Vehicle Speed | `0x15ED` | 8..15 | `raw` | km/h | N | Only observed constant |
| Cruise Request Throttle Opening Angle | `0x15ED` | 16..31 | `raw * 125 / 65536` | deg | N | Stayed 0 with cruise control on; likely unused on this hybrid |
| Driving Support ECU Target Acceleration | `0x15ED` | 32..47 | `raw * 0.0004 - 13.1` | m/s2 | Y |  |
| Cruise Target Acceleration | `0x15ED` | 48..63 | `raw * 0.0004 - 13.1` | m/s2 | Y |  |
| Cruise Request Driving Force | `0x15ED` | 64..71 | `raw * 0.078125 - 10` | kN | Y |  |
| Cruise Request Driving Force Feedback Status | `0x15ED` | 72..79 | `raw` | 0 = No Control; 1 = Suspend; 2 = Neg Calc Prohibited; 3 = Pos Calc Prohibited; 4 = Normal; 5 = Increasing at Start | N | Only observed constant |
| Cruise Control Condition | `0x15ED` | 80..87 | `raw` | 1 = No Control; 2 = Constant Speed Control Mode - Ready; 3 = Constant Speed Control Mode - Controlling; 4 = Vehicle Distance Control Mode - Ready; 5 = Vehicle Distance Control Mode - Controlling; 6 = Termination Control; 7 = Suspend; 8 = Abnormal Stop | N | Only observed constant |
| Wheel Speed Sensor and Deceleration Sensor | `0x15FD` | 8..8 | `raw` | 0 = Avail; 1 = Not Avl | N | Only observed constant |
| Acceleration Continuation Decision | `0x15FD` | 9..9 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Indicator | `0x15FD` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cancel Switch | `0x15FD` | 11..11 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| -SET Switch | `0x15FD` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| +RES Switch | `0x15FD` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Ready Sub-CPU | `0x15FD` | 14..14 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Ready Main-CPU | `0x15FD` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Stop Light Switch Sub-CPU | `0x15FE` | 9..9 | `raw` | 0 = OFF; 1 = ON | Y |  |
| Cruise Brake Control Decision | `0x15FE` | 10..10 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Accelerator Pedal | `0x15FE` | 11..11 | `raw` | 0 = ON; 1 = OFF | Y |  |
| Request Manual Cancel | `0x15FE` | 12..12 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Request Automatic Cancel | `0x15FE` | 13..13 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Cruise Brake Control Permission Condition | `0x15FE` | 14..14 | `raw` | 0 = NG; 1 = OK | Y |  |
| Cruise Control Permission Condition | `0x15FE` | 15..15 | `raw` | 0 = NG; 1 = OK | N | Only observed constant |
| Number of Emission DTC | `0x1F01` | 1..7 | `raw` |  | N | Only observed constant |

## EV

| Signal | DID | Bits | Formula | Unit / Values | Verified | Comment |
|---|---|---|---|---|---|---|
| Key Cycle | `0x0102` | 0..15 | `raw` | trip | N | Only observed constant |
| IG-ON Elapsed Time | `0x0102` | 16..47 | `raw` | ms | N | Only observed constant |
| Master Sync Information | `0x0102` | 48..55 | `raw` |  | N | Only observed constant |
| Total Distance Traveled | `0x0103` | 8..31 | `raw` | km | Y |  |
| Absolute Value Time (Year) | `0x0109` | 0..7 | `raw` | Year | N | Only observed constant |
| Absolute Value Time (Month) | `0x0109` | 8..15 | `raw` | Month | N | Only observed constant |
| Absolute Value Time (Day) | `0x0109` | 16..23 | `raw` | Day | Y |  |
| Absolute Value Time (Hour) | `0x0109` | 24..31 | `raw` | Hour | Y |  |
| Absolute Value Time (Minute) | `0x0109` | 32..39 | `raw` | Minute | Y |  |
| Absolute Value Time (Second) | `0x0109` | 40..47 | `raw` | Second | Y |  |
| BATT Voltage | `0x1002` | 0..15 | `raw * 0.001220703125` | V | Y |  |
| Inter Lock Switch | `0x1031` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Inverter Emergency Shutdown (Sub CPU) | `0x1032` | 14..14 | `raw` | 0 = ON; 1 = OFF | N | Only observed constant |
| Inverter Emergency Shutdown (Main CPU) | `0x1032` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
| Inverter Water Pump Duty Ratio | `0x1039` | 0..15 | `raw * 0.006103515625` | % | Y |  |
| Inverter Water Pump Revolution | `0x1039` | 16..31 | `raw` | rpm | Y |  |
| Insulation Resistance Division Check Completion using MG Inv | `0x1072` | 9..9 | `raw` | 0 = Not Complete; 1 = Complete | N | Only observed constant |
| Insulation Resistance Division Check Completion using A/C Inv | `0x1072` | 10..10 | `raw` | 0 = Not Complete; 1 = Complete | N | Only observed constant |
| Insulation Resistance Division Check Completion using SMR | `0x1072` | 11..11 | `raw` | 0 = Not Complete; 1 = Complete | N | Only observed constant |
| Short Wave Highest Value Availability just after MG Inv On/Off | `0x1072` | 13..13 | `raw` | 0 = Yes; 1 = No | N | Only observed constant |
| Short Wave Highest Value Availability just after A/C Inv On/Off | `0x1072` | 14..14 | `raw` | 0 = Yes; 1 = No | N | Only observed constant |
| Short Wave Highest Value Availability just after SMR On/Off | `0x1072` | 15..15 | `raw` | 0 = Yes; 1 = No | N | Only observed constant |
| Short Wave Highest Value Level | `0x107B` | 0..7 | `raw` | 0 = Not Judge; 1 = Normal; 2 = Insulation Lower LV2; 3 = Insulation Lower LV3 | N | Only observed constant |
| Inverter Coolant Water Temperature | `0x10B2` | 0..7 | `raw - 40` | ℃ | Y |  |
| Smoothed Value of BATT Voltage | `0x1F42` | 0..15 | `raw * 0.001` | V | Y |  |
| Inverter Water Pump | `0x2801` | 15..15 | `raw` | 0 = OFF; 1 = ON | N | Only observed constant |
