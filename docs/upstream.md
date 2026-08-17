# Field-guide upstream

This repository holds XA50 configuration, findings, and results. Every piece of
reusable method and executable code comes from
[`70Y074-telemetry`](https://github.com/4piu/70Y074-telemetry). There is no
tooling here — that is deliberate, and it is the check that the field guide is
actually complete.

> **Currently tracking `main` for pre-publish testing.** Both pins below name a
> branch rather than a commit, so a fresh install picks up upstream changes
> without a bump. That is deliberate while the repositories are unpublished and
> changing together; it is not reproducible, and both must become commit hashes
> before release.

Two pins record what was reviewed:

| File | Pins |
|---|---|
| [`telemetry-project.toml`](../telemetry-project.toml) | The field-guide revision this repository's configs and docs were written against. |
| [`toolkit-requirement.txt`](../toolkit-requirement.txt) | The `toyota-telemetry-tools` package revision. |

The repositories have unrelated root commits. A local Git remote named
`upstream` is for fetching and comparison only. Do not run a whole-repository
merge with `--allow-unrelated-histories`.

```powershell
git remote add upstream https://github.com/4piu/70Y074-telemetry.git
git fetch upstream
```

## What comes from upstream

| Component | Where it lives upstream |
|---|---|
| `telemetry-build-plan`, `telemetry-validate`, `telemetry-analyze-capture`, `telemetry-review` | `src/toyota_telemetry/` |
| `telemetry-pcan`, `telemetry-j2534` backends | `backends/` |
| Catalog, Techstream, and J2534 log tooling | `tools/` |
| Method documentation and the evidence model | `docs/` |
| Config templates | `templates/` |

## What stays here

The XA50 vehicle and ECU profile, the diagnostic endpoint and its session
setup, tier and privacy policy, reviewed decode overrides, experiment
schedules, the curated signal map, and the findings and evidence behind it.

The backend executables are built from the pinned field-guide revision into the
ignored `scratch/bin/` directory rather than committed or assumed to sit in a
sibling checkout. See [reproduce.md](reproduce.md).

## Updating the pin

1. Fetch `upstream` and review changes since the pinned revision.
2. Rebuild the backends and run `--self-test`.
3. Run `telemetry-validate status` and confirm progress numbers are unchanged —
   progress is reconstructed from immutable captures, so a difference means a
   behaviour change worth understanding before going further.
4. Run `telemetry-review --check data/map/signals.csv`.
5. Advance `upstream.revision` and `toolkit-requirement.txt` only to the
   revision actually reviewed.

Advancing the pin can change the generated plan. Inspect the plan diff before
any live session.
