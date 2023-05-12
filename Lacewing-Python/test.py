import time

from lacewing.drone import Drone


def main():
    drone = Drone()
    while True:
        print(drone.telemetry)
        drone.send_command(0, 0, 0, time.time() * 5 % 360)
        time.sleep(0.1)


if __name__ == '__main__':
    main()

# Headless: run unity with "-batchmode -nographics"
