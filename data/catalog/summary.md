# Static telemetry catalog summary

Profile: `2023-rav4-hv-xa50-a25a-fxs`

Every ECU here answered a stationary probe on this vehicle. The catalog listed 95 Phase-5 candidates; the rest did not reply. See scratch/analysis/probe-report.json.

| ECU key | ECU | Tester ID | Response ID | Endpoint evidence | Records | Distinct direct DIDs | Formula records joined |
|---:|---|---:|---:|---|---:|---:|---:|
| 372 | Engine | `0x700` | `0x708` | probe-confirmed | 2147 | 722 | 2147 |
| 374 | Cruise Control | `0x700` | `0x708` | probe-confirmed | 93 | 19 | 93 |
| 395 | Motor Generator | `0x724` | `0x72C` | probe-confirmed | 349 | 123 | 349 |
| 396 | Rear Motor Generator | `0x705` | `0x70D` | probe-confirmed | 198 | 70 | 198 |
| 397 | Hybrid Control | `0x7D2` | `0x7DA` | probe-confirmed | 1424 | 593 | 1424 |
| 399 | Cruise Control | `0x7D2` | `0x7DA` | probe-confirmed | 71 | 16 | 71 |
| 405 | EMPS | `0x7A1` | `0x7A9` | probe-confirmed; does not implement ReadDataByIdentifier | 222 | 176 | 222 |
| 409 | Combination Meter | `0x7C0` | `0x7C8` | probe-confirmed | 567 | 103 | 567 |
| 426 | Radar Cruise2 | `0x790` | `0x798` | probe-confirmed | 41 | 25 | 41 |
| 432 | Pre-Collision 2 | `0x791` | `0x799` | probe-confirmed | 60 | 22 | 60 |
| 435 | Brake/EPB | `0x7B0` | `0x7B8` | probe-confirmed | 538 | 167 | 538 |
| 444 | SRS Airbag | `0x780` | `0x788` | probe-confirmed; does not implement ReadDataByIdentifier | 214 | 92 | 214 |
| 445 | Steering Angle Sensor | `0x7B3` | `0x7BB` | probe-confirmed; does not implement ReadDataByIdentifier | 53 | 10 | 53 |
| 450 | Air Conditioner | `0x7C4` | `0x7CC` | probe-confirmed; does not implement ReadDataByIdentifier | 378 | 228 | 378 |
| 5005 | Rear Camera | `0x7A2` | `0x7AA` | probe-confirmed | 30 | 14 | 30 |
| 5013 | EV | `0x7D2` | `0x7DA` | probe-confirmed | 507 | 166 | 507 |

Total records: **6892**. Records carrying prior live evidence: **55**.

## Interpretation limits

- A database record is a candidate, not proof that the exact vehicle ECU implements that DID.
- ECU database files aggregate multiple ECU software/calibration variants.
- `direct DID` means the static record exposes a `22 xxxx` candidate; unsupported requests may still return a negative response.
- Endpoint evidence comes from the supplied vehicle profile. A response ID inferred from a protocol convention remains a candidate until observed.
- The numeric formula is joined from Techstream coefficients. Signedness, byte order, enumerations, invalid sentinels, and availability conditions still require further static analysis or live validation.
- Mapped/dynamic records need acquisition-path reconstruction before they can be queried independently.

## Files

- `telemetry.csv`: flat working catalog for filtering and spreadsheet use.
- `dids.csv`: one row per ECU/DID, with grouped signal names and estimated response size.
- `telemetry.json`: complete catalog plus profile and evidence metadata.
- `summary.md`: this compact generation report.
