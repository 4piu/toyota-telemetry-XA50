# Road collection safety

Road collection is not authorized until the operator has reviewed the current
XA50 targets, the readiness-checked scope, endpoint, adapter binding, output
path, scope hash, and complete bounded read-only transaction scope.

## Before moving

1. Check recordings offline (`telemetry recordings`) and recover or
   quarantine any interrupted run.
2. Run `telemetry collect --dry-run` (or open the desktop Collect page); do not
   continue if readiness reports missing or stale evidence, safety, cost,
   decode, condition, storage, or source facts.
3. Inspect the scope's DIDs, session steps, and storage bounds.
4. Close every competing CAN/diagnostic client.
5. Secure the adapter, cable, and computer so they cannot interfere with the
   driver or controls.
6. Use a passenger as operator or an unattended logger. The driver does not
   interact with the interface.

## During collection

Start only with `telemetry collect` (typed confirmation) or the desktop
Collect page's LIVE start after its scope dialog. One request is outstanding at
a time and the scheduler never emits catch-up bursts. Only one live job may run
per project.

The interface must keep connected, response-pending, transport-error,
stale-state, and disconnected visibly distinct. Stop immediately for unexpected
vehicle behavior, warning lamps, CAN/controller errors, repeated timeouts,
malformed transport, missing heartbeat, loose hardware, or any operator doubt.
The desktop Stop button, Ctrl+C, and `stop` on standard input share one
idempotent graceful path.

Do not induce hazardous events. ABS, stability control, collision avoidance,
airbag, overheating, and similar signals may be observed only when they occur
naturally and safely; they are not collection goals for an on-road test.

## After stopping

Run `telemetry recordings` (or open the Recordings page). Completed
segments form an anytime-valid prefix. A crashed partial earns no progress
unless explicit offline recovery (`telemetry recordings recover RUN`) creates
and validates a new finalized copy; the source partial remains untouched.

Inspect all-run observations on the Recordings page or with
`telemetry recordings export`, and save only reviewed conclusions. Keep GPS, video, exact trip history, and raw captures
under the ignored private boundary. Regenerate tracked output with
`telemetry result generate`.
