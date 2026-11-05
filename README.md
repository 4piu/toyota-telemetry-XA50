# XA50 diagnostic telemetry findings

Read-only diagnostic telemetry for one tested 2023 Toyota RAV4 Hybrid XA50
(A25A-FXS), `vehicle-a`: which UDS data identifiers (DIDs) its ECUs answer,
where each signal sits in the response, and the formula that turns it into a
physical value. Signal names and formulas come from the manufacturer's
Techstream database; recordings from the vehicle confirm which DIDs answer and
whether each value moves.

It is not a universal XA50 database. ECU calibration and market differences
can change availability, identifiers, routing, scaling, and validity.

## The signal reference

The current result is named in [`result/current.json`](result/current.json).
Its generation folder under [`result/generations/`](result/generations/) holds:

| File | Use |
|---|---|
| `signals.md` | Readable reference: one table per ECU with DID, bits, name, formula, unit, evidence, and recorded status. |
| `signals.csv` | The same rows for spreadsheets and scripts. |
| `signals.json` | Full detail, including the catalog variant and review for each signal. |
| `transport.json` | CAN addresses, diagnostic session, dynamic DIDs, and request limits. |

How to read a signal: send UDS `22 DID` to the ECU's request address and read
the `62 DID ...` reply on its response address. Bit 0 is the most significant
bit of the first data byte after the echo; the bits form an unsigned
big-endian raw value, and the formula converts it.

## How far each signal is verified

As of 2026-11-05: 1,797 signals on 532 DIDs across 13 ECU endpoints. Every one
of those DIDs answered this vehicle. The reference's **Recorded** column says
what the recordings show:

| Recorded | Signals | Meaning |
|---|---:|---|
| changed | 521 | Took more than one value, so the formula was exercised. |
| constant | 1,228 | Answered but never changed: mostly status and fault flags. The DID is readable; the formula is unchecked. |
| beyond response | 46 | The catalog places it past the end of this vehicle's response, so this calibration does not provide it. |
| not recorded | 2 | Never recorded. |

The **Evidence** column records human review: 90 signals are reviewed (66
Observed, 21 Correlated, and 3 Validated, all replayed); the rest are
Candidate, meaning the database row stands unreviewed. Confidence words follow
the toolkit's method.

State and switch signals carry the text Techstream shows for each raw value in
the **States** column, for example Shift Position `0=P, 2=R, 4=N, 6=D, 8=B`,
for 899 signals. Some switches are inverted (`0=ON, 1=OFF`), so read the
column rather than assuming. The text comes from the database; where a
recording agrees with it, the signal's review note says so.

## Repository layout

| Path | Contents |
|---|---|
| [`result/`](result/) | Exported result generations and the current pointer. |
| [`config/`](config/) | Exported transport, collection policy, and catalog manifest. |
| [`review/`](review/) | Exported signal review: identity choices, decode corrections, evidence, notes. |
| [`docs/findings/`](docs/findings/) | How the endpoints, transport, and formulas were established. |
| [`docs/evidence/`](docs/evidence/) | Results of past collection campaigns. |
| [`docs/hardware/`](docs/hardware/) | The CAN adapter, wiring, and measured timing. |

`result/`, `config/`, and `review/` are written by `telemetry export` from a
private workspace; do not edit them by hand. The method and tools that
produced them live in
[`70Y074-telemetry-tools`](https://github.com/4piu/70Y074-telemetry-tools).
This repository holds no programs and no raw recordings.

## Safety and scope

Read-only diagnostics only: no vehicle control, coding, security access, DTC
clearing, memory operations, or programming. Never use these signals as input
to vehicle control or a safety-critical warning. Collection rules are in the
toolkit's
[scope and safety](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/method/00-scope-safety.md).

Read [PRIVATE_DATA.md](PRIVATE_DATA.md) before adding evidence.
