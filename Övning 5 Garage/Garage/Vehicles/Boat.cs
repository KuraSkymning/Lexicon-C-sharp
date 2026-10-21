using System;

namespace Garage.Vehicles
{
    // Representerar en båt.
    // Förutom basegenskaperna har en båt även egenskapen längd.
    internal class Boat : Vehicle, IVehicleWithLength
    {
        public override VehicleType VType { get; } = VehicleType.Boat;

        public override int? NumberOfWheels { get; protected set; } = 0;

        public double? Length
        {
            get; set
            {
                field = value == null || value >= 1.5 && value <= 30 ?
                        value :
                        throw new ArgumentOutOfRangeException(nameof(value),
                        "Längden på en båt måste vara mellan 1,5 och 30 meter.");
            }
        }

        public override string[] Properties { get; } = [.. BaseProperties, "längd"];

        public Boat() : base() { }

        public Boat(string registrationNumber) : base(registrationNumber) { }

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
                    "längd" => Length == null ? "okänd" : $"{Length}",
                    _ => throw new ArgumentException("Egenskapen finns inte hos fordonet."),
                };
            }
        }

        protected override void RandomizeValues()
        {
            base.RandomizeValues();
            Length = 2 + 0.5 * Random.Next(40);
        }

        public override void CustomizeValues()
        {
            base.CustomizeValues();
            CustomizeLength(this);
        }

        // Generera ett slumpmässigt registreringsnummer.
        public override string GetRandomRegistrationNumber()
        {
            string regNr = "B-";
            regNr += Random.Next(10);
            regNr += Random.Next(10);
            regNr += Random.Next(10);
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
            return res;
        }
    }
}
