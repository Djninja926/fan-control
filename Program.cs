using System;
// using System.Threading;
using LibreHardwareMonitor.Hardware;

public class UpdateVisitor : IVisitor
{
    public void VisitComputer(IComputer computer)
    {
        computer.Traverse(this);
    }
    public void VisitHardware(IHardware hardware)
    {
        hardware.Update();
        foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
    }
    public void VisitSensor(ISensor sensor) { }
    public void VisitParameter(IParameter parameter) { }
}
class Program {
    static void Main(string[] args) {
        // Initialize the hardware monitor
    var computer = new Computer
    {
        IsCpuEnabled = true,
        IsGpuEnabled = true,
        IsMotherboardEnabled = true,
        IsMemoryEnabled = true,
        IsStorageEnabled = true,
        // IsNetworkEnabled = true,
        // IsControllerEnabled = true,
        // IsSensorEnabled = true,
        // IsTemperatureEnabled = true,
        // IsVoltageEnabled = true,
    };
    computer.Open();
    computer.Accept(new UpdateVisitor());

    foreach (IHardware hardware in computer.Hardware)
    {
        Console.WriteLine("Hardware: {0}", hardware.Name);
        
        foreach (IHardware subhardware in hardware.SubHardware)
        {
            Console.WriteLine("\tSubhardware: {0}", subhardware.Name);
            
            foreach (ISensor sensor in subhardware.Sensors)
            {
                Console.WriteLine("\t\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
            }
        }

        foreach (ISensor sensor in hardware.Sensors)
        {
            Console.WriteLine("\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
        }
    }
    
    computer.Close();

        Console.WriteLine("Finished");
    }
}