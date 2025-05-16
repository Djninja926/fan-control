import clr  # Comes from pythonnet
import os

# Point to the folder where LibreHardwareMonitorLib.dll lives
dll_path = "C:/Users/Apia/Documents/Coding/fan-control/LibreHardwareMonitor-net472/LibreHardwareMonitorLib.dll"  # Change this to your actual path

if not os.path.exists(dll_path):
    raise FileNotFoundError("Make sure the DLL path is correct.")

clr.AddReference(dll_path)

from LibreHardwareMonitor import Hardware

# Setup the hardware computer
computer = Hardware.Computer()
computer.CPUEnabled = True
computer.MainboardEnabled = True
computer.FanControllerEnabled = True
computer.Open()


for hardware in computer.Hardware:
    hardware.Update()
    print(f"Hardware: {hardware.HardwareType} - {hardware.Name}")
    for sensor in hardware.Sensors:
        print(f"  Sensor: {sensor.SensorType} - {sensor.Name} - {sensor.Value}")


computer.Close()
