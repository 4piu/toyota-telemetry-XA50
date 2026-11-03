# Progressive road validation: interim pass 1

> **Tooling note.** The commands quoted below ran against the `run-progressive-validation.py` wrapper and the XA50 C# clients, which have since been replaced by the installed toolkit and its generic backends. The evidence is unchanged; see [reproduce.md](../reproduce.md) for the current equivalents.

This is the first review of the progressive Hybrid Control road recordings after
stationary qualification was exhausted. Raw captures remain in the private
workbench. Exact trip timestamps and raw filenames are intentionally omitted.

## Recording integrity and progress

Ten progressive batches are complete: all eight fast-tier batches and the first
two normal-tier batches. Together they contain:

- 60 distinct DIDs;
- 11,903 complete positive samples;
- at least 197 samples per fast DID and 199 per normal DID, exceeding the
  configured minimums of 100 and 120;
- 100–101 ms median fast request cadence and 250–251 ms median normal cadence;
- no malformed records, sequence gaps, error endings, or transport retries.

The earlier fast-tier timeout is superseded by a clean completed recording of
the same six DIDs. A later normal batch was stopped by the operator after about
155 seconds. Its six DIDs (`0x1050`, `0x1052`, `0x1057`, `0x1062`, `0x106B`, and
`0x106D`) remain incomplete and are automatically the first road batch on the
next run. No completed batch is being invalidated or requeued.

Current all-tier progress is 76 road-complete DIDs and 52 qualified DIDs still
pending. The pending set includes one archive DID; excluding archive leaves 51
useful fast/normal/slow DIDs.

## Signal excitation

The actual completed payloads contain 150 catalog-mapped fields. Of those, 65
changed and 85 were constant in their sampling window. Constant fields are
retained as valid observations, not classified as failed recordings.

Strong same-batch relationships include:

| Signals | Pearson correlation | Interpretation |
| --- | ---: | --- |
| Accelerator sensor 1 / sensor 2 percentages | 0.9976 | coherent redundant pedal channels |
| Target / actual rear-motor torque | 0.9970 | coherent command and execution |
| Requested / executed regenerative torque | 0.9878 | repeated rolling confirmation |
| Inverter-water-pump duty / revolution | 0.9802 | coherent actuator command and speed |
| Minimum / maximum battery-block voltage | 0.9960 | coherent battery summary pair |
| Engine speed / throttle-sensor percentage | 0.8225 | plausible powertrain response |

IG-ON duration increased monotonically with zero reversals and advanced by
299,540 ms during its approximately 300-second batch. Additional plausible
rolling ranges include:

- engine speed: 0..3,008 rpm;
- vehicle speed: 0..94 km/h;
- rear-motor revolution: -749..2,582 rpm;
- target/actual rear-motor torque: -55..89.625 and -55.875..91.5 Nm;
- traction-battery control current: -107.15..124.51 A;
- inverter input current: -96.6..109.7 A;
- auxiliary-battery voltage: 12.705..14.412 V;
- steering angle: -490.5..504 degrees.

These are correlation and plausibility evidence, not independent calibrated
ground truth.

## Decode and catalog issues—not rerun failures

### DID `0x1F9A` battery current

The static catalog formula treats the 16-bit current as offset binary and
produces implausible extrema near ±3,276 A. The captured bytes instead decode
plausibly as signed two's-complement tenths of an ampere, yielding
-102.1..88.4 A. The recording is good; signedness/formula metadata needs
correction before this field enters the curated signal map.

### DID `0x1809` block-voltage layout

The aggregated catalog describes up to 20 block-voltage fields, but this
calibration returns 22 data bytes and DID `0x180A` reports 11 blocks. Only 11
catalog fields fit the payload. Several middle fields decode at roughly twice
the contemporaneous minimum/maximum block voltage, while the edge fields fall
inside that range. This is a calibration-specific layout mismatch, not a lost
CAN frame. Preserve the raw response and reconstruct the 11-block layout before
promoting individual block labels.

### Boolean polarity and enums

Several constant raw bits have labels whose apparent English meaning conflicts
with ordinary driving state—for example an emergency-shutdown sub-CPU bit and
unpressed switch-like fields reading one. They may be active-low, availability
flags, or labels from another calibration variant. A constant raw bit proves
transport observation only; it does not establish polarity or enum meaning.

The rear-temperature unit text is also mojibake in the generated OEM-derived
catalog. Its numeric formula/range is temperature-like, but the unit should be
normalized only in a reviewed curated record.

## Follow-up classes for constant fields

Safe controlled follow-up can target selector/switch families while parked:

- `0x1F65`: N/P and neutral selector-related bits;
- `0x1044`–`0x104F`: ignition, EV-mode, sport/shift, selector-sensor, and
  reverse-light-related bits;
- `0x1035`: backup-light relay.

Passive extended observation is more appropriate for battery-cooling fan
states (`0x1802`, `0x1079`), rear regenerative requests (`0x106A`, `0x1078`),
precharge snapshots (`0x107D`–`0x1080`), boost/control-mode fields, and
accumulators/history fields in the `0x15Ex` family.

Interlock and emergency-shutdown fields (`0x1031`, `0x1032`) must not be induced.
They remain transport-observed with unknown polarity until safe external
evidence exists. The same policy applies to later ABS, TCS/VSC, PCS/AEB,
collision, and airbag-related candidates.

## Rerun decision

- **Requeue:** only the operator-stopped normal batch; the progress engine has
  already done this automatically.
- **Do not requeue:** all ten completed batches; they meet structural, sample,
  cadence, and payload-integrity requirements.
- **Target later rather than generic rerun:** constant safe controls and passive
  event-driven states.
- **Skip by default:** archive DID `0x1010`; its positive qualification payload
  is shorter than its catalog bit layout and exposes no currently mapped field.

At the time, the next fixed batch was expected to start with the six
operator-stopped DIDs and omit the unmapped archive candidate. That scheduler
has since been removed. Any renewed need must be expressed as reviewed
objectives and compiled offline with `telemetry plan`.
