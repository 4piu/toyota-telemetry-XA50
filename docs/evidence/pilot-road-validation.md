# Pilot road-validation evidence

> **Tooling note.** The commands quoted below ran against the `run-progressive-validation.py` wrapper and the XA50 C# clients, which have since been replaced by the pinned toolkit and the generic backends. The evidence is unchanged; see [reproduce.md](../reproduce.md) for the current equivalents.

This report records the first completed rolling validation of the Hybrid Control
signal set on `vehicle-a`. Raw captures remain in the private workbench; this
document deliberately omits exact trip timestamps, raw filenames, and any
vehicle-unique identifiers.

## Capture integrity

The two baseline captures and all four direct-DID batches are structurally
complete. Every JSONL record parses, sample sequence numbers are contiguous,
and no transport-error record was emitted.

| Capture | Stop condition | Samples | Duration | Worst response latency |
| --- | --- | ---: | ---: | ---: |
| Baseline A | deliberate user stop | 7,587 | about 12 min 45 s | 119 ms |
| Baseline B | deliberate user stop | 2,818 | about 4 min 44 s | 44 ms |
| Powertrain batch | duration limit | 447 | 45 s | 18 ms |
| Electrical-energy batch | duration limit | 446 | 45 s | 23 ms |
| Thermal-state batch | duration limit | 446 | 45 s | 41 ms |
| Motion-context batch | duration limit | 446 | 45 s | 40 ms |

Baseline A had eight latency samples at or above 50 ms, with a 119 ms maximum.
All still completed within the transport timeout and produced valid decoded
responses. The suspected loose physical connection therefore did not truncate
or corrupt either retained recording.

The direct-DID batches also met their per-DID sample minimums:

- powertrain: 90, 90, 89, 89, and 89 samples across five DIDs;
- electrical-energy: 149, 149, and 148 samples across three DIDs;
- thermal-state: 90, 89, 89, 89, and 89 samples across five DIDs;
- motion-context: 149, 149, and 148 samples across three DIDs.

Four of the 20 stationary qualification candidates returned UDS NRC `0x31`
(request out of range) and were excluded from the road sequence: `0x10B1`,
`0x10A4`, `0x1047`, and `0x10C3`. This result means the tested Hybrid Control
endpoint did not support those reads in that session/configuration; it does not
prove that the named quantities never exist elsewhere in the vehicle.

## Baseline composite signals

The rolling data supports the following confidence changes:

- All four wheel speeds are now **Correlated and replayed**. They covered zero
  to approximately 71.6 km/h, pairwise correlations against RL were at least
  0.9997 in the longer capture, and average wheel speed correlated 0.9998 with
  MG2 revolution. The median MG2 ratio was about 89.35 rpm per km/h.
- Requested and executed regenerative torque are now **Correlated and
  replayed**. Their longer-capture correlation was 0.9987; median absolute
  disagreement was zero, 95th-percentile disagreement was 2.5 Nm, and the
  maximum transient disagreement was 51 Nm.
- Master-cylinder control torque remains **Correlated and replayed**. It ranged
  from -874.25 to 0 Nm and was nonzero only alongside braking indications. It
  must not be interpreted as direct brake-pad contact or clamping force.

Additional excitation observed in Baseline A included MG2-to-road-speed motion,
accelerator values from 0 to 51%, steering from -441 to 505.5 degrees, and both
states of the braking switches. Requested versus executed regenerative torque
also correlated 0.9984 in the shorter second capture, providing a repeat
observation rather than a single-run artifact.

## Direct-DID pilot batches

### Powertrain

Target and executed engine power covered 0..50,380 W and 0..56,660 W, with a
0.9109 correlation. Target and actual generator torque correlated 0.9965;
target and actual motor torque correlated 0.9989. These three paired families
are recorded as **Correlated and replayed**.

Target engine revolution varied from 934 to 2,775 rpm, while mode and control
fields produced plausible raw state changes. They remain **Observed and
replayed** until compared with an independent engine-speed value or their enum
labels are established.

### Electrical energy

The low-voltage system varied from 12.632 to 14.155 V. Before-boost voltage
covered 266.5..318.5 V and after-boost voltage 271.5..424.5 V; their difference
reached 117.5 V. The two traction-voltage fields are **Correlated and replayed**.

Maximum battery SOC rose monotonically from 42.5% to 49.5%, with 14 increments
and no decreases, supporting its accumulator meaning. The immediate-after-IG
and minimum values stayed constant at 40.5% and 39.0%, respectively, so those
two remain **Observed and replayed**.

### Thermal state

All decoded temperatures and sensor voltages were plausible and internally
ordered where current, IG-ON, and maximum variants were available. Examples
include motor temperature at 51..52 degC, motor-inverter temperature at
37..43 degC, and inverter coolant at 38..41 degC. The short run supplied little
thermal excitation and no independent thermometer, so all thermal fields remain
**Observed and replayed**.

### Motion context

Both shift-position fields changed between stationary Park qualification and
the road batch, and Ready remained true in the known READY condition. These are
**Correlated and replayed**, but the raw shift enum labels are still pending.

Yaw rate visited both signs and ranged from -3.480 to 10.184 deg/s. Other status
bits either transitioned once or remained constant. They remain **Observed and
replayed** pending synchronized maneuvers or an independent reference.

## Result and limitations

The curated live-evidence map now contains 55 signals: 3 Validated/Replayed, 22
Correlated/Replayed, and 30 Observed/Replayed. This pilot adds evidence for 42
signals beyond the original 13-signal composite set.

These conclusions establish transport replay, decoded variation, and internal
consistency on one vehicle. They do not establish universal XA50 availability,
exact enum labels, accuracy against calibrated instruments, or identity with a
simultaneous Techstream display. Candidate batches were executed sequentially,
so correlations are only claimed for fields sampled together in the same batch.

Re-run structural checks with:

```powershell
uv run python the retired road-capture analyzer <baseline.jsonl>
uv run python telemetry-analyze-capture <batch.jsonl> `
  --catalog data/catalog/telemetry.csv --minimum-samples 30
```

Only `.complete.jsonl` candidate-batch files count as completed progress.
Interrupted `.incomplete.jsonl` files remain quarantined and must not be used to
advance evidence state.
