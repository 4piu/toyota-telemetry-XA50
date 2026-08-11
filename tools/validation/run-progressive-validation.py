#!/usr/bin/env python3
"""Resume stationary qualification or road capture from a progressive XA50 plan.

Dry-run is the default. Add --live explicitly to open PCAN and transmit the
plan's allowlisted ReadDataByIdentifier requests.
"""

from __future__ import annotations

import argparse
from collections import Counter, defaultdict
from dataclasses import dataclass, field
from datetime import datetime, timezone
import hashlib
import json
from pathlib import Path
import re
import shutil
import subprocess
import sys
import time
from typing import Any, Iterable


REPOSITORY = Path(__file__).resolve().parents[2]
WORKSPACE = REPOSITORY.parent
DEFAULT_PLAN = (
    WORKSPACE / "XA50-workbench" / "plans" / "vehicle-a" / "hybrid-control-progressive.json"
)
DEFAULT_CAPTURE_ROOT = WORKSPACE / "XA50-workbench" / "captures" / "vehicle-a"
DEFAULT_OUTPUT_ROOT = DEFAULT_CAPTURE_ROOT / "progressive"
EXECUTABLE = REPOSITORY / "tools" / "validation" / "Xa50.Pcan" / "bin" / "Release" / "xa50-pcan.exe"
ANALYZER = REPOSITORY / "tools" / "analysis" / "analyze-candidate-capture.py"
CATALOG = REPOSITORY / "data" / "catalog" / "generated" / "telemetry.csv"
TERMINAL_NRCS = {"0x31", "0x33"}
DID_PATTERN = re.compile(r"0x[0-9A-F]{4}\Z")


@dataclass
class Progress:
    qualified: set[str] = field(default_factory=set)
    road_complete: set[str] = field(default_factory=set)
    negative_codes: dict[str, list[str]] = field(default_factory=lambda: defaultdict(list))
    qualification_attempts: Counter[str] = field(default_factory=Counter)
    corrupt_files: list[str] = field(default_factory=list)

    def terminal_negative(self, did: str) -> bool:
        return did not in self.qualified and any(
            code in TERMINAL_NRCS for code in self.negative_codes.get(did, [])
        )


def load_plan(path: Path) -> dict[str, Any]:
    plan = json.loads(path.read_text(encoding="utf-8-sig"))
    if plan.get("schema_version") != 1:
        raise ValueError("only progressive plan schema 1 is supported")
    endpoint = plan.get("endpoint", {})
    if (
        endpoint.get("ecu_key") != 397
        or endpoint.get("tester_can_id") != "0x7D2"
        or endpoint.get("response_can_id") != "0x7DA"
        or endpoint.get("bitrate") != 500000
    ):
        raise ValueError("plan endpoint is not the live-confirmed Hybrid Control route")
    candidates = plan.get("candidates")
    if not isinstance(candidates, list) or not candidates:
        raise ValueError("plan has no candidates")
    seen: set[str] = set()
    for candidate in candidates:
        did = candidate.get("data_id")
        tier = candidate.get("tier")
        if not isinstance(did, str) or not DID_PATTERN.fullmatch(did):
            raise ValueError(f"invalid candidate DID: {did!r}")
        if did in seen:
            raise ValueError(f"duplicate candidate DID: {did}")
        if tier not in plan.get("policies", {}):
            raise ValueError(f"candidate {did} has undefined tier {tier!r}")
        seen.add(did)
    return plan


def jsonl_records(path: Path) -> list[dict[str, Any]]:
    records: list[dict[str, Any]] = []
    with path.open("r", encoding="utf-8-sig") as handle:
        for line in handle:
            if line.strip():
                value = json.loads(line)
                if not isinstance(value, dict):
                    raise ValueError("JSONL record is not an object")
                records.append(value)
    return records


