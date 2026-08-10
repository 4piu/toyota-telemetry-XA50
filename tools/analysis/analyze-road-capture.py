#!/usr/bin/env python3
"""Validate and summarize an XA50 PCAN road-capture JSONL file."""

from __future__ import annotations

import argparse
import csv
import json
import math
import statistics
import sys
from collections import Counter, defaultdict
from pathlib import Path
from typing import Any, Iterable


EXPECTED_GROUPS = ("f301", "wheels", "f302", "steering")
EXPECTED_VALUE_KEYS = {
    "f301": {"mg1_rpm", "mg2_rpm", "accelerator_percent", "auxiliary_raw"},
    "wheels": {
        "front_right_kmh",
        "front_left_kmh",
        "rear_right_kmh",
        "rear_left_kmh",
    },
    "f302": {
        "stop_light",
        "brake_cancel",
        "master_cylinder_control_torque_nm",
        "requested_regen_torque_nm",
        "executed_regen_torque_nm",
        "switch_raw",
    },
    "steering": {"steering_angle_deg"},
}
EXPECTED_RESPONSE_PREFIX = {
    "f301": "62 F3 01",
    "wheels": "62 10 75",
    "f302": "62 F3 02",
    "steering": "62 10 90",
}


def u16(data: bytes, offset: int) -> int:
    return (data[offset] << 8) | data[offset + 1]


def decode_response(group: str, response_hex: str) -> dict[str, Any]:
    try:
        data = bytes.fromhex(response_hex)
    except ValueError as exc:
        raise ValueError(f"response_hex is not valid hexadecimal: {response_hex!r}") from exc
    minimum = {"f301": 9, "wheels": 11, "f302": 11, "steering": 5}[group]
    if len(data) < minimum:
        raise ValueError(f"{group} response has {len(data)} bytes; expected at least {minimum}")
    if group == "f301":
        return {
            "mg1_rpm": u16(data, 3) - 32768,
            "mg2_rpm": u16(data, 5) - 32768,
            "accelerator_percent": data[7] * 0.5,
            "auxiliary_raw": data[8],
        }
    if group == "wheels":
        return {
            "front_right_kmh": (u16(data, 3) - 32768) / 100,
            "front_left_kmh": (u16(data, 5) - 32768) / 100,
            "rear_right_kmh": (u16(data, 7) - 32768) / 100,
            "rear_left_kmh": (u16(data, 9) - 32768) / 100,
        }
    if group == "f302":
        switch = u16(data, 3)
        return {
            "stop_light": bool(switch & 0x0001),
            "brake_cancel": bool(switch & 0x0002),
            "master_cylinder_control_torque_nm": u16(data, 5) * 0.125 - 4096,
            "requested_regen_torque_nm": u16(data, 7) * 0.125 - 4096,
            "executed_regen_torque_nm": u16(data, 9) * 0.125 - 4096,
            "switch_raw": switch,
        }
    return {"steering_angle_deg": (u16(data, 3) - 32768) / 10}


def equivalent(left: Any, right: Any) -> bool:
    if isinstance(left, bool) or isinstance(right, bool):
        return left is right
    if isinstance(left, (int, float)) and isinstance(right, (int, float)):
        return math.isclose(float(left), float(right), rel_tol=0, abs_tol=1e-9)
    return left == right


def percentile(values: list[float], fraction: float) -> float | None:
    if not values:
        return None
    ordered = sorted(values)
    position = (len(ordered) - 1) * fraction
    lower = math.floor(position)
    upper = math.ceil(position)
    if lower == upper:
        return ordered[lower]
    return ordered[lower] * (upper - position) + ordered[upper] * (position - lower)


def distribution(values: Iterable[float]) -> dict[str, float | int | None]:
    numbers = [float(value) for value in values]
    if not numbers:
        return {"count": 0, "min": None, "median": None, "mean": None, "p95": None, "max": None}
    return {
        "count": len(numbers),
        "min": min(numbers),
        "median": statistics.median(numbers),
        "mean": statistics.fmean(numbers),
        "p95": percentile(numbers, 0.95),
        "max": max(numbers),
    }


def read_records(path: Path) -> list[dict[str, Any]]:
    records: list[dict[str, Any]] = []
    with path.open("r", encoding="utf-8-sig") as handle:
        for line_number, line in enumerate(handle, start=1):
            line = line.strip()
            if not line:
                continue
            try:
                record = json.loads(line)
            except json.JSONDecodeError as exc:
                raise ValueError(f"invalid JSON at line {line_number}: {exc}") from exc
            if not isinstance(record, dict) or not isinstance(record.get("record"), str):
                raise ValueError(f"line {line_number} is not a typed JSON object")
            record["_line"] = line_number
            records.append(record)
    if not records:
        raise ValueError("capture is empty")
    return records


