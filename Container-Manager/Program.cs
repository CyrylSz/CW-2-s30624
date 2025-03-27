namespace Container_Manager;

class Program
{
    static List<ContainerShip> ships = new ();
    static List<Container> containers = new ();

    static void Main()
    {
        while (true)
        {
            DisplayMainScreen();
            string choice = Console.ReadLine();
            try
            {
                switch (choice)
                {
                    case "1": AddShip(); break;
                    case "2": RemoveShip(); break;
                    case "3": AddContainer(); break;
                    case "4": LoadCargoToContainer(); break;
                    case "5": LoadContainerToShip(); break;
                    case "6": LoadContainersToShip(); break;
                    case "7": UnloadCargoFromContainer(); break;
                    case "8": UnloadContainerFromShip(); break;
                    case "9": ReplaceContainerOnShip(); break;
                    case "10": TransferContainerBetweenShips(); break;
                    case "11": DisplayContainerInfo(); break;
                    case "12": DisplayShipInfo(); break;
                    case "0": return;
                    default: Console.WriteLine("Nieprawidłowa opcja."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            Console.WriteLine("Naciśnij cokolwiek, aby kontynuować...");
            Console.ReadKey();
        }
    }

    static void DisplayMainScreen()
    {
        Console.Write("\x1b[3J");
        Console.Clear();

        Console.WriteLine("-----------------");
        Console.WriteLine("Lista statków:");
        if (ships.Count == 0)
        {
            Console.WriteLine("Brak");
        }
        else
        {
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ships[i].Name}");
            }
        }
        Console.WriteLine("-----------------");
        Console.WriteLine("Lista kontenerów:");
        if (containers.Count == 0)
        {
            Console.WriteLine("Brak");
        }
        else
        {
            for (int i = 0; i < containers.Count; i++)
            {
                var container = containers[i];
                string shipInfo = container.Ship != null ? $" (Załadowano na {ships.IndexOf(container.Ship) + 1}. {ships[i].Name})" : "";
                Console.WriteLine($"{i + 1}. {container.SerialNumber}{shipInfo}");
            }
        }
        Console.WriteLine("-----------------");
        Console.WriteLine("\nMożliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");
        Console.WriteLine("2. Usuń kontenerowiec");
        Console.WriteLine("3. Dodaj kontener");
        Console.WriteLine("4. Załaduj ładunek do kontenera");
        Console.WriteLine("5. Załaduj kontener na statek");
        Console.WriteLine("6. Załaduj listę kontenerów na statek");
        Console.WriteLine("7. Rozładuj kontener");
        Console.WriteLine("8. Usuń kontener ze statku");
        Console.WriteLine("9. Zastąp kontener na statku");
        Console.WriteLine("10. Przenieś kontener między statkami");
        Console.WriteLine("11. Wyświetl informacje o kontenerze");
        Console.WriteLine("12. Wyświetl informacje o statku");
        Console.WriteLine("0. Wyjdź");
        Console.Write("Wybierz opcję: ");
    }

    static void AddShip()
    {
        Console.Write("Podaj nazwę statku (opcjonalnie): ");
        string name = Console.ReadLine().Trim();
        Console.Write("Podaj maksymalną prędkość (węzły): ");
        double maxSpeed = double.Parse(Console.ReadLine());
        Console.Write("Podaj maksymalną liczbę kontenerów: ");
        int maxCount = int.Parse(Console.ReadLine());
        Console.Write("Podaj maksymalną wagę (tony): ");
        double maxWeight = double.Parse(Console.ReadLine());
        ships.Add(new ContainerShip(name, maxSpeed, maxCount, maxWeight));
        Console.WriteLine("Statek dodany.");
    }

    static void RemoveShip()
    {
        Console.Write("Podaj numer statku do usunięcia: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < ships.Count) ships.RemoveAt(index);
        else throw new ArgumentException("Nieprawidłowy numer statku.");
        Console.WriteLine("Statek usunięty.");
    }

    static void AddContainer()
    {
        Console.WriteLine("Wybierz typ: 1. Płynny, 2. Gazowy, 3. Chłodniczy");
        string type = Console.ReadLine();
        Console.Write("Wysokość (cm): ");
        double height = double.Parse(Console.ReadLine());
        Console.Write("Waga własna (kg): ");
        double tare = double.Parse(Console.ReadLine());
        Console.Write("Głębokość (cm): ");
        double depth = double.Parse(Console.ReadLine());
        Console.Write("Maksymalna ładowność (kg): ");
        double maxPayload = double.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                Console.Write("Niebezpieczny? (tak/nie): ");
                bool isHazardous = Console.ReadLine().ToLower() == "tak";
                containers.Add(new LiquidContainer(height, tare, depth, maxPayload, isHazardous));
                break;
            case "2":
                Console.Write("Ciśnienie (atm): ");
                double pressure = double.Parse(Console.ReadLine());
                containers.Add(new GasContainer(height, tare, depth, maxPayload, pressure));
                break;
            case "3":
                Console.Write("Rodzaj produktu (np. Bananas): ");
                string product = Console.ReadLine();
                Console.Write("Temperatura (°C): ");
                double temp = double.Parse(Console.ReadLine());
                containers.Add(new RefrigeratedContainer(height, tare, depth, maxPayload, product, temp));
                break;
            default: throw new ArgumentException("Nieprawidłowy typ kontenera.");
        }
        Console.WriteLine("Kontener dodany.");
    }

    static void LoadCargoToContainer()
    {
        Console.Write("Podaj numer kontenera: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Podaj masę ładunku (kg): ");
        double mass = double.Parse(Console.ReadLine());
        if (index >= 0 && index < containers.Count)
            containers[index].LoadCargo(mass);
        else
            throw new ArgumentException("Nieprawidłowy numer kontenera.");
        Console.WriteLine("Ładunek załadowany.");
    }

    static void LoadContainerToShip()
    {
        Console.Write("Podaj numer statku: ");
        int shipIndex = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Podaj numer kontenera: ");
        int contIndex = int.Parse(Console.ReadLine()) - 1;
        if (shipIndex >= 0 && shipIndex < ships.Count && contIndex >= 0 && contIndex < containers.Count)
            ships[shipIndex].LoadContainer(containers[contIndex]);
        else
            throw new ArgumentException("Nieprawidłowy numer statku lub kontenera.");
        Console.WriteLine("Kontener załadowany na statek.");
    }
    
    static void LoadContainersToShip()
    {
        Console.Write("Podaj numer statku: ");
        int shipIndex = int.Parse(Console.ReadLine()) - 1;
        if (shipIndex < 0 || shipIndex >= ships.Count)
            throw new ArgumentException("Nieprawidłowy numer statku.");

        Console.Write("Podaj numery kontenerów do załadowania (oddzielone przecinkami): ");
        string[] indices = Console.ReadLine().Split(',');
        List<Container> containersToLoad = new List<Container>();

        foreach (var idx in indices)
        {
            int contIndex = int.Parse(idx.Trim()) - 1;
            if (contIndex >= 0 && contIndex < containers.Count)
                containersToLoad.Add(containers[contIndex]);
            else
                throw new ArgumentException($"Nieprawidłowy numer kontenera: {idx}");
        }

        ships[shipIndex].LoadContainers(containersToLoad);
        Console.WriteLine("Kontenery załadowane na statek.");
    }
    
    static void UnloadCargoFromContainer()
    {
        Console.Write("Podaj numer kontenera do rozładowania: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < containers.Count)
        {
            containers[index].EmptyCargo();
            Console.WriteLine("Kontener rozładowany.");
        }
        else
        {
            throw new ArgumentException("Nieprawidłowy numer kontenera.");
        }
    }

    static void UnloadContainerFromShip()
    {
        Console.Write("Podaj numer statku: ");
        int shipIndex = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Podaj numer seryjny kontenera: ");
        string serial = Console.ReadLine();
        if (shipIndex >= 0 && shipIndex < ships.Count)
            ships[shipIndex].UnloadContainer(serial);
        else
            throw new ArgumentException("Nieprawidłowy numer statku.");
        Console.WriteLine("Kontener usunięty ze statku.");
    }

    static void ReplaceContainerOnShip()
    {
        Console.Write("Podaj numer statku: ");
        int shipIndex = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Podaj numer seryjny starego kontenera: ");
        string oldSerial = Console.ReadLine();
        Console.Write("Podaj numer nowego kontenera: ");
        int newIndex = int.Parse(Console.ReadLine()) - 1;
        if (shipIndex >= 0 && shipIndex < ships.Count && newIndex >= 0 && newIndex < containers.Count)
            ships[shipIndex].ReplaceContainer(oldSerial, containers[newIndex]);
        else
            throw new ArgumentException("Nieprawidłowy numer statku lub kontenera.");
        Console.WriteLine("Kontener zastąpiony.");
    }

    static void TransferContainerBetweenShips()
    {
        Console.Write("Podaj numer statku źródłowego: ");
        int sourceIndex = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Podaj numer statku docelowego: ");
        int targetIndex = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Podaj numer seryjny kontenera: ");
        string serial = Console.ReadLine();
        if (sourceIndex >= 0 && sourceIndex < ships.Count && targetIndex >= 0 && targetIndex < ships.Count)
            ships[sourceIndex].TransferContainer(ships[targetIndex], serial);
        else
            throw new ArgumentException("Nieprawidłowy numer statku.");
        Console.WriteLine("Kontener przeniesiony.");
    }

    static void DisplayContainerInfo()
    {
        Console.Write("Podaj numer kontenera: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < containers.Count)
            Console.WriteLine($"Kontener {containers[index].SerialNumber}: Masa={containers[index].CargoMass}kg, Max={containers[index].MaxPayload}kg");
        else
            throw new ArgumentException("Nieprawidłowy numer kontenera.");
    }

    static void DisplayShipInfo()
    {
        Console.Write("Podaj numer statku: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < ships.Count)
        {
            var ship = ships[index];
            Console.WriteLine(ship);
            Console.WriteLine("Kontenery na statku:");
            Console.WriteLine(ship.Containers.Count == 0 ? "Brak" : string.Join("\n", ship.Containers.Select(c => c.SerialNumber)));
        }
        else
            throw new ArgumentException("Nieprawidłowy numer statku.");
    }
}