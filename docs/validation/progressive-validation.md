# Progressive catalog validation

This workflow turns the static Hybrid Control catalog into resumable stationary
qualification and road-capture work. It is intended for many sessions: progress
is reconstructed from completed JSONL captures rather than a mutable checklist,
so adding candidates or rebuilding the plan does not erase prior work.

The reusable planner, analyzer, progress engine, and non-baseline runner now
live in the pinned `toyota-telemetry-tools` package. This repository's
[`validation-profile.json`](../../data/validation/validation-profile.json)
contains the XA50 endpoint and scheduling policy. The commands below retain the
live-proven local entry point until the pinned toolkit revision is published and
passes a live parity run; new platform projects should use the generic CLI
documented by the field guide.

After the pinned toolkit revision is available from its canonical repository,
the equivalent generic status and dry-run commands are:

```powershell
$toolkit = (Get-Content .\toolkit-requirement.txt -Raw).Trim()
uvx --from $toolkit telemetry-validate status `
  --project-root . --config data/validation/validation-profile.json
uvx --from $toolkit telemetry-validate road `
  --project-root . --config data/validation/validation-profile.json
```

Do not substitute an unpinned branch name for the reviewed commit.

## Current scope

The current PCAN client is hard-coded to the live-confirmed Hybrid Control route
`0x7D2 -> 0x7DA`. The generated default plan contains 588 direct DIDs covering
1,356 unique catalog signal names:

| Tier | DIDs | Request spacing | Batch duration | Minimum samples per DID |
| --- | ---: | ---: | ---: | ---: |
| fast | 245 | 100 ms | 120 s | 100 |
| normal | 229 | 250 ms | 300 s | 120 |
| slow | 68 | 1,000 ms | 900 s | 60 |
| archive | 46 | 2,000 ms | 600 s | 20 |

The tier is assigned to a complete DID, not an individual field. A payload with
at least one speed/current/torque-like value therefore uses the fast policy even
if it also contains slow fields. Temperature, pressure, SOC, and accumulator
families are sampled more slowly. Tiering is only scheduling metadata; it is not
evidence and can be reviewed in the private generated plan.

This covers most of the Hybrid Control ECU's 593 static direct DIDs, not all
4,458 catalog rows across four ECU databases. Engine, Motor Generator, and
Brake/EPB require endpoint validation and a parameterized transport client
before they can use the same runner.

## Privacy and selection

The planner never schedules reserved/client-disallowed DIDs `0x0000` or
`0xFFFE`. It also never schedules a DID whose catalog labels indicate a VIN or
other identity-bearing field. Three trip/time-history DIDs are excluded by
default; `--include-private` admits them to a private workbench plan. Their raw
captures must not be committed.

Static database aggregation means many candidates describe equipment absent
from this vehicle. That is expected. Stationary qualification cheaply separates
positive responses from UDS negatives before any road polling.

## Build or refresh the private plan

The default output is
`scratch/plans/vehicle-a/hybrid-control-progressive.json`:

```powershell
uv run python .\tools\validation\build-progressive-plan.py

# Rebuild after the catalog/evidence metadata changes:
uv run python .\tools\validation\build-progressive-plan.py --force
```

The plan contains catalog provenance, candidate names, estimated response size,
tier, prior-evidence flag, and exclusion reasons. It contains no raw vehicle
response. Keep it private because it is a derived slice of the OEM catalog.

## Inspect progress

```powershell
uv run python .\tools\validation\run-progressive-validation.py status
```

The runner scans completed candidate captures below
`scratch/captures/vehicle-a`, including the v1 pilot, and writes a
machine-readable `progress.snapshot.json` below the progressive output tree.
The initial post-pilot state is 16 qualified/road-complete DIDs, four terminal
NRC `0x31` negatives, and 568 untried DIDs.

## Capture the established baseline

The same entry point retains the XA50-specific 13-signal F301/wheel/F302/
steering capture formerly provided by a temporary PowerShell wrapper:

```powershell
uv run python .\tools\validation\run-progressive-validation.py baseline
uv run python .\tools\validation\run-progressive-validation.py baseline --live --duration-seconds 900
```

Baseline captures use separate partial/complete files and the dedicated road
analyzer. They do not alter per-DID progressive completion state.

