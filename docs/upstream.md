# Field-guide upstream

This repository holds XA50 configuration, findings, and results. Every piece of
reusable method and executable code comes from
[`70Y074-telemetry`](https://github.com/4piu/70Y074-telemetry). There is no
tooling here — that is deliberate, and it is the check that the field guide is
actually complete.

> **Currently tracking `main` for pre-publish testing.** The pin below names a
> branch rather than a commit, so a fresh install picks up upstream changes
> without a bump. That is deliberate while the repositories are unpublished and
> changing together; it is not reproducible, and it must become a commit hash
> before release. `setup-platform-project.ps1` warns whenever it is not one.

One pin records what was reviewed:
[`telemetry-project.toml`](../telemetry-project.toml) names the field-guide
revision this repository's configs and docs were written against. The Python
toolkit is installed from that same checkout rather than resolved separately,
so there is no second pin to keep in step with it.

The repositories have unrelated root commits. A local Git remote named
`upstream` is for fetching and comparison only. Do not run a whole-repository
merge with `--allow-unrelated-histories`: both repositories carry a `README.md`,
a `docs/` tree, a `.gitignore`, and a private-data policy, and in each case the
two files are different documents rather than versions of one. The READMEs
share nine lines out of eighty. A merge would raise those conflicts on every
update and resolve none of them. See the field guide's
[platform-project contract](https://github.com/4piu/70Y074-telemetry/blob/main/docs/09-platform-projects.md)
for what belongs on which side.

```powershell
git remote add upstream https://github.com/4piu/70Y074-telemetry.git
git fetch upstream
```

## What comes from upstream

| Component | Where it lives upstream |
|---|---|
| `telemetry-build-plan`, `telemetry-validate`, `telemetry-analyze-capture`, `telemetry-review`, `telemetry-map`, `telemetry-probe` | `src/toyota_telemetry/` |
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
5. Advance `upstream.revision` only to the revision actually reviewed, then
   re-run `setup-platform-project.ps1` so the backends and the installed
   toolkit both follow it.
6. Update the `blob/<revision>/` links in these docs to the same revision.
   Links that name a branch describe whatever upstream looks like today, which
   is not necessarily the procedure that produced the evidence here.

Advancing the pin can change the generated plan. Inspect the plan diff before
any live session.
