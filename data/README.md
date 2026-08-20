# Data

Three directories, by what the file *is* rather than which workflow phase
produced it:

| Directory | Kind | Hand-written or generated | In Git |
|---|---|---|---|
| [`config/`](config/) | Inputs you fill in | Hand-written, verified by use | Yes |
| [`map/`](map/) | The deliverable | Hand-curated from reviewed evidence | Yes |
| [`catalog/`](catalog/) | Machine output | Generated from the OEM database | Mostly no |

If you only want a working formula for a signal, you want
[`map/signals.csv`](map/signals.csv) and nothing else.

## config/ — verified inputs

Everything here was written by hand and proven by producing the results in
`map/`. These are the files a fork would copy and edit for its own vehicle.

| File | Purpose | Consumed by |
|---|---|---|
| [`validation-profile.json`](config/validation-profile.json) | Endpoint, DID range, tier and privacy policy, paths, backend command | every toolkit CLI |
| [`backend-config.json`](config/backend-config.json) | CAN bitrate, tester/response IDs, session setup, device selection | `telemetry-pcan`, `telemetry-j2534` |
| [`ecu-profile.json`](config/ecu-profile.json) | The sixteen probe-confirmed ECU keys, their databases, endpoints, and live-state notes | `build-catalog.py` |
| [`decode-overrides.json`](config/decode-overrides.json) | Reviewed corrections to OEM catalog formulas | `telemetry-analyze-capture`, `telemetry-review`, `telemetry-map` |
| [`curation.csv`](config/curation.csv) | **The only hand-written part of the map**: reviewed evidence state and note per signal, keyed by `ecu_key` + `data_id` + bit range | `telemetry-map` |
| [`experiments/*.toml`](config/experiments/) | Named follow-up batches with an explicit purpose and vehicle state | `telemetry-validate experiment` |

`validation-profile.json` and `backend-config.json` both describe the endpoint
and must agree; the toolkit validates every capture against the profile, so a
mismatch surfaces as a rejected capture rather than as bad data.

## map/ — the result

Both CSVs are **generated** by `telemetry-map` from the catalog, the completed
captures, and `config/curation.csv`. Do not edit them by hand — edit
`curation.csv` and regenerate, or the next run overwrites your change.

| File | Contents |
|---|---|
| [`signals.csv`](map/signals.csv) | Every catalog signal on a DID this vehicle answers: DID, request bytes, bit range, conversion, unit, **evidence state**, what was observed, and a note. |
| [`unsupported.csv`](map/unsupported.csv) | Every DID this vehicle would not return, with its negative-response code. Recorded because another car of the same platform may well have them. |
| [`transport.json`](map/transport.json) | Hand-maintained. Every ECU's endpoint and the dynamic-DID definitions — only what cannot be expressed per signal. |

The `observation` column says what the captures actually showed:

| Value | Meaning |
|---|---|
| `varied` | Changed across two or more independent runs. |
| `single_run` | Observed in one run only; no independent confirmation. |
| `constant` | Never changed in any capture. The formula may still be correct — it simply was not exercised. |
| `beyond_response` | The catalog maps this field past the end of the reply this vehicle sends. Absent from this calibration; present on a variant with more hardware. |
| `unobserved` | The DID answers, but no capture has covered this field. |

Observed minima, maxima, and value counts are deliberately **not** published.
They are captured readings — a key-cycle count, a state of charge — and this
file exists to define signals, not to report this car's state. The full
analysis stays local:

```powershell
telemetry-review --config data\config\validation-profile.json `
  --csv-out scratch\analysis\ledger.csv
```

`review_status` separates the two kinds of row: `reviewed` means a human wrote
that evidence state in `curation.csv`; `auto` means it was derived from
observation and never rises above *Observed and replayed*. Nothing promotes a
signal to Correlated or Validated except a person.

Read `evidence_state` before using a row. The words are the template's and
they are not interchangeable:

| State | Means |
|---|---|
| Observed | The bytes were seen and reproduced. Nothing about whether the value is right. |
| Correlated | A stimulus moved it in the expected direction and timing. |
| Validated | Decoded values matched an independent reference at several points. |
| Replayed | An independent client reproduced the exchange. |

A field that never changed during capture cannot be Correlated, however
plausible its values look.

Hold the map to the observations at any time:

```powershell
telemetry-review --config data\config\validation-profile.json `
  --check data\map\signals.csv
```

This is one tested vehicle. Calibration and market differences change
availability, scaling, and validity — re-verify before relying on a row.

## catalog/ — generated

Built from the local Techstream ECU databases by `build-catalog.py`. Rows here
are *Candidate* evidence: names and bit ranges the OEM database claims, not
things this vehicle was observed to answer.

| File | In Git | Why |
|---|---|---|
| `manifest.json` | Yes | Provenance: generator, hash, Techstream version, region. |
| `summary.md` | Yes | What the last build produced. |
| `telemetry.csv`, `dids.csv`, `telemetry.json` | No | A bulk OEM-database-derived atlas; redistribution rights are unreviewed. Regenerate locally — see [reproduce.md](../docs/reproduce.md). |

The toolkit reads `telemetry.csv`, so the plan and analyzer need a local build
even though the file is not committed.

## Not here

Raw captures, generated plans, backend binaries, and reverse-engineering state
live in the ignored `scratch/` tree. Nothing in `data/` is a capture; nothing
here contains a VIN, location, or adapter serial. See
[PRIVATE_DATA.md](../PRIVATE_DATA.md).