Progress is per DID. A positive qualification in any complete capture wins over
an earlier state-dependent negative. Only a candidate road capture whose final
record says `reason=completed` and which passes the offline analyzer marks its
DIDs road-complete.

## Add a stationary qualification slice

Dry-run the next 25 DIDs:

```powershell
uv run python .\tools\validation\run-progressive-validation.py qualify
```

Then, stationary in Park and IG-ON/READY with PCAN-View closed:

```powershell
uv run python .\tools\validation\run-progressive-validation.py qualify --live
```

Each DID is qualified in its own completed file. That costs some session setup
time but gives stable per-DID progress when a future plan adds or reorders
candidates. Use `--max-items N` to choose the slice. Conditional negatives are
retried up to two completed attempts; NRC `0x31` and `0x33` are terminal unless
`--retry-terminal` is supplied deliberately.

You can qualify more candidates before each drive, then run only the newly
positive set. There is no requirement to qualify all 588 first.

## Consume qualified DIDs during a drive

Preview up to four next batches:

```powershell
uv run python .\tools\validation\run-progressive-validation.py road --max-items 4
```

At the safe location, start an unattended sequence:

```powershell
uv run python .\tools\validation\run-progressive-validation.py road --live
```

The default road limit is 25 batches per invocation. Batches contain at most six
DIDs from one tier, with one outstanding request at a time. Road mode defaults
to fast, normal, and slow tiers; archive is opt-in because its current sole DID
has no mapped field inside the calibration-specific response length. Fast
batches run first, followed by normal and slow. Select archive deliberately or
narrow a trip with, for example, `--tiers slow,archive --max-items 3`.

The startup delay defaults to ten seconds. Every capture is create-new and
bounded to 16 MiB; the whole progressive tree defaults to 2 GiB and requires an
additional 256 MiB free-space reserve. These can be changed explicitly with
`--startup-delay-seconds`, `--max-log-mib`, and `--max-directory-mib`.

Candidate road polling retries an individual DID transaction twice by default
after a timeout. Each retry waits 100 ms, verifies PCAN status, drains stale
receive frames, and repeats the exact allowlisted request. Retry events and the
successful sample's `retry_count` are written to JSONL. Set
`--transport-retries N` from zero through five to change the bound. A transaction
that still fails after the configured retries ends and quarantines only the
active batch; earlier completed batches remain progress.

## Interruption and completion

Completed earlier batches survive an interruption. The active batch is renamed
to `.incomplete.jsonl` and does not count; the next invocation rebuilds its
batch from the still-pending DIDs. Analyzer summaries follow the same
`.complete.summary.json` or `.incomplete.summary.json` convention.

This is logical discard rather than immediate deletion, retaining enough data
to diagnose a loose cable or transport failure without contaminating evidence.
Incomplete files can be removed from the private workbench after review.

Qualification establishes availability, not telemetry identity. Road capture
establishes repeated raw observations. Confidence is promoted only later after
offline plausibility, relationship, and reference review; the runner never
edits `data/validated/signals.csv`.

## Constant and safely untestable signals

`constant_in_this_run` is an excitation result, not evidence that a signal is
wrong or useless. A short batch can legitimately miss changes in selector
position, turn indicators, door locks, charge/thermal states, accumulated
limits, warning states, and many other event-driven fields. Such a field may be
recorded as transport-supported and repeatedly observed while its identity or
enum mapping remains uncorrelated.

Classify post-drive candidates by testability before scheduling follow-up work:

- **Safe controlled:** a parked or ordinary-driving action can provide a clear
  reference without increasing risk. Examples include selecting ordinary gear
  positions while stationary, operating turn signals, or cycling door locks
  while parked.
- **Passive extended observation:** the state may occur naturally but should not
  be forced. Preserve it for a longer targeted capture across normal trips and
  promote confidence only if an event and the decoded transition can be aligned.
- **Do not induce:** intentional activation would require unsafe, emergency, or
  abusive operation. TCS/VSC intervention, PCS/AEB activation, ABS, collision
  states, airbag-related states, and similar signals remain untested unless a
  naturally occurring event or a proper controlled test facility provides
  evidence.

Never interpret an absence of transitions as a negative test for the latter two
classes. The first progressive pass should identify constant fields; only a
small reviewed subset should be scheduled for extended observation. Raw support,
formula plausibility, enum meaning, and behavioral correlation remain separate
claims in the evidence record.
