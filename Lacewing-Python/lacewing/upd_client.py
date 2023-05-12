import socket
from threading import Thread
from typing import Callable, Any

BUFFER_SIZE = 2048


class UdpClient:
    def __init__(self, on_receive: Callable[[str], Any], address: str = "127.0.0.1", port: int = 8456):
        self.on_receive = on_receive
        self.address = address
        self.port = port
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        self.socket.settimeout(1.0)
        self.addr = (address, port)
        self.thread = Thread(target=self._listening_thread)
        self.run = True

    def start_listening_thread(self):
        self.send_message("")
        self.thread.start()

    def _listening_thread(self):
        while self.run:
            data, server = self.socket.recvfrom(BUFFER_SIZE)
            self.on_receive(bytes.decode(data, 'utf8'))

    def send_message(self, message: str):
        self.socket.sendto(str.encode(message, encoding='utf8'), self.addr)
