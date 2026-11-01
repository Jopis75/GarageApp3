using GarageApp3.GarageLibrary.Enums;
using System.Text;

namespace GarageApp3.GarageLibrary
{
    public class Car(string registrationNumber, float length, int numberOfSeats, int numberOfEngines, float cylinderVolume, FuelType fuelType, float maxTrailerWeight) : MotorVehicle(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType)
    {
        public float MaxTrailerWeight
        {
            get
            {
                return maxTrailerWeight;
            }
            set
            {
                if (value <= 0.0f)
                {
                    throw new ArgumentException("MaxTrailerWeight must be greater than zero.");
                }

                maxTrailerWeight = value;
            }
        }

        public override string ToString()
        {
            return new StringBuilder().Append("Car").Append($";{base.ToString()}").Append($";{maxTrailerWeight}").ToString();
        }

        public override void Write(TextWriter textWriter)
        {
            textWriter.WriteLine($"Vehicle type: {GetType().Name}");

            base.Write(textWriter);

            textWriter.WriteLine($"Max trailer weight: {maxTrailerWeight}");
        }
    }
}
