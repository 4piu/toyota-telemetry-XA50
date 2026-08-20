# Verify with PCAN-Basic

Use this route for a PEAK-System USB/PCI CAN adapter and the PCAN-Basic API. It behaves like [USB-CAN](usb-can.md): the adapter exposes classical CAN frames only, so ISO-TP framing, flow control, and the diagnostic session are all application-managed.

## Wiring and controller checks

Follow the same DLC3/OBD-II wiring, termination, and bus-resistance checks as [USB-CAN](usb-can.md). Close PCAN-View or any other PCAN-Basic client before a live run; the API allows only one owner of a channel at a time, and a second client silently steals or duplicates traffic instead of failing loudly.

## Procedure

1. Confirm bitrate and controller state with `CAN_GetStatus` before transmitting.
2. Install the narrowest available hardware filter (`CAN_FilterMessages`) for the expected response ID; drain any stale queued frames left by a prior session.
3. Prove TX/RX using one standardized OBD-over-CAN request, then one benign tester-present request to the candidate OEM endpoint, each with a strict expected-response filter.
4. Reproduce the captured session/setup and read transaction using application-managed ISO-TP: single/first/consecutive/flow-control frame types, sequence numbers, block size, and separation time from captured evidence, not assumption.
5. Accept only expected response IDs; validate DLC, PCI type, declared length, sequence numbers, and UDS prefix before treating a payload as a response.
6. Send flow control promptly after a first frame; do not issue a new request until completion or timeout.
7. On exit, disconnect and uninitialize the channel even on error, Ctrl+C, or timeout.

A working backend for this route ships in [`backends/`](https://github.com/4piu/70Y074-telemetry-tools/blob/main/backends/README.md) as `telemetry-pcan`. It carries no vehicle constants: endpoint, session setup, and DID range come from a `backend-config.json` the platform project supplies, and the ISO-TP layer, transmit allowlist, and capture writer are shared with every other backend. The device-specific part is [`PcanChannel.cs`](https://github.com/4piu/70Y074-telemetry-tools/blob/main/backends/Telemetry.Backend.Pcan/PcanChannel.cs), which implements the seven-member boundary [adding an adapter](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/adding-an-adapter.md) describes.

Start with single allowlisted probes. Once exact replay succeeds, expand one DID or dynamic definition at a time.
