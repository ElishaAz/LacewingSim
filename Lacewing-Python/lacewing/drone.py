from typing import Optional

from .data.drone_command import DroneCommand
from .data.drone_telemetry import DroneTelemetry
from .data.ovector import OVector
from .data.pvector import PVector
from .drone_client import DroneClient


class Drone:
    def __init__(self, address: str = "127.0.0.1", port: int = 8456, max_speed=5, max_angle=15):
        self.address = address
        self.port = port
        self.max_speed = max_speed
        self.max_angle = max_angle
        self.drone_client = DroneClient(address, port)

    @property
    def telemetry(self) -> Optional[DroneTelemetry]:
        return self.drone_client.telemetry

    def _get_angle(self, speed: float):
        return speed / self.max_speed * self.max_angle

    def send_command(self, right: float, up: float, forward: float, yaw: float):
        velocity = PVector(right, up, forward)
        orientation = OVector(self._get_angle(right), self._get_angle(forward), yaw)
        self.drone_client.send_command(DroneCommand(velocity, orientation))
