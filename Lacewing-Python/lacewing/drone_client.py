from typing import Optional

from .data.drone_command import DroneCommand
from .data.drone_telemetry import DroneTelemetry
from .upd_client import UdpClient


class DroneClient:
    telemetry: Optional[DroneTelemetry]

    def __init__(self, address: str = "127.0.0.1", port: int = 8456):
        self.address = address
        self.port = port
        self.telemetry = None

        self.udp_client = UdpClient(self._on_receive, address, port)
        self.udp_client.start_listening_thread()

    def _on_receive(self, message: str) -> None:
        if message.isspace():
            return
        self.telemetry = DroneTelemetry.from_json(message)

    def send_command(self, command: DroneCommand):
        self.udp_client.send_message(command.to_json())
