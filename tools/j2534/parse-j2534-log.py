#!/usr/bin/env python3
"""Extract ISO-15765 messages from a Techstream/j2534-logger text log."""

from __future__ import annotations

import argparse
import collections
import re
from dataclasses import dataclass
from pathlib import Path
from typing import Callable


CALL_RE = re.compile(
    r"^\s*(?P<time>\d+\.\d+)s\s+(?P<marker>>>|<<|\+\+|--|\*\*)\s+"
    r"(?P<call>PT\w+)\("
)
MSG_RE = re.compile(
    r"^\s*Msg\[\s*\d+\]\s+(?:(?P<timestamp>\d+\.\d+)s\.\s+)?"
    r"(?P<protocol>\d+):(?P<protocol_name>[^.]+)\.\s+"
    r"(?:(?:Actual data \d+ of )?)(?P<size>\d+) bytes\."
)
BYTE_RE = re.compile(r"(?<![0-9A-Fa-f])\[?([0-9A-Fa-f]{2})\]?(?![0-9A-Fa-f])")


@dataclass(frozen=True)
class Message:
    call_time: float
    bus_time: float | None
    direction: str
    protocol: int
    protocol_name: str
    size: int
    arbitration_id: int | None
    extended_address: int | None
    payload: bytes
    start_of_message: bool


@dataclass(frozen=True)
class CandidateSignal:
    name: str
    first_bit: int
    last_bit: int
    convert: Callable[[int], float | int | bool]
    unit: str = ""


@dataclass(frozen=True)
class DynamicSource:
    data_id: int
    position: int
    size: int


def linear(a: int, b: int, c: int, decimals: int) -> Callable[[int], float]:
    return lambda raw: ((raw * a / b) + c) / (10**decimals)


# Extracted from the locally installed Techstream HV_P5.ddb (ECU key 397).
# These remain candidates until a focused live Data List capture confirms that
# the connected vehicle's Hybrid Control ECU selects the same definitions.
XA50_CANDIDATES: dict[int, tuple[CandidateSignal, ...]] = {
    0x1022: (
        CandidateSignal("FR Target Oil Pressure", 0, 7, linear(40, 51, 0, 1), "MPa"),
        CandidateSignal("FL Target Oil Pressure", 8, 15, linear(40, 51, 0, 1), "MPa"),
        CandidateSignal("RR Target Oil Pressure", 16, 23, linear(40, 51, 0, 1), "MPa"),
        CandidateSignal("RL Target Oil Pressure", 24, 31, linear(40, 51, 0, 1), "MPa"),
    ),
    0x102D: (
        CandidateSignal("Stop Light SW", 10, 10, bool),
        CandidateSignal("Regenerative Cooperation", 78, 78, bool),
    ),
    0x1043: (
        CandidateSignal("Stop Light Switch", 15, 15, bool),
    ),
    0x1048: (
        CandidateSignal("Reaction Force Pressure", 0, 7, linear(48, 5, 0, 2), "MPa"),
    ),
    0x104A: (
        CandidateSignal("Stroke Sensor", 0, 7, linear(10, 51, 0, 1), "V"),
    ),
    0x104B: (
        CandidateSignal("Stroke Sensor2", 0, 7, linear(10, 51, 0, 1), "V"),
    ),
    0x104C: (
        CandidateSignal("Quantity of Brake Pedal Stroke", 0, 7, linear(1, 1, 0, 0), "mm"),
    ),
    0x104D: (
        CandidateSignal("Servo Pressure", 0, 7, linear(48, 5, 0, 2), "MPa"),
    ),
    0x1054: (
        CandidateSignal("FR Regenerative Request", 0, 15, linear(16, 1, 0, 0), "Nm"),
    ),
    0x1055: (
        CandidateSignal("FR Regenerative Operation", 0, 15, linear(16, 1, 0, 0), "Nm"),
    ),
    0x1058: (
        CandidateSignal("Brake Pedal Stroke Change Speed", 0, 7, linear(20, 1, -2560, 0), "mm/s"),
    ),
    0x1060: (
        CandidateSignal("Accelerator Position", 0, 7, linear(5, 1, 0, 1), "%"),
    ),
    0x1069: (
        CandidateSignal(
            "Requested MG regenerative torque", 0, 15,
            linear(25, 2, -409600, 2), "Nm"
        ),
        CandidateSignal(
            "Executed MG regenerative torque", 16, 31,
            linear(25, 2, -409600, 2), "Nm"
        ),
    ),
    0x1075: (
        CandidateSignal("FR Wheel Speed", 0, 15, linear(1, 1, -32768, 2), "km/h"),
        CandidateSignal("FL Wheel Speed", 16, 31, linear(1, 1, -32768, 2), "km/h"),
        CandidateSignal("RR Wheel Speed", 32, 47, linear(1, 1, -32768, 2), "km/h"),
        CandidateSignal("RL Wheel Speed", 48, 63, linear(1, 1, -32768, 2), "km/h"),
    ),
    0x1090: (
        CandidateSignal("Steering Angle", 0, 15, linear(1, 1, -32768, 1), "deg"),
    ),
    0x10A0: (
        CandidateSignal("Generator Revolution (MG1)", 0, 15, linear(1, 1, -32768, 0), "rpm"),
    ),
    0x10A1: (
        CandidateSignal("Motor Revolution (MG2)", 0, 15, linear(1, 1, -32768, 0), "rpm"),
    ),
    0x10A2: (
        CandidateSignal("Rear Motor Revolution", 0, 15, linear(1, 1, -32768, 0), "rpm"),
    ),
}


