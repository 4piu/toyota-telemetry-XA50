# Hardware prototype and bring-up

## Decision

`MCP2515` and `MCP2551` are not alternative transceiver boards:

- `MCP2515` is a Classical-CAN controller. The host talks to it over SPI.
- `MCP2551` is the electrical CAN transceiver. It converts `TXCAN/RXCAN` logic into the differential `CAN-H/CAN-L` bus.

A design made from the separate parts uses both in series. However, many boards sold as "MCP2515 modules" already contain an MCP2551, TJA1050, or similar 8-pin transceiver and expose `CANH` and `CANL`. Such a board must not be followed by a second MCP2551.

Before assigning physical pins, record the exact board names, oscillator markings, and clear photographs of both sides. Hobby modules differ in supply arrangement, logic levels, oscillator frequency, transceiver, and termination.

## Recommended development path

The available isolated STM32F072 USB-CAN interface is the preferred Windows bring-up tool. It is recognized through the PEAK driver as `PCAN-USB`, allowing the already proven raw-CAN transaction to be tested through PCAN-Basic before developing the final SBC interface. A 500-kbit/s PCAN-View listen-only test at DLC3 produced no received frames; this is consistent with an on-demand diagnostic gateway and is not, by itself, evidence of incorrect wiring.

The active PCAN test is implemented under `tools/validation/Xa50.Pcan`. It filters for standard ID `0x7DA`, transmits the byte-exact tester-present frame at `0x7D2` once, accepts only `02 7E 00 ...`, and then closes the channel. It is dry-run by default and refuses to run live while PCAN-View is open.

The first live attempt successfully queued the one `0x7D2` frame and caused the adapter activity LED to blink, but no `0x7DA` reply arrived within 1.5 seconds and the reported final CAN status remained OK. The LED establishes USB/firmware activity, not CAN acknowledgement. A second control mode therefore sends the standardized, read-only Mode-01/PID-00 OBD support query once at functional ID `0x7DF` and accepts only positive responses at `0x7E8` through `0x7EF`. This separates basic CAN transmit/receive verification from Toyota gateway routing.

The first two active attempts received no replies because CAN-L and ground were inadvertently crossed at the adapter connection. After correcting the wiring, the standard filtered OBD probe succeeded immediately: ECU `0x7ED` returned `06 41 00 98 18 80 03 00` in 5 ms. This proves the PCAN-compatible firmware, 500-kbit/s timing, standard 11-bit transmit and receive, filtering, physical wiring, and DLC3 OBD path. No CAN error or apparent vehicle harm was observed.

The corrected connection then passed the Toyota-specific gate: one tester-present frame at `0x7D2` received the exact `0x7DA  02 7E 00 ...` reply in 9 ms. The PCAN client now also contains the validated application-managed ISO-TP state machine and an explicit `--f301` mode. It performs the captured session and dynamic-DID setup, verifies every response and transport sequence, and polls five MG1/MG2/accelerator samples at a 100-ms minimum start-to-start interval by default. Both UDS payloads and their generated raw CAN fragments are byte-exact allowlisted.

The first live `--f301 --verbose` run completed successfully. Hybrid Control accepted every setup request, returned flow control for the 20-byte definition, and provided each nine-byte telemetry payload as a first frame plus one consecutive frame after tester flow control. Five samples completed at approximately 100-ms start intervals with 13-17-ms end-to-end response latency. With light accelerator input in Park, MG1 was 5,367-5,383 rpm, MG2 was -3 to +1 rpm, accelerator was 20.5-21.5%, and the auxiliary byte remained zero. This independently reproduces the original J2534 result through the new hardware.

A subsequent 100-sample non-verbose stability run reached sample 100 and closed cleanly. The supplied sample-29-through-100 excerpt contained 72 consecutive responses: latency was 11-ms minimum, 14-ms median and mean, 17-ms p95, and 27-ms maximum. Approximate request-start intervals reconstructed from output timestamp minus latency were 99-112 ms with a 101-ms median. At rest, MG1 stayed between -2 and +1 rpm and MG2 between -2 and +2 rpm; the same small zero-speed variation is visible in Techstream. Accelerator and the auxiliary byte remained zero. The PCAN `F301` path is therefore stable at 10 Hz.

