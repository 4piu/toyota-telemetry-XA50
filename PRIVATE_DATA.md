# Private-data policy

This repository uses the arbitrary test instance alias `vehicle-a`. Do not add a VIN, a hash/reversible derivative of a VIN, owner/dealer information, registration, adapter serial, filesystem user path, GPS, exact trip history, odometer history, or an unreviewed Health Check/session export.

Keep raw `.TSE`, J2534, CAN, screenshots/video, Techstream databases/binaries, vendor DLLs, and Ghidra projects under the repository-local, ignored `scratch/` boundary. Promote only minimal sanitized evidence and source hashes/conclusions. Git ignore rules are a convenience rather than a security boundary: review staged files before every commit and never force-add material from `scratch/`.

ECU calibration/software IDs can be scientifically useful but form a configuration fingerprint. Tie them only to `vehicle-a` and review them before any future publication.

The normalized Techstream catalog input remains local because bulk
redistribution rights are uncertain. The tracked catalog provenance,
transport configuration, signal review, collection policy, and
privacy-minimized generated result are the durable portable record. The raw
captures and derived SQLite cache remain private and reproducible inputs under
`scratch/`.
