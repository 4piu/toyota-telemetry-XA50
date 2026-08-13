# Parking-lot road-validation protocol

The first execution of this protocol completed successfully. Its integrity and
signal conclusions are recorded in the
[pilot evidence report](../evidence/pilot-road-validation.md).

## Scope

This run targets the 13 signals already in `data/validated`, not the 4,458-row static candidate atlas. Do not bulk-scan DIDs while driving. The separate [direct-DID batch workflow](candidate-batches.md) admits only reviewed candidates that first pass stationary, byte-exact allowlisted qualification.

The combined PCAN mode uses one Hybrid Control session and repeats four known read transactions:

```text
F301 (MG1, MG2, accelerator)
1075 (four wheel speeds)
F302 (brake switches, master torque, requested/executed regeneration)
1090 (steering angle)
```

At the default 100-ms minimum request spacing, each group is sampled around 2.5 Hz. This favors breadth and conservative bus use over dashboard-rate acquisition.

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

Dry run:

```powershell
uv run python .\tools\validation\run-progressive-validation.py baseline --duration-seconds 900
```

Stationary rehearsal:

```powershell
uv run python .\tools\validation\run-progressive-validation.py baseline --live --duration-seconds 30
```

Parking-lot run:

```powershell
uv run python .\tools\validation\run-progressive-validation.py baseline --live --duration-seconds 900
```

The runner selects a timestamped partial/complete file below `$REPO_ROOT/scratch/captures/vehicle-a/progressive/baseline`. Per-sample console output is suppressed. A live run waits 10 seconds before opening PCAN so the operator can secure the computer; change this with `--startup-delay-seconds N`, or use zero to disable it. A baseline is promoted to `.complete.jsonl` only after it ends at the duration limit, contains every expected sample group, and passes offline analysis.

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
2. Run `tools/analysis/analyze-road-capture.py` and preserve its summary beside the private capture.
3. Review raw payloads around minima, maxima, sign changes, switch transitions, and any discontinuity.
4. Write down the actual maneuver timeline while it is fresh.
5. Promote only reviewed conclusions and sanitized excerpts into `data/validated`; keep the full road log, video, GPS, and exact timestamps in the workbench.
