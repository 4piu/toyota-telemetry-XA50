# Reproduce the XA50 result

This repository holds deliverables only. The result is reproduced with an
installed toolkit and a private project workspace kept outside any repository.
Read the toolkit's
[scope and safety](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/method/00-scope-safety.md)
and [PRIVATE_DATA.md](../PRIVATE_DATA.md) first. The toolkit's
[quickstart](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/quickstart.md)
documents each command.

Every operation is available both in the `telemetry` command line and in the
`telemetry-ui` desktop application. The commands below are the CLI form.

## 1. Install the toolkit

During coordinated pre-release development the toolkit is installed from a
checkout; a published package will replace this.

```powershell
uv tool install --editable PATH-TO/70Y074-telemetry-tools[gui]
telemetry --help
```

The adapter workers are bundled with the package. Each exported result
records the toolkit version and, when run from a Git checkout, its commit.
Check `result/current.json` and the generation manifest for the toolkit that
produced the published result.

## 2. Create a workspace

```powershell
telemetry init WORKSPACE --project-id xa50-hybrid-vehicle-a --profile-id 2023-rav4-hv-xa50-a25a-fxs
```

(GUI: Project > New project.) Choose a folder outside this repository. Then
replace the new workspace's empty authored files with the exported copies from
this repository:

| Copy from this repository | To the workspace |
|---|---|
| `config/transport.json` | `config/transport.json` |
| `config/collection-policy.json` | `config/collection-policy.json` |
| `config/catalog-manifest.json` | `config/catalog-manifest.json` |
| `review/signals.json` | `review/signals.json` |

Run the remaining commands from inside the workspace, or pass
`--project-root WORKSPACE`. Confirm the copy with `telemetry check`.

## 3. Rebuild the candidate list

The normalized Techstream catalog rows are private because their
redistribution status is not established; see
[static catalog](findings/static-catalog.md) for how they were built. Import
them into the workspace, previewing first:

```powershell
telemetry source import file PATH-TO/catalog-rows.json            # preview
telemetry source import file PATH-TO/catalog-rows.json --apply
```

Import keeps the rows in the workspace, rebuilds the disposable SQLite cache,
and updates the privacy-minimized catalog manifest (row count, row hash, and
source label only). With the same rows, the manifest matches the exported one.

## 4. Bind an adapter and verify

Adapter choice and channel binding are local workspace settings and are never
exported. The current hardware is described in
[CAN adapter](hardware/can-adapter.md).

```powershell
telemetry connector set pcan --channel PCAN_USBBUS1
telemetry verify ENDPOINT --dry-run
telemetry verify ENDPOINT
```

Verification is live. It prints its exact scope and asks for confirmation.

## 5. Review signals and set collection targets

Signal and decode decisions are reviewed with `telemetry signal set` or the
desktop Signals page. The exported collection policy is intentionally empty:
it does not treat earlier collection settings as current authority. Add
reviewed targets and shared DID settings, then check readiness offline:

```powershell
telemetry target set SIGNAL --min-samples 200 --min-runs 3
telemetry did set ENDPOINT/0xNNNN --interval 500 --max-interval 1000
telemetry collect --dry-run
```

With the current empty policy, readiness reports that the collection policy
has no included targets.

## 6. Collect only after reviewing the scope

`telemetry collect` is live. It prints the exact scope and asks for `yes`; a
scripted `--yes` is accepted only for the scope last shown by `--dry-run`.

```powershell
telemetry collect --note "stationary, IG-ON"
```

No road test is authorized merely by this documentation. Review the XA50
collection scope and bounds first, then use a passenger or unattended logger
and obey the stop rules in [road-test.md](validation/road-test.md).

## 7. Export into this repository

```powershell
telemetry export PATH-TO/70Y074-telemetry-XA50
git -C PATH-TO/70Y074-telemetry-XA50 status --short
```

(GUI: Recordings > Export result.) Export writes `config/`, `review/`, and a
new immutable generation under `result/generations/`, and updates
`result/current.json`. Exported files omit raw response bytes, timestamps,
trip notes, adapter identifiers, and local paths. Export refuses a destination
whose current result belongs to another project.

Review the complete diff before committing. Write findings and evidence
reports under `docs/` by hand; nothing from the workspace is copied here except
by export.

## Historical documentation

Some finding and evidence documents retain the exact earlier commands used to
produce past observations, including paths into a sibling toolkit checkout.
Those snippets are provenance, not runnable instructions, and have no
compatibility path in the current toolkit.
