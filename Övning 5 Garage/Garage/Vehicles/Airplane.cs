using System;
using System.IO;

namespace Garage.Vehicles
{
    // Representerar ett flygplan.
    // Förutom basegenskaperna har ett flygplan även egenskaperna antal säten, antal motorer och längd.
    internal class Airplane : Vehicle, IVehicleWithSeats, IVehicleWithLength
    {
        public override VehicleType VType { get; } = VehicleType.Airplane;

        public override int? NumberOfWheels
        {
            get; protected set
            {
                field = value == null || value >= 0 && value <= 8 ?
                        value :
                        throw new ArgumentOutOfRangeException(nameof(value),
                        "Antalet hjul på ett flygplan måste vara mellan 0 och 8.");
            }
        }

        public int? NumberOfSeats
        {
            get; set
            {
                field = value == null || value >= 1 && value <= 100 ?
                        value :
                        throw new ArgumentOutOfRangeException(nameof(value),
                        "Antalet säten i ett flygplan måste vara mellan 1 och 100.");
            }
        }

        public int? NumberOfEngines
        {
            get; protected set
            {
                field = value == null || value >= 1 && value <= 4 ?
                        value :
                        throw new ArgumentOutOfRangeException(nameof(value),
                        "Antalet motorer på ett flygplan måste vara mellan 1 och 4.");
            }
        }

        public double? Length
        {
            get; set
            {
                field = value == null || value >= 4 && value <= 19 ?
                        value :
                        throw new ArgumentOutOfRangeException(nameof(value),
                        "Längden på ett flygplan måste vara mellan 4 och 20 meter.");
            }
        }

        public override string[] Properties { get; } =
            [.. BaseProperties, "antal säten", "antal motorer", "längd"];

        public Airplane() : base() { }

        public Airplane(string registrationNumber) : base(registrationNumber) { }

        public override string GetPropertyValue(string property)
        {
            try
            {
                return base.GetPropertyValue(property);
            }
            catch (ArgumentException)
            {
                return property switch
                {
                    "antal motorer" => NumberOfEngines == null ? "okänt" : $"{NumberOfEngines}",
                    "antal säten" => NumberOfSeats == null ? "okänt" : $"{NumberOfSeats}",
                    "längd" => Length == null ? "okänd" : $"{Length}",
                    _ => throw new ArgumentException("Egenskapen finns inte hos fordonet."),
                };
            }
        }

        protected override void RandomizeValues()
        {
            base.RandomizeValues();
            NumberOfWheels = Random.Next(0, 9);
            NumberOfEngines = Random.Next(1, 3);
            NumberOfSeats = Random.Next(1, 100);
            Length = 4 + 0.5 * Random.Next(30);
        }

        public override void CustomizeValues()
        {
            base.CustomizeValues();
            CustomizeLength(this);
            CustomizeNumberOfSeats(this);

            while (true)
            {
                Console.Write("Ange antal motorer: ");
                string s = Console.ReadLine()?.Trim() ?? throw new IOException();
                if (s == "")
                {
                    break;
                }
                else if (int.TryParse(s, out int n))
                {
                    try
                    {
                        NumberOfEngines = n;
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

        // Generera ett slumpmässigt registreringsnummer.
        public override string GetRandomRegistrationNumber()
        {
            string regNr = "F-";
            regNr += (char)('A' + Random.Next(26));
            regNr += (char)('A' + Random.Next(26));
            regNr += Random.Next(10);
            regNr += Random.Next(10);
            regNr += Random.Next(10);
            regNr += Random.Next(10);
            return NormalizeRegistrationNumber(regNr);
        }

        public override string ToString()
        {
            string res = "";
            res += "Registreringsnummer: " + (RegistrationNumber ?? "okänt");
            res += "\nFordonstyp: " + GetPropertyValue("fordonstyp");
            res += "\nFärg: " + GetPropertyValue("färg");
            res += "\nAntal hjul: " + GetPropertyValue("antal hjul");
            res += "\nLängd: " + GetPropertyValue("längd") + (Length == null ? "" : " meter");
            res += "\nAntal säten: " + GetPropertyValue("antal säten");
            res += "\nAntal motorer: " + GetPropertyValue("antal motorer");
            return res;
        }
    }
}
