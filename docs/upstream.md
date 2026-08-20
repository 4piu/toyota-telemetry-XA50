# Toolkit upstream

This repository holds XA50 configuration, findings, and results. Every piece of
reusable method comes from the
[`70Y074-telemetry` template](https://github.com/4piu/70Y074-telemetry), and
every piece of executable code from the
[toolkit](https://github.com/4piu/70Y074-telemetry-tools) it points at. There
is no tooling here — that is deliberate, and it is the check that the toolkit
is actually complete.

> **Currently tracking `main` for pre-publish testing.** The pin below names a
> branch rather than a commit, so a fresh install picks up upstream changes
> without a bump. That is deliberate while the repositories are unpublished and
> changing together; it is not reproducible, and it must become a commit hash
> before release. `setup-platform-project.ps1` warns whenever it is not one.

One pin records what was reviewed:
[`telemetry-project.toml`](../telemetry-project.toml) names the toolkit
revision this repository's configs and docs were written against. The Python
toolkit is installed from that same checkout rather than resolved separately,
so there is no second pin to keep in step with it.

The repositories have unrelated root commits. A local Git remote named
`upstream` is for fetching and comparison only. Do not run a whole-repository
merge with `--allow-unrelated-histories`: both repositories carry a `README.md`,
a `docs/` tree, a `.gitignore`, and a private-data policy, and in each case the
two files are different documents rather than versions of one. The READMEs
share nine lines out of eighty. A merge would raise those conflicts on every
update and resolve none of them. See the template's
[platform-project contract](platform-projects.md)
for what belongs on which side. `upstream` here is the toolkit, since that is
what actually needs advancing:

```powershell
git remote add upstream https://github.com/4piu/70Y074-telemetry-tools.git
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

The backend executables are built from the pinned toolkit revision into the
ignored `scratch/bin/` directory rather than committed or assumed to sit in a
sibling checkout. See [reproduce.md](reproduce.md).

## Updating the pin

The [toolkit's checklist](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/updating-the-pin.md) has the
steps. Advancing the pin can change the generated plan — inspect the plan diff
before any live session.
