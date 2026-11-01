using GarageApp3.GarageLibrary.Extensions;

namespace GarageApp3.GarageLibrary.IO
{
    public static class GarageStreamReader
    {
        public static IList<Vehicle> Read(string path)
        {
            using var streamReader = new StreamReader(path);

            var lines = streamReader.ReadAllLines();

            var vehicles = VehicleLineParser.Parse(lines);

            return vehicles;
        }
    }
}
