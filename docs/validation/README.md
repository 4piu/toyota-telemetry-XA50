# XA50 validation status and policy

Procedure lives in the field guide's [quickstart](https://github.com/4piu/70Y074-telemetry/blob/main/docs/quickstart.md)
and [progressive validation](https://github.com/4piu/70Y074-telemetry/blob/main/docs/06-verify/progressive-validation.md).
This page records what is XA50-specific: the scope, the policy choices in
[`validation-profile.json`](../../data/validation/validation-profile.json), and
where the campaign currently stands.

## Scope

Only the Hybrid Control ECU (key 397) at the live-confirmed `0x7D2 -> 0x7DA`
endpoint. The generated plan contains **588 direct DIDs** covering 1,356 unique
catalog signal names — most of that ECU's 593 static direct DIDs, but not the
4,458 catalog rows across all four ECU databases. Engine, Motor Generator, and
Brake/EPB need endpoint validation before they can use the same runner.

This is not an ECU scanner. Every transmitted DID comes from the reviewed plan,
and the backend enforces a byte-exact transmit allowlist built from the DIDs
passed to it.

## Tier policy

The tier applies to a whole DID, not an individual field: a payload containing
any speed/current/torque-like value uses the fast policy even if it also carries
slow fields. Tiering is scheduling metadata only — it is not evidence.

| Tier | DIDs | Request spacing | Batch duration | Minimum samples per DID |
| --- | ---: | ---: | ---: | ---: |
| fast | 245 | 100 ms | 120 s | 100 |
| normal | 229 | 250 ms | 300 s | 120 |
| slow | 68 | 1,000 ms | 900 s | 60 |
| archive | 46 | 2,000 ms | 600 s | 20 |

Road mode defaults to fast, normal, and slow. Archive is opt-in.

## Privacy selection

The planner never schedules reserved or client-disallowed DIDs, and never
schedules a DID whose catalog labels indicate a VIN or other identity-bearing
field. Three trip/time-history DIDs are excluded by default; `--include-private`
admits them to a private workbench plan, and their raw captures must not be
committed. See [PRIVATE_DATA.md](../../PRIVATE_DATA.md).

## Negative-response policy

A negative reply means the read was refused in the tested session and vehicle
state — not that the DID is absent. Static database aggregation means many
candidates describe equipment this vehicle does not have, which is expected.

| NRC | Recorded classification | Interpretation |
|---|---|---|
| `0x21` | `busy_retry` | ECU was busy; retry while stationary. |
| `0x22` | `conditions_not_correct` | Refused in this vehicle state; may succeed in another. |
| `0x31` | `request_out_of_range` | Not supported by this calibration. Terminal by default. |
| `0x33` | `security_denied` | Requires security access this project does not perform. Terminal. |
| `0x7E`/`0x7F` | `session_restricted` | Not available in the session used. |

`0x31` and `0x33` are terminal unless `--retry-terminal` is passed deliberately.
A timeout or transport fault is **never** recorded as unsupported; the batch
stays incomplete and is retried.

## Current state

As of the last review — regenerate with
`telemetry-validate status --config data/validation/validation-profile.json`:

| Measure | Count |
|---|---:|
| Plan candidates | 588 |
| Qualified | 128 |
| Terminal negative | 460 |
| Road-complete | 127 |
| Pending road capture | 1 |

The evidence ledger over all completed captures
(`telemetry-review --config data/validation/validation-profile.json`) reports
291 observed catalog signals: 12 varied across independent runs, 137 seen in a
single run, and 142 constant in every run so far.

Constant is an excitation result, not a defect. A short batch can legitimately
miss selector changes, indicator states, charge/thermal transitions, and other
event-driven fields.

## What still needs work

Use `telemetry-review --pending` for the live list. Before scheduling anything
from it, classify by testability:

- **Safe controlled** — a parked or ordinary-driving action gives a clear
  reference: selecting ordinary gear positions while stationary, operating turn
  signals, cycling door locks while parked.
- **Passive extended observation** — the state occurs naturally but must not be
  forced. Capture longer across normal trips and promote only if an event and
  the decoded transition can be aligned.
- **Do not induce** — TCS/VSC intervention, PCS/AEB activation, ABS, collision
  and airbag-related states. These stay untested unless a naturally occurring
  event or a proper test facility provides evidence.

Never read an absence of transitions as a negative test for the latter two.

Reviewed follow-up schedules live in
[`data/validation/experiments/`](../../data/validation/experiments/); see
[focused custom experiments](custom-experiments.md).

## Promotion

Qualification establishes availability. Road capture establishes repeated raw
observation. Neither promotes a signal. Confidence is raised only after offline
plausibility, relationship, and reference review, and the tooling never edits
[`data/validated/signals.csv`](../../data/validated/signals.csv).

Hold the curated map to the observations with:

```powershell
telemetry-review --config data\validation\validation-profile.json `
  --check data\validated\signals.csv
```
