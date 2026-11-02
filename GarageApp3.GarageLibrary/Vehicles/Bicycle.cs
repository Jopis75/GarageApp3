using System.Text;

namespace GarageApp3.GarageLibrary.Vehicles
{
    public class Bicycle(string registrationNumber, float length, int numberOfSeats, int numberOfGears) : Vehicle(registrationNumber, length, numberOfSeats)
    {
        public int NumberOfGears
        {
            get
            {
                return numberOfGears;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("NumberOfGears must be greater than zero.");
                }

                numberOfGears = value;
            }
        }

        public override string ToString()
        {
            return new StringBuilder().Append("Bicycle").Append($";{base.ToString()}").Append($";{numberOfGears}").ToString();
        }

        public override void Write(TextWriter textWriter)
        {
            textWriter.WriteLine($"Vehicle type: {GetType().Name}");

            base.Write(textWriter);

            textWriter.WriteLine($"Number of gears: {numberOfGears}");
        }
    }
}
