using System;

namespace Garage.Vehicles
{
    // Representerar en bil.
    // Förutom basegenskaperna har en bil även egenskaperna antal säten och bränsletyp.
    internal class Car : Vehicle, IVehicleWithFuel, IVehicleWithSeats
    {
        public override VehicleType VType { get; } = VehicleType.Car;

        public override string RegistrationNumber
        {
            get; protected set
            {
                field = value == "" ?
                    throw new ArgumentException("Registreringsnumret får inte vara tomt.") :
                    value.Length < 2 || value.Length > 7 ?
                    throw new ArgumentException("Registreringsnumret måste innehålla mellan 2 och 7 tecken.") :
                    value;
            }
        }

        public override int? NumberOfWheels { get; protected set; } = 4;

        public int? NumberOfSeats
        {
            get; set
            {
                field = value == null || value >= 1 && value <= 5 ?
                    value :
                    throw new ArgumentOutOfRangeException(nameof(value),
                    "Antalet säten i en bil måste vara mellan 1 och 5.");
            }
        }

        public FuelType? Fueltype { get; set; }

        public override string[] Properties { get; } =
            [.. BaseProperties, "bränsletyp", "antal säten"];

        public Car() : base()
        {
            RegistrationNumber = GetRandomRegistrationNumber();
        }

        public Car(string registrationNumber) : base()
        {
            RegistrationNumber = registrationNumber;
        }

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
                    "bränsletyp" => Fueltype == null ? "okänd" : GetFuelHumanName(Fueltype.Value),
                    "antal säten" => NumberOfSeats == null ? "okänt" : $"{NumberOfSeats}",
                    _ => throw new ArgumentException("Egenskapen finns inte hos fordonet."),
                };
            }
        }

        protected override void RandomizeValues()
        {
            base.RandomizeValues();
            NumberOfSeats = Random.Next(1, 5);
            Fueltype = (FuelType)Random.Next(0, Enum.GetValues(typeof(FuelType)).Length);
        }

        public override void CustomizeValues()
        {
            base.CustomizeValues();
            CustomizeFuelType(this);
            CustomizeNumberOfSeats(this);
        }

        public override string ToString()
        {
            string res = "";
            res += "Registreringsnummer: " + (RegistrationNumber ?? "okänt");
            res += "\nFordonstyp: " + GetPropertyValue("fordonstyp");
            res += "\nFärg: " + GetPropertyValue("färg");
            res += "\nAntal säten: " + GetPropertyValue("antal säten");
            res += "\nBränsletyp: " + GetPropertyValue("bränsletyp");
            res += "\nAntal hjul: " + GetPropertyValue("antal hjul");
            return res;
        }
    }
}