def validate(records: list[dict[str, Any]]) -> tuple[list[dict[str, Any]], list[str]]:
    warnings: list[str] = []
    start = records[0]
    if start.get("record") != "session_start":
        raise ValueError("first record must be session_start")
    if start.get("schema_version") != 1:
        raise ValueError(f"unsupported schema_version: {start.get('schema_version')!r}")
    if start.get("mode") != "road_capture":
        raise ValueError(f"unexpected session mode: {start.get('mode')!r}")

    samples = [record for record in records if record["record"] == "sample"]
    previous_sequence = 0
    previous_request_elapsed = -1
    configured_period = start.get("period_ms")
    short_interval_reported = False
    for sample in samples:
        line = sample["_line"]
        sequence = sample.get("sequence")
        if sequence != previous_sequence + 1:
            raise ValueError(
                f"non-contiguous sequence at line {line}: expected {previous_sequence + 1}, got {sequence!r}"
            )
        previous_sequence = sequence
        group = sample.get("group")
        if group not in EXPECTED_GROUPS:
            raise ValueError(f"unknown group at line {line}: {group!r}")
        expected_group = EXPECTED_GROUPS[(sequence - 1) % len(EXPECTED_GROUPS)]
        if group != expected_group:
            raise ValueError(
                f"schedule mismatch at line {line}: sequence {sequence} should be {expected_group}, got {group}"
            )
        values = sample.get("values")
        if not isinstance(values, dict):
            raise ValueError(f"sample values are not an object at line {line}")
        missing = EXPECTED_VALUE_KEYS[group] - values.keys()
        if missing:
            raise ValueError(f"missing {group} value(s) at line {line}: {sorted(missing)}")
        response = sample.get("response_hex")
        if not isinstance(response, str) or not response.startswith(EXPECTED_RESPONSE_PREFIX[group]):
            raise ValueError(f"unexpected {group} response prefix at line {line}: {response!r}")
        decoded = decode_response(group, response)
        for name, expected in decoded.items():
            if not equivalent(values.get(name), expected):
                raise ValueError(
                    f"decoded/raw mismatch at line {line} for {name}: "
                    f"logged {values.get(name)!r}, raw response gives {expected!r}"
                )
        request_elapsed = sample.get("request_elapsed_ms")
        response_elapsed = sample.get("response_elapsed_ms")
        latency = sample.get("latency_ms")
        if not all(isinstance(value, int) and value >= 0 for value in (request_elapsed, response_elapsed, latency)):
            raise ValueError(f"invalid timing field at line {line}")
        if request_elapsed < previous_request_elapsed:
            raise ValueError(f"request monotonic time regressed at line {line}")
        if (
            isinstance(configured_period, int)
            and previous_request_elapsed >= 0
            and request_elapsed - previous_request_elapsed < configured_period - 2
            and not short_interval_reported
        ):
            warnings.append(
                "At least one aggregate request interval is shorter than the configured period minus 2 ms."
            )
            short_interval_reported = True
        if response_elapsed < request_elapsed:
            raise ValueError(f"response precedes request at line {line}")
        previous_request_elapsed = request_elapsed

    end_records = [record for record in records if record["record"] == "session_end"]
    if not end_records:
        warnings.append("No session_end record: capture may have been interrupted or truncated.")
    elif records[-1]["record"] != "session_end":
        warnings.append("session_end is not the final record.")
    if any(record["record"] == "error" for record in records):
        warnings.append("Capture contains one or more error records.")
    if end_records:
        declared = end_records[-1].get("group_counts")
        actual = Counter(sample["group"] for sample in samples)
        if isinstance(declared, dict) and any(
            declared.get(group, 0) != actual[group] for group in EXPECTED_GROUPS
        ):
            warnings.append("session_end group_counts do not match parsed samples.")
    return samples, warnings


