# CAN adapter and vehicle connection

This is the interface every XA50 result in this repository was produced with.
It is the only hardware path here that has been live-validated on the vehicle.

## Adapter

A **CANable v1.0** — an isolated STM32F072-based USB-CAN interface — flashed
with firmware that the PEAK Windows driver enumerates as `PCAN-USB`. That is
what makes the PCAN-Basic backend work against it: the application opens
`PCAN_USBBUS1` through `PCANBasic.dll` and never knows the device is not a
PEAK-branded adapter.

Two consequences worth stating plainly:

- A genuine PEAK PCAN-USB adapter works with the same backend and the same
  configuration. Nothing in this project depends on the CANable specifically.
- A CANable running stock `candleLight` or `slcan` firmware will **not** work
  with the PCAN-Basic backend, because those present a different device
  interface. Either reflash it, or use a backend written for that interface.

Galvanic isolation is not optional in the way the rest of this is. A
mains-earthed desktop plus a non-isolated adapter can create an unintended
ground path through the vehicle; prefer an isolated adapter, a battery-powered
laptop, or both.

## DLC3 / OBD-II connection

Connect only these three pins:

| DLC3 pin | Function | Connection |
|---:|---|---|
| 5 | Signal ground | Adapter ground |
| 6 | CAN-H | Adapter `CANH` |
| 14 | CAN-L | Adapter `CANL` |
| 16 | Battery positive | **Leave disconnected** |

Pin 4 is chassis ground and is not required. Use a short twisted pair for pins
6 and 14, and provide strain relief.

OBD plug diagrams are easily mirrored: numbering viewed from the mating face is
not the same as the solder side. Use the molded pin numbers and confirm
continuity from each solder terminal to the mating contact before plugging into
the car.

**Do not fit a 120-ohm terminator at the diagnostic tap.** The vehicle bus is
already terminated; adding a third terminator loads it. With all equipment
disconnected and the vehicle powered down and asleep, an intact terminated bus
commonly measures near 60 ohms across pins 6 and 14. Resistance measurements
are not meaningful on a powered bus.

**Do not power the adapter from DLC3 pin 16.** USB power avoids exposing the
setup to automotive battery transients. A permanent installation would need a
fuse, reverse-polarity protection, surge/load-dump and ESD protection, an
automotive regulator, sleep control, and verified low quiescent current — none
of which this project has built.

## Bring-up sequence and what it proved

Recorded here because each step isolates a different failure, and the order
mattered when it went wrong.

1. **Listen-only at 500 kbit/s in PCAN-View produced no frames.** This is
   expected: the DLC3 gateway is on-demand and does not mirror bus traffic to
   the diagnostic connector. It is *not* by itself evidence of bad wiring.
2. **One allowlisted tester-present frame at `0x7D2` produced no reply**, while
   the adapter activity LED blinked and CAN status stayed OK. The LED proves
   USB and firmware activity, nothing about the bus.
3. **A standardized OBD Mode-01/PID-00 query at functional ID `0x7DF` also
   failed** — which separated "basic CAN TX/RX is broken" from "Toyota gateway
   routing is broken." It was the former.
4. **Root cause: CAN-L and ground were crossed at the adapter.** After
   correcting the wiring, the OBD probe succeeded immediately: ECU `0x7ED`
   returned `06 41 00 98 18 80 03 00` in 5 ms. That single result proves
   firmware, 500-kbit/s bit timing, 11-bit TX and RX, filtering, physical
   wiring, and the DLC3 OBD path all at once.
5. **The Toyota-specific gate then passed**: one tester-present frame at
   `0x7D2` returned exactly `0x7DA  02 7E 00 ...` in 9 ms.
6. **Full ISO-TP session and dynamic-DID setup replayed successfully**, with
   the ECU returning flow control for the 20-byte definition and delivering
   telemetry as first frame plus consecutive frame.

The wiring fault is the lesson worth keeping: a blinking adapter LED and an OK
controller status are consistent with a completely miswired connection.

## Measured performance

A 100-sample stability run at a 100-ms minimum request interval completed
cleanly. Over samples 29–100:

| Metric | Value |
|---|---|
| Complete-response latency | 11 ms min, 14 ms median, 17 ms p95, 27 ms max |
| Reconstructed request interval | 99–112 ms, 101 ms median |

Direct wheel-speed responses took 12–19 ms, steering 7–9 ms, and the
multi-frame packed-brake response 12–18 ms. See [throughput](throughput.md)
for what this implies about achievable sample rates.

## Safety boundary

- Never connect or alter wiring while the vehicle is moving.
- Do the first active test stationary, in Park, with the parking brake applied.
- Close PCAN-View and any other CAN client before a live run. The backend
  refuses to start while PCAN-View is open, because two clients on one channel
  silently corrupt each other's view of the bus.
- Stop transmitting on malformed responses, receive overflow, rising CAN error
  counts, bus-off, or host loss.

Road procedure is in the [road-test protocol](../validation/road-test.md).
Do not operate the computer while driving.

## Not built

An RP2040 + MCP2515 bridge and an SBC/SocketCAN path were designed but never
constructed or tested. Those notes live in the untracked
`scratch/hardware-exploration/` directory rather than here, so that this
directory documents only what actually produced results.