The first live `--motion` run also passed every session, transport, and response check. Five paired samples returned all four wheels at 0.00 km/h and steering at a constant -3.0 degrees. Wheel-response latency was 12-19 ms; steering-response latency was 7-9 ms. This reproduces the known stationary baseline through PCAN. It does not replace the pending rolling wheel-speed validation.

The first live `--f302` run passed the multi-frame dynamic-DID definition and all five multi-frame polls. During a continuous brake application, switch word `0x0303` was present from the first sample and master-cylinder control torque progressed -164.250, -315.125, -457.125, -532.500, then -696.750 Nm. Requested and executed regenerative torque stayed 0 Nm, as expected while stationary in Park. Response latency was 12-18 ms. A steering touch during this run is not observable because `--f302` does not request steering DID `1090`.

The `--open-filter` mode remains available as a troubleshooting tool, but is no longer needed for normal focused testing.

```powershell
.\tools\validation\build-pcan-client.ps1
.\tools\validation\Xa50.Pcan\bin\Release\xa50-pcan.exe --dry-run

# Only while stationary in Park, IG-ON/READY, with PCAN-View closed:
.\tools\validation\Xa50.Pcan\bin\Release\xa50-pcan.exe --live

# One standardized OBD Mode-01/PID-00 query instead:
.\tools\validation\Xa50.Pcan\bin\Release\xa50-pcan.exe --live --obd-probe

# The same one-shot OBD query while receiving every CAN ID locally:
.\tools\validation\Xa50.Pcan\bin\Release\xa50-pcan.exe --live --obd-probe --open-filter

# First conservative telemetry run (five F301 samples at >= 100 ms):
.\tools\validation\Xa50.Pcan\bin\Release\xa50-pcan.exe --live --f301 --verbose

# Five paired wheel-speed/steering samples, with >= 100 ms between requests:
.\tools\validation\Xa50.Pcan\bin\Release\xa50-pcan.exe --live --motion --verbose

# Five synchronized packed-brake samples at >= 100 ms:
.\tools\validation\Xa50.Pcan\bin\Release\xa50-pcan.exe --live --f302 --verbose
```

The three telemetry modes have independent UDS and raw-frame allowlists. A selected mode cannot transmit requests belonging to either of the others. Offline self-checks assert the exact single, first, consecutive, and flow-control CAN frames before PCAN-Basic is opened. All three modes have now also completed live stationary tests.

The future dashboard repository can still use the MCP2515 module through an SBC SPI interface. The following RP2040 bridge remains a useful fallback and an independent hardware-development path. These arrangements are planned, not yet ported or live-validated.

The first prototype should use the RP2040 as a narrow USB-to-CAN bridge:

```text
Windows PC
  <- native USB CDC ->
RP2040
  <- SPI + interrupt ->
MCP2515
  <- TXCAN/RXCAN ->
MCP2551 or the transceiver already on the MCP2515 module
  <- CAN-H/CAN-L ->
DLC3 pins 6/14
```

This lets the PC reuse the already validated application-managed ISO-TP behavior. The bridge initially transports raw timestamped CAN frames and enforces a transmit allowlist. ISO-TP and UDS remain on the PC until the hardware path is proven.

The RP2040's native USB is preferable to the USB-UART dongle. The dongle is useful as a separate debug console or a fallback if the particular RP2040 board does not expose native USB conveniently.

For that future application, a Linux AArch64 SBC is the preferred UI host if its maintained kernel/device tree supports its SPI and interrupt pins. Two candidate arrangements are:

```text
AArch64 SBC -> SPI MCP2515 -> CAN transceiver -> DLC3
             SocketCAN + kernel ISO-TP
```

or:

```text
AArch64 SBC -> USB -> RP2040 CAN bridge -> DLC3
```

