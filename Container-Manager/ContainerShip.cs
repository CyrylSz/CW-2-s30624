namespace Container_Manager;

public class ContainerShip
{
    public string Name { get; }
    public List<Container> Containers { get; } = new ();
    public double MaxSpeed { get; }
    public int MaxContainerCount { get; }
    public double MaxWeight { get; }

    public ContainerShip(string name, double maxSpeed, int maxContainerCount, double maxWeight)
    {
        Name = string.IsNullOrWhiteSpace(name) ? "Statek" : name;
        MaxSpeed = maxSpeed;
        MaxContainerCount = maxContainerCount;
        MaxWeight = maxWeight;
    }

    public void LoadContainer(Container container)
    {
        if (Containers.Count >= MaxContainerCount)
            throw new InvalidOperationException("Przekroczono maksymalną liczbę kontenerów.");
        if (Containers.Any(c => c.SerialNumber == container.SerialNumber))
            throw new InvalidOperationException($"Kontener {container.SerialNumber} jest już załadowany na tym statku.");
        if (container.Ship != null)
            throw new InvalidOperationException($"Kontener {container.SerialNumber} jest już załadowany na innym statku.");
        
        double totalWeight = Containers.Sum(c => c.CargoMass + c.TareWeight) / 1000 + (container.CargoMass + container.TareWeight) / 1000;
        if (totalWeight > MaxWeight)
            throw new InvalidOperationException("Przekroczono maksymalną wagę statku.");
        
        Containers.Add(container);
        container.Ship = this;
    }

    public void LoadContainers(List<Container> containers)
    {
        foreach (var container in containers)
            LoadContainer(container);
    }

    public void UnloadContainer(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
            throw new InvalidOperationException("Kontener nie znaleziony.");

        Containers.Remove(container);
        container.Ship = null;
    }

    public void ReplaceContainer(string oldSerial, Container newContainer)
    {
        UnloadContainer(oldSerial);
        LoadContainer(newContainer);
    }

    public void TransferContainer(ContainerShip otherShip, string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null) throw new InvalidOperationException("Kontener nie znaleziony.");
        UnloadContainer(serialNumber);
        otherShip.LoadContainer(container);
    }

    public override string ToString()
    {
        return $"Statek (prędkość={MaxSpeed} węzłów, max kontenerów={MaxContainerCount}, max waga={MaxWeight}t)";
    }
}