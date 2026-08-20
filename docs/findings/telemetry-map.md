# XA50 telemetry and decoding

## Confidence legend

- **Candidate:** extracted from repair data, Techstream databases, or static analysis.
- **Validated:** observed live and decoded values agree with the synchronized Techstream display.
- **Unknown:** field is present but its meaning is not yet established.

The current validation applies to this vehicle/profile. Exact availability may vary with model year, market, drivetrain, battery, and ECU calibration.

## Useful Data List concepts

| Desired value | Techstream name(s) | Likely ECU/menu | Notes |
|---|---|---|---|
| MG1 rpm | `Generator Revolution` | Hybrid Control or Motor Generator | Signed MG1 resolver speed. |
| MG2 rpm | `Motor Revolution` | Hybrid Control or Motor Generator | Signed MG2 resolver speed. |
| Rear motor rpm | `Rear Motor Revolution` | Rear Motor Generator | AWD/e-Four only. |
| Accelerator pedal | `Accelerator Position`, sensor voltages/percent | Hybrid Control or Engine | Prefer scaled percentage for the dash. |
| Individual wheel speeds | `FR/FL/RR/RL Wheel Speed` | Hybrid Control or ABS/VSC/TRAC | Hybrid Control may avoid a second ECU session. |
| Steering wheel angle | `Steering Angle` or `Steering Angle Value` | Hybrid Control, ABS/VSC/TRAC, or EMPS | Road-wheel angle would be derived from steering geometry. |
| Brake request | `Stop Light SW`, `Stroke Sensor`, `Master Cylinder Control Torque` | ABS/VSC/TRAC or Hybrid Control | These describe switch, travel, and request—not the same state. |
| Regenerative braking | `Regenerative Cooperation`, `Motor Regenerate Brake Execution Torque`, `FR Regenerative Request/Operation` | Hybrid Control or ABS/VSC/TRAC | Useful for regen/friction blending. |
| Friction application | `Wheel Cylinder Pressure Sensor`, `Regulator Pressure Sensor Output After Filter` | ABS/VSC/TRAC | Pressure is a proxy, not a pad-contact sensor. |
| Vehicle dynamics | yaw rate, G sensors, wheel acceleration | ABS/VSC/TRAC or Hybrid Control | Optional dash values. |

