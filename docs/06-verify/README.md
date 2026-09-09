# Verification routes

Select the transport available while keeping the experiment and evidence model the same.

| Route | Use | Confidence gain |
|---|---|---|
| [J2534 replay](j2534.md) | Reuse a supported VIM and captured API setup | Replayed without Techstream |
| [USB-CAN](usb-can.md) | Raw CAN/ISO-TP through a PC adapter | Independent transport replay |
| [PCAN-Basic](pcan.md) | PEAK-System USB/PCI adapter, application-managed ISO-TP | Independent transport replay |
| [SocketCAN](socketcan.md) | Linux SBC or PC | Ported transport path |

For every route, compare raw payload and decoded value with an independent reference at zero/rest, middle, high, negative/reverse where applicable, transition boundaries, and an invalid/unavailable condition. Use a synchronized reference display or trusted measurement. Physical plausibility alone is Correlated evidence.

Across many captures, use `telemetry-tui` to aggregate per-signal evidence
and list remaining objective deficits; see the toolkit
[quickstart](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/quickstart.md).

Measure performance only after correctness. Follow [timing and load](timing-and-load.md), and promote results through [the evidence schema](../07-catalog/evidence-and-schema.md).
