namespace Container_Manager;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; }

    public LiquidContainer(double height, double tareWeight, double depth, double maxPayload, bool isHazardous)
        : base(height, tareWeight, depth, maxPayload, "L")
    {
        IsHazardous = isHazardous;
    }

    public override void LoadCargo(double mass)
    {
        double maxAllowed = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
        if (mass > maxAllowed)
        {
            NotifyHazard($"Próba załadowania {mass} kg do kontenera {SerialNumber} przekracza limit {maxAllowed} kg.");
            throw new OverfillException("Przekroczono dopuszczalny limit załadunku.");
        }
        CheckOverfill(mass);
        CargoMass = mass;
    }

    public override void EmptyCargo()
    {
        CargoMass = 0;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"[UWAGA] Kontener {SerialNumber}: {message}");
    }
}