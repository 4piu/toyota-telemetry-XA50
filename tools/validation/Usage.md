```sh
# Review without transmitting
.\tools\validation\start-road-capture.ps1 -DurationSeconds 30

# Mandatory stationary rehearsal
.\tools\validation\start-road-capture.ps1 -Live -DurationSeconds 30

# Later parking-lot run
.\tools\validation\start-road-capture.ps1 -Live -DurationSeconds 900
```

For the short baseline plus experimental direct-DID batches:

```powershell
# Inspect both plans; neither command transmits without -Live
.\tools\validation\qualify-road-batches.ps1
.\tools\validation\start-validation-sequence.ps1

# Run this while stationary in Park, before the driving session
.\tools\validation\qualify-road-batches.ps1 -Live

# One unattended command at the safe test location
.\tools\validation\start-validation-sequence.ps1 -Live
```

See [candidate-batches.md](../../docs/validation/candidate-batches.md) for completion, restart, bus-load, and confidence rules.
