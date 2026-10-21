using System;
using System.IO;

namespace Garage
{
    // En lista med garage.
    internal class GarageList
    {
        private Garage[] _garages = [];

        // Det aktuella garaget som används.
        public Garage? Current { get; private set; }

        // Antal garage i listan.
        public int Count => _garages.Length;

        // Max antal garage i listan.
        public static int MaxCount => 10000;

        // Lista alla garage.
        public void ListGarages()
        {
            for (int i = 0; i < _garages.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {_garages[i].Name}");
            }
        }

        // Hitta garage med namn.
        // Returnerar null om inget garage med det namnet finns.
        private Garage? FindGarage(string name)
        {
            return Array.Find(_garages, g => g.Name == name);
        }

        // Generera ett namnförslag för ett nytt garage.
        // Namnet är unikt inom garagelistan.
        private string GenerateSuggestedGarageName()
        {
            string name;
            int counter = 1;
            do
            {
                name = $"Garage {counter}";
                counter++;
            } while (FindGarage(name) != null && counter <= MaxCount);
            return name;
        }

        // Lägg till garage via användarinput.
        // Returnerar true om ett garage skapades, annars false.
        public bool AddGarage()
        {
            if (_garages.Length >= MaxCount)
            {
                Console.WriteLine("Max antal garage uppnått. Kan inte lägga till fler garage.");
                return false;
            }

            string name;
            string suggestedName = GenerateSuggestedGarageName();
            while (true)
            {
                Console.Write($"Ange garagets namn ({suggestedName}): ");
                name = Console.ReadLine()?.Trim() ?? throw new IOException();
                if (name == "")
                {
                    Console.WriteLine($"Använder det föreslagna namnet: {suggestedName}.");
                    name = suggestedName;
                    break;
                }
                else if (FindGarage(name) != null)
                {
                    Console.WriteLine("Ett garage med det namnet finns redan.");
                }
                else
                {
                    break;
                }
            }

            int capacity;
            while (true)
            {
                int defaultCapacity = 100;
                Console.Write($"Ange antal parkeringsplatser ({defaultCapacity}): ");
                string inp = Console.ReadLine()?.Trim() ?? throw new IOException();
                if (inp == "")
                {
                    Console.WriteLine($"Använder det föreslagna värdet: {defaultCapacity}.");
                    capacity = defaultCapacity;
                    break;
                }
                else if (!int.TryParse(inp, out capacity))
                {
                    Console.WriteLine("Ogiltigt värde. Ange ett giltigt heltal.");
                }
                else if (capacity < 1 || capacity > Garage.MaxCapacity)
                {
                    Console.WriteLine($"Antalet platser måste vara mellan 1 och {Garage.MaxCapacity}.");
                }
                else
                {
                    break;
                }
            }

            Garage garage = new(capacity) { Name = name };
            Array.Resize(ref _garages, _garages.Length + 1);
            _garages[_garages.Length - 1] = garage;
            Current = garage;

            Console.WriteLine();
            Console.WriteLine($"Du har skapat ett nytt garage med namnet {garage.Name}.");
            Console.WriteLine($"Kapaciteten är {garage.Capacity}.");
            Console.WriteLine("Det nya garaget väljs nu som aktuellt garage.");

            Console.WriteLine("\nOm du vill kan du lägga till slumpmässiga fordon till det nya garaget.");
            garage.AddRandomVehicles();

            Console.WriteLine($"Antalet lediga platser är {garage.FreeCapacity}.");

            return true;
        }

        // Ta bort garage via index.
        private void RemoveGarage(int n)
        {
            if (n < 0 || n >= _garages.Length)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = n; i < _garages.Length - 1; i++)
            {
                _garages[i] = _garages[i + 1];
            }
            Array.Resize(ref _garages, _garages.Length - 1);
        }

        // Ta bort garage via användarinput
        public void RemoveGarage()
        {
            if (this.Count == 0)
            {
                Console.WriteLine("Det finns ännu inget garage tillagt. Avbryter.");
                return;
            }

            Console.WriteLine("Tillgängliga garage:");
            ListGarages();

            while (true)
            {
                Console.Write("Ange numret på garaget att ta bort: ");
                string s = Console.ReadLine()?.Trim() ?? throw new IOException();

                if (s == "")
                {
                    Console.WriteLine("Avbryter. Inget garage togs bort.");
                    return;
                }

                if (!int.TryParse(s, out int n))
                {
                    Console.WriteLine("Ogiltig inmatning. Du måste ange ett giltigt garagenummer.");
                }
                else if (n < 1 || n > _garages.Length)
                {
                    Console.WriteLine("Ogiltigt nummer. Det finns inget garage med det numret.");
                }
                else
                {
                    string name = _garages[n - 1].Name;
                    RemoveGarage(n - 1);
                    Console.WriteLine($"Garaget {name} togs bort.");
                    return;
                }
            }
        }

        // Välj garage via användarinput.
        public bool SelectGarage()
        {
            if (this.Count == 0)
            {
                Console.WriteLine("Det finns ännu inget garage tillagt. Avbryter.");
                return false;
            }

            Console.WriteLine("Tillgängliga garage:");
            ListGarages();

            while (true)
            {
                Console.Write("Ange numret på garaget att välja: ");
                string inp = Console.ReadLine()?.Trim() ?? throw new IOException();
                if (inp == "")
                {
                    Console.WriteLine("Avbryter. Inget garage valdes.");
                    return false;
                }

                if (!int.TryParse(inp, out int n))
                {
                    Console.WriteLine("Ogiltig inmatning. Du måste ange ett giltigt garagenummer.");
                }
                else if (n < 1 || n > _garages.Length)
                {
                    Console.WriteLine("Ogiltigt nummer. Det finns inget garage med det numret.");
                }
                else
                {
                    try
                    {
                        SelectGarage(n - 1);
                        Console.WriteLine("Du valde garaget " + Current!.Name + ".");
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Det gick inte att välja det angivna garaget. " + e.Message);
                        return false;
                    }
                }
            }
        }

        // Välj garage via index
        private void SelectGarage(int index)
        {
            if (index < 0 || index >= _garages.Length)
            {
                throw new IndexOutOfRangeException();
            }
            Current = _garages[index];
        }

        // Ta bort alla garage
        public void Clear()
        {
            Current = null;
            _garages = [];
        }

        // Ta bort alla garage via användarinput
        public void AskAndClear()
        {
            Console.WriteLine("Är du säker på att du vill ta bort alla garage? Skriv i så fall: ja");
            string inp = Console.ReadLine()?.Trim() ?? throw new IOException();
            if (inp == "ja")
            {
                Clear();
                Console.WriteLine("Alla garage togs bort.");
            }
            else
            {
                Console.WriteLine("Avbryter. Inga garage togs bort.");
            }
        }
    }
}
