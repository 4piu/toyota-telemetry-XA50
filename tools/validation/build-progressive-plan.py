#!/usr/bin/env python3
"""Build a resumable validation plan from the XA50 static DID catalog.

This is an offline planner. It never opens PCAN or communicates with a vehicle.
"""

from __future__ import annotations

import argparse
import csv
from collections import defaultdict
from datetime import datetime, timezone
import hashlib
import json
from pathlib import Path
import re
from typing import Any


REPOSITORY = Path(__file__).resolve().parents[2]
WORKSPACE = REPOSITORY.parent
DEFAULT_CATALOG = REPOSITORY / "data" / "catalog" / "generated" / "telemetry.csv"
DEFAULT_OUTPUT = (
    WORKSPACE
    / "XA50-workbench"
    / "plans"
    / "vehicle-a"
    / "hybrid-control-progressive.json"
)

POLICIES: dict[str, dict[str, int]] = {
    "fast": {"period_ms": 100, "duration_seconds": 120, "minimum_samples_per_did": 100},
    "normal": {"period_ms": 250, "duration_seconds": 300, "minimum_samples_per_did": 120},
    "slow": {"period_ms": 1000, "duration_seconds": 900, "minimum_samples_per_did": 60},
    "archive": {"period_ms": 2000, "duration_seconds": 600, "minimum_samples_per_did": 20},
}

FAST_WORDS = (
    "speed", "revolution", "torque", "current", "power", "voltage",
    "accelerator", "steering", "yaw", "rate", "frequency", "duty",
)
SLOW_WORDS = (
    "temperature", "pressure", "state of charge", "soc", "level",
    "resistance", "capacity", "thermal",
)
ARCHIVE_WORDS = (
    "counter", "history", "integrated", "accumulated", "elapsed", "total time",
    "life attainment", "just after ig on", "before last", "trip before",
    "freeze", "learning value", "maximum", "minimum", "specification",
)
PRIVATE_WORDS = (
    "odometer", "distance traveled", "absolute value time", "trip before",
)
IDENTITY_WORDS = (
    "vin", "vehicle identification", "customer", "dealer", "registration",
)


def text_has(text: str, words: tuple[str, ...]) -> bool:
    lowered = text.lower()
    return any(
        re.search(r"(?<![a-z0-9])" + re.escape(word) + r"(?![a-z0-9])", lowered)
        for word in words
    )


def classify_tier(names: list[str], privacy_sensitive: bool) -> str:
    joined = "; ".join(names)
    if privacy_sensitive:
        return "archive"
    if names and all(text_has(name, ARCHIVE_WORDS) for name in names):
        return "archive"
    if text_has(joined, FAST_WORDS):
        return "fast"
    if text_has(joined, SLOW_WORDS) or text_has(joined, ARCHIVE_WORDS):
        return "slow"
    return "normal"


def load_candidates(
    catalog: Path,
    profile_id: str | None,
    ecu_key: int,
    include_private: bool,
    max_response_bytes: int,
) -> tuple[str, list[dict[str, Any]], list[dict[str, Any]]]:
    rows_by_did: dict[str, list[dict[str, str]]] = defaultdict(list)
    detected_profile = ""
    with catalog.open("r", encoding="utf-8-sig", newline="") as handle:
        for row in csv.DictReader(handle):
            if int(row["ecu_key"]) != ecu_key or row["acquisition"] != "direct DID":
                continue
            if profile_id and row["profile_id"] != profile_id:
                continue
            detected_profile = row["profile_id"]
            did_number = int(row["data_id"], 16)
            rows_by_did[f"0x{did_number:04X}"].append(row)
    if not rows_by_did:
        raise ValueError(f"no direct-DID candidates found for ECU key {ecu_key}")

    candidates: list[dict[str, Any]] = []
    excluded: list[dict[str, Any]] = []
    for did, rows in sorted(rows_by_did.items()):
        names = list(dict.fromkeys(row["name"] for row in rows if row["name"]))
        response_bytes = max((int(row["last_bit"]) // 8 + 1 for row in rows), default=0)
        joined_names = "; ".join(names)
        privacy_sensitive = text_has(joined_names, PRIVATE_WORDS)
        identity_sensitive = text_has(joined_names, IDENTITY_WORDS)
        record = {
            "data_id": did,
            "signals": names,
            "signal_count": len(names),
            "response_data_bytes_min": response_bytes,
            "tier": classify_tier(names, privacy_sensitive),
            "privacy_sensitive": privacy_sensitive,
            "identity_sensitive": identity_sensitive,
            "prior_live_evidence": any(row["live_state"] != "Not tested" for row in rows),
        }
        did_number = int(did, 16)
        if not 0x0100 <= did_number <= 0xEFFF:
            record["excluded_reason"] = "reserved_or_client_disallowed_did"
            excluded.append(record)
        elif identity_sensitive:
            record["excluded_reason"] = "identity_bearing_did_is_never_scheduled"
            excluded.append(record)
        elif response_bytes > max_response_bytes:
            record["excluded_reason"] = "response_size_limit"
            excluded.append(record)
        elif privacy_sensitive and not include_private:
            record["excluded_reason"] = "privacy_sensitive_requires_opt_in"
            excluded.append(record)
        else:
            candidates.append(record)
    return detected_profile, candidates, excluded


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--catalog", type=Path, default=DEFAULT_CATALOG)
    parser.add_argument("--output", type=Path, default=DEFAULT_OUTPUT)
    parser.add_argument("--profile-id")
    parser.add_argument("--ecu-key", type=int, default=397)
    parser.add_argument("--include-private", action="store_true")
    parser.add_argument("--max-response-bytes", type=int, default=512)
    parser.add_argument("--force", action="store_true", help="replace an existing plan")
    args = parser.parse_args()
    if args.ecu_key != 397:
        parser.error("the current PCAN client is restricted to live-confirmed ECU key 397")
    if args.max_response_bytes < 1 or args.max_response_bytes > 4092:
        parser.error("--max-response-bytes must be 1..4092")
    catalog = args.catalog.resolve()
    output = args.output.resolve()
    if output.exists() and not args.force:
        parser.error(f"output exists; use --force to replace it: {output}")

    profile, candidates, excluded = load_candidates(
        catalog, args.profile_id, args.ecu_key, args.include_private, args.max_response_bytes
    )
    catalog_hash = hashlib.sha256(catalog.read_bytes()).hexdigest()
    tier_counts = {
        tier: sum(candidate["tier"] == tier for candidate in candidates) for tier in POLICIES
    }
    document = {
        "schema_version": 1,
        "plan_id": "hybrid-control-progressive-v1",
        "profile_id": profile,
        "generated_at_utc": datetime.now(timezone.utc).isoformat(),
        "catalog": {"file": catalog.name, "sha256": catalog_hash},
        "endpoint": {
            "ecu_key": 397,
            "tester_can_id": "0x7D2",
            "response_can_id": "0x7DA",
            "bitrate": 500000,
        },
        "limits": {"maximum_dids_per_road_batch": 6, "max_response_bytes": args.max_response_bytes},
        "policies": POLICIES,
        "selection": {
            "include_private": args.include_private,
            "candidate_count": len(candidates),
            "excluded_count": len(excluded),
            "tier_counts": tier_counts,
        },
        "candidates": candidates,
        "excluded": excluded,
    }
    output.parent.mkdir(parents=True, exist_ok=True)
    temporary = output.with_suffix(output.suffix + ".tmp")
    temporary.write_text(json.dumps(document, indent=2) + "\n", encoding="utf-8")
    temporary.replace(output)
    print(json.dumps({"output": str(output), **document["selection"]}, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
