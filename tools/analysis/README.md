# Road-capture analysis

`analyze-road-capture.py` validates the JSONL structure, response prefixes, sequence/timing monotonicity, required decoded fields, and session boundary. It reports per-group latency/cadence, signal ranges, switch counts, wheel-to-wheel spread, and requested/executed regeneration tracking error.

```powershell
uv run python .\tools\analysis\analyze-road-capture.py `
  '..\XA50-workbench\captures\vehicle-a\vehicle-a_road_TIMESTAMP.jsonl'

# Create-new outputs for spreadsheet and later automation:
uv run python .\tools\analysis\analyze-road-capture.py `
  '..\XA50-workbench\captures\vehicle-a\vehicle-a_road_TIMESTAMP.jsonl' `
  --summary-json '..\XA50-workbench\analysis\road-summary.json' `
  --samples-csv '..\XA50-workbench\analysis\road-samples.csv'
```

The analyzer never edits the capture and refuses to overwrite derived outputs. `fixtures/road-capture-synthetic.jsonl` is identifying-data-free test input, not vehicle evidence.

`analyze-candidate-capture.py` performs the corresponding structural and
catalog-assisted review for stationary qualification and sequential direct-DID
batches:

```powershell
uv run python .\tools\analysis\analyze-candidate-capture.py `
  '..\XA50-workbench\captures\vehicle-a\road-batches\BATCH.complete.jsonl' `
  --catalog '.\data\catalog\generated\telemetry.csv' `
  --minimum-samples 30
```

It reports evidence hints but never promotes a signal automatically.
