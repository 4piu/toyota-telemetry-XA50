# XA50 documentation

Platform-specific claims and evidence for one tested 2023 RAV4 Hybrid (XA50,
A25A-FXS). The method they were produced with, written for any vehicle, lives
with the toolkit in
[`docs/method/`](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/method/README.md).

**To reproduce the result, start with [reproduce.md](reproduce.md).**

## Findings

How the endpoints, transport, and formulas were established.

- [Diagnostic data path](findings/diagnostic-data-path.md) — Techstream/J2534
  observations, endpoints, and independent replay history.
- [Telemetry map](findings/telemetry-map.md) — source DIDs, dynamic DIDs,
  formulas, and per-signal evidence.
- [Static catalog](findings/static-catalog.md) — generated atlas structure,
  provenance, and limitations.
- [Pre-migration provenance](findings/pre-migration-provenance.md) — what the
  older provenance paths cited by the transport refer to.

## Evidence

Capture integrity and correlation results, per campaign.

- [Pilot road validation](evidence/pilot-road-validation.md) — the first drive:
  integrity, correlation results, and confidence decisions.
- [Progressive road interim review](evidence/progressive-road-interim-1.md) —
  batch quality, signal excitation, decode anomalies, and rerun decisions after
  the fast tier and two normal batches.
- [Multi-ECU road expansion](evidence/progressive-road-interim-2.md) — road
  coverage across ten additional ECU plans, conservative curation, and decode
  candidates held back for follow-up.

## Validation

- [Status and policy](validation/README.md) — scope, tier and privacy policy,
  negative-response handling, current campaign state, and what still needs work.
- [Parking-lot road protocol](validation/road-test.md) — safe maneuver plan and
  unattended capture procedure.
- [Focused custom experiments](validation/custom-experiments.md) — reviewed
  reruns for selected signals without altering progressive status.

## Hardware

- [CAN adapter and vehicle connection](hardware/can-adapter.md) — the CANable
  v1.0 interface that produced every result here, DLC3 wiring, and bring-up.
- [Measured throughput](hardware/throughput.md) — J2534 and PCAN response
  timing and what limits the sample rate.

Designs that were never built (RP2040/MCP2515 bridge, SBC/SocketCAN) are kept
in private working notes, not here.

## Exported data

`config/`, `review/`, and `result/` at the repository root are written by
`telemetry export` from the private workspace. They are not edited here.

Confidence words follow the method: Candidate, Observed, Correlated,
Validated, Replayed, Ported. This profile is one tested vehicle instance, not
every XA50 calibration.
