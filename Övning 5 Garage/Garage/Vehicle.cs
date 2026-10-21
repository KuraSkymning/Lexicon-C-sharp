using Garage.Vehicles;
using System;
using System.IO;

namespace Garage
{
    // Fordonstyper.
    public enum VehicleType
    {
        Car,
        Bus,
        Boat,
        Airplane,
        Motorcycle,
    }

    // Bränsletyper.
    public enum FuelType
    {
        Gasoline,
        Diesel,
        Electricity,
    }

    // Basklass för fordon.
    abstract class Vehicle
    {
        // Fordonstyp.
        public abstract VehicleType VType { get; }

        // Fordonets registreringsnummer.
        public virtual string RegistrationNumber
        {
            get; protected set
            {
                field = value == "" ?
                    throw new ArgumentException("Registreringsnumret får inte vara tomt.") :
                    value;
            }
        }

        // Färg på fordonet.
        public string? Colour
        {
            get; set
            {
                field = value == "" ? null : value?.ToLower();
            }
        }

        // Antal hjul på fordonet.
        public virtual int? NumberOfWheels
        {
            get; protected set
            {
                field = value < 0 ?
                    throw new ArgumentException("Antalet hjul kan inte vara ett negativt tal.") :
                    value;
            }
        }

        // Tillgängliga egenskaper på fordonet.
        public virtual string[] Properties { get; } = BaseProperties;

        // Hjälpproperty som underklasser kan använda för att initiera Properties.
        protected static string[] BaseProperties { get; } = ["fordonstyp", "färg", "antal hjul"];

        // Hjälpproperty för att generera slumptal.
        protected static Random Random { get; } = new();

        public Vehicle()
        {
            RegistrationNumber = GetRandomRegistrationNumber();
        }

        public Vehicle(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }

        // Hämta ett strängvärde för en egenskap.
        public virtual string GetPropertyValue(string property)
        {
            return property switch
            {
                "fordonstyp" => GetVehicleHumanName(VType),
                "färg" => Colour ?? "okänd",
                "antal hjul" => NumberOfWheels == null ? "okänt" : $"{NumberOfWheels}",
                _ => throw new ArgumentException("Egenskapen finns inte hos fordonet."),
            };
        }

        // Kontrollera om fordonets egenskap property matchar något av värdena i values.
        public bool Matches(string property, string[] values)
        {
            try
            {
                string value = GetPropertyValue(property);
                return Array.IndexOf(values, value) >= 0;
            }
            catch
            {
                return false;
            }
        }

        // Kontrollera om fordonet matchar värdena i properties.
        // Varje nyckel i properties anger en fordonsegenskap,
        // och motsvarande värden anger värden att matcha mot.
        public bool Matches(StringDictionary properties)
        {
            string[] keys = properties.Keys();
            for (int i = 0; i < keys.Length; i++)
            {
                string property = keys[i];
                string[] values = properties.Get(property)!.Values();
                if (!Matches(property, values))
                {
                    return false;
                }
            }
            return true;
        }

        // Lägg till slumpmässiga värden för fordonets egenskaper.
        protected virtual void RandomizeValues()
        {
            Colour = GetRandomColour();
        }

        // Ändra värdena för fordonets egenskaper genom användarinput.
        public virtual void CustomizeValues()
        {
            Console.Write("Ange färg: ");
            string inp = Console.ReadLine()?.Trim().ToLower() ?? throw new IOException();
            Colour = inp == "" ? null : inp;
        }

        // Normalisera ett registreringsnummer.
        public static string NormalizeRegistrationNumber(string registrationNumber)
        {
            return registrationNumber.ToUpper().Replace(" ", "");
        }

        // Skapa ett fordon av en viss typ med ett visst registreringsnummer.
        public static Vehicle CreateVehicle(VehicleType type, string registrationNumber)
        {
            string regNr = NormalizeRegistrationNumber(registrationNumber);
            Vehicle vehicle = type switch
            {
                VehicleType.Motorcycle => new Motorcycle(regNr),
                VehicleType.Airplane => new Airplane(regNr),
                VehicleType.Car => new Car(regNr),
                VehicleType.Bus => new Bus(regNr),
                _ => new Boat(regNr),
            };
            return vehicle;
        }

        // Skapa ett slumpmässigt fordon.
        public static Vehicle CreateRandomVehicle()
        {
            var types = Enum.GetValues<VehicleType>();
            Vehicle vehicle = types[Random.Next(types.Length)] switch
            {
                VehicleType.Motorcycle => new Motorcycle(),
                VehicleType.Airplane => new Airplane(),
                VehicleType.Car => new Car(),
                VehicleType.Bus => new Bus(),
                _ => new Boat(),
            };
            vehicle.RandomizeValues();
            return vehicle;
        }

