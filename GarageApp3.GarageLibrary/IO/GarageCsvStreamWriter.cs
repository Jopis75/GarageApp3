using GarageApp3.GarageLibrary.Interfaces;
using GarageApp3.GarageLibrary.Vehicles;

namespace GarageApp3.GarageLibrary.IO
{
    public static class GarageCsvStreamWriter<TVehicle>
        where TVehicle : Vehicle
    {
        public static int Write(StreamWriter streamWriter, IGarage<TVehicle> garage)
        {
            var count = 0;

            foreach (var vehicle in garage)
            {
                streamWriter.WriteLine(vehicle.ToString());
                count++;
            }

            streamWriter.Flush();

            return count;
        }

        public static int Write(string path, IGarage<TVehicle> garage)
        {
            using var streamWriter = new StreamWriter(path);

            return Write(streamWriter, garage);
        }
    }
}
