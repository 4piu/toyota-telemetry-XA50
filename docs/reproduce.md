# Reproduce and review the XA50 project

This is the schema-2 pre-release workflow. Run commands from the repository
root after reading [scope and safety](00-scope-safety.md) and
[PRIVATE_DATA.md](../PRIVATE_DATA.md).

## 1. Install the selected toolkit

The project intentionally selects toolkit `main` during coordinated
development. The managed checkout must be clean before setup changes it.

```powershell
.\bootstrap.ps1 -NonInteractive
.\.venv\Scripts\Activate.ps1
telemetry --help
```

Setup builds all adapter workers into ignored `scratch/bin/` and installs the
Python dispatcher from the same toolkit checkout.

## 2. Rebuild private state

The normalized catalog input is private because its redistribution status is
not established. Place the reviewed input at `scratch/catalog-rows.json` and
import it once as the candidate list (toolkit 0.5.0 replaced
`cache rebuild --catalog-rows`):

```powershell
telemetry source import file scratch/catalog-rows.json            # preview
telemetry source import file scratch/catalog-rows.json --apply
telemetry recordings
```

Import keeps the rows in ignored `scratch/inventory.json`, rebuilds the
disposable SQLite cache, and rewrites the privacy-minimized
`result/catalog-manifest.json` (row count, row hash, and source label only).
Review that manifest diff and regenerate the result (step 6) in the same
change. Afterwards `telemetry cache rebuild` needs no arguments. The committed
result is never an input. Removing `scratch/project.sqlite` loses no authored
decision; verification results live separately in
`scratch/verification-facts.json`.

## 3. Review transport and signal meaning

Portable vehicle-facing facts live in `config/transport.json`. Adapter choice
and channel binding live only in ignored `scratch/local-settings.json`
(`telemetry connector set`). Signal/decode conclusions live in
`review/signals.json`. Review signals with `telemetry signal set` or the
desktop Signals page; hand edits are allowed and validated by
`telemetry check`.

## 4. Set collection targets

The migrated `config/collection-policy.json` is intentionally empty. It does
not treat earlier collection settings as current authority. Add reviewed
targets and shared DID settings, then check readiness offline:

```powershell
telemetry target set SIGNAL --min-samples 200 --min-runs 3
telemetry did set ENDPOINT/0xNNNN --interval 500 --max-interval 1000
telemetry collect --dry-run
```

Readiness verifies source hashes, target references, cadence, measured cost,
storage bounds, and transport safety, and writes the compiled scope under
ignored `scratch/plans/` without opening an adapter. With the current empty
policy it reports `collection policy has no included targets`.

## 5. Collect only after reviewing the scope

`telemetry collect` is live. It prints the exact scope and asks for `yes`; a
scripted `--yes` is accepted only for the scope last shown by `--dry-run`. The
desktop Collect page shows the same scope in a confirmation dialog.

```powershell
telemetry collect --note "stationary, IG-ON"
```

The Python collector owns scheduling, decode, target progress, immutable
capture writing, status, and stop. Its private C# worker owns only the adapter,
CAN, ISO-TP, and session lifetime and applies the built-in guard to resolved
transactions. One request is outstanding at a time; the scheduler does not
send catch-up bursts.

No road test is authorized merely by this documentation. Review the XA50
collection scope and bounds first, then use a passenger or unattended logger
and obey the stop rules in [road-test.md](validation/road-test.md).

## 6. Regenerate tracked results

```powershell
telemetry result generate
git status --short
```

Generation reads authored sources, the candidate list in the cache, and every
structurally valid capture. It writes one immutable generation under
`result/generations/` and atomically updates `result/current.json`. Generated
files omit raw response bytes, timestamps, trip notes, adapter identifiers, and
local paths.

Review the complete diff before committing. Never force-add anything under
`scratch/`.

## Historical documentation

Some finding and evidence documents retain the exact pre-0.5 commands used to
produce earlier observations. Those snippets are provenance, not runnable
instructions. Their former command names, fixed batches, and live-mode flags
have no compatibility path in the current toolkit.
