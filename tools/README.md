# XA50 tools

These are vehicle-specific forks and clients. Reusable counterparts and method documentation live in the [`70Y074-telemetry` repository](https://github.com/4piu/70Y074-telemetry).

| Path | Role | State |
|---|---|---|
| `catalog/build-telemetry-catalog.py` | Generate the XA50 candidate atlas from the local Techstream database/profile. | Offline/reproducible |
| `techstream-db/query-techstream-db.ps1` | Query observed Phase-5 database records. | Offline |
| `techstream-session/inspect-techstream-session.py` | Privacy-minimized `.TSE` ECU-key inventory. | Offline |
| `j2534/save-j2534-log.ps1` | Snapshot the compatible Techstream logger. | Legacy until a J2534 VIM is available |
| `j2534/parse-j2534-log.py` | Parse logs with embedded XA50 signal candidates. | Offline/legacy capture analysis |
| `validation/Xa50.Pcan` | Strict PCAN-Basic raw-CAN/ISO-TP probes and pollers. | Live-confirmed |
| `validation/Xa50.J2534` | Strict J2534 replay client. | Previously confirmed; current adapter failed |
| `validation/build-progressive-plan.py` | Live-workflow-compatible XA50 planner retained while the pinned generic toolkit is adopted. | Superseded generically; parity verified |
| `validation/run-progressive-validation.py` | Capture the XA50-only 13-signal baseline and provide the currently live-proven progressive command surface. | Generic orchestration extracted; XA50 baseline remains platform-specific |
| `analysis/analyze-road-capture.py` | Validate, summarize, and optionally flatten the bounded road JSONL log. | Live captures and synthetic fixture verified |
| `analysis/analyze-candidate-capture.py` | Validate qualification/batch JSONL and decode candidate fields against the catalog. | Live captures and fixtures verified |

Build outputs are ignored. Use `validation/build-pcan-client.ps1` or `validation/build-j2534-client.ps1`; both place output under their project directory.

Generic plan construction, candidate-capture analysis, immutable progress,
batch scheduling, bounded execution, and backend invocation now live in the
versioned `toyota-telemetry-tools` package. This repository supplies
[`data/validation/validation-profile.json`](../data/validation/validation-profile.json)
plus the XA50 PCAN backend. The existing local Python copies are not the source
for new generic development; they remain until the final collection and live
package-parity test are complete.
