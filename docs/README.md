# XA50 documentation

Platform-specific claims and evidence for one tested 2023 RAV4 Hybrid (XA50,
A25A-FXS), alongside the method they were produced with.

**To repeat this work on your own vehicle, start with
[reproduce.md](reproduce.md).**

## The method

Carried from the [template](https://github.com/4piu/70Y074-telemetry) this
repository was created from, at the revision it was created at. It is written
for any vehicle; nothing in it is specific to this one.

| Step | Page |
|---|---|
| 0 | [Scope and safety](00-scope-safety.md) — read before connecting anything |
| 1 | [Record the vehicle profile](01-vehicle-profile.md) |
| 2 | [Define the target signal](02-target-signal.md) |
| 3 | [Locate acquisition metadata](03-locate/README.md) |
| 4 | [Reconstruct diagnostic transport](04-reconstruct/diagnostic-transport.md) |
| 5 | [Decode formulas and bitfields](05-decode/formulas-and-bitfields.md) |
| 6 | [Verify on the vehicle](06-verify/README.md) |
| 7 | [Evidence records and catalog schema](07-catalog/evidence-and-schema.md) |

[Platform projects](platform-projects.md) is the contract this repository
implements. [Open questions](open-questions.md) records where the method is
still soft. Command-level usage lives with the
[toolkit](https://github.com/4piu/70Y074-telemetry-tools).

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

Designs that were never built (RP2040/MCP2515 bridge, SBC/SocketCAN) are in the
ignored `scratch/hardware-exploration/` directory, not here.

## Relationship to the template and the toolkit

- [Upstream and update policy](upstream.md) — what comes from where, and how to
  advance the pinned revision.

Confidence words follow the template: Candidate, Observed, Correlated,
Validated, Replayed, Ported. This profile is one tested vehicle instance, not
every XA50 calibration.