Toyota repair data describes the hybrid values in the [motor-generator Data List](https://estimateca.mymitchell.com/t3Portal/document/rm/RM3510U/xhtml/RM100000001H3DV.html) and the steering, wheel, pedal, regenerative, and pressure values in the [electronically controlled brake Data List](https://estimateca.mymitchell.com/t3Portal/document/rm/RM3510U/xhtml/RM100000001HG89.html).

## Brake terminology

There is no known sensor reporting literal pad-to-rotor contact. A truthful dashboard should distinguish:

- driver intent: stop-light switch and/or pedal stroke;
- regenerative component: actual MG regenerative torque;
- friction component: hydraulic pressure above a learned released baseline;
- special cases: brake hold, ABS, VSC/TRAC, prefill, and low-speed stopping.

Until stronger evidence exists, label the result **friction brake pressure/application**, not **pad engagement**. Per-wheel commands do not prove per-wheel pad contact.

## Techstream Phase-5 database findings

The installed regional database uses `Toyota.ddb` as a catalog and loads ECU databases on demand:

| ECU key | ECU | Database |
|---:|---|---|
| 397 | Hybrid Control | `HV_P5.ddb` |
| 395 | Motor Generator | `MG_P5.ddb` |
| 435 | Brake/EPB | `ABS_P5.ddb` |
| 466 | Brake Booster | `Brk_Bst_P5.ddb` |

Reverse engineering `DataMonitorPhase5_DT.dll` and `DataListIF.dll` established:

1. A monitor record contains an internal ID, actual ECU DID, response bit range, physical-conversion group, and flags.
2. Flags select direct, mapped, or dynamic acquisition behavior.
3. Direct Phase-5 reads use `22 DID_hi DID_lo` and expect `62 DID_hi DID_lo ...`.
4. Signals sharing a DID are packed in the same response.
5. Techstream can define a dynamic DID that combines slices from multiple source DIDs.

Numeric physical-conversion records hold `A`, `B`, `C`, unit, and decimal position `N`:

```text
physical_value = ((raw * A / B) + C) / (10 ** N)
```

For example, the rpm group uses `A=1, B=1, C=-32768, N=0`; accelerator uses `A=5, B=1, C=0, N=1`.

Database query examples:

```powershell
# Focused monitor definitions and direct request candidates
.\tools\techstream-db\query-techstream-db.ps1 -Keys 397 `
  -NamePattern '^(Generator Revolution|Motor Revolution|Accelerator Position)$' -NoRaw

# Physical conversion records
.\tools\techstream-db\query-techstream-db.ps1 -Keys 397 -PhysicalGroups '0x15,0x20,0x17'

# Unit records
.\tools\techstream-db\query-techstream-db.ps1 -Keys 397 -UnitIds '0x02,0x5d,0x32' -NoRaw

# Other ECU databases and keys
.\tools\techstream-db\query-techstream-db.ps1 -Catalog -NamePattern 'Hybrid|Motor Generator|Brake' -NoRaw
```

The helper is read-only and relaunches under 32-bit PowerShell because the relevant Techstream libraries are 32-bit.

## Hybrid Control direct-DID candidates

| Signal | Request | Response bits | Conversion | Confidence |
|---|---|---:|---|---|
| Accelerator Position | `22 10 60` | 0-7 | `raw_u8 * 0.5 %` | Validated through composite DID; direct request not replayed. |
| FR Wheel Speed | `22 10 75` | 0-15 | `(raw_u16_be - 32768) / 100 km/h` | Observed at 0 km/h while stationary; rolling correlation pending. |
| FL Wheel Speed | `22 10 75` | 16-31 | same | Observed at 0 km/h while stationary; rolling correlation pending. |
| RR Wheel Speed | `22 10 75` | 32-47 | same | Observed at 0 km/h while stationary; rolling correlation pending. |
| RL Wheel Speed | `22 10 75` | 48-63 | same | Observed at 0 km/h while stationary; rolling correlation pending. |
| Steering Angle | `22 10 90` | 0-15 | `(raw_u16_be - 32768) / 10 deg` | Correlated with repeated steering nudges, -25.5 to +21.0 degrees. |
| Generator Revolution (MG1) | `22 10 A0` | 0-15 | `raw_u16_be - 32768 rpm` | Validated through composite DID; direct request not replayed. |
| Motor Revolution (MG2) | `22 10 A1` | 0-15 | `raw_u16_be - 32768 rpm` | Validated through composite DID; direct request not replayed. |
| Rear Motor Revolution | `22 10 A2` | 0-15 | `raw_u16_be - 32768 rpm` | Candidate. |
| Master Cylinder Control Torque | `22 10 67` | 0-15 | `raw_u16_be * 0.125 - 4096 Nm` | Correlated with brake-pedal effort, 0 to -1024 Nm; scale not display-validated. |
| Requested MG regenerative torque | `22 10 69` | 0-15 | `raw_u16_be * 0.125 - 4096 Nm` | Observed at 0 Nm in Park; rolling correlation pending. |
| Executed MG regenerative torque | `22 10 69` | 16-31 | same | Observed at 0 Nm in Park; rolling correlation pending. |
| Brake Cancel Switch | `22 10 43` | bit 14 | enumerated off/on | Correlated with stronger/longer pedal application; exact behavioral threshold pending. |
| Stop Light Switch | `22 10 43` | bit 15 | enumerated off/on | Correlated with brake-pedal contact. |

## Brake/EPB direct-DID candidates

| Signal(s) | Request | Response bits | Conversion/use | Confidence |
|---|---|---:|---|---|
| FR/FL/RR/RL Target Oil Pressure | `22 10 22` | four bytes, 0-31 | `raw_u8 * 40 / 51 / 10 MPa`; commanded per wheel | Candidate. |
| Stop Light SW | `22 10 2D` | bit 10 | driver brake-switch state | Candidate. |
| Regenerative Cooperation | `22 10 2D` | bit 78 | regen-cooperation active | Candidate. |
| Reaction Force Pressure | `22 10 48` | 0-7 | `raw_u8 * 0.096 MPa` | Candidate. |
| Stroke Sensor / Stroke Sensor2 | `22 10 4A` / `22 10 4B` | 0-7 | `raw_u8 / 51 V` | Candidate. |
| Quantity of Brake Pedal Stroke | `22 10 4C` | 0-7 | `raw_u8 mm` | Candidate. |
| Servo Pressure | `22 10 4D` | 0-7 | `raw_u8 * 0.096 MPa`; actual system pressure proxy | Candidate. |
| FR Regenerative Request / Operation | `22 10 54` / `22 10 55` | 0-15 | `raw_u16_be * 16 Nm` | Candidate. |

`Target Oil Pressure` is a command. `Servo Pressure` is an actual pressure measurement but system-level rather than per-caliper. A future brake display can combine pedal stroke, regen cooperation/torque, target wheel pressures, and servo pressure after live validation.

## Validated Hybrid Control conversation

### Evidence captures

- `j2534_08082026034222.log`: 4,289,308 bytes; setup and extended running data.
- `j2534_08082026034256.log`: 302,951 bytes; 353 consecutive steady-state polls after the first snapshot.

The logs reside under `C:\Users\Public\Documents\Techstream\ErrorReport`.

### Transport

```text
TX CAN ID: 0x7D2
RX CAN ID: 0x7DA
Protocol:  ISO15765 at 500 kbit/s
TX flag:   ISO15765_FRAME_PAD

FLOW_CONTROL_FILTER
  mask:         FF FF FF FF
  receive:      00 00 07 DA
  flow control: 00 00 07 D2
```

### Setup and definition

```text
TX  3E 00
RX  7E 00
TX  10 01
RX  50 01 00 32 01 F4
TX  10 03
RX  50 03 00 32 01 F4

TX  2C 03 F3 01
RX  6C 03 F3 01
TX  2C 03 F3 02
RX  6C 03 F3 02

TX  2C 01 F3 01  10 A0 01 02  10 A1 01 02  10 60 01 01  1F 01 01 01
RX  6C 01 F3 01
```

The last request defines dynamic DID `F301`:

| Composite bytes | Source slice | Meaning | Decode |
|---:|---|---|---|
| 0-1 | DID `10A0`, position 1, length 2 | MG1 / Generator Revolution | `u16_be - 32768 rpm` |
| 2-3 | DID `10A1`, position 1, length 2 | MG2 / Motor Revolution | `u16_be - 32768 rpm` |
| 4 | DID `1060`, position 1, length 1 | Accelerator Position | `u8 * 0.5 %` |
| 5 | DID `1F01`, position 1, length 1 | auxiliary/validity item | Unknown. |

### Repeated poll

```text
TX  22 F3 01
RX  62 F3 01  <MG1_hi MG1_lo> <MG2_hi MG2_lo> <accelerator> <aux>
```

Observed in the later snapshot:

- 353 request/response pairs over 33.68 seconds;
- median request interval 96 ms, approximately 10.4 samples/s;
- interval range 79-160 ms;
- median host-observed complete-response latency 48 ms;
- latency range 31-59 ms;
- no negative responses.

A later independent PCAN-Basic raw-CAN implementation reproduced the same setup and dynamic DID without J2534. Its first five live polls completed at a 100-ms minimum period with 13-17-ms complete-response latency. During light accelerator input in Park, decoded MG1 was 5,367-5,383 rpm, MG2 was -3 to +1 rpm, accelerator was 20.5-21.5%, and the auxiliary byte was zero. The raw ISO-TP exchange included correct flow control in both directions.

The next PCAN run completed 100 samples at the same configured period. In the retained sample-29-through-100 excerpt, median latency was 14 ms, p95 was 17 ms, and maximum was 27 ms. With the powertrain at rest, MG1 varied from -2 to +1 rpm and MG2 from -2 to +2 rpm. Techstream displays the same approximately two-rpm zero-speed variation, supporting the signed integer decode and showing that the variation originates in the reported signal rather than the PCAN client.

The larger capture showed MG1 approximately -18 to 6413 rpm, MG2 near zero while stationary, and accelerator 0-26%. The synchronized Techstream display agreed, validating the field layout and conversions for this vehicle/profile.

The parser can learn the dynamic definition from the earlier log and apply it to the later snapshot:

```powershell
python ..\telemetry-tools\tools\j2534\parse-j2534-log.py `
  'C:\Users\Public\Documents\Techstream\ErrorReport\j2534_08082026034256.log' `
  --timeline `
  --definitions-from 'C:\Users\Public\Documents\Techstream\ErrorReport\j2534_08082026034222.log'
```

## Remaining decoding work

- Name and understand source DID `1F01` and its auxiliary byte.
- Test the three source DIDs through independent direct reads.
- Correlate wheel speeds and regenerative torque during a controlled rolling/deceleration capture.
- Compare steering and master-cylinder torque against synchronized Techstream display values before promoting their scaling to Validated.
- Add actual hydraulic pressure from the brake ECU if a friction-application indicator is still desired.
- Attach all mappings to the exact vehicle and calibration profile.

## Direct motion-DID observation

The independent client now provides `--motion-probe`, which enters the proven Hybrid Control session and alternates the two allowlisted direct reads at a minimum 100-ms request-start interval.

Stationary live responses on 2026-08-08 were:

```text
TX  22 10 75
RX  62 10 75 80 00 80 00 80 00 80 00

TX  22 10 90
RX  62 10 90 7F E2
```

Using the locally extracted conversions, these decode as:

```text
FR/FL/RR/RL wheel speed: 0.00 / 0.00 / 0.00 / 0.00 km/h
Steering angle:          -3.0 degrees
```

Five initial samples and a later 75-sample session returned the same values without negative responses or transport failures. A coordinated 150-sample session then recorded repeated small steering nudges through 32 distinct values from -25.5 to +21.0 degrees while all four wheel speeds remained zero. This promotes steering angle to **Correlated**. Scaling still needs a synchronized Techstream comparison for **Validated**, and wheel-speed validation requires a controlled rolling capture.

```powershell
.\tools\validation\Xa50.J2534\bin\Release\xa50-j2534.exe `
  --live --motion-probe --samples 20 --period-ms 100 --verbose
```

The same direct reads are implemented over PCAN raw-CAN ISO-TP with a separate per-mode allowlist. The first stationary live test completed five paired samples:

```powershell
.\tools\validation\Xa50.Pcan\bin\Release\telemetry-pcan.exe `
  --live --motion --samples 5 --period-ms 100 --verbose
```

Every response matched the expected payload. All four wheel speeds remained 0.00 km/h and steering remained exactly -3.0 degrees across the five samples. Wheel-response latency was 12-19 ms, while steering-response latency was 7-9 ms. This independently reproduces the stationary values but adds no new steering movement evidence; any possible steering touch during the separate `F302` run was not sampled.

## Packed Hybrid Control brake observation

Direct reads established the response shape and pedal correlation for:

```text
1043: two-byte switch word
1067: Master Cylinder Control Torque
1069: requested and executed MG regenerative torque
```

Three sequential direct requests produced approximately 3.3 combined samples/s and could disagree briefly during transitions because the fields were sampled about 100 ms apart. The client therefore defined dynamic DID `F302`:

```text
TX  2C 03 F3 02
RX  6C 03 F3 02

TX  2C 01 F3 02  10 43 01 02  10 67 01 02  10 69 01 04
RX  6C 01 F3 02
```

Composite layout:

| Composite bytes | Source slice | Meaning | Decode |
|---:|---|---|---|
| 0-1 | DID `1043`, position 1, length 2 | switch word | bit 15 stop-light; bit 14 brake-cancel |
| 2-3 | DID `1067`, position 1, length 2 | Master Cylinder Control Torque | `u16_be * 0.125 - 4096 Nm` |
| 4-5 | DID `1069`, position 1, length 2 | requested MG regenerative torque | same |
| 6-7 | DID `1069`, position 3, length 2 | executed MG regenerative torque | same |

The repeated exchange is:

```text
TX  22 F3 02
RX  62 F3 02 <switch:2> <master:2> <regen-request:2> <regen-executed:2>
```

The ECU accepted the definition and returned synchronized samples at approximately 10 Hz. During repeated pedal presses in Park:

- the switch word progressed from `0300` (released), through `0301` (stop-light on), to `0303` (both bits on);
- Master Cylinder Control Torque moved continuously from 0 to -1024 Nm and returned to zero on release;
- requested and executed regenerative torque remained 0 Nm, which is expected while stationary in Park;
- 150 packed polls completed without negative responses or transport failures.

This makes stop-light state and master-cylinder torque **Correlated**, not yet display-validated. Master-cylinder control torque represents braking request/control, not physical pad contact. The regenerative fields require a controlled rolling/deceleration capture.

```powershell
.\tools\validation\Xa50.J2534\bin\Release\xa50-j2534.exe `
  --live --packed-brake --samples 100 --period-ms 100 --verbose
```

The PCAN client reproduces the same `F302` definition, fragmentation, polling, and decoder under its own strict allowlist. The first stationary live test completed five samples during one brake application:

```powershell
.\tools\validation\Xa50.Pcan\bin\Release\telemetry-pcan.exe `
  --live --f302 --samples 5 --period-ms 100 --verbose
```

Every setup and poll response was positive and the expected ISO-TP flow-control exchange completed. Switch word `0x0303` was already active in the first sample, so this run contains no released baseline. Master-cylinder control torque then progressed monotonically from -164.250 through -315.125, -457.125, and -532.500 to -696.750 Nm. Requested and executed regenerative torque remained 0 Nm in Park. Complete-response latency was 12-18 ms with a 15-ms mean and median. This is a strong independent PCAN confirmation of the previously established brake-input correlation; it is not evidence of physical pad engagement.

Decoding a log against XA50 signal names needs a profile in the parser's
`--signals` format. The hand-maintained one was removed because it duplicated
catalog bit ranges with nothing keeping the two in sync; build one from
`data/catalog/telemetry.csv` if you need it.
