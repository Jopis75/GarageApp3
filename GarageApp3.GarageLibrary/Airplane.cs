using GarageApp3.GarageLibrary.Enums;
using System.Text;

namespace GarageApp3.GarageLibrary
{
    public class Airplane(string registrationNumber, float length, int numberOfSeats, int numberOfEngines, float cylinderVolume, FuelType fuelType, float maxThrust) : MotorVehicle(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType)
    {
        public float MaxThrust
        {
            get
            {
                return maxThrust;
            }
            set
            {
                if (value <= 0.0f)
                {
                    throw new ArgumentException("MaxThrust must be greater than zero.");
                }

                maxThrust = value;
            }
        }

        public override string ToString()
        {
            return new StringBuilder().Append("Airplane").Append($";{base.ToString()}").Append($";{maxThrust}").ToString();
        }

        public override void Write(TextWriter textWriter)
        {
            textWriter.WriteLine($"Vehicle type: {GetType().Name}");

            base.Write(textWriter);

            textWriter.WriteLine($"Max thrust: {maxThrust}");
        }
    }
}
