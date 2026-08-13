# XA50 documentation

These pages contain platform-specific claims and evidence. Reusable procedure is maintained in the [`70Y074-telemetry` documentation](https://github.com/4piu/70Y074-telemetry/tree/main/docs).

## Findings

- [Diagnostic data path](findings/diagnostic-data-path.md) — Techstream/J2534 observations, endpoints, and independent replay history.
- [Telemetry map](findings/telemetry-map.md) — source DIDs, dynamic DIDs, formulas, and per-signal evidence.
- [Static catalog](findings/static-catalog.md) — generated atlas structure, provenance, and limitations.

## Hardware and performance

- [PCAN and prototype path](hardware/prototype.md) — tested PC adapter and planned SBC/MCU boundary.
- [RP2040/MCP2515/TJA1050 setup](hardware/rp2040-mcp2515-tja1050.md) — electrical and firmware plan; planned state is labeled explicitly.
- [Measured throughput](hardware/throughput.md) — J2534 and PCAN response timing.
- [Parking-lot road validation](validation/road-test.md) — unattended bounded capture, safe maneuver plan, and offline review.
- [Direct-DID validation batches](validation/candidate-batches.md) — stationary qualification, resumable short road batches, and confidence review.
- [Progressive catalog validation](validation/progressive-validation.md) — stable per-DID qualification and tiered multi-drive harvesting for the remaining Hybrid Control map.

The [pilot road-validation evidence](evidence/pilot-road-validation.md) records capture integrity, correlation results, and the confidence decisions from the first drive.

The [first progressive-road interim review](evidence/progressive-road-interim-1.md) records completed-batch quality, signal excitation, decode anomalies, and rerun decisions after the fast tier and two normal batches.

Confidence words follow the general guide: Candidate, Observed, Correlated, Validated, Replayed, and Ported. The profile is one tested vehicle instance, not every XA50 calibration.
