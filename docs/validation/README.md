# XA50 validation status and policy

Procedure lives in the toolkit's [quickstart](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/quickstart.md)
and [progressive validation](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/progressive-validation.md).
This page records what is XA50-specific: the scope, the policy choices in
[`validation-profile.json`](../../data/config/validation-profile.json), and
where the campaign currently stands.

## Scope

**Twelve ECUs, nine endpoints.** A catalog-wide probe found sixteen ECUs that
answer a stationary tester-present frame. Twelve of them implement
ReadDataByIdentifier and are in
[`validation-profile.json`](../../data/config/validation-profile.json); their
plans total **2,025 direct DIDs**. Several share one diagnostic address —
Techstream models some ECUs as separate databases over the same endpoint — so
twelve ECUs occupy nine endpoints.

The other four answer the probe but reject service `0x22` outright, so they
contribute no signals; see [below](#ecus-that-answer-but-cannot-be-read).

Hybrid Control (key 397, `0x7D2 -> 0x7DA`) is the most complete: every DID it
answers has also been read on the road. The other eleven are qualified but only
partly road-captured; `telemetry-validate status` prints where each stands.

This is not an ECU scanner. Every transmitted DID comes from the reviewed plan,
and the backend enforces a byte-exact transmit allowlist built from the DIDs
passed to it.

## Tier policy

The tier applies to a whole DID, not an individual field: a payload containing
any speed/current/torque-like value uses the fast policy even if it also carries
slow fields. Tiering is scheduling metadata only — it is not evidence.

| Tier | DIDs | Request spacing | Batch duration | Minimum samples per DID | DIDs per batch |
| --- | ---: | ---: | ---: | ---: | ---: |
| fast | 760 | 100 ms | 120 s | 100 | 9 |
| normal | 840 | 250 ms | 300 s | 120 | 8 |
| slow | 316 | 1,000 ms | 900 s | 60 | 12 |
| archive | 109 | 2,000 ms | 600 s | 20 | 12 |

DIDs per batch costs no bus traffic — one request is outstanding at a time, so
the request rate is set by the spacing — but it divides that rate across more
identifiers. Each cap keeps at least a 1.25x margin over the tier's minimum
sample count.

Road mode defaults to fast, normal, and slow. Archive is opt-in, and on this
vehicle there is nothing there to collect:

- 96 of the 109 archive candidates returned NRC `0x31` at stationary
  qualification — not supported by this calibration.
- The 13 that answered are counters and histories: seven Engine DIDs, one each
  on Motor Generator, Hybrid Control, and Combination Meter, and three on
  Brake/EPB. They are in [`signals.csv`](../../data/map/signals.csv) as
  *Candidate*, since nothing has read them on the road.
- Hybrid Control's `0x1010` is the clearest case for leaving the tier out. It
  returns 8 data bytes, and the catalog maps its only two signals (Rock and
  Dirt Switch History, Mud and Sand Switch History) at bits 73 and 74 — past
  the end of that response. A road capture would record samples with nothing
  decodable in them.

Archive fields change over a vehicle's life rather than over a drive, so a
bounded batch is the wrong instrument for them either way. Include the tier
explicitly if you want them: `telemetry-validate road --tiers archive`.

## Selection policy

The planner skips DIDs outside `0x0100`-`0xEFFF`; `0x0000` and `0xFFFE` are
UDS-reserved identifiers rather than telemetry.

There is no privacy word list. An earlier revision withheld odometer, clock,
and trip-history DIDs from the plan, which cost coverage without protecting
anything: every capture lands in the ignored `scratch/` tree, and the published
map contains signal definitions rather than readings. Recording "we chose not
to look at the odometer" in a published CSV was noise, not a safeguard.

What does protect the data is unchanged: captures are never committed, and the
map carries no observed values. See [PRIVATE_DATA.md](../../PRIVATE_DATA.md)
before publishing an example capture — that is where sanitization matters,
because a raw capture of an identity-bearing DID does contain the identifier.

## ECUs that answer but cannot be read

Four of the sixteen ECUs on this vehicle respond at their endpoint yet do not
implement ReadDataByIdentifier, so they contribute no signals and are absent
from `validation-profile.json`:

| ECU | Endpoint | `3E 00` | `10 01` | `10 03` | `22 xxxx` |
|---|---|---|---|---|---|
| 405 EMPS | `0x7A1` | NRC 0x12 | positive, bare `50` | NRC 0x12 | NRC 0x11 x175 |
| 444 SRS Airbag | `0x780` | NRC 0x12 | positive, bare `50` | NRC 0x12 | NRC 0x10 x91 |
| 445 Steering Angle Sensor | `0x7B3` | NRC 0x12 | positive, bare `50` | NRC 0x12 | NRC 0x11 x9 |
| 450 Air Conditioner | `0x7C4` | NRC 0x11 | positive, bare `50` | NRC 0x12 | NRC 0x11 x226 |

Every refusal is at the subfunction or service level and is uniform across all
501 catalog DIDs, so this is not a question of which identifiers exist. A
positive session-control response that omits the subfunction echo, tester
present rejected for its subfunction, and ReadDataByIdentifier rejected
outright are together consistent with a different diagnostic generation from
the twelve ECUs that do answer - but which one is not established here.

The catalog claims `22` reads for all four, so the database and the vehicle
disagree, and the database is not a reliable guide to how these are addressed.
Settling it needs a capture of Techstream reading one of them, the same route
that recovered the session setup in the first place. See
[open questions](../open-questions.md).

Their endpoints remain in `backend-config.json` and in `ecu-profile.json`, so
nothing has to be rediscovered when that work happens.

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
`telemetry-validate status --config data/config/validation-profile.json`:

| Measure | Count |
|---|---:|
| Plan candidates | 2,025 |
| Qualified | 553 |
| Terminal negative | 1,472 |
| Road-complete | 360 |
| Pending road capture | 193 |

Road progress is distributed as follows:

| ECU | Qualified DIDs | Road-complete | Pending |
|---|---:|---:|---:|
| 372 Engine | 163 | 69 | 94 |
| 374 Cruise Control | 4 | 4 | 0 |
| 395 Motor Generator | 65 | 62 | 3 |
| 396 Rear Motor Generator | 31 | 31 | 0 |
| 397 Hybrid Control | 131 | 130 | 1 |
| 399 Cruise Control | 10 | 10 | 0 |
| 409 Combination Meter | 42 | 21 | 21 |
| 426 Radar Cruise2 | 20 | 18 | 2 |
| 432 Pre-Collision 2 | 13 | 1 | 12 |
| 435 Brake/EPB | 57 | 9 | 48 |
| 5005 Rear Camera | 5 | 2 | 3 |
| 5013 EV | 12 | 3 | 9 |

The generated map (`telemetry-map --config data/config/validation-profile.json`)
carries 1,846 signals: 12 varied across independent runs, 390 varied within a
single completed run, 655 stayed constant, 48 lie past the end of this
calibration's replies, and 741 have not yet received a road observation. See
the [multi-ECU road review](../evidence/progressive-road-interim-2.md) for the
latest evidence and decode caveats.

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
[`data/config/experiments/`](../../data/config/experiments/); see
[focused custom experiments](custom-experiments.md).

## Promotion

Qualification establishes availability. Road capture establishes repeated raw
observation. Neither promotes a signal. Confidence is raised only after offline
plausibility, relationship, and reference review, and the tooling never edits
[`data/map/signals.csv`](../../data/map/signals.csv).

Hold the curated map to the observations with:

```powershell
telemetry-review --config data\config\validation-profile.json `
  --check data\map\signals.csv
```
