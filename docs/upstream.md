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
2. Update the pinned dependency in `toolkit-requirement.txt` or explicitly port
   applicable template/schema changes.
3. Run the XA50 tests and inspect the diff.
4. Advance `upstream.revision` only to the revision actually reviewed.

The progressive planner, candidate-capture analyzer, immutable progress engine,
bounded runner, and executable backend contract have been extracted as the
`toyota-telemetry-tools` package. The XA50 profile, endpoint restrictions,
allowlists, PCAN/J2534 implementations, baseline decoder, and evidence remain
here. The live-proven XA50 orchestration copies remain temporarily for
collection continuity. The pinned package revision is published and has passed
canonical-install, plan, progress, dry-run, and fixture-analysis parity. Remove
the compatibility copies only after its CLI also passes one live batch.
