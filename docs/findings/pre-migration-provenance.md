# Pre-migration transport provenance

`config/transport.json` cites two pre-schema-2 paths as provenance for
confirmed endpoints and reviewed dynamic-DID definitions:

- `data/config/backend-config.json` — session setup and the per-ECU endpoint
  pairs behind the DLC3 gateway.
- `data/map/transport.json` — the hand-maintained dynamic-DID `0x2C`
  definitions and their expected response prefixes.

Both paths were removed from the tracked tree by commit `a18e476` ("Cut XA50
project over to schema 2"), which carried their content forward verbatim into
`config/transport.json`. Neither file exists in the current working tree, but
both remain retrievable from history at their last committed revision,
`e403fad` ("Expand signal map from multi-ECU road captures"):

```powershell
git show e403fad:data/config/backend-config.json
git show e403fad:data/map/transport.json
```

This document exists so a `config/transport.json` provenance entry that still
cites one of the two old paths remains traceable without needing a checkout of
the pre-migration tree. It is itself listed alongside the old path in each
affected `provenance` array.
