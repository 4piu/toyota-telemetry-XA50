# Evidence records and catalog schema

A catalog is a set of claims with provenance, not a flat list of “supported PIDs.” Keep static candidates separate from curated live-verified signals.

## Signal dossier

Copy [the signal template](../../templates/signal-dossier.yaml). It records:

- stable signal/profile IDs and exact semantic/display names;
- ECU and transport addressing with endpoint confidence;
- diagnostic acquisition and any dynamic-DID source slice;
- bit layout, raw type, coefficients/expression, unit, enums and invalid states;
- independent validation points and raw/capture references;
- timing distribution, limitations, and evidence state.

## Generated catalog manifest

Every bulk extraction should include schema/tool revision, generation time, Techstream/database version/region, input filenames plus SHA-256 hashes, vehicle-profile evidence, row counts, and warnings. Do not include source databases.

Recommended separation:

- `generated/`: reproducible broad candidates; possibly ignored if redistribution rights are unclear.
- `validated/`: small reviewed records backed by sanitized evidence; safe input to applications.
- `fixtures/`: minimal sanitized bytes used by tests.

Promote confidence only with a cited experiment. Formula confidence, endpoint confidence, semantic confidence, and transport state should remain independently representable. Never let generated code silently convert Candidate rows into production signals.
