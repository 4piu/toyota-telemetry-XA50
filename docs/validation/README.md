# XA50 validation status

The current workflow is documented in [reproduce.md](../reproduce.md). This
page records XA50-specific state rather than executable instructions.

## Migrated evidence

The accepted one-time conversion preserved 2,105 structurally complete runs,
99,262 exchanges, and 338,867 decoded observations. It rejected 503 legacy
files with explicit reasons and found no discrepancy between imported and
current DID support counts. Converted run independence and applicability remain
unknown unless separate reviewed evidence establishes them.

All structurally valid observations remain visible across later collection
policy changes. The settings stored with a run explain how it was collected;
they do not decide whether its values may be plotted or reviewed.

## Transport status

Tracked authority contains 13 confirmed endpoints and two reviewed dynamic-DID
definitions. The conservative safety envelope uses a 100 ms request-start
floor and 10 Hz maximum request rate, supported by the PCAN stability evidence
in [throughput.md](../hardware/throughput.md). Faster sustained polling remains
unvalidated.

Support evidence currently records 553 positive ECU/DID pairs and 1,472
negative responses. A negative response describes the tested session and
vehicle state; it does not prove that a DID can never exist or answer.

## Collection policy

`config/collection-policy.json` is intentionally empty. Earlier tier names,
periods, batch widths, sample limits, and durations were not promoted into new
authority. Consequently collection readiness (`telemetry collect --dry-run`
or the desktop Collect page) refuses to start until reviewed targets are
authored.

New targets should state their completion requirements directly: samples and
runs for snapshots, span for slow trends, required states and transitions for
categorical signals, changes with a minimum difference for continuous change,
and a condition for condition-dependent signals. Do not infer semantic class or applicability from catalog names alone.
Unsafe events remain passive-only or excluded; never induce ABS, VSC, PCS/AEB,
collision, overheating, or abusive operation to satisfy a counter.

## Reviewed signal conclusions

The tracked signal review contains 90 decisions: 66 Observed and replayed, 21
Correlated and replayed, and 3 Validated and replayed. Generated results contain
1,797 normalized signal identities and are selected through
`result/current.json`.
