# Hardware and throughput

## What currently limits the update rate

The validated dynamic-DID poll contains three displayed values plus one auxiliary byte. At the ISO-TP/CAN level, one transaction is approximately:

1. tester single-frame request;
2. ECU first-frame response;
3. tester flow-control frame;
4. ECU consecutive frame.

At 10.4 transactions/s, four padded Classical-CAN frames per transaction consume only roughly 1-2% of a 500-kbit/s bus after allowing for frame overhead and bit stuffing. Raw CAN bandwidth is not the present bottleneck.

The host trace measured:

- complete reply 31-59 ms after the request, median 48 ms;
- request-to-request interval 79-160 ms, median 96 ms.

This includes gateway, ECU, adapter, USB/driver, and host API timing. Techstream also clears buffers and performs work before its next request. A dedicated poller may reduce that post-response delay, but changing transceivers cannot make the gateway or ECU process a request faster.

A sensible initial target is 10-15 samples/s. Twenty samples/s is only a rough upper bound implied by the median latency and is not validated; the 59-ms tail already makes a fixed 50-ms period aggressive.

The independent native-J2534 client has now completed 100 consecutive requests at a configured 100-ms minimum start-to-start interval without a transport error. Most complete-response latencies were approximately 43-62 ms, with isolated host/driver outliers above 100 ms. Faster sustained intervals remain to be tested; the scheduler deliberately does not “catch up” after an outlier.

The first PCAN-Basic raw-CAN ISO-TP run completed five `F301` samples at the same 100-ms minimum period with 13-17-ms complete-response latency. This includes receiving the ECU first frame, transmitting tester flow control, and receiving the final consecutive frame. The much lower host-observed latency shows that the previous 43-62-ms result was dominated by the J2534 adapter/driver path rather than the DLC3 gateway or ECU. The later 100-sample stability run below supersedes this initial short-run caveat.

The follow-up PCAN stability run completed all 100 requested samples and closed normally. In the retained 72-sample excerpt (samples 29-100), complete-response latency was 11-ms minimum, 14-ms median, 14.03-ms mean, 17-ms p95, and 27-ms maximum. Reconstructed request-start intervals were 99-112 ms with a 101-ms median and 100.96-ms mean; the scheduler did not attempt to catch up after the isolated latency outlier. This validates sustained 10-Hz `F301` polling while leaving considerable timing headroom. Faster polling remains optional rather than necessary for the first dashboard.

The first live PCAN motion and `F302` runs also completed without transport or validation errors. Direct wheel responses took 12-19 ms and steering responses 7-9 ms. The nine-byte packed-brake response, including ECU first frame, tester flow control, and the consecutive frame, took 12-18 ms with a 15-ms mean and median. These results show that all three implemented telemetry modes fit comfortably within the initial 100-ms scheduling interval.

## MCP2515 and MCP2551 assessment

The existing pair is adequate for the focused prototype:

- MCP2515 supports Classical CAN through 1 Mbit/s and SPI through 10 MHz. Its two receive buffers and three transmit buffers are sufficient for one narrowly filtered 500-kbit/s ISO-TP session.
- MCP2551 is the physical-layer transceiver and supports up to 1 Mbit/s. It does not schedule UDS or accelerate ECU response.
- The car bus must remain at the captured 500 kbit/s; a controller's higher maximum rate does not change an existing network bitrate.

The MCP2515's small receive-buffer count is a concern for broad passive capture on a busy bus or a host that services interrupts slowly. It is unlikely to be a problem when hardware-filtering for the single `0x7DA` diagnostic response.

References: [MCP2515 data sheet](https://ww1.microchip.com/downloads/en/DeviceDoc/MCP2515-Family-Data-Sheet-DS20001801K.pdf), [MCP2551 data sheet](https://ww1.microchip.com/downloads/en/devicedoc/21667e.pdf), and [MCP2551 product status](https://www.microchip.com/en-us/product/MCP2551).

## Rate experiment

Run this only after the minimal independent client works at the captured cadence, while the vehicle is stationary:

1. Start with a 100-ms minimum request interval.
2. Send no request until the preceding ISO-TP response is complete; do not pipeline.
3. Record request interval, complete-response latency, timeout count, negative responses, and transport errors.
4. If clean, test 80 ms, then 70 ms. The capture contained isolated 79-ms intervals but does not prove sustained faster operation.
5. Back off immediately on malformed data, dropped frames, receive overruns, CAN errors, UDS `7F 22 21` (`busyRepeatRequest`), or repeated `7F 22 78` (`responsePending`).

Render the UI independently from acquisition. Interpolation can make visual motion smooth, but each value must retain its real sample timestamp and stale-data indication.

## SBC software path

For a Linux SBC:

```text
application UDS payloads
  -> kernel CAN_ISOTP socket
  -> SocketCAN MCP2515 driver
  -> SPI MCP2515 CAN controller
  -> MCP2551 or replacement transceiver
  -> DLC3 CAN bus
```

The Linux ISO-TP layer handles first/consecutive frames and flow control that J2534 currently hides. See the [Linux kernel ISO-TP documentation](https://docs.kernel.org/networking/iso15765-2.html).

For a microcontroller, use or implement an ISO-TP state machine that handles flow control promptly, timeouts correctly, and never sends a second UDS request before the previous response completes.

The Windows client now includes a validated raw-CAN ISO-TP implementation as a behavioral reference. It has successfully handled the exact multi-frame dynamic-DID definition and multi-frame polling response used by this project.

## Electrical and installation cautions

- Disable or remove the 120-ohm termination fitted to many MCP2515 modules. The vehicle CAN bus is already terminated; an OBD tap should not add a third terminator.
- MCP2551 is a 5-V, end-of-life part. Verify the module schematic and logic levels before connecting RXD or SPI circuitry to a 3.3-V SBC. Use level translation or a current 3.3-V-compatible CAN transceiver where required.
- Use common signal ground and verify CAN-H/CAN-L wiring before enabling transmission.
- Begin in listen-only mode for hardware bring-up, then enable only the exact diagnostic allowlist.
- A permanent device needs fused/protected power, reverse-polarity protection, load-dump/transient protection, ESD protection, and reliable sleep behavior. A bare hobby module is not installation-ready.
- Do not add a device that prevents the vehicle ECUs from sleeping or drains the 12-V battery.

## Final acquisition properties

- Explicit transmit allowlist.
- Per-ECU rate limits, timeouts, and exponential backoff.
- Hardware acceptance filters where possible.
- Fail closed on unknown vehicle/calibration profiles.
- Stop transmitting on malformed replies, excessive latency, or bus errors.
- Timestamp and quality metadata attached to every decoded value.
