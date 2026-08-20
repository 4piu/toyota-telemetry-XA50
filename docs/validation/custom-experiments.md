# Focused custom experiments

The progressive pass establishes one bounded road observation for every
qualified DID. Repeating already-complete DIDs under a particular condition is
kept separate from that completion state. Custom experiments use the reusable
toolkit's `experiment` phase and a reviewed TOML schedule.

The reviewed XA50 schedules are:

- [`stationary-controls-v1.toml`](../../data/config/experiments/stationary-controls-v1.toml),
  with two bounded selector passes and an independently selectable cabin-switch
  batch (the split preserves the profile's six-DID wire-load cap);
- [`extended-road-v1.toml`](../../data/config/experiments/extended-road-v1.toml),
  with three independently selectable ordinary-driving batches:

  - battery-current cross-check: five minutes;
  - rear-regeneration context: ten minutes;
  - passive battery-cooling observation: thirty minutes.

Preview the whole sequence from the repository root:

```powershell
telemetry-validate experiment `
  --project-root . `
  --config data/config/validation-profile.json `
  --experiment data/config/experiments/extended-road-v1.toml
```

Add `--live` only after reviewing the schedule. To use a shorter trip or a
particular vehicle condition, select one or more batch IDs:

```powershell
telemetry-validate experiment `
  --project-root . `
  --config data/config/validation-profile.json `
  --experiment data/config/experiments/extended-road-v1.toml `
  --experiment-batch battery-current-crosscheck `
  --live
```

The stationary schedule is also run one batch at a time. For example:

```powershell
telemetry-validate experiment `
  --project-root . `
  --config data/config/validation-profile.json `
  --experiment data/config/experiments/stationary-controls-v1.toml `
  --experiment-batch selector-core `
  --live
```

For the selector batch, use a level clear area, remain stopped, and hold the
service brake while selecting only normal driver-accessible positions. For the
switch batch, operate only fitted cabin controls and restore the normal state,
including VSC/TRC on, before any motion. Exact operator timing belongs in the
local scratch experiment note so it can be adjusted without treating a test
log as a durable platform claim.

The default ten-second startup delay, disk bounds, transport retries,
create-new logs, and offline validation all apply. A custom experiment may
finish at its duration bound or with Ctrl+C after the planned operator sequence.
The manual stop counts only when every DID reached its minimum sample count and
the final log, endpoint, schedule, and transport integrity checks pass. An early
or malformed stop remains quarantined. Set `duration_seconds = 0` in a custom
batch when it should have no time deadline; the byte and directory limits still
bound disk use. Completed experiment runs are configuration-hashed and skipped
on restart.
Their captures and snapshots live below the corresponding experiment ID under
`scratch/captures/vehicle-a/progressive/experiments/`.

Experiment batch IDs begin with `experiment-`. The progress scanner excludes
that namespace, so a focused rerun can neither satisfy nor invalidate the
progressive catalog map. Increase a batch's `repetitions`, change its schedule,
or create a new experiment ID when another independent run is desired.

The fan batch is passive observation. Do not deliberately overheat the battery.
The rear-regeneration batch is ordinary acceleration/deceleration only; do not
induce traction, stability-control, or emergency-system operation.
