using GarageApp3.GarageLibrary.Enums;
using System.Text;

namespace GarageApp3.GarageLibrary.Vehicles
{
    public class Boat(string registrationNumber, float length, int numberOfSeats, int numberOfEngines, float cylinderVolume, FuelType fuelType, float draft) : MotorVehicle(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType)
    {
        public float Draft
        {
            get
            {
                return draft;
            }
            set
            {
                if (value <= 0.0f)
                {
                    throw new ArgumentException("Draft must be greater than zero.");
                }

                draft = value;
            }
        }

        public override string ToString()
        {
            return new StringBuilder().Append("Boat").Append($";{base.ToString()}").Append($";{draft}").ToString();
        }

        public override void Write(TextWriter textWriter)
        {
            textWriter.WriteLine($"Vehicle type: {GetType().Name}");

            base.Write(textWriter);

            textWriter.WriteLine($"Draft: {draft}");
        }
    }
}
