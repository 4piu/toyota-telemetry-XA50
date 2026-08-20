# Reproduce this on your own XA50

Every command needed to rebuild this repository's results from scratch on a
comparable vehicle. This is the field guide's
[quickstart](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/quickstart.md)
with the XA50 blanks filled in.

**Read [scope and safety](00-scope-safety.md)
first.** Everything here is read-only diagnostics. Do not use it for vehicle
control, coding, security access, DTC clearing, or programming.

Your calibration and market may differ. Treat every value in
`data/map/` as a claim to re-verify, not a specification.

## What you need

| | |
|---|---|
| Vehicle | Toyota RAV4 Hybrid XA50 (A25A-FXS). Other Toyota hybrids may share DIDs; verify. |
| Adapter | CANable v1.0 with PCAN-compatible firmware, or a PEAK PCAN-USB. See [CAN adapter](hardware/can-adapter.md). |
| Host | Windows with Python 3.11+ and `uv`. No .NET SDK needed. |
| Optional | Techstream, to rebuild the catalog from the local database. |

## 1. Clone and set up

```powershell
git clone <this-repository> xa50; cd xa50
git clone https://github.com/4piu/70Y074-telemetry.git ..\field-guide
..\field-guide\tools\setup-platform-project.ps1
```

[`telemetry-project.toml`](../telemetry-project.toml) pins the reviewed
field-guide revision, and the setup script reads it: it checks the field guide
out at that revision, builds the backends into this repository's ignored
scratch tree, and installs the toolkit into a local `.venv` from that same
checkout. One revision therefore fixes the Python tools, the backends, and the
Techstream scripts together.

The first clone is optional — the script clones for you if the directory is
absent. It refuses to touch a checkout with uncommitted changes, and `-Offline`
makes it fail rather than reach the network.

```powershell
.\.venv\Scripts\Activate.ps1
telemetry-validate --help
```

Every command below assumes that activation. Nothing here needs the network
again until the pin moves.

## 2. Verify the backend

[`validation-profile.json`](../data/config/validation-profile.json) already
points at `scratch/bin/telemetry-pcan.exe`. Confirm it offline, before any
vehicle:

```powershell
.\scratch\bin\telemetry-pcan.exe --self-test --output scratch\analysis\selftest.jsonl
```

## 3. Rebuild the catalog (optional)

[`data/catalog/`](../data/catalog/) ships only the summary
and manifest; the full atlas is derived from the OEM database and is not
redistributed. With Techstream installed:

```powershell
..\field-guide\tools\techstream\query-techstream-db.ps1 -Catalog     # list every ECU key
python ..\field-guide\tools\catalog\build-catalog.py --profile data\config\ecu-profile.json
```

[`ecu-profile.json`](../data/config/ecu-profile.json) records the sixteen
probe-confirmed ECU keys, their endpoints, and which implement
ReadDataByIdentifier. Compare your `summary.md` against the committed
one; a different Techstream version or market will produce different counts.

## 4. Prove the connection

Stationary, in Park, parking brake on, IG-ON/READY, with PCAN-View closed.

```powershell
$cfg = "data\config\backend-config.json"

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
$profile = "data\config\validation-profile.json"

telemetry-build-plan --config $profile
telemetry-validate status  --config $profile
telemetry-validate qualify --config $profile               # preview
telemetry-validate qualify --config $profile --live --max-items 25
```

Qualification is stationary and reads each DID exactly once, round-robin across
every ECU in the profile. Expect roughly 553 of 2025 to qualify; most negatives
are NRC `0x31` for equipment this vehicle does not have. See
[validation policy](validation/README.md).

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

## 7. Review, regenerate the map, and compare

```powershell
telemetry-review --config $profile --ecu 397 --pending   # what still needs excitation
telemetry-map    --config $profile                # regenerate signals.csv + unsupported.csv
```

`telemetry-map` builds the map from the catalog, the completed captures, and
your hand-written `data\config\curation.csv`. It never raises an evidence state
on its own, and refuses to write if a curated row claims more than the captures
support.

The ledger is the point of comparison. If your vehicle produces a materially
different set of qualified DIDs or decoded ranges, that is a finding about
calibration or market differences — record it rather than assuming this
repository is authoritative.

## 8. Follow up on the doubtful signals

```powershell
telemetry-validate experiment --config $profile `
  --experiment data\config\experiments\stationary-controls-v1.toml
```

Two reviewed schedules ship here: stationary selector and cabin-switch passes,
and extended ordinary-driving batches. See
[focused custom experiments](validation/custom-experiments.md).

## Where results live

| Path | Contents |
|---|---|
| [`data/map/signals.csv`](../data/map/signals.csv) | The curated map, with an evidence state per signal. |
| [`data/map/unsupported.csv`](../data/map/unsupported.csv) | Every DID this vehicle refused, with its NRC. Yours may differ. |
| [`data/map/transport.json`](../data/map/transport.json) | Endpoint and dynamic-DID definitions — the parts not expressible per signal. |
| [`data/config/curation.csv`](../data/config/curation.csv) | The hand-written evidence states and notes behind the map. |
| [`data/config/decode-overrides.json`](../data/config/decode-overrides.json) | Reviewed corrections to OEM catalog formulas. |
| [`docs/findings/`](findings/) | How the endpoints, transport, and formulas were established. |
| [`docs/evidence/`](evidence/) | Capture integrity and correlation results per campaign. |

Raw captures are not published. `scratch/` is ignored in full.

## 9. Extend to ECUs this repository has not found

The endpoints here came from one sweep of the catalog's own protocol table.
A different market or calibration may expose others. Probe for them:

```powershell
telemetry-probe --catalog data\catalog\telemetry.csv `
  --base-config data\config\backend-config.json `
  --base-profile data\config\validation-profile.json --live `
  --emit-ecus scratch\analysis\new-ecus.json
```

Each probe sends exactly one tester-present frame and accepts only the expected
reply. `--emit-ecus` writes the `ecus` entries for whatever answered; add them
to `backend-config.json`, and to `validation-profile.json` with a `plan_id` and
capture paths, then run the same qualify/road/review loop. One profile covers
every ECU — they share the transport, being behind the same gateway.

The session setup a new entry inherits is a guess for an ECU never captured
from; if a probe fails while the OBD probe still succeeds, the endpoint or its
session sequence is wrong rather than the wiring.
