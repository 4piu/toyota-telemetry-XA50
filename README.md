# XA50 diagnostic telemetry project

A read-only telemetry project for one tested Toyota RAV4 Hybrid XA50
(A25A-FXS), produced with the
[`70Y074-telemetry`](https://github.com/4piu/70Y074-telemetry) method.
This project is under coordinated pre-release development and is not a public
compatibility surface.

This repository contains configuration, reviewed findings, and generated
results—no reusable tooling. Programs belong in the sibling
`70Y074-telemetry-tools` repository and are consumed here through the checkout
declared in `telemetry-project.toml`. The toolkit revision intentionally tracks
`main` until release preparation so integration problems appear early.

It is not a universal XA50 database and it is not the dashboard application.
ECU calibration and market differences can change availability, identifiers,
routing, scaling, and validity.

## Current state

Diagnostics have been independently replayed over classical CAN at the DLC3
gateway at 500 kbit/s. The current generated result contains 1,797 normalized
signal identities from 1,846 catalog rows across 13 confirmed endpoints. The
review source contains 90 human-reviewed signal decisions. Converted private
evidence accounts for 2,105 structurally complete runs, 99,262 exchanges, and
338,867 decoded observations.

The one-time migration deliberately did not convert old tiers, periods, sample
limits, or durations into collection authority. `config/collection-policy.json`
is empty, so `telemetry plan` fails safely until new objectives are reviewed.
All valid observations remain available for review regardless of the settings
under which they were originally collected.

Measured PCAN response timing supports the conservative initial 100 ms request
floor and 10 Hz maximum request rate. Faster sustained operation remains
unvalidated. See [throughput](docs/hardware/throughput.md).

## Start here

- [Reproduce and review](docs/reproduce.md)
- [Documentation index](docs/README.md)
- [Reviewed signal decisions](review/signals.json)
- [Transport authority](config/transport.json)

## Layout

| Path | Contents |
|---|---|
| [`config/`](config/) | Tracked transport authority and reviewed collection objectives. |
| [`review/`](review/) | Tracked signal identities, decode corrections, evidence states, and notes. |
| [`result/`](result/) | Tracked, generated, privacy-minimized project output and catalog provenance. |
| [`docs/findings/`](docs/findings/) | How endpoints, transport, and formulas were established. |
| [`docs/evidence/`](docs/evidence/) | Capture integrity and correlation results per campaign. |
| [`docs/hardware/`](docs/hardware/) | The CAN adapter, wiring, and measured timing. |
| `scratch/` | Raw captures, catalog input, derived cache, plans, binaries, and analysis; ignored in full. |

## Hardware

The current route is a CANable v1.0 with PCAN-compatible firmware connected to
DLC3 pins 5, 6, and 14 only. A genuine PEAK PCAN-USB uses the same backend.
See [CAN adapter](docs/hardware/can-adapter.md).

The original XHorse J2534 adapter failed electrically partway through the
project. Its conclusions remain in
[the data-path findings](docs/findings/diagnostic-data-path.md); no proprietary
adapter DLL is stored here.

## Safety and scope

Offline work uses commands such as `telemetry plan` and `telemetry export`.
`telemetry-tui` is a separate offline-first operator application, although its
explicit collection action starts the same live collection service used by
`telemetry collect`. `telemetry collect` is unambiguously live and has no live
mode flag. It can start only from a verified admitted plan, and the private
vehicle-facing worker enforces that plan's narrow read-only allowlist
byte-for-byte.

Do not use this work for vehicle control, coding, security access, DTC
clearing, memory operations, or programming. Validate stationary; road tests
require a passenger or an unattended logger. Do not operate the computer while
driving.

Raw captures are not published. Read [PRIVATE_DATA.md](PRIVATE_DATA.md) before
adding captures or vehicle identifiers.
