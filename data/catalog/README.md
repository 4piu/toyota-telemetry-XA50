# XA50 static candidate catalog

`ecu-profile.json` binds extraction to the saved-session/live evidence for `vehicle-a`. Running `tools/catalog/build-telemetry-catalog.py` joins Techstream monitor, protocol, physical-conversion, and unit records.

`generated/summary.md` and `generated/manifest.json` describe the latest local build. The full `telemetry.csv`, `dids.csv`, and `telemetry.json` remain available locally but are ignored for initial Git history while rights to redistribute a bulk OEM-database-derived atlas are reviewed.

Static rows are Candidate evidence. Use `../validated/signals.csv` for reviewed live claims; an application must not treat the generated atlas as universally supported.
