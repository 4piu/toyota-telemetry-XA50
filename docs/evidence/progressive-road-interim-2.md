# Progressive road validation: multi-ECU expansion

This review records the map update after ordinary road trips expanded coverage
beyond Hybrid Control. Raw captures and exact trip timestamps remain in the
private ignored workbench.

## Progress and integrity

The progress engine accepted completed road batches for ten ECU plans that had
previously contributed unobserved map rows: Engine, Motor Generator, Rear Motor
Generator, Cruise Control at the Hybrid Control endpoint, Combination Meter,
Radar Cruise2, Pre-Collision 2, Brake/EPB, Rear Camera, and EV. Discovery and
review read only completed captures; incomplete or quarantined files do not
count toward the ledger or map.

Across all twelve readable ECUs, 360 of 553 qualified DIDs are now
road-complete and 193 remain pending. The new batches changed 410 signal rows
from `unobserved` to a decoded observation: 127 varied within their first
completed run and 283 stayed constant during the sampled window.

The complete map now contains:

| Observation | Signals |
|---|---:|
| Varied across at least two runs | 12 |
| Varied within one completed run | 390 |
| Constant in every completed run | 655 |
| Catalog field beyond this reply | 48 |
| No road observation yet | 741 |

Constant fields remain successful transport observations, not negative
results. Most of the newly covered DIDs have one road run, so a second run is
still needed before the review ledger can classify their variation as
repeatable.

## Curation decisions

Thirty-four curation identities were added, bringing `curation.csv` to 89.
They cover useful signals whose observed decoded ranges are plausible: engine
and vehicle speed, auxiliary voltage, motor/generator revolution and
temperature, boost-side voltage, rear-motor revolution and temperature,
combination-meter speed/RPM, radar speed/yaw, Brake/EPB wheel speeds and
steering/yaw, rear-camera vehicle speed, and EV auxiliary voltage/pump speed.

Every new entry remains **Observed and replayed**. Notes state the observed
range, sample count, and that the evidence comes from one completed road run.
No new signal was promoted to Correlated or Validated because these trips did
not include a synchronized independent reference.

One Engine Speed identity (`ECU 372`, DID `0x1F0C`, bits `0..15`) corresponds
to two OEM catalog records with different internal IDs, so the 89 curation
identities mark 90 output rows as reviewed.

## Decode candidates held back

The following newly observed fields were not curated because their present
catalog conversion or unit is not yet defensible:

- Motor Generator and Rear Motor Generator DID `0x1F9A` battery current crosses
  the 16-bit wrap and decodes to extrema near plus/minus 3,276 A. This is the
  previously identified signedness mismatch, not a capture failure.
- Rear Camera DID `0x1001` steering angle reaches raw `65521` and is rendered as
  `6552.1 deg`; the field needs a reviewed signed conversion.
- Combination Meter DID `0x1641` HV/EV System Indicator reaches `1023` while
  the catalog calls the unit percent; its scale or semantic type is unresolved.
- Radar Cruise2 DID `0x100F` Estimated Vehicle Acceleration reaches the unsigned
  endpoint `10.2 m/s2`; signedness/invalid-state handling needs review before
  promotion.

These should be resolved in `decode-overrides.json` only after checking the raw
layout and a suitable reference. The generated map retains them with automatic
observation evidence so the anomaly remains visible.

## Next review

Prioritize the 193 pending qualified DIDs, then repeat high-value single-run
signals in batches that include a reference from another ECU or an external
instrument. Safe ordinary-driving comparisons include wheel speed versus
vehicle speed, motor revolution versus vehicle speed, redundant auxiliary
voltage fields, and steering angle versus yaw response. Do not deliberately
induce ABS, stability-control, pre-collision, or airbag events.
