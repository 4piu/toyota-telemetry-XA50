#!/usr/bin/env python3
"""Structurally validate and summarize an XA50 direct-DID candidate capture."""

from __future__ import annotations

import argparse
import ast
import csv
import json
import math
import statistics
import sys
from collections import Counter, defaultdict
from pathlib import Path
from typing import Any


def read_jsonl(path: Path) -> list[dict[str, Any]]:
    records: list[dict[str, Any]] = []
    with path.open("r", encoding="utf-8-sig") as handle:
        for line_number, line in enumerate(handle, 1):
            if not line.strip():
                continue
            try:
                record = json.loads(line)
            except json.JSONDecodeError as exc:
                raise ValueError(f"invalid JSON on line {line_number}: {exc}") from exc
            if not isinstance(record, dict) or "record" not in record:
                raise ValueError(f"line {line_number} is not a typed JSON object")
            record["_line"] = line_number
            records.append(record)
    if not records:
        raise ValueError("capture is empty")
    return records


def classify_nrc(code: int) -> str:
    return {
        0x21: "busy_retry",
        0x22: "conditions_not_correct",
        0x31: "request_out_of_range",
        0x33: "security_denied",
        0x7E: "session_restricted",
        0x7F: "session_restricted",
    }.get(code, "negative_response")


def validate(records: list[dict[str, Any]], minimum_samples: int) -> tuple[dict[str, Any], list[dict[str, Any]]]:
    start = records[0]
    if start.get("record") != "session_start" or start.get("schema_version") != 1:
        raise ValueError("first record must be a candidate schema-v1 session_start")
    mode = start.get("mode")
    if mode not in {"candidate_qualification", "candidate_batch"}:
        raise ValueError(f"unsupported mode: {mode!r}")
    dids = start.get("dids")
    if not isinstance(dids, list) or not 1 <= len(dids) <= 6 or len(set(dids)) != len(dids):
        raise ValueError("session_start must contain 1-6 unique DIDs")
    samples = [record for record in records if record.get("record") == "candidate_sample"]
    for index, sample in enumerate(samples, 1):
        line = sample["_line"]
        if sample.get("sequence") != index:
            raise ValueError(f"non-contiguous sample sequence at line {line}")
        expected_did = dids[(index - 1) % len(dids)]
        did = sample.get("did")
        if did != expected_did:
            raise ValueError(f"schedule mismatch at line {line}: expected {expected_did}, got {did!r}")
        response_hex = sample.get("response_hex")
        try:
            response = bytes.fromhex(response_hex)
        except (TypeError, ValueError) as exc:
            raise ValueError(f"invalid response_hex at line {line}") from exc
        did_number = int(did, 16)
        positive_prefix = bytes((0x62, did_number >> 8, did_number & 0xFF))
        supported = sample.get("supported")
        if supported is True:
            if not response.startswith(positive_prefix) or sample.get("negative_response_code") is not None:
                raise ValueError(f"inconsistent positive response at line {line}")
            if sample.get("qualification_status", "supported") != "supported":
                raise ValueError(f"inconsistent qualification status at line {line}")
        elif supported is False:
            if mode != "candidate_qualification":
                raise ValueError(f"negative response in road batch at line {line}")
            if len(response) < 3 or response[:2] != b"\x7f\x22":
                raise ValueError(f"invalid UDS negative response at line {line}")
            if sample.get("negative_response_code") != f"0x{response[2]:02X}":
                raise ValueError(f"negative-response code mismatch at line {line}")
            expected_status = classify_nrc(response[2])
            if sample.get("qualification_status", expected_status) != expected_status:
                raise ValueError(f"negative-response classification mismatch at line {line}")
        else:
            raise ValueError(f"supported must be boolean at line {line}")
        timing = (sample.get("request_elapsed_ms"), sample.get("response_elapsed_ms"), sample.get("latency_ms"))
        if not all(isinstance(value, int) and value >= 0 for value in timing):
            raise ValueError(f"invalid timing at line {line}")
        if timing[1] < timing[0]:
            raise ValueError(f"response precedes request at line {line}")
        retry_count = sample.get("retry_count", 0)
        if not isinstance(retry_count, int) or isinstance(retry_count, bool) or retry_count < 0:
            raise ValueError(f"invalid retry_count at line {line}")

    ends = [record for record in records if record.get("record") == "session_end"]
    if len(ends) != 1 or records[-1].get("record") != "session_end":
        raise ValueError("capture is incomplete: exactly one final session_end is required")
    end = ends[0]
    if end.get("reason") != "completed":
        raise ValueError(f"capture is incomplete: session_end reason is {end.get('reason')!r}")
    if any(record.get("record") == "error" for record in records):
        raise ValueError("capture contains an error record")
    counts = Counter(sample["did"] for sample in samples)
    if end.get("sample_count") != len(samples) or end.get("did_counts") != dict(counts):
        raise ValueError("session_end sample counts do not match parsed records")
    if mode == "candidate_qualification":
        if len(samples) != len(dids) or any(counts[did] != 1 for did in dids):
            raise ValueError("qualification must contain exactly one response for every DID")
    elif any(counts[did] < minimum_samples for did in dids):
        shortage = {did: counts[did] for did in dids if counts[did] < minimum_samples}
        raise ValueError(f"batch did not reach the minimum {minimum_samples} samples per DID: {shortage}")
    return start, samples