The second arrangement avoids SBC-specific device-tree work and keeps all bus access behind one small, tightly allowlisted firmware component.

## Logic-level constraint

The RP2040 and normal SBC GPIO are 3.3-V logic and are not 5-V tolerant.

The MCP2515 itself can operate from 2.7 V through 5.5 V, so a separate MCP2515 should be powered at 3.3 V for direct SPI compatibility. The MCP2551, by contrast, requires a 4.5-5.5-V supply. Its `TXD` input recognizes 2.0 V as high, so a 3.3-V `TXCAN` signal is acceptable. Its `RXD` output is referenced to the 5-V supply and must pass through a proper 5-V-to-3.3-V logic buffer or level translator before reaching a 3.3-V MCP2515.

A combined MCP2515 module powered at 5 V may also place 5-V levels on `MISO` and `INT`, and 3.3-V inputs may not meet the MCP2515's guaranteed thresholds. Do not connect such a header directly to an RP2040 or SBC unless the module schematic explicitly provides level translation. A modern transceiver with a separate 3.3-V `VIO` supply, or a fully 3.3-V-compatible CAN module, is cleaner for the final build.

## Net-level wiring for separate parts

These are signal names, not board pin numbers. Exact header pins must wait for identification of the boards.

### Host to MCP2515

| Host | MCP2515 | Note |
|---|---|---|
| 3.3 V | `VDD` | Only for a separate controller or a module documented for 3.3 V. |
| Ground | `VSS` | Common logic and CAN-interface ground. |
| SPI clock | `SCK` | Configure conservatively for first bring-up. |
| SPI MOSI | `SI` | Host to controller. |
| SPI MISO | `SO` | Controller to host; must be 3.3-V safe. |
| GPIO chip select | `CS` | Active low. |
| GPIO interrupt | `INT` | Active low; needed to service the small receive buffers promptly. |
| Optional GPIO or pull-up | `RESET` | Keep high during normal operation. |

The MCP2515 needs its crystal/oscillator circuit. Firmware or the Linux device tree must use the frequency actually printed on the module, commonly 8 MHz or 16 MHz; guessing it produces the wrong CAN bit timing.

### MCP2515 to MCP2551

| MCP2515 | MCP2551 | Note |
|---|---|---|
| `TXCAN` | pin 1 `TXD` | A 3.3-V high meets the MCP2551's specified 2.0-V input-high threshold. |
| `RXCAN` | pin 4 `RXD` through 5-to-3.3-V translation | Do not feed the 5-V output directly into a 3.3-V controller. |
| Ground | pin 2 `VSS` | Common ground. |
| - | pin 3 `VDD` | Regulated 5 V; place 100 nF decoupling close to the IC. |
| - | pin 5 `VREF` | Leave unused for this design. |
| - | pin 6 `CANL` | To DLC3 pin 14. |
| - | pin 7 `CANH` | To DLC3 pin 6. |
| - | pin 8 `RS` | Ground selects high-speed mode. |

## DLC3/OBD-II plug

For the first USB-powered prototype, connect only:

| DLC3 pin | Function | Prototype connection |
|---:|---|---|
| 5 | Signal ground | Interface ground. |
| 6 | CAN-H | Transceiver `CANH`. |
| 14 | CAN-L | Transceiver `CANL`. |
| 16 | Battery positive | Leave disconnected initially. |

Pin 4 is chassis ground and is not required for the initial signal connection. Later hardware may deliberately use both grounds as part of a reviewed protection design.

OBD plug diagrams are easily mirrored: numbering viewed from the mating face is not the same as the solder side. Use molded pin numbers and confirm continuity from each solder terminal to the mating contact before plugging it into the car. Use a short twisted pair for pins 6 and 14 and provide strain relief.

Do not add a 120-ohm terminator at the diagnostic tap. Many MCP2515 modules fit one across `CANH` and `CANL`; remove it or open its jumper. With all equipment disconnected and the vehicle powered down/asleep, an intact terminated vehicle bus commonly measures near 60 ohms across pins 6 and 14. Resistance measurements are not meaningful on a powered bus.

