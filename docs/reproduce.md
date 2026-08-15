# Reproduce this on your own XA50

Every command needed to rebuild this repository's results from scratch on a
comparable vehicle. This is the field guide's
[quickstart](https://github.com/4piu/70Y074-telemetry/blob/main/docs/quickstart.md)
with the XA50 blanks filled in.

**Read [scope and safety](https://github.com/4piu/70Y074-telemetry/blob/main/docs/00-scope-safety.md)
first.** Everything here is read-only diagnostics. Do not use it for vehicle
control, coding, security access, DTC clearing, or programming.

Your calibration and market may differ. Treat every value in
`data/validated/` as a claim to re-verify, not a specification.

## What you need

| | |
|---|---|
| Vehicle | Toyota RAV4 Hybrid XA50 (A25A-FXS). Other Toyota hybrids may share DIDs; verify. |
| Adapter | CANable v1.0 with PCAN-compatible firmware, or a PEAK PCAN-USB. See [CAN adapter](hardware/can-adapter.md). |
| Host | Windows with Python 3.11+ and `uv`. No .NET SDK needed. |
| Optional | Techstream, to rebuild the catalog from the local database. |

## 1. Clone and pin

```powershell
git clone <this-repository> xa50; cd xa50
```

[`telemetry-project.toml`](../telemetry-project.toml) already pins the reviewed
field-guide revision, and [`toolkit-requirement.txt`](../toolkit-requirement.txt)
pins the toolkit package. Do not substitute a branch name for the commit.

```powershell
$toolkit = (Get-Content .\toolkit-requirement.txt -Raw).Trim()
uvx --from $toolkit telemetry-validate --help
```

## 2. Build the backend

Clone the field guide at the pinned revision and build straight into this
repository's ignored scratch tree:

```powershell
git clone https://github.com/4piu/70Y074-telemetry.git ..\field-guide
cd ..\field-guide; git checkout <pinned revision>
.\backends\build.ps1 -OutputDirectory ..\xa50\scratch\bin
cd ..\xa50
```

[`validation-profile.json`](../data/validation/validation-profile.json) already
points at `scratch/bin/telemetry-pcan.exe`. Verify offline:

```powershell
.\scratch\bin\telemetry-pcan.exe --self-test --output scratch\analysis\selftest.jsonl
```

## 3. Rebuild the catalog (optional)

[`data/catalog/generated/`](../data/catalog/generated/) ships only the summary
and manifest; the full atlas is derived from the OEM database and is not
redistributed. With Techstream installed:

```powershell
..\field-guide\tools\techstream\query-techstream-db.ps1 -Keys 397,395,435,466 -Catalog
python ..\field-guide\tools\catalog\build-catalog.py --profile data\catalog\ecu-profile.json
```

[`ecu-profile.json`](../data/catalog/ecu-profile.json) records the four ECU keys
and which are live-confirmed. Compare your `summary.md` against the committed
one; a different Techstream version or market will produce different counts.

## 4. Prove the connection

Stationary, in Park, parking brake on, IG-ON/READY, with PCAN-View closed.

```powershell
$cfg = "data\validation\backend-config.json"

.\scratch\bin\telemetry-pcan.exe --config $cfg --probe              # offline plan
.\scratch\bin\telemetry-pcan.exe --config $cfg --obd-probe --live   # wiring and timing
.\scratch\bin\telemetry-pcan.exe --config $cfg --probe --live       # gateway routing
```

Expect the OBD probe to return `06 41 00 ...` from `0x7E8`-`0x7EF` in a few
milliseconds, and the endpoint probe to return `7E 00` from `0x7DA`.

If the OBD probe fails, the fault is electrical or bit timing, not the ECU —
on this project it was CAN-L crossed with ground, and the adapter LED blinked
happily throughout. See [CAN adapter](hardware/can-adapter.md).

## 5. Plan and qualify

```powershell
$profile = "data\validation\validation-profile.json"

telemetry-build-plan --config $profile
telemetry-validate status  --config $profile
telemetry-validate qualify --config $profile               # preview
telemetry-validate qualify --config $profile --live --max-items 25
```

Qualification is stationary and reads each DID exactly once. Expect roughly 128
of 588 to qualify; most negatives are NRC `0x31` for equipment this vehicle does
not have. See [validation policy](validation/README.md).

You do not have to qualify everything before driving. Qualify a slice, drive it,
repeat.

## 6. Capture on the road

Read the [parking-lot protocol](validation/road-test.md) first. Arrange a
passenger or an unattended logger. **Do not operate the computer while driving.**

```powershell
telemetry-validate road --config $profile --max-items 4     # preview
telemetry-validate road --config $profile --live --max-items 4
```

Interrupting is safe: completed batches remain progress and only the active
capture is quarantined.

## 7. Review and compare

```powershell
telemetry-review --config $profile
telemetry-review --config $profile --pending
telemetry-review --config $profile --check data\validated\signals.csv
```

The ledger is the point of comparison. If your vehicle produces a materially
different set of qualified DIDs or decoded ranges, that is a finding about
calibration or market differences — record it rather than assuming this
repository is authoritative.

## 8. Follow up on the doubtful signals

```powershell
telemetry-validate experiment --config $profile `
  --experiment data\validation\experiments\stationary-controls-v1.toml
```

Two reviewed schedules ship here: stationary selector and cabin-switch passes,
and extended ordinary-driving batches. See
[focused custom experiments](validation/custom-experiments.md).

## Where results live

| Path | Contents |
|---|---|
| [`data/validated/signals.csv`](../data/validated/signals.csv) | The curated map, with an evidence state per signal. |
| [`data/validated/hybrid-control-signals.json`](../data/validated/hybrid-control-signals.json) | The same map plus transport and dynamic-DID definitions, for applications. |
| [`data/validated/decode-overrides.json`](../data/validated/decode-overrides.json) | Reviewed corrections to OEM catalog formulas. |
| [`docs/findings/`](findings/) | How the endpoints, transport, and formulas were established. |
| [`docs/evidence/`](evidence/) | Capture integrity and correlation results per campaign. |

Raw captures are not published. `scratch/` is ignored in full.
