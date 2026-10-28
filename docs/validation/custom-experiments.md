# Retired fixed experiments

The pre-0.4 stationary and extended-road experiment files were part of the
fixed-batch scheduler and were removed in the clean break. They are not parsed,
translated, or accepted as collection authority.

Express a new need as shared DID settings and signal targets in
`config/collection-policy.json` (`telemetry did set` / `telemetry target set`,
or the desktop Signals page). Use explicit completion requirements, conditions,
cadence, and storage bounds, then check readiness with
`telemetry collect --dry-run`. A run may carry a free-text note, but that note is metadata
only and cannot alter scheduling or evidence credit.

If a future experiment cannot be represented by the current policy schema,
specify that requirement before adding a parallel runner or capture format.
