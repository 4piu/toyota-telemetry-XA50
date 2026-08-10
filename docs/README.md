# XA50 documentation

These pages contain platform-specific claims and evidence. Reusable procedure is maintained in `../../70Y074-telemetry/docs`.

## Findings

- [Diagnostic data path](findings/diagnostic-data-path.md) — Techstream/J2534 observations, endpoints, and independent replay history.
- [Telemetry map](findings/telemetry-map.md) — source DIDs, dynamic DIDs, formulas, and per-signal evidence.
- [Static catalog](findings/static-catalog.md) — generated atlas structure, provenance, and limitations.

## Hardware and performance

- [PCAN and prototype path](hardware/prototype.md) — tested PC adapter and planned SBC/MCU boundary.
- [RP2040/MCP2515/TJA1050 setup](hardware/rp2040-mcp2515-tja1050.md) — electrical and firmware plan; planned state is labeled explicitly.
- [Measured throughput](hardware/throughput.md) — J2534 and PCAN response timing.
- [Parking-lot road validation](validation/road-test.md) — unattended bounded capture, safe maneuver plan, and offline review.

Confidence words follow the general guide: Candidate, Observed, Correlated, Validated, Replayed, and Ported. The profile is one tested vehicle instance, not every XA50 calibration.
