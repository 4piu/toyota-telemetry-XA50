# Road collection safety

Road collection is not authorized until the operator has reviewed the current
XA50 policy, offline admitted plan, endpoint, adapter binding, output path, plan
hash, and complete byte-exact transmit allowlist.

## Before moving

1. Rebuild private state and inspect captures offline.
2. Run `telemetry plan`; do not continue if admission reports missing or stale
   evidence, safety, cost, decode, applicability, storage, or source facts.
3. Inspect the admitted acquisitions and session steps.
4. Close every competing CAN/diagnostic client.
5. Secure the adapter, cable, and computer so they cannot interfere with the
   driver or controls.
6. Use a passenger as operator or an unattended logger. The driver does not
   interact with the interface.

## During collection

Start only with `telemetry collect` or the operator workspace's explicit Start
action. The command is inherently live and has no alternate mode. One request
is outstanding at a time and the scheduler never emits catch-up bursts.

The interface must keep connected, response-pending, transport-error,
stale-state, and disconnected visibly distinct. Stop immediately for unexpected
vehicle behavior, warning lamps, CAN/controller errors, repeated timeouts,
malformed transport, missing heartbeat, loose hardware, or any operator doubt.
The Stop button, hotkey, and Ctrl+C share one idempotent graceful path.

Do not induce hazardous events. ABS, stability control, collision avoidance,
airbag, overheating, and similar signals may be observed only when they occur
naturally and safely; they are not collection goals for an on-road test.

## After stopping

Run `telemetry capture inspect` and rebuild the private cache. Completed
segments form an anytime-valid prefix. A crashed partial earns no progress
unless explicit offline recovery creates and validates a new finalized copy;
the source partial remains untouched.

Use `telemetry workspace` to inspect all-run observations and save only reviewed
conclusions. Keep GPS, video, exact trip history, and raw captures under the
ignored private boundary. Regenerate tracked output with `telemetry export`.
