namespace Container_Manager;

public class RefrigeratedContainer : Container
{
    public string ProductType { get; }
    public double Temperature { get; }
    private static readonly Dictionary<string, double> ProductTemperatures = new ()
    {
        {"Bananas", 13.3}, {"Chocolate", 18}, {"Fish", 2}, {"Meat", -15}, {"Ice cream", -18},
        {"Frozen pizza", -30}, {"Cheese", 7.2}, {"Sausages", 5}, {"Butter", 20.5}, {"Eggs", 19}
    };

    public RefrigeratedContainer(double height, double tareWeight, double depth, double maxPayload, string productType, double temperature)
        : base(height, tareWeight, depth, maxPayload, "C")
    {
        if (!ProductTemperatures.ContainsKey(productType) || temperature < ProductTemperatures[productType])
            throw new ArgumentException($"Temperatura {temperature}°C jest za niska dla produktu {productType}. Wymagana: {ProductTemperatures[productType]}°C.");
        ProductType = productType;
        Temperature = temperature;
    }

    public override void LoadCargo(double mass)
    {
        CheckOverfill(mass);
        CargoMass = mass;
    }

    public override void EmptyCargo()
    {
        CargoMass = 0;
    }
}