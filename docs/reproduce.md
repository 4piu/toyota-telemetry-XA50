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
not established. Place the reviewed input at `scratch/catalog-rows.json`, then
rebuild the disposable SQLite cache from authored files and immutable captures:

```powershell
telemetry cache rebuild --catalog-rows scratch/catalog-rows.json
telemetry capture inspect
```

The committed result is never an input to either command. Removing
`scratch/project.sqlite` loses no authored decision.

## 3. Review transport and signal meaning

Portable vehicle-facing facts live in `config/transport.json`. Adapter choice
and channel binding live only in ignored `scratch/local-settings.json`.
Signal/decode conclusions live in `review/signals.json`.
The desktop curation application is not yet implemented, so changes to tracked
review and policy remain a development task requiring direct diff review.

## 4. Author collection objectives

The migrated `config/collection-policy.json` is intentionally empty. It does
not treat earlier collection settings as current authority. Add reviewed
objectives to the tracked policy, then compile them offline:

```powershell
telemetry plan
```

Planning verifies source hashes, objective references, cadence, measured cost,
storage bounds, and transport safety. It writes an admitted plan under ignored
`scratch/plans/`. With the current empty policy it must fail with
`collection policy has no included objectives`.

## 5. Collect only after plan inspection

`telemetry collect` is a live verb; there is no live-mode option and no dry-run
form of that command. Before the adapter opens it verifies the admitted plan
against the exact current authored and capture sources and prints adapter,
endpoint, output, and plan-hash scope.

```powershell
telemetry collect
```

The Python collector owns scheduling, decode, objective progress, immutable
capture writing, status, and stop. Its private C# worker owns only the adapter,
CAN, ISO-TP, and session lifetime and applies the built-in guard to resolved
transactions. One request is outstanding at a time; the scheduler does not
send catch-up bursts.

No road test is authorized merely by this documentation. Inspect the admitted
XA50 plan, transaction scope, and bounds first, then use a passenger or
unattended logger and obey the stop rules in
[road-test.md](validation/road-test.md).

## 6. Regenerate tracked results

After offline review and cache rebuild:

```powershell
telemetry export
git status --short
```

Export reads authored sources, the normalized private catalog input in the
cache, and every structurally valid capture. It writes one immutable generation
under `result/generations/` and atomically updates `result/current.json`.
Generated files omit raw response bytes, timestamps, trip notes, adapter
identifiers, and local paths.

Review the complete diff before committing. Never force-add anything under
`scratch/`.

## Historical documentation

Some finding and evidence documents retain the exact pre-0.4 commands used to
produce earlier observations. Those snippets are provenance, not runnable
instructions. Their former command names, fixed batches, and live-mode flags
have no compatibility path in the current toolkit.
