# XA50 tools

These are vehicle-specific forks/clients. Reusable counterparts and method documentation live in `../../70Y074-telemetry`.

| Path | Role | State |
|---|---|---|
| `catalog/build-telemetry-catalog.py` | Generate the XA50 candidate atlas from the local Techstream database/profile. | Offline/reproducible |
| `techstream-db/query-techstream-db.ps1` | Query observed Phase-5 database records. | Offline |
| `techstream-session/inspect-techstream-session.py` | Privacy-minimized `.TSE` ECU-key inventory. | Offline |
| `j2534/save-j2534-log.ps1` | Snapshot the compatible Techstream logger. | Legacy until a J2534 VIM is available |
| `j2534/parse-j2534-log.py` | Parse logs with embedded XA50 signal candidates. | Offline/legacy capture analysis |
| `validation/Xa50.Pcan` | Strict PCAN-Basic raw-CAN/ISO-TP probes and pollers. | Live-confirmed |
| `validation/Xa50.J2534` | Strict J2534 replay client. | Previously confirmed; current adapter failed |
| `validation/build-progressive-plan.py` | Build a private, tiered direct-DID plan from the Hybrid Control catalog. | Offline verified |
| `validation/run-progressive-validation.py` | Capture the 13-signal XA50 baseline or resume per-DID qualification and tiered road batches across many sessions. | Live-confirmed transport; offline completion checks tested |
| `analysis/analyze-road-capture.py` | Validate, summarize, and optionally flatten the bounded road JSONL log. | Live captures and synthetic fixture verified |
| `analysis/analyze-candidate-capture.py` | Validate qualification/batch JSONL and decode candidate fields against the catalog. | Live captures and fixtures verified |

Build outputs are ignored. Use `validation/build-pcan-client.ps1` or `validation/build-j2534-client.ps1`; both place output under their project directory.

The validation runner is XA50-specific despite being written in Python. It
binds the Hybrid Control endpoint, XA50 catalog/analyzers, and `Xa50.Pcan`
backend. Its scheduling and completion-state machinery is a candidate for later
extraction into the generic telemetry toolkit.
