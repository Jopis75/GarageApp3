using GarageApp3.GarageLibrary.Interfaces;
using System.Text;

namespace GarageApp3.GarageLibrary.Vehicles
{
    public abstract class Vehicle(string registrationNumber, float length, int numberOfSeats) : IVehicle
    {
        public float Length
        {
            get
            {
                return length;
            }
            set
            {
                if (value <= 0.0f)
                {
                    throw new ArgumentException("Length must be greater than zero.");
                }

                length = value;
            }
        }

        public int NumberOfSeats
        {
            get
            {
                return numberOfSeats;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("NumberOfSeats must be greater than zero.");
                }

                numberOfSeats = value;
            }
        }

        public string RegistrationNumber
        {
            get
            {
                return registrationNumber;
            }
        }

        public override string ToString()
        {
            return new StringBuilder().Append(registrationNumber).Append($";{length}").Append($";{numberOfSeats}").ToString();
        }

        public virtual void Write(TextWriter textWriter)
        {
            textWriter.WriteLine($"Registration number: {registrationNumber}");
            textWriter.WriteLine($"Length: {length}");
            textWriter.WriteLine($"Number of seats: {numberOfSeats}");
        }
    }
}
