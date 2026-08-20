# XA50 diagnostic telemetry map

A read-only telemetry map for one tested Toyota RAV4 Hybrid XA50 (A25A-FXS),
produced with the [`70Y074-telemetry` field guide](https://github.com/4piu/70Y074-telemetry)
and published as a worked reference for anyone repeating the method on a
comparable vehicle.

This repository contains **configuration, findings, and results — no tooling**.
Every program used here comes from the pinned field guide. If something here
needs a script that the field guide does not provide, that is a gap in the field
guide, not something to solve locally.

It is not a universal XA50 database, and it is not the dashboard application.
ECU calibration and market differences change availability, identifiers,
routing, scaling, and validity.

## Result

Diagnostics are independently replayed over CAN at the DLC3 gateway,
500 kbit/s. Sixteen ECUs answered a stationary probe; twelve of them implement
ReadDataByIdentifier and are mapped, across nine distinct endpoints — Techstream
models several ECUs as separate databases over one diagnostic address.

| Measure | Count |
|---|---:|
| ECUs mapped | 12 |
| Catalog DIDs across them | 2025 |
| Answered a read | 553 |
| Refused (recorded in `unsupported.csv`) | 1472 |
| Signals in the map | 1846 |
| of which human-reviewed | 55 |

Evidence is uneven by design, and every row says which it is. 695 signals have
been read on the road: 12 varied across independent runs, 263 were seen in a
single run, 372 stayed constant, and 48 are mapped by the catalog past the end
of the reply this vehicle sends — absent hardware, mostly extra battery blocks
and temperature sensors. The remaining 1151 sit on DIDs that answer but have
not yet been road-captured; they carry evidence state *Candidate*.

Hybrid Control (ECU 397, tester `0x7D2` to response `0x7DA`) is the most
complete: every DID it answers has been read. A gas XA50 has no ECU 397 at
all, so that part of this map does not apply to one.

Four further ECUs answer a probe but reject service `0x22` outright — SRS
Airbag, EMPS, Steering Angle Sensor, and Air Conditioner. Their endpoints are
recorded; see [validation status](docs/validation/README.md).

Typical complete-response latency is 11–17 ms, and sustained 10 Hz polling is
validated. See [throughput](docs/hardware/throughput.md).

## Start here

- **[Reproduce this on your own XA50](docs/reproduce.md)** — every command, in
  order, from clone to map.
- [Documentation index](docs/README.md)
- [The curated map](data/map/signals.csv) — the deliverable, with an
  evidence state per signal.

## Layout

| Path | Contents |
|---|---|
| [`data/map/`](data/map/) | **The deliverable.** Curated signals with an evidence state each, plus the endpoint and dynamic-DID definitions. |
| [`data/config/`](data/config/) | The verified inputs used to produce it: endpoint, session setup, tier policy, decode corrections, experiment schedules. |
| [`data/catalog/`](data/catalog/) | Generated from the OEM database. Provenance is tracked; the bulk atlas is local-only. |
| [`docs/findings/`](docs/findings/) | How endpoints, transport, and formulas were established. |
| [`docs/evidence/`](docs/evidence/) | Capture integrity and correlation results per campaign. |
| [`docs/hardware/`](docs/hardware/) | The CAN adapter, wiring, and measured timing. |
| `scratch/` | Raw captures, generated plans, backend binaries, reverse-engineering state. Ignored in full. |

## Hardware

A CANable v1.0 with PCAN-compatible firmware, connected to DLC3 pins 5, 6, and
14 only. A genuine PEAK PCAN-USB works identically. See
[CAN adapter](docs/hardware/can-adapter.md).

The original XHorse J2534 adapter failed electrically partway through the
project. Its conclusions are preserved in
[the data-path findings](docs/findings/diagnostic-data-path.md); no proprietary
adapter DLL is stored here.

## Safety and scope

All tooling defaults to dry-run and requires explicit live authorization, with
narrow read-only allowlists enforced byte-exactly in the vehicle-facing process.

Do not use this work for vehicle control, coding, security access, DTC
clearing, memory operations, or programming. Validate stationary; road tests
require a passenger or an unattended logger. Do not operate the computer while
driving.

Raw captures are not published. See [PRIVATE_DATA.md](PRIVATE_DATA.md) before
adding captures or vehicle identifiers.
