# Static XA50 telemetry catalog

## Purpose and scope

The catalog turns the locally installed Techstream Phase-5 database into a searchable telemetry atlas. It is deliberately broader than the future acquisition/display application's needs and is generated without communicating with the vehicle.

The original extraction covered the sixteen ECU datasets whose endpoints
answered a stationary probe. The endpoints came from each ECU's protocol table
and were then confirmed on the vehicle. Current portable authority is
[`config/transport.json`](../../config/transport.json); [validation status](../validation/README.md)
records the migrated support evidence.

Twelve ECUs occupy nine distinct endpoints: Techstream models several as separate databases over one diagnostic address, which means one ECU to capture from with more than one catalog to read it with. Hybrid Control (`HV_P5.ddb`, key 397, `0x7D2 -> 0x7DA`) was the first established and is the most completely captured.

An earlier revision of this page listed four ECUs with response IDs inferred as tester + 8. The inference happened to be right in every case, but it was an assumption; the protocol table states both IDs, so nothing here relies on it now.

## Generated map

Run:

```powershell
python ..\telemetry-tools\tools\catalog\build-catalog.py --profile <private-extraction-profile>
```

The generator queries the read-only local database interface and writes:

- `scratch/catalog-rows.json`: normalized private input consumed by the project;
- ignored archival extraction files: full signal/DID atlases;
- [`result/catalog-manifest.json`](../../result/catalog-manifest.json): tracked
  source provenance and row count;
- the generation selected by [`result/current.json`](../../result/current.json):
  normalized reviewed signal output.

The current build contains 6,892 signal records across 2,546 distinct ECU/DID pairs. For every row it preserves the source ECU, internal monitor ID, acquisition flags, `22 xxxx` request candidate, response bit range, physical conversion group, coefficients, decimal divisor, unit, and live-evidence override.

The exact stored conversion is:

```text
physical_value = ((raw * A / B) + C) / (10 ** N)
```

The catalog keeps that form instead of simplifying it, so the database evidence remains auditable.

## Protocol-table finding

Static analysis of `DataMonitorPhase5_DT.dll` shows that it obtains Phase-5 protocol records from table `0x010D`. The local query helper now exposes these through `-ProtocolInfo`. The physical tester-ID field is anchored by the known Hybrid value `0x7D2`; the other three IDs are therefore strong static candidates.

```powershell
.\tools\techstream-db\query-techstream-db.ps1 -Keys '372,395,397,435' -ProtocolInfo
```

The response ID is not directly labeled in the extracted record. `tester + 8` matches the live `0x7D2 -> 0x7DA` pair and normal Toyota 11-bit diagnostic addressing, but it remains an inference for Engine, Motor Generator, and Brake until each receives a focused positive response.

## What “candidate” means

Techstream ECU databases aggregate records used by more than one ECU software/calibration variant. Therefore:

- a named row does not prove that this vehicle implements the DID;
- a direct `22 xxxx` record may return a UDS negative response on the exact vehicle;
- several names may describe the same DID slice or alternate calibration;
- byte order, signedness, enumeration labels, invalid sentinels, and availability conditions are not yet fully represented;
- some values may require a different diagnostic session or initialization sequence.

No bulk DID scan should be sent to the vehicle. Select one useful candidate, create a byte-exact allowlist, establish the endpoint/session conservatively, and validate a small number of controlled samples.

## Remaining static-analysis layers

1. Extract the complete installed-ECU list from an actual Health Check or another saved session. The existing `.TSE` contains only three opened ECU sections; `inspect-techstream-session.py` can inventory those sections without printing vehicle identifiers.
2. Decode the protocol/communication-set records beyond the physical tester ID: response routing, initialization mode, session requirements, timeouts, and keep-alives.
3. Decode enumeration resources and unavailable/invalid value rules. Numeric coefficient coverage is complete for the initial four datasets, but identity formulas such as switch bits still need their display labels.
4. Identify duplicate/variant monitor records so the map can rank likely-supported entries before live testing.
5. Extend the profile to HV Battery and other installed XA50 modules only when vehicle evidence confirms the relevant Techstream key.

## Techstream through USB-CAN

PEAK publishes a PCAN-PassThru package implementing J2534 04.04 with 32-bit and 64-bit DLLs, CAN, ISO 15765, message filters, periodic messages, and common buffer/configuration IOCTLs. Architecturally, this is exactly the boundary Techstream needs: after installation and J2534 registration, Techstream can select it as a VIM without an internal Techstream patch.

However, PEAK's license permits those APIs only with PEAK hardware or hardware from its partners and explicitly excludes third-party compatible hardware. The current STM32 adapter is a third-party PCAN-compatible board, so the official PCAN-PassThru package is not a suitable route for it. It would become the easiest Techstream option if a genuine supported PEAK adapter were used.

For this board, the technically possible alternative is a custom 32-bit J2534 compatibility DLL over a lawfully usable USB-CAN firmware/API. At minimum it must correctly implement `PassThruOpen/Close`, `Connect/Disconnect`, `ReadMsgs`, `WriteMsgs`, ISO15765 `FLOW_CONTROL_FILTER`, periodic messages, relevant configuration and clear-buffer IOCTLs, error codes, timing, threading, and registry discovery. That is tractable but considerably more work and risk than continuing database-first discovery plus focused PCAN validation, or waiting for a replacement J2534.

Official references: [PCAN-PassThru API product and version information](https://www.peak-system.com/support/software-information/development-kits/pcan-passthru-api/) and [PCAN-PassThru API 04.04 manual](https://www.peak-system.com/produktcd/Develop/PC%20interfaces/Windows/HighLayerProtocols/PCAN-PassThru/PCAN-PassThru-API_UserMan_eng.pdf).