def percentile(values: list[float], fraction: float) -> float | None:
    if not values:
        return None
    ordered = sorted(values)
    position = (len(ordered) - 1) * fraction
    low, high = math.floor(position), math.ceil(position)
    return ordered[low] if low == high else ordered[low] * (high - position) + ordered[high] * (position - low)


def extract_raw(data: bytes, first_bit: int, last_bit: int) -> int:
    if first_bit < 0 or last_bit < first_bit or last_bit >= len(data) * 8:
        raise ValueError("catalog bit range exceeds the response data")
    width = last_bit - first_bit + 1
    shift = len(data) * 8 - 1 - last_bit
    return (int.from_bytes(data, "big") >> shift) & ((1 << width) - 1)


def evaluate_formula(expression: str, raw: int) -> float:
    tree = ast.parse(expression, mode="eval")

    def visit(node: ast.AST) -> float:
        if isinstance(node, ast.Expression):
            return visit(node.body)
        if isinstance(node, ast.Name) and node.id == "raw":
            return float(raw)
        if isinstance(node, ast.Constant) and isinstance(node.value, (int, float)):
            return float(node.value)
        if isinstance(node, ast.UnaryOp) and isinstance(node.op, (ast.UAdd, ast.USub)):
            value = visit(node.operand)
            return value if isinstance(node.op, ast.UAdd) else -value
        if isinstance(node, ast.BinOp) and isinstance(node.op, (ast.Add, ast.Sub, ast.Mult, ast.Div, ast.Pow)):
            left, right = visit(node.left), visit(node.right)
            if isinstance(node.op, ast.Add): return left + right
            if isinstance(node.op, ast.Sub): return left - right
            if isinstance(node.op, ast.Mult): return left * right
            if isinstance(node.op, ast.Div): return left / right
            return left**right
        raise ValueError(f"unsupported formula syntax: {expression}")

    return visit(tree)


def load_catalog(path: Path, dids: set[str]) -> dict[str, list[dict[str, str]]]:
    result: dict[str, list[dict[str, str]]] = defaultdict(list)
    if not path.is_file():
        return result
    normalized = {f"0x{int(did, 16):04X}" for did in dids}
    with path.open("r", encoding="utf-8-sig", newline="") as handle:
        for row in csv.DictReader(handle):
            try:
                did = f"0x{int(row.get('data_id', ''), 16):04X}"
            except ValueError:
                continue
            if row.get("ecu_key") == "397" and did in normalized and row.get("first_bit") and row.get("last_bit"):
                result[did].append(row)
    return result


