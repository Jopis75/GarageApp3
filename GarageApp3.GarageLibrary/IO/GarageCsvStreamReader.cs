using GarageApp3.GarageLibrary.Extensions;
using GarageApp3.GarageLibrary.Vehicles;

namespace GarageApp3.GarageLibrary.IO
{
    public static class GarageCsvStreamReader
    {
        public static IList<Vehicle> Read(string path)
        {
            using var streamReader = new StreamReader(path);

            var lines = streamReader.ReadAllLines();

            var vehicles = VehicleCsvLineParser.Parse(lines);

            return vehicles;
        }
    }
}
