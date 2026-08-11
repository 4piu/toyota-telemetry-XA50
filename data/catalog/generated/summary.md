# XA50 static telemetry catalog summary

Profile: `2023-rav4-hv-xa50-a25a-fxs`

This is a candidate catalog for the installed vehicle profile, not a claim that every database record is supported by every ECU calibration.

| ECU key | ECU | Tester ID | Response ID | Endpoint evidence | Records | Distinct direct DIDs | Formula records joined |
|---:|---|---:|---:|---|---:|---:|---:|
| 372 | Engine | `0x700` | `0x708` | static candidate; live validation pending | 2147 | 722 | 2147 |
| 395 | Motor Generator | `0x724` | `0x72C` | static candidate; live validation pending | 349 | 123 | 349 |
| 397 | Hybrid Control | `0x7D2` | `0x7DA` | 0x7D2/0x7DA live-confirmed | 1424 | 593 | 1424 |
| 435 | Brake/EPB | `0x7B0` | `0x7B8` | static candidate; live validation pending | 538 | 167 | 538 |

Total records: **4458**. Records carrying prior live evidence: **55**.

## Interpretation limits

- A database record is a candidate, not proof that the exact vehicle ECU implements that DID.
- ECU database files aggregate multiple ECU software/calibration variants.
- `direct DID` means the static record exposes a `22 xxxx` candidate; unsupported requests may still return a negative response.
- Only Hybrid Control `0x7D2 -> 0x7DA` is live-confirmed. Other tester IDs come from the static protocol table; their response IDs are explicitly inferred as tester ID plus 8 until validated.
- The numeric formula is joined from Techstream coefficients. Signedness, byte order, enumerations, invalid sentinels, and availability conditions still require further static analysis or live validation.
- Mapped/dynamic records need acquisition-path reconstruction before they can be queried independently.

## Files

- `telemetry.csv`: flat working catalog for filtering and spreadsheet use.
- `dids.csv`: one row per ECU/DID, with grouped signal names and estimated response size.
- `telemetry.json`: complete catalog plus profile and evidence metadata.
- `summary.md`: this compact generation report.
