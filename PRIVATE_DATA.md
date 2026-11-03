# Private-data policy

This repository publishes deliverables only. Private working data lives in the
project workspace, outside any repository, and never comes here.

This repository uses the arbitrary test instance alias `vehicle-a`. Do not add a VIN, a hash/reversible derivative of a VIN, owner/dealer information, registration, adapter serial, filesystem user path, GPS, exact trip history, odometer history, or an unreviewed Health Check/session export.

Keep raw `.TSE`, J2534, CAN, screenshots/video, Techstream databases/binaries, vendor DLLs, Ghidra projects, local settings, plans, and the derived SQLite cache in the private workspace. Promote only minimal sanitized evidence and source hashes/conclusions into `docs/`. Ignore rules are a convenience rather than a security boundary: review staged files before every commit.

ECU calibration/software IDs can be scientifically useful but form a configuration fingerprint. Tie them only to `vehicle-a` and review them before any future publication.

| Kind | Where it lives | Published? |
|---|---|---|
| Raw captures | Workspace | No |
| Local analysis, plans, settings, cache | Workspace | No |
| Normalized Techstream catalog rows and full atlases | Workspace | No; only provenance is exported |
| Transport, collection policy, signal review, catalog manifest | Exported `config/` and `review/` | Yes |
| Result generations | Exported `result/` | Yes |
| Findings, evidence, hardware, validation write-ups | `docs/` | Yes |

The normalized Techstream catalog input stays private because bulk
redistribution rights are uncertain. Exported results contain
privacy-minimized aggregate observations, but not raw payloads, timestamps,
run notes, adapter identifiers, or local paths. Review each export diff for
identity-bearing values before committing it.
