# XA50 documentation

Platform-specific claims and evidence for one tested 2023 RAV4 Hybrid (XA50,
A25A-FXS). Reusable procedure lives in the
[`70Y074-telemetry` field guide](https://github.com/4piu/70Y074-telemetry/tree/main/docs).

**To repeat this work on your own vehicle, start with
[reproduce.md](reproduce.md).**

## Findings

How the endpoints, transport, and formulas were established.

- [Diagnostic data path](findings/diagnostic-data-path.md) — Techstream/J2534
  observations, endpoints, and independent replay history.
- [Telemetry map](findings/telemetry-map.md) — source DIDs, dynamic DIDs,
  formulas, and per-signal evidence.
- [Static catalog](findings/static-catalog.md) — generated atlas structure,
  provenance, and limitations.

## Evidence

Capture integrity and correlation results, per campaign.

- [Pilot road validation](evidence/pilot-road-validation.md) — the first drive:
  integrity, correlation results, and confidence decisions.
- [Progressive road interim review](evidence/progressive-road-interim-1.md) —
  batch quality, signal excitation, decode anomalies, and rerun decisions after
  the fast tier and two normal batches.

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

Designs that were never built (RP2040/MCP2515 bridge, SBC/SocketCAN) are in the
ignored `scratch/hardware-exploration/` directory, not here.

## Relationship to the field guide

- [Upstream and update policy](upstream.md) — what comes from where, and how to
  advance the pinned revisions.

Confidence words follow the field guide: Candidate, Observed, Correlated,
Validated, Replayed, Ported. This profile is one tested vehicle instance, not
every XA50 calibration.
