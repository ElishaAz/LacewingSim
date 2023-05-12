from dataclasses import dataclass
from typing import Dict


@dataclass
class PVector:
    right: float = 0
    up: float = 0
    forward: float = 0

    def to_dict(self) -> Dict[str, float]:
        return {'right': self.right, 'up': self.up, 'forward': self.forward}

    @staticmethod
    def from_dict(json_dict: Dict[str, float]) -> "PVector":
        return PVector(json_dict['right'], json_dict['up'], json_dict['forward'])
