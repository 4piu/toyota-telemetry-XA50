# XA50 diagnostic telemetry knowledge base

This repository records vehicle-specific telemetry discovery and read-only replay results for one tested Toyota RAV4 Hybrid XA50 profile. It contains a static candidate atlas, a small curated live-evidence map, transport findings, and validation clients.

It is not a universal XA50 database and it is not the dashboard application. ECU calibration and market differences can change availability, identifiers, routing, scaling, and validity. The future acquisition/display application has its own boundary in `../XA50-dashboard`; the reusable discovery process lives in `../70Y074-telemetry`.

## Current result

Hybrid Control diagnostics have been independently replayed through PCAN-USB on the DLC3 gateway at 500 kbit/s:

- tester `0x7D2`, response `0x7DA`;
- session setup and dynamic DID definition reproduced over raw CAN/ISO-TP;
- repeated `22 F3 01` returns MG1, MG2, and accelerator with roughly 11–17 ms typical complete-response latency in the recorded runs;
- a second composite request covers wheel speed, steering, and braking-related candidates.

MG1, MG2, and accelerator are Validated/Replayed for `vehicle-a`. The first rolling pilot expanded the curated map to 55 signals: 22 are Correlated/Replayed and 30 remain Observed/Replayed. See [the evidence report](docs/evidence/pilot-road-validation.md) for the confidence rationale.

The original XHorse J2534 adapter has failed electrically. Its client and captured conclusions remain under `tools/validation/Xa50.J2534` as reproducibility/legacy evidence; current live validation uses PCAN. No proprietary adapter DLL is stored here.

## Navigate

- [Documentation index](docs/README.md)
- [Static catalog and limitations](docs/findings/static-catalog.md)
- [Hybrid telemetry/protocol findings](docs/findings/telemetry-map.md)
- [Diagnostic data-path evidence](docs/findings/diagnostic-data-path.md)
- [Hardware and validation notes](docs/hardware/prototype.md)
- [Unattended parking-lot validation protocol](docs/validation/road-test.md)
- [Qualified sequential candidate batches](docs/validation/candidate-batches.md)
- [Progressive multi-drive catalog validation](docs/validation/progressive-validation.md)
- [Vehicle/ECU profile](data/catalog/ecu-profile.json)
- [Generated catalog summary](data/catalog/generated/summary.md)
- [Curated live-evidence signals](data/validated/signals.csv)
- [Tool index](tools/README.md)

## Safety and scope

All clients default to dry-run or require explicit live authorization and use narrow read-only allowlists. Do not use this work for vehicle control, coding, security access, DTC clearing, memory operations, or programming. Validate stationary; road tests require a passenger or unattended logger.

See [PRIVATE_DATA.md](PRIVATE_DATA.md) before adding captures or vehicle identifiers.
