# XA50 validation profile

[`validation-profile.json`](validation-profile.json) is the platform boundary
consumed by the reusable telemetry toolkit. It records the XA50 facts and
policy that do not belong in generic code:

- Hybrid Control ECU key 397 and the live-confirmed `0x7D2 -> 0x7DA` route;
- the XA50 catalog, plan, capture, and decode-override paths;
- privacy/identity exclusions and DID range;
- fast, normal, slow, and archive scheduling policies;
- the current PCAN backend executable and its candidate modes.

All paths are relative to the repository root identified by
`telemetry-project.toml`. Nothing assumes a sibling checkout or a particular
repository directory name.

The generic toolkit version is pinned in `toolkit-requirement.txt` and in the
project marker. The existing XA50 Python orchestration remains temporarily as a
live-proven compatibility implementation while the package revision is
published and adopted. Generated plan parity has been checked against it: both
select 588 candidates with tier counts 245/229/68/46 and the same DID/tier
ordering.