Do not power the first prototype from DLC3 pin 16. USB power avoids exposing a breadboard to automotive battery transients. A permanent installation needs a fuse, reverse-polarity protection, surge/load-dump and ESD protection, a suitable automotive regulator, sleep control, and a verified low quiescent current.

## Staged bring-up

1. **Identify the hardware off-car.** Record board models, IC markings, oscillator frequency, supply pins, onboard regulator/translator, and whether `CANH-CANL` measures about 120 ohms on the loose module.
2. **Prove MCP2515 SPI off-car.** Reset it, read back registers, enter internal loopback mode, and send/receive a frame without a transceiver or vehicle attached.
3. **Prove USB transport.** Exchange timestamped fake or loopback CAN frames between an RP2040 bridge and a small PC test program. Keep transmission disabled by default.
4. **Attach to the stationary vehicle in listen-only mode.** Use USB power and DLC3 pins 5, 6, and 14 only. Configure 500 kbit/s with the exact oscillator value. Confirm plausible valid traffic and no receive overflow before permitting transmission. MCP2515 error counters are inactive in listen-only mode.
5. **Run one allowlisted raw diagnostic probe.** Send standard ID `0x7D2` with padded data `02 3E 00 00 00 00 00 00`; accept only the expected `0x7DA` response beginning `02 7E 00`. Abort on CAN errors or unexpected traffic.
6. **Reproduce one known value.** Move the PC-side raw ISO-TP transport behind the USB bridge and run five `F301` samples at a 100-ms minimum request interval.
7. **Expand only after comparison.** Compare the results with the J2534 client, then add the existing `F302` transaction. Keep hard-coded CAN-ID and payload allowlists in both firmware and host software.

The MCP2515 has only two receive buffers. A brief unfiltered listen-only check is useful for wiring, but normal operation should hardware-filter for response ID `0x7DA` and service `INT` promptly.

## PC software boundary

The first bridge should expose only a small raw-frame API:

- set 500-kbit/s/listen-only mode;
- install the `0x7DA` receive filter;
- receive a standard 11-bit CAN frame with a monotonic timestamp;
- transmit an 8-byte standard frame only if it matches the explicit allowlist;
- report controller error counters, receive overflow, and bus-off state;
- return to non-transmitting mode on USB disconnect or command timeout.

On Windows, the existing raw ISO-TP code can then be refactored from the J2534 backend to a generic `SendFrame`/`ReceiveFrame` transport and reused unchanged above that boundary. A binary length-delimited USB protocol is preferred for timestamps and error reporting; SLCAN text can be added later for interoperability.

On Linux, a direct MCP2515 attachment can use the kernel `mcp251x` driver, SocketCAN, and `CAN_ISOTP`. The exact overlay depends on the SBC, selected SPI controller/chip-select, interrupt GPIO, and MCP2515 oscillator, so it should be generated only after the exact SBC model is known.

## Bench-to-car safety boundary

- Never connect or alter prototype wiring while the vehicle is moving.
- Do the first active test stationary in Park, with the parking brake applied.
- Prefer a battery-powered laptop or galvanically isolated USB/CAN arrangement. A mains-earthed desktop can introduce an unintended ground path through the vehicle.
- Treat listen-only mode as a bring-up aid, not a substitute for correct wiring and bit timing.
- Stop transmitting on malformed responses, receive overflow, rising CAN error counts, bus-off, or host loss.

References: [MCP2515 family data sheet](https://ww1.microchip.com/downloads/en/DeviceDoc/MCP2515-Family-Data-Sheet-DS20001801K.pdf), [MCP2551 data sheet](https://ww1.microchip.com/downloads/en/devicedoc/21667e.pdf), [Linux SocketCAN documentation](https://docs.kernel.org/networking/can.html), and [Linux ISO-TP documentation](https://docs.kernel.org/networking/iso15765-2.html).
