import json

from lacewing.data.ovector import OVector
from lacewing.data.pvector import PVector


class DroneCommand:
    velocity: PVector
    orientation: OVector

    def __init__(self, velocity: PVector, orientation: OVector):
        self.velocity = velocity
        self.orientation = orientation

    def to_json(self):
        return json.dumps({'velocity': self.velocity.to_dict(), 'orientation': self.orientation.to_dict()})
