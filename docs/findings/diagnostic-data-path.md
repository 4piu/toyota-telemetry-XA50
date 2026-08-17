# Diagnostic data path and tooling

## Layer model

The proven observation path is:

```text
Techstream
  -> J2534 API calls
  -> Techstream ptshim32 logger
  -> XHorse MVCI32 vendor driver
  -> USB vehicle interface
  -> CAN / ISO-TP at DLC3
  -> central gateway
  -> addressed ECU diagnostic server
```

J2534 is an API between a Windows application and a vehicle interface module. It hides vendor-specific USB discovery and adapter firmware behavior. It does not hide the Toyota-relevant diagnostic payloads, ECU addressing, filters, session setup, replies, or host-observed timing.

When Techstream opens an `ISO15765` channel, the VIM normally handles CAN-frame segmentation and exposes complete diagnostic payloads. A future SBC does not need to reproduce XHorse USB initialization; it can replace the J2534/VIM layers with SocketCAN and ISO-TP.

References: [Toyota Techstream Lite](https://www.techinfo.toyota.com/techInfoPortal/appmanager/t3/ti?_nfpb=true&_pageLabel=ti_ts_lite) and [SAE J2534](https://saemobilus.sae.org/standards/j2534_201907-recommended-practice-pass-thru-vehicle-programming).

## Local installation

Read-only inspection found:

| Component | Value |
|---|---|
| Techstream executable | `C:\Program Files (x86)\Toyota Diagnostics\Techstream\bin\Techstream.exe` |
| Techstream version | `18.0.0.3` |
| Configured VIM | `XHorse - MVCI` |
| Vendor DLL | `C:\Program Files (x86)\XHorse Electronics\MVCI Driver for TOYOTA TIS\MVCI32.dll` |
| J2534 registry branch | `HKLM\SOFTWARE\WOW6432Node\PassThruSupport.04.04` |
| Bundled logger | `ptshim32.dll`, product `j2534-logger`, version `102.0.10.0` |
| Techstream controller | `J2534Ctrl.dll` |

All components in this path are 32-bit. A test client loading `MVCI32.dll` must therefore also be 32-bit.

Static analysis established that, for a VIM other than DENSO's `TIS Techstream VIM`, `J2534Ctrl.dll`:

1. loads `ptshim32.dll`;
2. calls `PassThruLoadLibrary` with the selected vendor `FunctionLibrary`;
3. routes standard `PassThru*` calls through the shim;
4. retains output in a circular buffer;
5. writes the buffer when `SAVE_J2534_LOG_FILE_EVENT` is signaled;
6. signals `FINISH_J2534_LOG_FILE_EVENT` after the save.

The log is written under:

```text
C:\Users\Public\Documents\Techstream\ErrorReport\j2534_*.log
```

No registry modification or replacement DLL is needed. This behavior is similar to the open-source [j2534-logger project](https://github.com/joeyoravec/j2534-logger).

## Logger semantics

`save-techstream-log.ps1` (in the field-guide checkout) triggers an immediate snapshot of the shim's existing in-memory circular buffer. `-WaitForLoggerSeconds` waits for the named logger event to become available; it does not record for that duration.

For a controlled interval:

1. Save once to snapshot/flush the old context.
2. Let the minimal Data List run for the desired interval.
3. Save again.

Example commands from the workspace root:

```powershell
# Save immediately when a Techstream J2534 session is active
.\tools\j2534\save-j2534-log.ps1

# Wait up to two minutes for a Data List session to become active, then save
.\tools\j2534\save-j2534-log.ps1 -WaitForLoggerSeconds 120

# Summarize all diagnostic payloads
uv run python .\tools\j2534\parse-j2534-log.py 'C:\path\to\j2534_log.log'

# Show a chronological exchange for selected CAN IDs
uv run python .\tools\j2534\parse-j2534-log.py 'C:\path\to\j2534_log.log' `
  --timeline --id 0x700 --id 0x708

# Decode known XA50 values and show changes
uv run python .\tools\j2534\parse-j2534-log.py 'C:\path\to\j2534_log.log' `
  --timeline
```

Raw logs may contain a VIN or calibration identifiers. Review them before publication or committing them to a repository.

## First live transport trace

The 2026-08-08 trace proved the complete route through the gateway.

Observed setup:

- `PassThruConnect` protocol `6` (`ISO15765`) at 500,000 bit/s.
- ISO-TP messages appear as complete diagnostic payloads.
- `ISO15765_FRAME_PAD` is enabled.
- Techstream installs `FLOW_CONTROL_FILTER` pairs per endpoint.
- Both normal addressing and ISO-TP extended addressing (`ISO15765_ADDR_TYPE`) occur.
- Normal pairs include `0x700 -> 0x708`, `0x7A2 -> 0x7AA`, and functional `0x7DF -> 0x7E8`.
- One extended-addressed group uses `0x750 -> 0x758`, with the fifth J2534 byte as the extended address; examples include `0x40` and `0xD3`.

Examples:

```text
TX 0x700       3E 00
RX 0x708       7E 00

TX 0x700       10 01
RX 0x708       50 01 00 32 01 F4

TX 0x700       22 01 03
RX 0x708       62 01 03 ...

TX 0x750/EA=D3 A8 01
RX 0x758/EA=D3 E8 01 ...
```

The later focused trace identified Hybrid Control at `0x7D2 -> 0x7DA`; its full conversation is recorded in [XA50 telemetry and decoding](telemetry-map.md).

## Independent x86 replay client

An x86 .NET console client replayed the captured conversation independently of Techstream, dynamically loading the configured vendor DLL and refusing to run live without an explicit flag. The original XHorse adapter later failed electrically, and the client itself has since been replaced by the field guide's generic `telemetry-j2534` backend; the constraints and findings below carried over to it. It dynamically loads a configured vendor DLL and refuses to run live unless `--live` is supplied. The original XHorse adapter later failed; this client is retained as legacy/reproducibility evidence until another compatible VIM is available.

Built-in constraints include:

- verification that the process and vendor DLL are x86;
- verification that `PASSTHRU_MSG` marshals to 4,152 bytes;
- a byte-exact allowlist containing only the seven captured setup/poll payloads;
- ISO15765 at 500 kbit/s and the `0x7DA` / `0x7D2` flow-control filter;
- explicit handling of the observed XHorse receive-side `ProtocolID=0` quirk;
- rejection of negative, malformed, short, and unexpected responses;
- no request pipelining and a minimum configurable period of 70 ms;
- filter, channel, device, and native-library cleanup on success or failure.

Build and check without opening the adapter:

```powershell
.\tools\validation\build-j2534-client.ps1
.\tools\validation\Xa50.J2534\bin\Release\xa50-j2534.exe --dry-run
```

Controlled live invocation:

```powershell
.\tools\validation\Xa50.J2534\bin\Release\xa50-j2534.exe `
  --live --samples 20 --period-ms 100 --verbose
```

The first live attempt initially appeared to time out after the adapter accepted the connection and filter. A raw-CAN probe immediately proved that the ECU was answering:

```text
TX 0x7D2  02 3E 00 00 00 00 00 00
RX 0x7DA  02 7E 00 00 00 00 00 00  (32 ms)
```

The root cause was local receive filtering. XHorse `MVCI32.dll` reports `ProtocolID=0` in received `PASSTHRU_MSG` structures even though the channel is ISO15765 or CAN. The old Techstream log had recorded the same frames as `0:?protocol?`. The client now accepts either zero or the standards-conforming channel protocol while still requiring the exact `0x7DA` CAN ID and expected UDS response prefix.

After that correction, native ISO15765 replay completed the entire captured sequence and returned decoded `F301` values. A 100-sample stationary run completed without a transport failure; most response latencies were approximately 43-62 ms, with isolated host/driver outliers above 100 ms.

### Raw-CAN ISO-TP fallback

The client also implements `--raw-isotp`. This mode uses J2534 protocol `CAN` and performs ISO-TP in the application:

- padded single-frame transmit and receive;
- 12-bit first-frame lengths;
- ECU flow-control parsing, block size, and separation time;
- consecutive-frame sequence generation and validation;
- tester flow control for multi-frame ECU responses;
- complete-payload UDS validation using the same transmit allowlist.

The live test successfully transmitted the 20-byte dynamic-DID definition as one first frame plus two consecutive frames. The ECU replied with flow control `30 00 00`, accepted the definition, and returned each nine-byte `F301` response as a first frame plus one consecutive frame.

```powershell
.\tools\validation\Xa50.J2534\bin\Release\xa50-j2534.exe `
  --live --raw-isotp --samples 20 --period-ms 100 --verbose
```

Native ISO15765 remains preferable on this Windows adapter because it had lower normal host-observed latency. The raw mode is a diagnostic fallback and a working reference for the later SocketCAN/MCU port.

### Direct motion probe

The native client can alternate the locally extracted Hybrid Control wheel-speed and steering DIDs without defining a dynamic DID:

```powershell
.\tools\validation\Xa50.J2534\bin\Release\xa50-j2534.exe `
  --live --motion-probe --samples 20 --period-ms 100 --verbose
```

The allowlist additions are exactly `22 10 75` and `22 10 90`. Each direct request retains the configured minimum request-start interval, so one printed sample contains two sequentially rate-limited ECU transactions.

### Packed brake probe

The client can define dynamic DID `F302` from source DIDs `1043`, `1067`, and `1069`, then return the switch word and three torque fields in one synchronized response:

```powershell
.\tools\validation\Xa50.J2534\bin\Release\xa50-j2534.exe `
  --live --packed-brake --samples 100 --period-ms 100 --verbose
```

The definition and poll payloads are byte-exact allowlist entries. A separate `--brake-probe` mode retains the three direct reads for diagnosis and comparison.
