# Private-data policy

This repository publishes deliverables only. Private working data lives in the
project workspace, outside any repository, and never comes here.

This repository uses the arbitrary test instance alias `vehicle-a`. Do not add a VIN, a hash/reversible derivative of a VIN, owner/dealer information, registration, adapter serial, filesystem user path, GPS, exact trip history, odometer readings or history, or an unreviewed Health Check/session export.

Keep raw `.TSE`, J2534, CAN, screenshots/video, Techstream databases/binaries, vendor DLLs, Ghidra projects, local settings, plans, and the derived SQLite cache in the private workspace. Promote only minimal sanitized evidence and source hashes/conclusions into `docs/`. Ignore rules are a convenience rather than a security boundary: review staged files before every commit.

ECU calibration/software IDs can be scientifically useful but form a configuration fingerprint. Tie them only to `vehicle-a` and review them before any future publication.

| Kind | Where it lives | Published here? |
|---|---|---|
| Raw captures and recorded values | Workspace | No |
| Local analysis, plans, settings, cache | Workspace | No |
| Normalized Techstream catalog rows and full atlases | Workspace | No; only provenance is exported |
| Signal names, DIDs, bit ranges, formulas, units derived from Techstream | Exported `result/` | Yes, while this repository is private |
| Transport, collection policy, signal review, catalog manifest | Exported `config/` and `review/` | Yes |
| Findings, evidence, hardware write-ups | `docs/` | Yes |

## Techstream-derived content

The signal reference publishes names, DIDs, bit ranges, formulas, and units
taken from the Techstream database, because that is the finding. Bulk
redistribution rights for that content are not established. It is accepted
here only while the GitHub repository is private. Before the repository or
any copy of the result is made public, review those rights and reduce the
result if needed.

## Recorded values

Exported results carry recordings only as counts and a status (changed,
constant, beyond response, not recorded); no recorded value is exported,
because values include readings such as the odometer. Result generations made
before 2026-11-05 contained recorded minima and maxima, including an odometer
reading; they were deleted from the working tree on that date but remain in
this repository's Git history. Review notes are written by hand: keep them to
ranges that identify nothing, such as speed or voltage ranges.
