# Platform projects

This template and a vehicle/platform knowledge repository have separate
ownership boundaries. Neither repository needs to contain or merge the other's
Git history:

1. This repository establishes a platform repository's initial records and
   layout. *Use this template* on GitHub copies it; a platform repository is
   that structure filled in.
2. Reusable executable code is consumed as a pinned dependency on the
   [toolkit](https://github.com/4piu/70Y074-telemetry-tools); it is never copied into a platform repository.
3. Vehicle profiles, endpoint policy, decode overrides, plans, evidence, and
   adapter implementations remain in the platform repository.

## Instantiation, not a fork

A platform repository starts from this one's structure and never shares its
history. That is the same distinction GitHub draws between a template
repository and a fork, and it exists for the reason below rather than as a
formality.

Both repositories carry a `README.md`, a `docs/` tree, a `.gitignore`, and a
private-data policy. Those are not duplicates waiting to be reconciled: the
template answers *how this is done on any vehicle*, and a platform
repository answers *what is true of this one*. Measured against the worked
example, the two `README.md` files share nine lines out of eighty, and the two
`docs/README.md` files share three out of forty-five. They are different
documents that happen to agree on a filename.

So a merge relationship would not merge anything. It would raise the same
handful of conflicts on every update, none of them resolvable automatically,
and each resolution would have to re-delete the template's answer in favour
of the platform's. Fetching the template as a remote for reading and
comparison is useful; merging it is not.

The test for where a file belongs is whether a second vehicle would want it
unchanged:

| Path | Template | Platform repository |
|---|---|---|
| `README.md` | The method, and what the toolkit does | One vehicle's result, and how to reproduce it |
| `docs/` | Procedure, contracts, adapter and transport notes | Findings, evidence, and validation status for this vehicle |
| `data/config/` | Blank configs to fill in | The verified inputs, filled in |
| `data/` | — | Verified configs and the generated map |
| Every program | — (they live in the toolkit) | — |
| `.gitignore` | Build output | Build output, plus captures and the whole scratch tree |
| `PRIVATE_DATA.md` | What counts as private, and why | What is private in this vehicle's captures |

A program that would help a second vehicle is a gap in the toolkit, not
something to add locally. That rule is what keeps the split honest, and it is
the reason the platform repository ships no tooling of its own.

## Project marker

Copy [the project marker template](../telemetry-project.toml) to the
root of the platform repository as `telemetry-project.toml`. It records:

- the project kind and stable project identifier;
- the toolkit source URL and pinned reviewed revision;
- the repository-relative private scratch directory;
- where the toolkit checkout lives, if not `../telemetry-tools`.

Tools discover the project root by walking upward to this marker. They must not
derive paths from a repository's parent directory or checkout name. All project
paths in profiles and experiment records resolve relative to the marker's
directory. Explicit command-line paths may override defaults.

## Scratch contract

`scratch/` is the private workspace for every template instance. The entire
directory is ignored. Suggested contents are:

```text
scratch/
|-- captures/
|-- plans/
|-- analysis/
|-- reverse-engineering/
`-- local-config/
```

Tools create only the subdirectories they need, refuse unexpected overwrites,
and retain size/time bounds. Ignore rules prevent routine staging but are not a
confidentiality boundary; never force-add the directory.

## Updating the toolkit

`upstream.revision` in the marker is the only pin, and it belongs to the
platform repository's owner: nothing reaches a project until they change it.
The [toolkit's update procedure](https://github.com/4piu/70Y074-telemetry-tools/blob/main/docs/updating-the-pin.md)
has the steps and the checks.
