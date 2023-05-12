import json
from typing import Dict

from .ovector import OVector
from .pvector import PVector


class DroneTelemetry:
    position: PVector
    orientation: OVector
    lidars: Dict[str, float]

    def __init__(self, position: PVector, orientation: OVector, lidars: Dict[str, float]):
        self.position = position
        self.orientation = orientation
        self.lidars = lidars

    @staticmethod
    def from_json(json_str: str) -> "DroneTelemetry":
        json_dict = json.loads(json_str)
        return DroneTelemetry(PVector.from_dict(json_dict['position']), OVector.from_dict(json_dict['orientation']),
                              json_dict['lidars'])

    def __repr__(self):
        return self.__str__()

    def __str__(self):
        lidars = ', '.join(F"{k}={v}" for k, v in self.lidars.items())
        return F"DroneTelemetry(position={self.position}, orientation={self.orientation}, lidars=[{lidars}]"
