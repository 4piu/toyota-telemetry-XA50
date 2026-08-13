```powershell
# Preview or record the established 13-signal baseline
uv run python .\tools\validation\run-progressive-validation.py baseline
uv run python .\tools\validation\run-progressive-validation.py baseline --live --duration-seconds 30

# Preview the next 25 qualifications
uv run python .\tools\validation\run-progressive-validation.py qualify

# Then perform them in Park, IG-ON/READY
uv run python .\tools\validation\run-progressive-validation.py qualify --live --startup-delay-seconds 0 --max-items 100

# Preview qualified road batches
uv run python .\tools\validation\run-progressive-validation.py road

# Start the unattended sequence
uv run python .\tools\validation\run-progressive-validation.py road --live
```

The runner is dry-run by default. See
[progressive-validation.md](../../docs/validation/progressive-validation.md) for
completion, restart, bus-load, and confidence rules. The fixed v1 pilot plan is
retained in [candidate-batches.md](../../docs/validation/candidate-batches.md)
as historical evidence, but its temporary PowerShell wrappers have been
removed.