def parse_messages(path: Path) -> list[Message]:
    messages: list[Message] = []
    call_time = 0.0
    direction: str | None = None
    pending: dict[str, object] | None = None
    start_of_message = False
    extended_addressing = False

    for line in path.read_text(encoding="utf-8", errors="replace").splitlines():
        call_match = CALL_RE.match(line)
        if call_match:
            call_time = float(call_match.group("time"))
            marker = call_match.group("marker")
            call = call_match.group("call")
            if call == "PTWriteMsgs" and marker == ">>":
                direction = "TX"
            elif call == "PTReadMsgs" and marker == "<<":
                direction = "RX"
            else:
                direction = None
            pending = None
            start_of_message = False
            extended_addressing = False
            continue

        msg_match = MSG_RE.match(line)
        if msg_match and direction:
            pending = {
                "bus_time": (
                    float(msg_match.group("timestamp"))
                    if msg_match.group("timestamp")
                    else None
                ),
                "protocol": int(msg_match.group("protocol")),
                "protocol_name": msg_match.group("protocol_name"),
                "size": int(msg_match.group("size")),
            }
            start_of_message = False
            extended_addressing = False
            continue

        if pending and "START_OF_MESSAGE" in line:
            start_of_message = True
            continue

        if pending and "ISO15765_ADDR_TYPE" in line:
            extended_addressing = True
            continue

        if pending and line.lstrip().startswith("\\__"):
            raw = bytes(int(value, 16) for value in BYTE_RE.findall(line))
            size = int(pending["size"])
            if len(raw) != size:
                raise ValueError(
                    f"Declared {size} bytes but parsed {len(raw)} at call time {call_time:.3f}s"
                )

            candidate_id = int.from_bytes(raw[:4], "big") if len(raw) >= 4 else None
            # ISO-15765 carries a 4-byte, big-endian CAN ID in the J2534 data
            # buffer. Do not mislabel records from a different protocol as CAN.
            arbitration_id = (
                candidate_id
                if candidate_id is not None and candidate_id <= 0x1FFFFFFF
                else None
            )
            payload_offset = 4 if arbitration_id is not None else 0
            extended_address = None
            if extended_addressing and not start_of_message and len(raw) > payload_offset:
                extended_address = raw[payload_offset]
                payload_offset += 1
            payload = raw[payload_offset:]
            messages.append(
                Message(
                    call_time=call_time,
                    bus_time=pending["bus_time"],  # type: ignore[arg-type]
                    direction=direction,
                    protocol=int(pending["protocol"]),
                    protocol_name=str(pending["protocol_name"]),
                    size=size,
                    arbitration_id=arbitration_id,
                    extended_address=extended_address,
                    payload=payload,
                    start_of_message=start_of_message,
                )
            )
            pending = None
            start_of_message = False
            extended_addressing = False

    return messages


def hex_bytes(value: bytes) -> str:
    return " ".join(f"{byte:02X}" for byte in value) or "<empty>"


def endpoint_name(message: Message) -> str:
    if message.arbitration_id is None:
        return "<non-CAN>"
    endpoint = f"0x{message.arbitration_id:X}"
    if message.extended_address is not None:
        endpoint += f"/EA=0x{message.extended_address:02X}"
    return endpoint