def summarize(start: dict[str, Any], samples: list[dict[str, Any]], catalog_path: Path) -> dict[str, Any]:
    dids = start["dids"]
    supported = sorted({sample["did"] for sample in samples if sample["supported"]})
    negatives = {
        sample["did"]: {
            "nrc": sample["negative_response_code"],
            "classification": sample.get(
                "qualification_status", classify_nrc(int(sample["negative_response_code"], 16))
            ),
        }
        for sample in samples
        if not sample["supported"]
    }
    counts = Counter(sample["did"] for sample in samples)
    latency: dict[str, list[float]] = defaultdict(list)
    data_by_did: dict[str, list[bytes]] = defaultdict(list)
    retry_counts: Counter[str] = Counter()
    for sample in samples:
        latency[sample["did"]].append(float(sample["latency_ms"]))
        if sample["supported"]:
            data_by_did[sample["did"]].append(bytes.fromhex(sample["response_hex"])[3:])
        retry_counts[sample["did"]] += int(sample.get("retry_count", 0))
    catalog = load_catalog(catalog_path, set(supported))
    signals: list[dict[str, Any]] = []
    for did in supported:
        for row in catalog.get(did, []):
            try:
                raw_values = [extract_raw(data, int(row["first_bit"]), int(row["last_bit"])) for data in data_by_did[did]]
            except ValueError:
                continue
            formula = row.get("formula") or "raw"
            try:
                values = [evaluate_formula(formula, raw) for raw in raw_values]
            except (SyntaxError, ValueError, ZeroDivisionError, OverflowError):
                values = []
            signals.append({
                "did": did,
                "name": row.get("name"),
                "bits": f"{row['first_bit']}..{row['last_bit']}",
                "formula": formula,
                "unit": row.get("unit"),
                "sample_count": len(raw_values),
                "raw_min": min(raw_values),
                "raw_max": max(raw_values),
                "unique_raw_values": len(set(raw_values)),
                "value_min": min(values) if values else None,
                "value_max": max(values) if values else None,
                "evidence_hint": "varied" if len(set(raw_values)) > 1 else "constant_in_this_run",
            })
    return {
        "schema_version": 1,
        "source_mode": start["mode"],
        "batch_id": start.get("batch_id"),
        "complete": True,
        "dids": dids,
        "supported_dids": supported,
        "negative_dids": negatives,
        "sample_counts": dict(counts),
        "latency_ms": {
            did: {"min": min(values), "median": statistics.median(values), "p95": percentile(values, 0.95), "max": max(values)}
            for did, values in latency.items()
        },
        "transport_retries": {
            "total": sum(retry_counts.values()),
            "samples_with_retry": sum(int(sample.get("retry_count", 0)) > 0 for sample in samples),
            "maximum_for_one_sample": max(
                (int(sample.get("retry_count", 0)) for sample in samples), default=0
            ),
            "by_did": {did: count for did, count in sorted(retry_counts.items()) if count},
        },
        "catalog_signal_observations": signals,
        "catalog_available": catalog_path.is_file(),
        "confidence_policy": "Evidence hints are review aids only; this tool never changes the validated signal map.",
    }


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("capture", type=Path)
    parser.add_argument("--catalog", type=Path, default=Path("data/catalog/generated/telemetry.csv"))
    parser.add_argument("--minimum-samples", type=int, default=0)
    parser.add_argument("--summary-json", type=Path)
    args = parser.parse_args()
    try:
        records = read_jsonl(args.capture)
        start, samples = validate(records, args.minimum_samples)
        report = summarize(start, samples, args.catalog)
    except (OSError, ValueError) as exc:
        print(f"ERROR: {exc}", file=sys.stderr)
        return 1
    rendered = json.dumps(report, indent=2, ensure_ascii=False)
    if args.summary_json:
        args.summary_json.parent.mkdir(parents=True, exist_ok=True)
        args.summary_json.write_text(rendered + "\n", encoding="utf-8")
    print(rendered)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
