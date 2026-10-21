using System;
using System.Globalization;
using System.IO;

namespace Garage
{
    // Representerar ett garage.
    internal class Garage
    {
        private readonly Vehicle?[] _vehicles;

        private static readonly StringComparer _comparer =
            StringComparer.Create(CultureInfo.InvariantCulture, CompareOptions.NumericOrdering);

        // Namnet på garaget.
        public string Name { get; set; } = "Garage 1";

        // Max antal parkeringsplatser i ett garage.
        public static int MaxCapacity => 100000;

        // Antal parkeringsplatser i garaget.
        public int Capacity => _vehicles.Length;

        // Antal lediga parkeringsplatser i garaget.
        public int FreeCapacity
        {
            get
            {
                int result = 0;
                for (int i = 0; i < _vehicles.Length; i++)
                {
                    if (_vehicles[i] == null)
                    {
                        result++;
                    }
                }
                return result;
            }
        }

        // Antal parkerade fordon i garaget.
        public int Count
        {
            get
            {
                int result = 0;
                for (int i = 0; i < _vehicles.Length; i++)
                {
                    if (_vehicles[i] != null)
                    {
                        result++;
                    }
                }
                return result;
            }
        }

        // Skapa ett garage med en viss kapacitet.
        public Garage(int capacity)
        {
            if (capacity < 1 || capacity > MaxCapacity)
            {
                throw new ArgumentException($"Antalet parkeringsplatser måste vara mellan 1 och {MaxCapacity}.");
            }

            _vehicles = new Vehicle[capacity];
        }

        // Lista samtliga parkerade fordon i garaget.
        public void ListVehicles()
        {
            int count = Count;
            if (count == 0)
            {
                Console.WriteLine("Det finns inga fordon i garaget.");
                return;
            }

            Console.WriteLine($"Det finns {count} fordon parkerade i garaget.");
            int pagesize = 5;
            Console.Write($"Ange hur många du vill visa åt gången ({pagesize}): ");
            string s = Console.ReadLine()?.Trim() ?? throw new IOException();
            if (int.TryParse(s, out int n) && n > 0)
            {
                pagesize = n;
            }

            ListVehiclesInPages(_vehicles, pagesize);
        }