def extract_msb_bit_range(data: bytes, first_bit: int, last_bit: int) -> int:
    """Extract an inclusive bit range where bit zero is the first payload MSB."""
    if first_bit < 0 or last_bit < first_bit or last_bit >= len(data) * 8:
        raise ValueError(
            f"bit range {first_bit}-{last_bit} does not fit {len(data)} data bytes"
        )
    width = last_bit - first_bit + 1
    shift = len(data) * 8 - 1 - last_bit
    return (int.from_bytes(data, "big") >> shift) & ((1 << width) - 1)


def format_value(value: float | int | bool, unit: str) -> str:
    if isinstance(value, bool):
        rendered = "ON" if value else "OFF"
    elif isinstance(value, float) and value.is_integer():
        rendered = str(int(value))
    elif isinstance(value, float):
        rendered = f"{value:.3f}".rstrip("0").rstrip(".")
    else:
        rendered = str(value)
    return f"{rendered} {unit}".rstrip()


def parse_dynamic_definition(payload: bytes) -> tuple[int, tuple[DynamicSource, ...]] | None:
    """Parse UDS DefineByIdentifier: 2C 01 target-DID (source-DID position size)+."""
    if len(payload) < 8 or payload[:2] != b"\x2c\x01" or (len(payload) - 4) % 4:
        return None
    target_data_id = int.from_bytes(payload[2:4], "big")
    sources = tuple(
        DynamicSource(
            data_id=int.from_bytes(payload[offset:offset + 2], "big"),
            position=payload[offset + 2],
            size=payload[offset + 3],
        )
        for offset in range(4, len(payload), 4)
    )
    if any(source.position == 0 or source.size == 0 for source in sources):
        return None
    return target_data_id, sources


def collect_declared_dynamic_definitions(
    messages: list[Message],
) -> dict[int, tuple[DynamicSource, ...]]:
    """Collect the last explicit definition for each dynamic DID, ignoring later clears."""
    definitions: dict[int, tuple[DynamicSource, ...]] = {}
    for message in messages:
        if message.direction != "TX" or message.start_of_message:
            continue
        definition = parse_dynamic_definition(message.payload)
        if definition:
            target_data_id, sources = definition
            definitions[target_data_id] = sources
    return definitions


def decode_candidate_source(
    source_data_id: int,
    source_data: bytes,
    source_position: int = 1,
) -> list[tuple[CandidateSignal, float | int | bool]]:
    signals = XA50_CANDIDATES.get(source_data_id, ())
    selected_first_bit = (source_position - 1) * 8
    selected_last_bit = selected_first_bit + len(source_data) * 8 - 1
    decoded: list[tuple[CandidateSignal, float | int | bool]] = []
    for signal in signals:
        if signal.first_bit < selected_first_bit or signal.last_bit > selected_last_bit:
            continue
        raw = extract_msb_bit_range(
            source_data,
            signal.first_bit - selected_first_bit,
            signal.last_bit - selected_first_bit,
        )
        decoded.append((signal, signal.convert(raw)))
    return decoded


def print_xa50_candidates(
    messages: list[Message],
    changes_only: bool,
    initial_dynamic_definitions: dict[int, tuple[DynamicSource, ...]] | None = None,
) -> None:
    last_values: dict[tuple[int | None, int | None, int, str], float | int | bool] = {}
    dynamic_definitions = dict(initial_dynamic_definitions or {})
    matches = 0
    for message in messages:
        payload = message.payload
        if message.direction == "TX" and not message.start_of_message:
            definition = parse_dynamic_definition(payload)
            if definition:
                target_data_id, sources = definition
                dynamic_definitions[target_data_id] = sources
            elif len(payload) == 4 and payload[:2] == b"\x2c\x03":
                dynamic_definitions.pop(int.from_bytes(payload[2:4], "big"), None)
            continue
        if (
            message.direction != "RX"
            or message.start_of_message
            or len(payload) < 3
            or payload[0] != 0x62
        ):
            continue
        data_id = int.from_bytes(payload[1:3], "big")
        data = payload[3:]
        decoded_values: list[tuple[int, CandidateSignal, float | int | bool]] = []
        if data_id in XA50_CANDIDATES:
            decoded_values.extend(
                (data_id, signal, value)
                for signal, value in decode_candidate_source(data_id, data)
            )
        elif data_id in dynamic_definitions:
            cursor = 0
            for source in dynamic_definitions[data_id]:
                source_data = data[cursor:cursor + source.size]
                cursor += source.size
                if len(source_data) != source.size:
                    break
                decoded_values.extend(
                    (source.data_id, signal, value)
                    for signal, value in decode_candidate_source(
                        source.data_id, source_data, source.position
                    )
                )
        if not decoded_values:
            continue
        decoded: list[str] = []
        for source_data_id, signal, value in decoded_values:
            key = (
                message.arbitration_id,
                message.extended_address,
                source_data_id,
                signal.name,
            )
            if changes_only and last_values.get(key) == value:
                continue
            last_values[key] = value
            decoded.append(f"{signal.name}={format_value(value, signal.unit)}")
        if decoded:
            matches += 1
            print(
                f"{message.call_time:9.3f}s {endpoint_name(message):>18} "
                f"DID 0x{data_id:04X}  " + "; ".join(decoded)
            )
    if matches == 0:
        print(
            "No candidate XA50 Hybrid Control 0x62 responses found. "
            "This may be a non-Data-List capture, a different ECU definition, "
            "or an ID/address filter that excludes the response endpoint."
        )


