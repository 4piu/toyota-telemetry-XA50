# Scope and safety

## Purpose

Establish a read-only experimental boundary before connecting a diagnostic requester. This guide is for telemetry acquisition, not control, coding, customization, DTC clearing, security access, memory access, or reflashing.

## Rules

- Begin stationary in Park with the parking brake applied; use IG-ON or READY only when the experiment requires it.
- Treat every diagnostic client as an active CAN participant, even when it only requests data.
- Permit exactly one tester to own an ECU diagnostic session. Close Techstream/PCAN-View or any competing client before transmitting.
- Default to dry-run. Live mode must require an explicit flag and a byte-exact transmit allowlist.
- Allow only required session control, tester present, dynamic-DID definition/clear, flow control, and read requests.
- Reject unknown vehicle profiles, unexpected arbitration IDs, negative responses not anticipated by the procedure, malformed ISO-TP, and timeouts.
- Send the next request after the previous transaction completes. Start at the observed OEM cadence.
- Do not keep sleeping ECUs awake or risk 12-V battery depletion.
- During road validation, use an unattended logger or a passenger. The driver must not operate a computer.
- Never use reverse-engineered telemetry as an input to vehicle control or a safety-critical warning.

## Evidence produced

Copy [the experiment template](../templates/experiment.yaml) and record the state, permitted transmissions, stop conditions, operator arrangement, and recovery plan. A result without this context is not reproducible.

## Stop conditions

Disconnect the tester after repeated UDS `busyRepeatRequest`, prolonged `responsePending`, transport errors, CAN controller errors, receive overruns, unexplained warning lamps, ECU resets, or behavior outside the experiment plan.
