# Field-guide upstream

This vehicle repository consumes the reusable method and generic tooling from
[`70Y074-telemetry`](https://github.com/4piu/70Y074-telemetry). The reviewed
source revision and local scratch convention are recorded in
[`telemetry-project.toml`](../telemetry-project.toml).

The repositories currently have unrelated root commits. The local Git remote
named `upstream` is therefore for fetching, comparison, and deliberately
porting shared changes. Do not run a whole-repository merge with
`--allow-unrelated-histories`; similarly named XA50 tools contain platform
behavior that must not be overwritten by a generic file.

Configure a fresh clone with:

```powershell
git remote add upstream https://github.com/4piu/70Y074-telemetry.git
git fetch upstream
```

The intended update model is:

1. Fetch `upstream` and review changes since the marker's pinned revision.
2. Update versioned generic-tool dependencies or explicitly port applicable
   template/schema changes.
3. Run the XA50 tests and inspect the diff.
4. Advance `upstream.revision` only to the revision actually reviewed.

As reusable code is extracted from the current vehicle-specific forks, it will
be exposed by the upstream repository as a versioned toolkit. The XA50 profile,
endpoint restrictions, allowlists, decode overrides, and evidence remain here.