        // Listar fordon några åt gången.
        public static void ListVehiclesInPages(Vehicle?[] vehicles, int pagesize)
        {
            int size = 0;
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] != null)
                {
                    size++;
                }
            }

            int done = 0;
            int j;
            for (int i = 0; i < vehicles.Length;)
            {
                if (i > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{done} av {size} fordon har visats. Tryck Enter för att fortsätta.");
                    Console.WriteLine("Du kan ange en siffra för att ändra hur många som visas åt gången. Ange 0 för att avbryta.");
                    string s = Console.ReadLine() ?? throw new IOException();
                    if (int.TryParse(s, out int n))
                    {
                        if (n == 0)
                        {
                            Console.WriteLine("Avbryter.");
                            return;
                        }
                        else if (n > 0)
                        {
                            pagesize = n;
                        }
                    }
                }

                for (j = 0; j < pagesize && i < vehicles.Length; i++)
                {
                    if (vehicles[i] != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine(vehicles[i]);
                        j++;
                        done++;
                    }
                }

                while (i < vehicles.Length && vehicles[i] == null)
                {
                    i++;
                }
            }

            if (done > 1)
            {
                Console.WriteLine($"{done} av {size} fordon har visats.");
            }
        }

        // Lista fordonstyper och hur många fordon av varje typ som står i garaget.
        // Implementationen förutsätter att VehicleType har värdena 0, 1, 2, ... utan luckor.
        public void ListVehicleTypes()
        {
            if (Count == 0)
            {
                Console.WriteLine("Det finns inga fordon i garaget.");
                return;
            }

            Console.WriteLine("Följande fordonstyper finns i garaget. Siffrorna anger antal fordon av varje typ.\n");

            var types = Enum.GetValues<VehicleType>();
            int[] numberOfVehicles = new int[types.Length];

            for (int i = 0; i < _vehicles.Length; i++)
            {
                if (_vehicles[i] != null)
                {
                    numberOfVehicles[(int)_vehicles[i]!.VType]++;
                }
            }

            for (int i = 0; i < types.Length; i++)
            {
                if (numberOfVehicles[i] > 0)
                {
                    Console.WriteLine($"- {Vehicle.GetVehicleHumanName(types[i])}: {numberOfVehicles[i]}");
                }
            }
        }

        // Lägg till fordon i garaget.
        public void AddVehicle(Vehicle vehicle)
        {
            if (FindVehicle(vehicle.RegistrationNumber) != null)
            {
                throw new ArgumentException("Ett fordon med det registreringsnumret finns redan i garaget.");
            }

            for (int i = 0; i < _vehicles.Length; i++)
            {
                if (_vehicles[i] == null)
                {
                    _vehicles[i] = vehicle;
                    return;
                }
            }

            throw new ArgumentException("Det finns inga lediga parkeringsplatser i garaget.");
        }

        // Lägg till fordon i garaget via användarinput.
        public void AddVehicle()
        {
            if (FreeCapacity == 0)
            {
                Console.WriteLine("Det finns inga lediga parkeringsplatser i garaget.");
                return;
            }

            Console.WriteLine("Vilken fordonstyp vill du lägga till?");
            Console.Write("Tillgängliga fordonstyper är: ");
            var types = Enum.GetValues<VehicleType>();
            for (int i = 0; i < types.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write(", ");
                }
                Console.Write($"{i + 1} {Vehicle.GetVehicleHumanName(types[i])}");
            }
            Console.WriteLine();

            VehicleType vehicleType;
            while (true)
            {
                Console.Write("Ange fordonstyp: ");
                string s = Console.ReadLine() ?? throw new IOException();
                if (s == "")
                {
                    Console.WriteLine("Du angav ingen fordonstyp. Avbryter.");
                    return;
                }
                else if (int.TryParse(s, out int n))
                {
                    if (n < 1 || n > types.Length)
                    {
                        Console.WriteLine("Ogiltigt värde. Du måste ange en giltig fordonstyp.");
                    }
                    else
                    {
                        vehicleType = types[n - 1];
                        break;
                    }
                }
                else
                {
                    VehicleType? vt = Vehicle.GetVehicleType(s);
                    if (vt == null)
                    {
                        Console.WriteLine("Ogiltigt värde. Du måste ange en giltig fordonstyp.");
                    }
                    else
                    {
                        vehicleType = (VehicleType)vt;
                        break;
                    }
                }
            }

            string registrationNumber;
            while (true)
            {
                Console.Write("Ange registreringsnummer: ");
                registrationNumber = Console.ReadLine()?.Trim() ?? throw new IOException();
                if (registrationNumber == "")
                {
                    Console.WriteLine("Du angav inget registreringsnummer. Avbryter.");
                    return;
                }
                else if (FindVehicle(registrationNumber) != null)
                {
                    Console.WriteLine($"Ett fordon med registreringsnummer {registrationNumber} finns redan i garaget.");
                }
                else
                {
                    break;
                }
            }

            try
            {
                Vehicle vehicle = Vehicle.CreateVehicle(vehicleType, registrationNumber);

                Console.WriteLine("\nÖvrigt egenskaper är frivilliga att lägga till.");
                Console.WriteLine("Tryck bara Enter om du inte vill lägga till en viss egenskap.\n");

                vehicle.CustomizeValues();
                AddVehicle(vehicle);

                Console.WriteLine($"\nDu har lagt till följande fordon:");
                Console.WriteLine(vehicle);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Det gick inte att lägga till ett fordon. " + ex.Message);
            }
        }

        // Ta bort fordon ur garaget.
        public void RemoveVehicle(string registrationNumber)
        {
            int n = FindIndexOfVehicle(registrationNumber);
            if (n >= 0)
            {
                _vehicles[n] = null;
            }
            else
            {
                throw new ArgumentException("Något fordon med angivet registreringsnummer finns inte i garaget.");
            }
        }

        // Ta bort fordon ur garaget via användarinput.
        public void RemoveVehicle()
        {
            if (Count == 0)
            {
                Console.WriteLine("Det finns inga fordon i garaget.");
                return;
            }

            Console.Write("Ange registreringsnummer för fordon att ta bort: ");
            string s = Console.ReadLine()?.Trim() ?? throw new IOException();
            if (s == "")
            {
                Console.WriteLine("Inget registreringsnummer angivet. Avbryter.");
                return;
            }

            try
            {
                RemoveVehicle(s);
                Console.WriteLine($"Fordonet med registreringsnummer {s} har tagits bort.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Det gick inte att ta bort något fordon. " + ex.Message);
            }
        }

        // Lägg till slumpmässiga fordon i garaget.
        public void AddRandomVehicles(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Antalet fordon att lägga till får inte vara ett negativt tal.");
            }
            else if (n > FreeCapacity)
            {
                throw new ArgumentException("Det finns inte tillräckligt många lediga platser i garaget.");
            }

            int i;
            for (i = 0; i < n; i++)
            {
                int tries = 0;
                while (tries < 100)
                {
                    Vehicle vehicle = Vehicle.CreateRandomVehicle();
                    if (FindVehicle(vehicle.RegistrationNumber) == null)
                    {
                        AddVehicle(vehicle);
                        break;
                    }
                    tries++;
                }
                if (tries == 100)
                {
                    throw new Exception("Det gick inte att hitta något unikt registreringsnummer.");
                }
            }
        }

        // Returnerar "1 ledig plats" eller $"{n} lediga platser" beroende på värdet av n.
        private static string LedigaPlatser(int n)
        {
            return n == 1 ? "1 ledig plats" : $"{n} lediga platser";
        }

        // Lägg till slumpmässiga fordon i garaget via användarinput.
        public void AddRandomVehicles()
        {
            if (FreeCapacity == 0)
            {
                Console.WriteLine("Det går inte att lägga till fler fordon eftersom garaget är fullt. Avbryter.");
                return;
            }

            Console.WriteLine($"Garaget har {LedigaPlatser(FreeCapacity)}.");

            int nRandom;
            int defaultRandom = FreeCapacity / 2;
            if (defaultRandom < 1)
            {
                defaultRandom = 1;
            }
            while (true)
            {
                Console.Write($"Ange antal slumpmässiga fordon att lägga till ({defaultRandom}): ");
                string s = Console.ReadLine() ?? throw new IOException();
                if (s == "")
                {
                    Console.WriteLine($"Använder det föreslagna värdet: {defaultRandom}.");
                    nRandom = defaultRandom;
                    break;
                }
                else if (!int.TryParse(s, out nRandom) || nRandom < 0)
                {
                    Console.WriteLine("Ogiltigt värde. Ange ett giltigt icke-negativt heltal.");
                }
                else if (nRandom > FreeCapacity)
                {
                    Console.WriteLine($"Du kan inte lägga till så många fordon. Garaget har bara {LedigaPlatser(FreeCapacity)}.");
                }
                else
                {
                    break;
                }
            }

            if (nRandom == 0)
            {
                Console.WriteLine("Inga fordon kommer att läggas till.");
                return;
            }

            Console.WriteLine();

            int oldCount = Count;
            try
            {
                AddRandomVehicles(nRandom);
                Console.WriteLine($"Du lade till {nRandom} fordon i garaget.");
            }
            catch (Exception ex)
            {
                if (Count > oldCount)
                {
                    Console.WriteLine($"Du lade till {Count - oldCount} fordon i garaget.");
                    Console.WriteLine("Det gick inte att lägga till alla fordon. " + ex.Message);
                }
                else
                {
                    Console.WriteLine("Det gick inte att lägga till några fordon i garaget. " + ex.Message);
                }
            }
        }

        // Hitta index i garaget för ett fordon via registreringsnumret.
        // Returnerar -1 om fordonet inte finns i garaget.
        public int FindIndexOfVehicle(string registrationNumber)
        {
            string regNr = Vehicle.NormalizeRegistrationNumber(registrationNumber);
            return Array.FindIndex(_vehicles, v => v != null && v.RegistrationNumber == regNr);
        }

        // Hitta ett fordon via registreringsnumret.
        // Returnerar null om fordonet inte finns i garaget.
        public Vehicle? FindVehicle(string registrationNumber)
        {
            string regNr = Vehicle.NormalizeRegistrationNumber(registrationNumber);
            return Array.Find(_vehicles, v => v != null && v.RegistrationNumber == regNr);
        }

        // Hitta ett fordon via registreringsnumret via användarinput.
        public void FindVehicle()
        {
            Console.Write("Ange registreringsnummer: ");
            string registrationNumber = Console.ReadLine()?.Trim() ?? throw new IOException();

            if (registrationNumber == "")
            {
                Console.WriteLine("Inget registreringsnummer angavs. Avbryter.");
                return;
            }

            Vehicle? vehicle = FindVehicle(registrationNumber);
            if (vehicle == null)
            {
                Console.WriteLine($"Det finns inget fordon med registreringsnummer {registrationNumber} i garaget.");
            }
            else
            {
                Console.WriteLine("\nDet sökta fordonet finns i garaget:");
                Console.WriteLine(vehicle);
            }
        }

        // Ta emot input om en sökegenskap.
        public static void InputSearchProperty(string property, StringDictionary properties,
            StringDictionary searchValues)
        {
            StringSet stringset = properties.Get(property)!;
            if (stringset.Count > 0)
            {
                Console.WriteLine("\nEgenskap: " + property);
                Console.Write($"Möjliga värden: ");
                string[] availableValues = stringset.Values();
                Array.Sort(availableValues, _comparer);
                const int maxDisplayed = 50;
                int len = Math.Min(availableValues.Length, maxDisplayed);
                Console.Write(String.Join(", ", availableValues[0..len]));
                if (availableValues.Length > maxDisplayed)
                {
                    Console.Write(", ...");
                    Console.Write("\nAntalet möjliga värden är så många att alla inte visas här.");
                }
                Console.WriteLine();

                int counter = 0;
                while (true)
                {
                    Console.Write($"Ange ett{(counter > 0 ? " till" : "")} sökvärde för {property}: ");
                    string s = Console.ReadLine()?.Trim().ToLower() ?? throw new IOException();
                    if (s == "")
                    {
                        break;
                    }
                    if (!stringset.Contains(s))
                    {
                        Console.WriteLine("Det angivna värdet finns inte bland de möjliga värdena.");
                        Console.WriteLine("Eftersom värdet inte skulle matcha några fordon har det ignorerats.");
                    }
                    else
                    {
                        searchValues.Add(property, s);
                        counter++;
                    }
                }
            }
            else
            {
                Console.WriteLine($"{property}: Inga värden tillgängliga.");
            }
        }

        // Hitta fordon utifrån egenskaper via användarinput.
        public void FilterVehicles()
        {
            Console.WriteLine("För att hitta fordon, ange värden för de egenskaper du vill söka på.");
            Console.WriteLine("Om du anger flera värden för samma egenskap så får du matchningar\nsom stämmer överens med något av värdena.");
            Console.WriteLine("Tryck Enter när du inte vill ange fler sökvärden.");

            StringDictionary properties = new();
            for (int i = 0; i < _vehicles.Length; i++)
            {
                if (_vehicles[i] == null)
                {
                    continue;
                }
                Vehicle vehicle = _vehicles[i]!;
                for (int j = 0; j < vehicle.Properties.Length; j++)
                {
                    string property = vehicle.Properties[j];
                    properties.Add(property, vehicle.GetPropertyValue(property));
                }
            }

            StringDictionary searchValues = new();
            string[] propKeys = properties.Keys();
            Array.Sort(propKeys, _comparer);

            while (true)
            {
                Console.WriteLine();
                bool hasValues = searchValues.Count > 0;
                if (hasValues)
                {
                    Console.WriteLine("Sökningen innehåller för närvarande följande sökvärden:");
                    Console.WriteLine(searchValues);
                }

                Console.Write("Följande egenskaper kan sökas på: ");
                Console.WriteLine(String.Join(", ", propKeys));

                Console.Write($"Ange{(hasValues ? " en till" : "")} egenskap att söka på: ");
                string s = Console.ReadLine()?.Trim().ToLower() ?? throw new IOException();
                if (s == "")
                {
                    break;
                }
                string? prop = Array.Find(propKeys, p => p == s);
                if (prop == null)
                {
                    Console.WriteLine("Ogiltigt värde. Ange en giltig egenskap, eller tryck Enter för att inte ange fler egenskaper.");
                    continue;
                }
                else
                {
                    InputSearchProperty(prop, properties, searchValues);
                }
            }

            Console.WriteLine("\nLägger inte till fler sökvärden.");
            Console.WriteLine();

            if (searchValues.Count == 0)
            {
                Console.WriteLine("Du har inte angivit några sökvärden. Avbryter.");
                return;
            }

            Console.WriteLine("Du har angivit följande sökvärden:");
            Console.WriteLine(searchValues);

            Vehicle[] filteredVehicles = Search(searchValues);
            if (filteredVehicles.Length == 0)
            {
                Console.WriteLine("Inga fordon matchade sökningen.");
            }
            else
            {
                Console.WriteLine($"Följande {filteredVehicles.Length} fordon matchar sökningen:");

                ListVehiclesInPages(filteredVehicles, 5);
            }
        }

        // Sök efter fordon utifrån en eller flera egenskaper.
        public Vehicle[] Search(StringDictionary searchValues)
        {
            Vehicle?[] result = Vehicles();
            for (int i = 0; i < result.Length; i++)
            {
                if (!result[i]!.Matches(searchValues))
                {
                    result[i] = null;
                }
            }

            // Ta bort null-värden från result
            int size = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != null)
                {
                    result[size++] = result[i];
                }
            }
            Array.Resize(ref result, size);
            return result!;
        }

        // Returnerar en array med alla fordon i garaget.
        // Om det have varit tillåtet att använda System.Collections
        // hade det varit bättre att returnera en IEnumerable<Vehicle> istället.
        // Man hade också kunnat implementera en indexer istället,
        // men det är oklart huruvida det är tillåtet.
        public Vehicle[] Vehicles()
        {
            Vehicle[] result = new Vehicle[_vehicles.Length];
            int index = 0;
            for (int i = 0; i < _vehicles.Length; i++)
            {
                if (_vehicles[i] != null)
                {
                    result[index++] = _vehicles[i]!;
                }
            }
            Array.Resize(ref result, index);
            return result;
        }

        // Ta bort alla fordon från garaget.
        public void Clear()
        {
            for (int i = 0; i < _vehicles.Length; i++)
            {
                _vehicles[i] = null;
            }
        }

        // Ta bort alla fordon från garaget via användarinput.
        public void AskAndClear()
        {
            Console.WriteLine("Är du säker på att du vill ta bort alla fordon? Skriv i så fall: ja");
            string inp = Console.ReadLine()?.Trim() ?? throw new IOException();
            if (inp == "ja")
            {
                Clear();
                Console.WriteLine("Alla fordon togs bort.");
            }
            else
            {
                Console.WriteLine("Avbryter. Inga fordon togs bort.");
            }
        }
    }
}
