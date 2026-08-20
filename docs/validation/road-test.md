# Parking-lot road-validation protocol

The first execution of this protocol completed successfully. Its integrity and
signal conclusions are recorded in the
[pilot evidence report](../evidence/pilot-road-validation.md).

## Scope

A road batch reads a small group of DIDs that have already qualified stationary — never the whole catalog. Do not bulk-scan DIDs while driving. The [validation status and policy](README.md) records the tier policy that decides how many DIDs a batch carries and for how long.

The batch is built by the planner, not written by hand: `telemetry-validate road` takes the next queued batch per ECU and transmits only those identifiers, through the same byte-exact allowlist as any other capture. One request is outstanding at a time, so the tier's request spacing sets the bus load regardless of how many DIDs the batch holds.

## Recorder guarantees

- No interaction is required after starting; stop occurs at the configured duration, log limit, error, or Ctrl+C.
- The JSONL file is create-new and cannot overwrite an earlier run.
- Default bounds are 15 minutes and 64 MiB. Start is refused unless the destination also has a 256-MiB free-space reserve.
- Every line is flushed and contains either session metadata, an event, a complete sample, an error, or the final summary.
- Samples contain host UTC, monotonic request/response offsets, latency, raw UDS response, and decoded values.
- Unknown responses, malformed ISO-TP, timeouts, CAN errors, and logging failures stop the session and close PCAN.
- VIN, GPS/location, odometer, and adapter serial are not requested or recorded.

The expected default volume is only a few megabytes, but the hard limits are enforced independently of that estimate.

## Before driving

1. Inspect wiring and strain relief. Termination remains disabled on the adapter.
2. Secure the adapter, connector, and computer so nothing can reach pedals, steering, or airbags.
3. Disable sleep/hibernate and unnecessary automatic restarts; confirm adequate battery power.
4. Close PCAN-View and every other diagnostic client.
5. Run the wrapper without `-Live`; review the exact plan and destination.
6. While stationary in Park and READY, run a short 30-second live rehearsal and analyze it before the road run.
7. Set up any camera/reference before moving. The driver must not touch or look at the computer while the vehicle moves.
8. Prefer a passenger to supervise equipment. If alone, start while parked, secure the computer, perform the route, park, and only then inspect/stop it.

Preview the queue — nothing is transmitted without `--live`:

```powershell
$profile = "data\config\validation-profile.json"
telemetry-validate road --config $profile --max-items 4
```

Stationary rehearsal, one short batch:

```powershell
telemetry-validate road --config $profile --live --max-items 1
```

Parking-lot run:

```powershell
telemetry-validate road --config $profile --live --max-items 4
```

Each batch writes a timestamped partial/complete file below the capture root its ECU declares in the profile. A live run waits 10 seconds before opening the adapter so the operator can secure the computer; change this with `--startup-delay-seconds N`, or use zero to disable it. A progress line reports the elapsed time and record count while a batch runs; `--no-progress` suppresses it for an unattended log. A batch is promoted to `.complete.jsonl` only after it ends at the duration limit, carries the minimum samples for every DID in it, and passes offline analysis.

## Low-risk maneuver sequence

Perform only maneuvers legal and comfortable for the site. Abort for pedestrians, traffic, warning lamps, loose equipment, unexpected vehicle behavior, or degraded control.

1. Remain stationary for about 30 seconds: released pedals, then several gentle brake applications.
2. Drive straight at several steady low speeds. The four wheel values should be close and change smoothly with speed.
3. Make broad, slow left and right curves. Steering should change sign/direction consistently and return near its prior straight-ahead baseline; inner/outer wheel differences should be plausible.
4. Apply and release the accelerator gently. Accelerator should follow input; MG values should remain continuous and physically plausible, including ICE start/stop transitions.
5. Coast and perform several gentle decelerations, progressing from light lift-off to ordinary braking. Stop-light state, master control torque, and requested/executed regenerative torque should transition coherently.
6. If safe and useful, reverse at walking speed to learn whether the reported wheel/MG fields preserve direction.
7. Finish with another stationary released/brake baseline, park, and then handle the computer.

Do not deliberately trigger ABS, traction/stability control, emergency braking, rapid steering, or high-speed maneuvers for this validation.

## Evidence without Techstream

These checks can raise confidence from Observed to Correlated, but generally not to independent display-Validated:

- stopped state gives a strong wheel-speed zero reference;
- four wheels should agree on a straight path and diverge plausibly in a turn;
- steering direction, continuity, and return-to-baseline can be checked against the maneuver;
- accelerator direction and released zero are direct references;
- stop-light transition can be compared with the vehicle's brake lamps by a passenger/camera;
- requested and executed regeneration should broadly track during normal regenerative deceleration;
- hydraulic/master-control torque should respond to pedal braking but still does not prove literal pad contact;
- raw fields should be continuous and stay within plausible ranges.

A secured instrument-cluster video can provide an approximate speed/reference timeline. Optional phone GPS/IMU data can strengthen correlation, but it is location-sensitive and must stay in the private workbench. Record clock offset before the run rather than interacting with devices while moving.

## After the run

1. Confirm the JSONL ends with `session_end`; an `error` or missing end record requires investigation.
2. Run `the retired road-capture analyzer` and preserve its summary beside the private capture.
3. Review raw payloads around minima, maxima, sign changes, switch transitions, and any discontinuity.
4. Write down the actual maneuver timeline while it is fresh.
5. Promote only reviewed conclusions into `data/config/curation.csv` and regenerate `data/map/`; keep the full road log, video, GPS, and exact timestamps in the workbench.