def print_timeline(messages: list[Message], include_start_indications: bool) -> None:
    for message in messages:
        if message.start_of_message and not include_start_indications:
            continue
        endpoint = endpoint_name(message)
        suffix = " [ISO-TP start indication]" if message.start_of_message else ""
        print(
            f"{message.call_time:9.3f}s {message.direction:2} "
            f"{endpoint:>18}  {hex_bytes(message.payload)}{suffix}"
        )


def print_summary(messages: list[Message]) -> None:
    complete = [message for message in messages if not message.start_of_message]
    print(f"Parsed messages: {len(complete)} complete, {len(messages) - len(complete)} start indications")

    by_endpoint: dict[tuple[str, int | None, int | None], list[Message]] = collections.defaultdict(list)
    for message in complete:
        by_endpoint[(
            message.direction,
            message.arbitration_id,
            message.extended_address,
        )].append(message)

    for (direction, arbitration_id, extended_address), endpoint_messages in sorted(
        by_endpoint.items(),
        key=lambda item: (
            item[0][0],
            item[0][1] if item[0][1] is not None else -1,
            item[0][2] if item[0][2] is not None else -1,
        ),
    ):
        endpoint = endpoint_name(endpoint_messages[0])
        unique_payloads = collections.Counter(message.payload for message in endpoint_messages)
        print(f"\n{direction} {endpoint}: {len(endpoint_messages)} messages, {len(unique_payloads)} unique payloads")
        for payload, count in unique_payloads.most_common():
            print(f"  {count:4}x  {hex_bytes(payload)}")


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("log", type=Path, help="Path to a j2534_*.log file")
    parser.add_argument(
        "--timeline",
        action="store_true",
        help="Print messages chronologically instead of grouping them",
    )
    parser.add_argument(
        "--include-start-indications",
        action="store_true",
        help="Include J2534 ISO-TP START_OF_MESSAGE indications in the timeline",
    )
    parser.add_argument(
        "--id",
        action="append",
        type=lambda value: int(value, 0),
        dest="arbitration_ids",
        metavar="CAN_ID",
        help="Only show this CAN arbitration ID; repeat for multiple IDs",
    )
    parser.add_argument(
        "--decode-xa50",
        action="store_true",
        help="Decode candidate XA50 HV direct DIDs extracted from local Techstream data",
    )
    parser.add_argument(
        "--changes-only",
        action="store_true",
        help="With --decode-xa50, suppress unchanged values per response endpoint",
    )
    parser.add_argument(
        "--definitions-from",
        type=Path,
        metavar="LOG",
        help="Learn dynamic-DID definitions from an earlier J2534 log snapshot",
    )
    args = parser.parse_args()

    messages = parse_messages(args.log)
    if args.arbitration_ids:
        selected_ids = set(args.arbitration_ids)
        messages = [
            message for message in messages
            if message.arbitration_id in selected_ids
        ]
    if args.decode_xa50:
        initial_definitions = None
        if args.definitions_from:
            initial_definitions = collect_declared_dynamic_definitions(
                parse_messages(args.definitions_from)
            )
        print_xa50_candidates(messages, args.changes_only, initial_definitions)
    elif args.timeline:
        print_timeline(messages, args.include_start_indications)
    else:
        print_summary(messages)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
