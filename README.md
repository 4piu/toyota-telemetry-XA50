# XA50 diagnostic telemetry results

Read-only diagnostic telemetry findings and results for one tested Toyota RAV4
Hybrid XA50 (A25A-FXS), produced with the
[`70Y074-telemetry-tools`](https://github.com/4piu/70Y074-telemetry-tools)
method and toolkit. This project is under coordinated pre-release development
and is not a public compatibility surface.

This repository holds deliverables only: findings, evidence write-ups,
hardware and validation notes, and exported results. It contains no programs
and no private working data. The work itself happens in a private project
workspace outside any repository; `telemetry export` writes the results here.

It is not a universal XA50 database and it is not the dashboard application.
ECU calibration and market differences can change availability, identifiers,
routing, scaling, and validity.

## Current state

Diagnostics have been independently replayed over classical CAN at the DLC3
gateway at 500 kbit/s. The current exported result contains 1,797 normalized
signal identities from 1,846 catalog rows across 13 confirmed endpoints. The
signal review contains 90 human-reviewed signal decisions. Converted private
evidence accounts for 2,105 structurally complete runs, 99,262 exchanges, and
338,867 decoded observations.

The one-time migration deliberately did not convert old tiers, periods, sample
limits, or durations into collection authority. The collection policy is
empty, so collection readiness refuses to start until new targets are
reviewed. All valid observations remain available for review regardless of the
settings under which they were originally collected.

Measured PCAN response timing supports the conservative initial 100 ms request
floor and 10 Hz maximum request rate. Faster sustained operation remains
unvalidated. See [throughput](docs/hardware/throughput.md).

## Start here

- [Documentation index](docs/README.md)
- [Reproduce the result](docs/reproduce.md)
- [Reviewed signal decisions](review/signals.json)
- [Transport](config/transport.json)

## Layout

| Path | Contents |
|---|---|
| [`config/`](config/) | Exported transport, collection policy, and catalog manifest. |
| [`review/`](review/) | Exported signal identities, decode corrections, evidence states, and notes. |
| [`result/`](result/) | Exported, immutable, privacy-minimized result generations and the current pointer. |
| [`docs/findings/`](docs/findings/) | How endpoints, transport, and formulas were established. |
| [`docs/evidence/`](docs/evidence/) | Capture integrity and correlation results per campaign. |
| [`docs/hardware/`](docs/hardware/) | The CAN adapter, wiring, and measured timing. |
| [`docs/validation/`](docs/validation/) | Scope, policy, and campaign status. |

`config/`, `review/`, and `result/` are written by `telemetry export`. Do not
edit them by hand: change the workspace and export again.

## Hardware

The current route is a CANable v1.0 with PCAN-compatible firmware connected to
DLC3 pins 5, 6, and 14 only. A genuine PEAK PCAN-USB uses the same backend.
See [CAN adapter](docs/hardware/can-adapter.md).

The original XHorse J2534 adapter failed electrically partway through the
project. Its conclusions remain in
[the data-path findings](docs/findings/diagnostic-data-path.md); no proprietary
adapter DLL is stored here.

## Safety and scope

The method, including
[scope and safety](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/method/00-scope-safety.md),
lives with the toolkit. Only its verification and collection operations
transmit; both show their exact finite scope and need explicit confirmation,
and collection starts only from an automatically readiness-checked scope. The
private vehicle-facing worker applies the built-in read-only telemetry guard to
every resolved request.

Do not use this work for vehicle control, coding, security access, DTC
clearing, memory operations, or programming. Validate stationary; road tests
require a passenger or an unattended logger. Do not operate the computer while
driving.

Raw captures are not published. Read [PRIVATE_DATA.md](PRIVATE_DATA.md) before
adding evidence or vehicle identifiers.
