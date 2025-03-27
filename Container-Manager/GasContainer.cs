namespace Container_Manager;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; }

    public GasContainer(double height, double tareWeight, double depth, double maxPayload, double pressure)
        : base(height, tareWeight, depth, maxPayload, "G")
    {
        Pressure = pressure;
    }

    public override void LoadCargo(double mass)
    {
        CheckOverfill(mass);
        CargoMass = mass;
    }

    public override void EmptyCargo()
    {
        CargoMass *= 0.05;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"[UWAGA] Kontener {SerialNumber}: {message}");
    }
}