def discover_progress(capture_root: Path, profile_id: str) -> Progress:
    progress = Progress()
    if not capture_root.exists():
        return progress
    for path in capture_root.rglob("*.complete.jsonl"):
        try:
            records = jsonl_records(path)
            if not records or records[0].get("profile_id") != profile_id:
                continue
            start, end = records[0], records[-1]
            if end.get("record") != "session_end" or end.get("reason") != "completed":
                continue
            mode = start.get("mode")
            samples = [record for record in records if record.get("record") == "candidate_sample"]
            if mode == "candidate_qualification":
                for sample in samples:
                    did = sample.get("did")
                    if not isinstance(did, str):
                        continue
                    progress.qualification_attempts[did] += 1
                    if sample.get("supported") is True:
                        progress.qualified.add(did)
                    elif sample.get("supported") is False:
                        code = sample.get("negative_response_code")
                        if isinstance(code, str):
                            progress.negative_codes[did].append(code)
            elif mode == "candidate_batch":
                progress.road_complete.update(
                    did for did in start.get("dids", []) if isinstance(did, str)
                )
        except (OSError, ValueError, json.JSONDecodeError):
            progress.corrupt_files.append(str(path))
    return progress


def summarize(plan: dict[str, Any], progress: Progress, tiers: set[str]) -> dict[str, Any]:
    candidates = [c for c in plan["candidates"] if c["tier"] in tiers]
    dids = {candidate["data_id"] for candidate in candidates}
    qualified = dids & progress.qualified
    road_complete = dids & progress.road_complete
    terminal = {did for did in dids if progress.terminal_negative(did)}
    retryable = {
        did for did in dids
        if progress.negative_codes.get(did) and did not in qualified and did not in terminal
    }
    untried = dids - qualified - terminal - retryable
    pending_road = qualified - road_complete
    return {
        "schema_version": 1,
        "plan_id": plan["plan_id"],
        "profile_id": plan["profile_id"],
        "candidate_dids": len(dids),
        "qualification": {
            "qualified": len(qualified),
            "terminal_negative": len(terminal),
            "retryable_negative": len(retryable),
            "untried": len(untried),
        },
        "road": {"complete": len(road_complete), "pending_qualified": len(pending_road)},
        "by_tier": {
            tier: {
                "candidates": sum(c["tier"] == tier for c in candidates),
                "qualified": sum(c["tier"] == tier and c["data_id"] in qualified for c in candidates),
                "road_complete": sum(c["tier"] == tier and c["data_id"] in road_complete for c in candidates),
            }
            for tier in sorted(tiers)
        },
        "corrupt_complete_files": progress.corrupt_files,
    }


