# Progressive catalog validation

This workflow turns the static Hybrid Control catalog into resumable stationary
qualification and road-capture work. It is intended for many sessions: progress
is reconstructed from completed JSONL captures rather than a mutable checklist,
so adding candidates or rebuilding the plan does not erase prior work.

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
`../XA50-workbench/plans/vehicle-a/hybrid-control-progressive.json`:

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
`../XA50-workbench/captures/vehicle-a`, including the v1 pilot, and writes a
machine-readable `progress.snapshot.json` below the progressive output tree.
The initial post-pilot state is 16 qualified/road-complete DIDs, four terminal
NRC `0x31` negatives, and 568 untried DIDs.

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
DIDs from one tier, with one outstanding request at a time. Fast batches run
first, followed by normal, slow, and archive. Narrow a trip with, for example,
`--tiers slow,archive --max-items 3`.

The startup delay defaults to ten seconds. Every capture is create-new and
bounded to 16 MiB; the whole progressive tree defaults to 2 GiB and requires an
additional 256 MiB free-space reserve. These can be changed explicitly with
`--startup-delay-seconds`, `--max-log-mib`, and `--max-directory-mib`.

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
