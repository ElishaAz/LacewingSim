from dataclasses import dataclass
from typing import Dict


@dataclass
class OVector:
    roll: float = 0  # positive is right
    pitch: float = 0  # positive is forward
    yaw: float = 0  # positive is clockwise (top down)

    def to_dict(self) -> Dict[str, float]:
        return {'roll': self.roll, 'pitch': self.pitch, 'yaw': self.yaw}

    @staticmethod
    def from_dict(json_dict: Dict[str, float]) -> "OVector":
        return OVector(json_dict['roll'], json_dict['pitch'], json_dict['yaw'])