def atomic_json(path: Path, document: dict[str, Any]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = path.with_suffix(path.suffix + ".tmp")
    temporary.write_text(json.dumps(document, indent=2) + "\n", encoding="utf-8")
    temporary.replace(path)


def assert_budget(output_root: Path, max_directory_mib: int, max_log_mib: int) -> None:
    used = sum(path.stat().st_size for path in output_root.rglob("*") if path.is_file()) if output_root.exists() else 0
    projected = used + max_log_mib * 1024 * 1024
    if projected > max_directory_mib * 1024 * 1024:
        raise RuntimeError("progressive capture directory budget would be exceeded")
    output_root.mkdir(parents=True, exist_ok=True)
    free = shutil.disk_usage(output_root).free
    if free < (max_log_mib + 256) * 1024 * 1024:
        raise RuntimeError("less than the required log size plus 256 MiB free-space reserve")


def timestamp() -> str:
    return datetime.now(timezone.utc).strftime("%Y%m%dT%H%M%S%f")[:-3] + "Z"


def quarantine(partial: Path) -> None:
    if partial.exists():
        partial.replace(Path(str(partial).replace(".partial.jsonl", ".incomplete.jsonl")))
    summary = partial.with_name(partial.name.replace(".jsonl", ".summary.json"))
    if summary.exists():
        summary.replace(Path(str(summary).replace(".partial.summary.json", ".incomplete.summary.json")))


def run_and_validate(
    arguments: list[str],
    partial: Path,
    complete: Path,
    minimum_samples: int,
) -> None:
    summary_partial = partial.with_name(partial.name.replace(".jsonl", ".summary.json"))
    summary_complete = complete.with_name(complete.name.replace(".jsonl", ".summary.json"))
    try:
        capture = subprocess.run([str(EXECUTABLE), *arguments], cwd=REPOSITORY, check=False)
        if capture.returncode:
            raise RuntimeError(f"PCAN client exited with code {capture.returncode}")
        analysis = subprocess.run(
            [
                sys.executable, str(ANALYZER), str(partial), "--catalog", str(CATALOG),
                "--minimum-samples", str(minimum_samples), "--summary-json", str(summary_partial),
            ],
            cwd=REPOSITORY,
            stdout=subprocess.DEVNULL,
            check=False,
        )
        if analysis.returncode:
            raise RuntimeError(f"offline analyzer exited with code {analysis.returncode}")
        summary_partial.replace(summary_complete)
        # The capture rename is the commit marker and therefore happens last.
        partial.replace(complete)
    except BaseException:
        if summary_complete.exists() and not complete.exists():
            incomplete_summary = Path(
                str(partial).replace(".partial.jsonl", ".incomplete.summary.json")
            )
            summary_complete.replace(incomplete_summary)
        quarantine(partial)
        raise


def batches(values: list[dict[str, Any]], size: int) -> Iterable[list[dict[str, Any]]]:
    for start in range(0, len(values), size):
        yield values[start : start + size]


def qualification_queue(
    plan: dict[str, Any], progress: Progress, tiers: set[str], max_attempts: int, retry_terminal: bool
) -> list[dict[str, Any]]:
    queue: list[dict[str, Any]] = []
    for candidate in plan["candidates"]:
        did = candidate["data_id"]
        if candidate["tier"] not in tiers or did in progress.qualified:
            continue
        if progress.terminal_negative(did) and not retry_terminal:
            continue
        if progress.qualification_attempts[did] >= max_attempts:
            continue
        queue.append(candidate)
    return queue


def road_queue(
    plan: dict[str, Any], progress: Progress, tiers: set[str]
) -> list[tuple[str, list[dict[str, Any]]]]:
    by_tier: dict[str, list[dict[str, Any]]] = defaultdict(list)
    for candidate in plan["candidates"]:
        did = candidate["data_id"]
        if (
            candidate["tier"] in tiers
            and did in progress.qualified
            and did not in progress.road_complete
        ):
            by_tier[candidate["tier"]].append(candidate)
    result: list[tuple[str, list[dict[str, Any]]]] = []
    for tier in ("fast", "normal", "slow", "archive"):
        if tier in tiers:
            result.extend((tier, group) for group in batches(by_tier[tier], 6))
    return result


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("phase", choices=("status", "qualify", "road"))
    parser.add_argument("--plan", type=Path, default=DEFAULT_PLAN)
    parser.add_argument("--capture-root", type=Path, default=DEFAULT_CAPTURE_ROOT)
    parser.add_argument("--output-root", type=Path, default=DEFAULT_OUTPUT_ROOT)
    parser.add_argument("--tiers", default="fast,normal,slow,archive")
    parser.add_argument("--live", action="store_true")
    parser.add_argument("--max-items", type=int, default=25, help="qualification DIDs or road batches")
    parser.add_argument("--max-qualification-attempts", type=int, default=2)
    parser.add_argument("--retry-terminal", action="store_true")
    parser.add_argument("--startup-delay-seconds", type=int, default=10)
    parser.add_argument("--max-log-mib", type=int, default=16)
    parser.add_argument("--max-directory-mib", type=int, default=2048)
    args = parser.parse_args()
    if args.max_items < 1 or args.max_qualification_attempts < 1:
        parser.error("--max-items and --max-qualification-attempts must be positive")
    if not 0 <= args.startup_delay_seconds <= 300:
        parser.error("--startup-delay-seconds must be 0..300")
    if args.max_log_mib < 1 or args.max_directory_mib < args.max_log_mib:
        parser.error("invalid log/directory size bounds")

    plan_path = args.plan.resolve()
    plan = load_plan(plan_path)
    available_tiers = set(plan["policies"])
    tiers = {tier.strip() for tier in args.tiers.split(",") if tier.strip()}
    unknown = tiers - available_tiers
    if not tiers or unknown:
        parser.error(f"invalid --tiers; unknown values: {sorted(unknown)}")
    capture_root = args.capture_root.resolve()
    output_root = args.output_root.resolve()
    try:
        output_root.relative_to(capture_root)
    except ValueError:
        parser.error("--output-root must be inside --capture-root so completed progress is discoverable")
    progress = discover_progress(capture_root, plan["profile_id"])
    status = summarize(plan, progress, tiers)
    print(json.dumps(status, indent=2))
    atomic_json(
        output_root / "progress.snapshot.json",
        summarize(plan, progress, available_tiers),
    )
    if args.phase == "status":
        return 0
    if args.phase == "qualify":
        queue = qualification_queue(
            plan, progress, tiers, args.max_qualification_attempts, args.retry_terminal
        )[: args.max_items]
        print(f"Qualification queue: {len(queue)} DID(s); mode={'LIVE' if args.live else 'DRY RUN'}")
    else:
        queue = road_queue(plan, progress, tiers)[: args.max_items]
        print(f"Road queue: {len(queue)} batch(es); mode={'LIVE' if args.live else 'DRY RUN'}")
    if not args.live:
        for item in queue:
            if args.phase == "qualify":
                print(f"  {item['data_id']} tier={item['tier']} signals={item['signal_count']}")
            else:
                tier, group = item
                policy = plan["policies"][tier]
                print(
                    f"  {tier}: {','.join(candidate['data_id'] for candidate in group)} "
                    f"period={policy['period_ms']}ms duration={policy['duration_seconds']}s"
                )
        return 0
    if not queue:
        return 0
    if not EXECUTABLE.exists():
        raise FileNotFoundError(f"build the PCAN client first: {EXECUTABLE}")

    print(f"Live sequence will start in {args.startup_delay_seconds} seconds.")
    if args.startup_delay_seconds:
        time.sleep(args.startup_delay_seconds)
    print("Startup delay finished; opening PCAN.")
    for item in queue:
        assert_budget(output_root, args.max_directory_mib, args.max_log_mib)
        stamp = timestamp()
        if args.phase == "qualify":
            candidate = item
            did = candidate["data_id"]
            stem = f"hybrid-{did[2:].lower()}-{stamp}.qualification"
            partial = output_root / "qualification" / f"{stem}.partial.jsonl"
            complete = output_root / "qualification" / f"{stem}.complete.jsonl"
            partial.parent.mkdir(parents=True, exist_ok=True)
            arguments = [
                "--candidate-qualify", "--live", "--batch-id", f"progressive-{did[2:].lower()}",
                "--did", did, "--period-ms", "100", "--max-log-mib", str(args.max_log_mib),
                "--output", str(partial), "--quiet",
            ]
            print(f"QUALIFY {did}")
            run_and_validate(arguments, partial, complete, 1)
        else:
            tier, group = item
            policy = plan["policies"][tier]
            dids = [candidate["data_id"] for candidate in group]
            digest = hashlib.sha256(",".join(dids).encode()).hexdigest()[:10]
            stem = f"{tier}-{digest}-{stamp}"
            partial = output_root / "road" / f"{stem}.partial.jsonl"
            complete = output_root / "road" / f"{stem}.complete.jsonl"
            partial.parent.mkdir(parents=True, exist_ok=True)
            arguments = [
                "--candidate-batch", "--live", "--batch-id", f"progressive-{tier}-{digest}",
                "--duration-seconds", str(policy["duration_seconds"]),
                "--period-ms", str(policy["period_ms"]),
                "--max-log-mib", str(args.max_log_mib), "--output", str(partial), "--quiet",
            ]
            for did in dids:
                arguments.extend(("--did", did))
            print(f"ROAD {tier} {','.join(dids)}")
            run_and_validate(
                arguments, partial, complete, int(policy["minimum_samples_per_did"])
            )
        progress = discover_progress(capture_root, plan["profile_id"])
        atomic_json(
            output_root / "progress.snapshot.json",
            summarize(plan, progress, available_tiers),
        )
    print("Sequence complete; progress snapshot updated.")
    return 0


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except KeyboardInterrupt:
        print("Interrupted; the current partial run does not count as progress.", file=sys.stderr)
        raise SystemExit(130)
