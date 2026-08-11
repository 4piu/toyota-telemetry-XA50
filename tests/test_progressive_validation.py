from __future__ import annotations

import importlib.util
from pathlib import Path
import sys
import unittest


REPOSITORY = Path(__file__).resolve().parents[1]


def load_module(name: str, relative_path: str):
    spec = importlib.util.spec_from_file_location(name, REPOSITORY / relative_path)
    assert spec and spec.loader
    module = importlib.util.module_from_spec(spec)
    sys.modules[name] = module
    spec.loader.exec_module(module)
    return module


planner = load_module("progressive_planner", "tools/validation/build-progressive-plan.py")
runner = load_module("progressive_runner", "tools/validation/run-progressive-validation.py")


class ProgressivePlannerTests(unittest.TestCase):
    def test_keyword_matching_uses_boundaries(self):
        self.assertFalse(planner.text_has("Driving Control", planner.IDENTITY_WORDS))
        self.assertFalse(planner.text_has("Temperature", ("rate",)))
        self.assertTrue(planner.text_has("VIN", planner.IDENTITY_WORDS))

    def test_tier_classification_prioritizes_archive_only_payloads(self):
        self.assertEqual(planner.classify_tier(["Motor Torque"], False), "fast")
        self.assertEqual(planner.classify_tier(["Coolant Temperature"], False), "slow")
        self.assertEqual(
            planner.classify_tier(["Power Steering Available Specification"], False),
            "archive",
        )


class ProgressiveProgressTests(unittest.TestCase):
    def test_qualification_cli_omits_batch_only_quiet_flag(self):
        arguments = runner.qualification_arguments(
            "0x0102", Path("qualification.partial.jsonl"), 16
        )
        self.assertIn("--candidate-qualify", arguments)
        self.assertNotIn("--quiet", arguments)

    def test_existing_fixtures_are_reconstructed_per_did(self):
        progress = runner.discover_progress(
            REPOSITORY / "tests" / "fixtures",
            "2023-rav4-hv-xa50-a25a-fxs",
        )
        self.assertIn("0x1063", progress.qualified)
        self.assertIn("0x1063", progress.road_complete)
        self.assertIn("0x1064", progress.road_complete)
        self.assertEqual(progress.negative_codes["0x1064"], ["0x31"])
        self.assertTrue(progress.terminal_negative("0x1064"))
        self.assertFalse(progress.corrupt_files)


if __name__ == "__main__":
    unittest.main()
