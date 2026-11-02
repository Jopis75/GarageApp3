using GarageApp3.GarageLibrary.Enums;
using System.Text;

namespace GarageApp3.GarageLibrary.Vehicles
{
    public class Motorcycle(string registrationNumber, float length, int numberOfSeats, int numberOfEngines, float cylinderVolume, FuelType fuelType, float seatHeight) : MotorVehicle(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType)
    {
        public float SeatHeight
        {
            get
            {
                return seatHeight;
            }
            set
            {
                if (value <= 0.0f)
                {
                    throw new ArgumentException("SeatHeight must be greater than zero.");
                }

                seatHeight = value;
            }
        }

        public override string ToString()
        {
            return new StringBuilder().Append("Motorcycle").Append($";{base.ToString()}").Append($";{seatHeight}").ToString();
        }

        public override void Write(TextWriter textWriter)
        {
            textWriter.WriteLine($"Vehicle type: {GetType().Name}");

            base.Write(textWriter);

            textWriter.WriteLine($"Seat height: {seatHeight}");
        }
    }
}
