# Direct-DID validation batches

This workflow uses short, sequential captures to gather evidence for selected Hybrid Control catalog candidates while preserving the already validated 13-signal baseline. It is deliberately not an ECU scanner: every transmitted DID is present in a reviewed manifest, and only the live-confirmed `0x7D2` to `0x7DA` endpoint is used.

## Safety boundary

Do stationary qualification in Park before the road session. Qualification sends one `22 <DID>` request at a time and records either a positive response or a UDS negative-response code. A silent timeout, transport fault, or interrupted qualification is not interpreted as “unsupported”; the batch remains incomplete and must be retried or investigated while stationary.

Road capture admits only DIDs with a positive qualification response. The client enforces these limits independently of the PowerShell wrapper:

- one to six exact DIDs per invocation;
- a dynamically constructed byte-exact transmit allowlist;
- one outstanding request at a time;
- at least 70 ms between aggregate requests (the manifest uses 100 ms);
- 45 seconds per initial candidate batch and a bounded create-new log;
- a default 256 MiB aggregate road-batch directory budget (128 MiB for qualification), in addition to each file's cap and free-space reserve;
- Hybrid Control sessions and service `0x22` only—no broad scan, writes, routines, or other ECU endpoints.

At 100 ms per request, a five-DID batch obtains about two samples per second per DID; a six-DID batch obtains about 1.7. The 30-sample completion threshold leaves margin below the expected count for a 45-second run.

## Files and completion semantics

The reviewed initial set is [hybrid-control-road-v1.json](../../data/validation-batches/hybrid-control-road-v1.json). Its SHA-256 prefix is part of every completion filename, so editing the manifest invalidates old progress rather than silently reusing it.

A run is progress only when it has all of the following:

1. a final `session_end` with the expected successful reason;
2. no error record;
3. contiguous round-robin samples with valid UDS response prefixes;
4. the configured minimum sample count for every admitted DID;
5. successful offline structural analysis;
6. an atomic rename to `.complete.jsonl`.

Ctrl+C, a disk/log limit, an ECU/transport error, or failed analysis leaves the batch logically incomplete. The wrapper renames its file to `.incomplete.jsonl` for troubleshooting and does not advance. The next invocation starts that same batch from the beginning. Raw incomplete files are not evidence for confidence promotion and may be removed from the private workbench after review.

## Procedure

Build once, then inspect both plans without transmitting:

```powershell
.\tools\validation\build-pcan-client.ps1
.\tools\validation\qualify-road-batches.ps1
.\tools\validation\start-validation-sequence.ps1
```

With the vehicle stationary in Park, IG-ON/READY, PCAN-View closed, and wiring checked, qualify the batches:

```powershell
.\tools\validation\qualify-road-batches.ps1 -Live
```

Review the qualification summaries. Negative responses are retained as useful results and automatically excluded from the later road run. A timeout stops the sequence because it does not establish whether the candidate is unsupported.

At the safe test location, start the road sequence once and do not interact with the computer:

```powershell
.\tools\validation\start-validation-sequence.ps1 -Live
```

The sequence first captures the existing 13 signals for 45 seconds, then runs every not-yet-complete qualified candidate batch. Completed batches are skipped on a later invocation. Use `-MaxBatches 1` for a rehearsal or `-SkipBaseline` only when an adequate baseline from this manifest revision is already complete.

Captures default to the ignored sibling `XA50-workbench/captures/vehicle-a/` tree. They contain no VIN, GPS, adapter serial, or explicit location, but timestamps and vehicle behavior still make them private by default.

## Offline evidence and confidence

`analyze-candidate-capture.py` rechecks raw UDS framing and reports sample counts, latency, catalog-derived raw/value ranges, and whether each field changed. It never edits [signals.csv](../../data/validated/signals.csv).

Catalog decoding is best-effort: the analyzer does not yet apply every Techstream invalid/sentinel rule or enum label, and the full generated atlas may be absent from a clean checkout pending provenance review. Structural validation and raw capture still work without it; treat implausible extrema as a prompt to inspect invalid-value metadata, not as disproof of the transport.

Use these evidence levels conservatively:

- **Candidate:** static catalog only.
- **Observed and replayed:** positive live response, structurally valid repeated samples, and a plausible decoded range. A constant value may qualify only when the test state explains it.
- **Correlated and replayed:** a planned maneuver or known state produces the expected transition, sign, direction, or relationship. Examples include yaw sign during opposing turns, shift-state changes, or target/actual values moving together.
- **Validated and replayed:** independent ground truth or multiple controlled reference points confirm identity, scale, offset, and sign. Sanity alone does not reach this level.

Review signals individually, even when they share a DID. One field can correlate while another field in the same payload remains merely observed. Preserve the completed raw log and analysis summary as the evidence link when editing the curated map.

## Initial batches

The v1 manifest has four deliberately small groups:

- powertrain state: engine mode, target engine speed/power, and full MG1/MG2 payloads;
- electrical energy: 12 V supply, SOC bounds, and boost-converter voltages;
- thermal state: motor/generator/transaxle/inverter/coolant temperatures;
- motion context: yaw, shift/drive mode, request/status bits, and calculated transmission speed.

These are high-value, plausibly sanity-checkable direct DIDs. Historical counters, fail-safe records, customization/programming data, dynamic/mapped pseudo-records, DID `0x0000`, and unconfirmed Engine/MG/Brake endpoints are intentionally excluded.
