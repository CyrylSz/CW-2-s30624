namespace Container_Manager;

public abstract class Container
{
    private static int _serialCounter = 1;

    public string SerialNumber { get; }
    public double CargoMass { get; protected set; }
    public double Height { get; }
    public double TareWeight { get; }
    public double Depth { get; }
    public double MaxPayload { get; }
    public ContainerShip Ship { get; set; }

    protected Container(double height, double tareWeight, double depth, double maxPayload, string type)
    {
        SerialNumber = $"KON-{type}-{_serialCounter++}";
        Height = height;
        TareWeight = tareWeight;
        Depth = depth;
        MaxPayload = maxPayload;
        CargoMass = 0;
        Ship = null;
    }

    public abstract void LoadCargo(double mass);
    public abstract void EmptyCargo();

    protected void CheckOverfill(double mass)
    {
        if (mass > MaxPayload)
            throw new OverfillException($"Próba załadowania {mass} kg przekracza maksymalną ładowność {MaxPayload} kg.");
    }
}

public class OverfillException : Exception
{
    public OverfillException(string message) : base(message) { }
}