def summarize(records: list[dict[str, Any]], samples: list[dict[str, Any]], warnings: list[str]) -> dict[str, Any]:
    start = records[0]
    group_counts = Counter(sample["group"] for sample in samples)
    latency_by_group: dict[str, list[float]] = defaultdict(list)
    request_times: list[int] = []
    request_times_by_group: dict[str, list[int]] = defaultdict(list)
    values_by_name: dict[str, list[float]] = defaultdict(list)
    booleans_by_name: dict[str, Counter[bool]] = defaultdict(Counter)
    wheel_spread: list[float] = []
    regen_tracking_error: list[float] = []

    for sample in samples:
        group = sample["group"]
        latency_by_group[group].append(sample["latency_ms"])
        request_times.append(sample["request_elapsed_ms"])
        request_times_by_group[group].append(sample["request_elapsed_ms"])
        values = sample["values"]
        for name, value in values.items():
            if isinstance(value, bool):
                booleans_by_name[name][value] += 1
            elif isinstance(value, (int, float)):
                values_by_name[name].append(float(value))
        if group == "wheels":
            wheels = [
                float(values["front_right_kmh"]),
                float(values["front_left_kmh"]),
                float(values["rear_right_kmh"]),
                float(values["rear_left_kmh"]),
            ]
            wheel_spread.append(max(wheels) - min(wheels))
        if group == "f302":
            regen_tracking_error.append(
                abs(
                    float(values["requested_regen_torque_nm"])
                    - float(values["executed_regen_torque_nm"])
                )
            )

    intervals = [later - earlier for earlier, later in zip(request_times, request_times[1:])]
    group_intervals = {
        group: distribution(
            later - earlier for earlier, later in zip(times, times[1:])
        )
        for group, times in request_times_by_group.items()
    }
    value_ranges = {
        name: {"count": len(values), "min": min(values), "max": max(values)}
        for name, values in sorted(values_by_name.items())
        if values
    }
    boolean_counts = {
        name: {"false": counts[False], "true": counts[True]}
        for name, counts in sorted(booleans_by_name.items())
    }
    end = next((record for record in reversed(records) if record["record"] == "session_end"), None)
    for name, bounds in {
        "accelerator_percent": (0, 100),
        "front_right_kmh": (-300, 300),
        "front_left_kmh": (-300, 300),
        "rear_right_kmh": (-300, 300),
        "rear_left_kmh": (-300, 300),
        "steering_angle_deg": (-1080, 1080),
        "mg1_rpm": (-30000, 30000),
        "mg2_rpm": (-30000, 30000),
    }.items():
        values = values_by_name.get(name, [])
        if values and (min(values) < bounds[0] or max(values) > bounds[1]):
            warnings.append(
                f"{name} left the broad sanity range {bounds}: {min(values)}..{max(values)}"
            )
    for name in (
        "accelerator_percent",
        "front_right_kmh",
        "steering_angle_deg",
        "requested_regen_torque_nm",
    ):
        values = values_by_name.get(name, [])
        if len(values) >= 10 and min(values) == max(values):
            warnings.append(f"{name} never changed; the intended maneuver may be absent.")
    for name in ("stop_light", "brake_cancel"):
        counts = booleans_by_name.get(name, Counter())
        if sum(counts.values()) >= 10 and (not counts[True] or not counts[False]):
            warnings.append(f"{name} did not visit both boolean states.")
    return {
        "schema_version": 1,
        "source": str(start.get("profile_id", "unknown")),
        "capture": {
            "started_utc": start.get("created_utc"),
            "end_reason": end.get("reason") if end else None,
            "records": len(records),
            "samples": len(samples),
            "group_counts": dict(sorted(group_counts.items())),
            "error_records": sum(record["record"] == "error" for record in records),
        },
        "timing_ms": {
            "aggregate_request_interval": distribution(intervals),
            "request_interval_by_group": group_intervals,
            "latency_by_group": {
                group: distribution(values) for group, values in latency_by_group.items()
            },
        },
        "value_ranges": value_ranges,
        "boolean_counts": boolean_counts,
        "sanity_metrics": {
            "wheel_speed_spread_kmh": distribution(wheel_spread),
            "regen_request_execution_abs_error_nm": distribution(regen_tracking_error),
        },
        "warnings": warnings,
    }


def write_csv(path: Path, samples: list[dict[str, Any]]) -> None:
    value_names = sorted(
        {name for sample in samples for name in sample.get("values", {}).keys()}
    )
    fields = [
        "sequence",
        "group",
        "utc",
        "request_elapsed_ms",
        "response_elapsed_ms",
        "latency_ms",
        "request_hex",
        "response_hex",
        *value_names,
    ]
    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("x", newline="", encoding="utf-8") as handle:
        writer = csv.DictWriter(handle, fieldnames=fields)
        writer.writeheader()
        for sample in samples:
            row = {field: sample.get(field, "") for field in fields}
            row.update(sample["values"])
            writer.writerow(row)


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("capture", type=Path)
    parser.add_argument("--summary-json", type=Path, help="Create a JSON summary instead of only printing it")
    parser.add_argument("--samples-csv", type=Path, help="Create a flattened sample CSV; refuses to overwrite")
    args = parser.parse_args()

    records = read_records(args.capture)
    samples, warnings = validate(records)
    summary = summarize(records, samples, warnings)
    rendered = json.dumps(summary, indent=2, sort_keys=True) + "\n"
    if args.summary_json:
        args.summary_json.parent.mkdir(parents=True, exist_ok=True)
        with args.summary_json.open("x", encoding="utf-8") as handle:
            handle.write(rendered)
    else:
        print(rendered, end="")
    if args.samples_csv:
        write_csv(args.samples_csv, samples)
    return 0


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except Exception as exc:
        print(f"ERROR: {exc}", file=sys.stderr)
        raise SystemExit(1)
