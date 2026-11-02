using GarageApp3.GarageLibrary.Enums;
using System.Text;

namespace GarageApp3.GarageLibrary.Vehicles
{
    public class Truck(string registrationNumber, float length, int numberOfSeats, int numberOfEngines, float cylinderVolume, FuelType fuelType, float maxLoad) : MotorVehicle(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType)
    {
        public float MaxLoad
        {
            get
            {
                return maxLoad;
            }
            set
            {
                if (value <= 0.0f)
                {
                    throw new ArgumentException("MaxLoad must be greater than zero.");
                }

                maxLoad = value;
            }
        }

        public override string ToString()
        {
            return new StringBuilder().Append("Truck").Append($";{base.ToString()}").Append($";{maxLoad}").ToString();
        }

        public override void Write(TextWriter textWriter)
        {
            textWriter.WriteLine($"Vehicle type: {GetType().Name}");

            base.Write(textWriter);

            textWriter.WriteLine($"Max load: {maxLoad}");
        }
    }
}