        // Generera ett slumpmässigt registreringsnummer.
        public virtual string GetRandomRegistrationNumber()
        {
            string regNr = "";
            regNr += (char)('A' + Random.Next(26));
            regNr += (char)('A' + Random.Next(26));
            regNr += (char)('A' + Random.Next(26));
            regNr += Random.Next(10);
            regNr += Random.Next(10);
            regNr += Random.Next(2) == 0 ? Random.Next(10) : (char)('A' + Random.Next(26));
            regNr = regNr.Replace('I', 'J');
            regNr = regNr.Replace('Q', 'U');
            regNr = regNr.Replace('V', 'M');
            if (regNr[^1] == 'O')
            {
                regNr = regNr[..^1] + 'F';
            }
            return NormalizeRegistrationNumber(regNr);
        }

        // Generera en slumpmässig färg.
        public static string GetRandomColour()
        {
            string[] colours = [
                "vit",
                "svart",
                "grå",
                "röd",
                "silver",
                "blå",
                "grön",
                "brun",
                "gul",
                "orange",
                "lila",
                "rosa",
            ];
            return colours[Random.Next(colours.Length)];
        }

        public static string GetVehicleHumanName(VehicleType type)
        {
            return type switch
            {
                VehicleType.Motorcycle => "motorcykel",
                VehicleType.Airplane => "flygplan",
                VehicleType.Car => "bil",
                VehicleType.Bus => "buss",
                VehicleType.Boat => "båt",
                _ => "okänd",
            };
        }

        public static VehicleType? GetVehicleType(string type)
        {
            return type switch
            {
                "motorcykel" => VehicleType.Motorcycle,
                "flygplan" => VehicleType.Airplane,
                "bil" => VehicleType.Car,
                "buss" => VehicleType.Bus,
                "båt" => VehicleType.Boat,
                _ => null,
            };
        }

        public static string GetFuelHumanName(FuelType type)
        {
            return type switch
            {
                FuelType.Gasoline => "bensin",
                FuelType.Diesel => "diesel",
                FuelType.Electricity => "el",
                _ => "okänd",
            };
        }

        // Ange antal hjul via användarinput.
        public static void CustomizeNumberOfWheels(Vehicle vehicle)
        {
            while (true)
            {
                Console.Write("Ange antal hjul: ");
                string s = Console.ReadLine()?.Trim() ?? throw new IOException();
                if (s == "")
                {
                    break;
                }
                else if (int.TryParse(s, out int n))
                {
                    try
                    {
                        vehicle.NumberOfWheels = n;
                        break;
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Ogiltigt värde. " + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt värde. Du måste ange ett heltal.");
                }
            }
        }

        // Ange antal bränsletyp via användarinput.
        public static void CustomizeFuelType(IVehicleWithFuel vehicle)
        {
            while (true)
            {
                Console.Write("Ange bränsletyp: ");
                string s = Console.ReadLine()?.Trim()?.ToLower() ?? throw new IOException();
                if (s == "")
                {
                    break;
                }
                else if (s == "bensin")
                {
                    vehicle.Fueltype = FuelType.Gasoline;
                    break;
                }
                else if (s == "diesel")
                {
                    vehicle.Fueltype = FuelType.Diesel;
                    break;
                }
                else if (s == "el")
                {
                    vehicle.Fueltype = FuelType.Electricity;
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltigt värde. Giltiga värden är: bensin, diesel, el.");
                }
            }

        }

        // Ange antal säten via användarinput.
        public static void CustomizeNumberOfSeats(IVehicleWithSeats vehicle)
        {
            while (true)
            {
                Console.Write("Ange antal säten: ");
                string s = Console.ReadLine()?.Trim() ?? throw new IOException();
                if (s == "")
                {
                    break;
                }
                else if (int.TryParse(s, out int n))
                {
                    try
                    {
                        vehicle.NumberOfSeats = n;
                        break;
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Ogiltigt värde. " + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt värde. Du måste ange ett heltal.");
                }
            }
        }

        // Ange längd via användarinput.
        public static void CustomizeLength(IVehicleWithLength vehicle)
        {
            while (true)
            {
                Console.Write("Ange längd i meter: ");
                string s = Console.ReadLine()?.Trim() ?? throw new IOException();
                if (s == "")
                {
                    break;
                }
                else if (double.TryParse(s, out double n))
                {
                    try
                    {
                        vehicle.Length = n;
                        break;
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Ogiltigt värde. " + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt värde. Du måste ange ett tal.");
                }
            }
        }
    }

    public interface IVehicleWithFuel
    {
        FuelType? Fueltype { get; set; }
    }

    public interface IVehicleWithSeats
    {
        int? NumberOfSeats { get; set; }
    }

    public interface IVehicleWithLength
    {
        double? Length { get; set; }
    }
}
