# XA50 tools

These are vehicle-specific forks and clients. Reusable counterparts and method documentation live in the [`70Y074-telemetry` repository](https://github.com/4piu/70Y074-telemetry).

| Path | Role | State |
|---|---|---|
| `catalog/build-telemetry-catalog.py` | Generate the XA50 candidate atlas from the local Techstream database/profile. | Offline/reproducible |
| `techstream-db/query-techstream-db.ps1` | Query observed Phase-5 database records. | Offline |
| `techstream-session/inspect-techstream-session.py` | Privacy-minimized `.TSE` ECU-key inventory. | Offline |
| `j2534/save-j2534-log.ps1` | Snapshot the compatible Techstream logger. | Legacy until a replacement J2534 VIM arrives |
| `j2534/parse-j2534-log.py` | Vendored, unmodified generic log parser/decoder. | Offline/legacy capture analysis |
| `data/catalog/xa50-j2534-signals.json` | XA50 candidate signal profile for `parse-j2534-log.py --signals`. | Offline/legacy capture analysis |
| `validation/Xa50.Pcan` | Strict PCAN-Basic raw-CAN/ISO-TP probes and pollers. | Live-confirmed |
| `validation/Xa50.Pcan/Adapters/PcanBasic.cs` | Vendored copy of the upstream generic PCAN-Basic P/Invoke wrapper. | Vendored; do not edit directly |
| `validation/Xa50.J2534` | Strict J2534 replay client. | Previously confirmed; current adapter failed; replacement VIM on order |
| `validation/Xa50.J2534/Adapters/J2534Api.cs` | Vendored copy of the upstream generic J2534 PassThru API wrapper. | Vendored; do not edit directly |
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

The vehicle-agnostic PCAN-Basic and J2534 API wrappers used by the two hardware
backends are likewise upstream, generic code: `Adapters/PcanBasic.cs` and
`Adapters/J2534Api.cs` are vendored (copied) from `70Y074-telemetry`'s
`tools/pcan/` and `tools/j2534/`, with the reviewed revision recorded in each
file's header comment. Do not edit a vendored file directly; port the fix
upstream and re-vendor, matching the pin-and-review discipline in
[`docs/upstream.md`](../docs/upstream.md). Everything else in `Xa50.Pcan` and
`Xa50.J2534` — DIDs, dynamic-DID definitions, the transmit allowlist, and the
polling/capture loops — is XA50-specific hardware integration and stays here.

`j2534/parse-j2534-log.py` is vendored the same way: it used to be a fork with
an `XA50_CANDIDATES` table and `--decode-xa50` flag hardcoded in, which has
been replaced by the upstream file unmodified plus
`data/catalog/xa50-j2534-signals.json`, a config file in the format the
generic `--signals` option already accepts.
