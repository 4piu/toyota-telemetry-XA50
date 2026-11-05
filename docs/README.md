# XA50 documentation

Platform-specific claims and evidence for one tested 2023 RAV4 Hybrid (XA50,
A25A-FXS). The signal reference itself is the exported result; see the
[repository README](../README.md). The method these were produced with,
written for any vehicle, lives with the toolkit in
[`docs/method/`](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/method/README.md).

Findings and evidence quote the commands of the tools used at the time, which
the toolkit has since replaced. Those commands are provenance, not
instructions; the conclusions stand.

## Findings

How the endpoints, transport, and formulas were established.

- [Diagnostic data path](findings/diagnostic-data-path.md) — Techstream/J2534
  observations, endpoints, and independent replay history.
- [Telemetry map](findings/telemetry-map.md) — source DIDs, dynamic DIDs,
  formulas, and per-signal evidence.
- [Static catalog](findings/static-catalog.md) — how the Techstream database
  became the candidate list, and its limitations.
- [Pre-migration provenance](findings/pre-migration-provenance.md) — what the
  older provenance paths cited by the transport refer to.

## Evidence

Results of past collection campaigns.

- [Pilot road validation](evidence/pilot-road-validation.md) — the first drive:
  integrity, correlation results, and confidence decisions.
- [Progressive road interim review](evidence/progressive-road-interim-1.md) —
  batch quality, signal excitation, decode anomalies, and rerun decisions.
- [Multi-ECU road expansion](evidence/progressive-road-interim-2.md) — road
  coverage across ten additional ECUs and decode candidates held back.

## Hardware

- [CAN adapter and vehicle connection](hardware/can-adapter.md) — the CANable
  v1.0 interface that produced every result here, DLC3 wiring, and bring-up.
- [Measured throughput](hardware/throughput.md) — J2534 and PCAN response
  timing and what limits the request rate (100 ms floor, 10 requests/s).
