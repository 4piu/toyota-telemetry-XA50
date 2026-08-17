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

## Why the bus runs at 500 kbit/s

500 kbit/s is a property of the vehicle's diagnostic network, not a setting this
project chose. The 2026-08-08 Techstream trace shows `PassThruConnect` at
500,000 bit/s, and the standardized OBD probe answers at that rate, so it is
what the gateway and the ECUs behind it are configured for.

An adapter rated for a higher bitrate does not change that. Both ends of a CAN
network must agree on bit timing: configure an adapter for 1 Mbit/s on a
500-kbit/s bus and it cannot decode a single frame, and anything it transmits
is malformed from every other node's point of view. Sustained error frames on a
live vehicle bus can drive nodes into bus-off. Do not raise it to match what an
adapter supports.

Bandwidth was never the limit in any case. One diagnostic transaction at a
100-ms request interval is four Classical-CAN frames, roughly 1-2% of the bus
after overhead and stuffing. What bounds the sample rate is the round trip -
11-17 ms typical, 27 ms worst case in the stability run - and how hard it is
reasonable to work an ECU that is simultaneously doing its real job while the
vehicle moves.
