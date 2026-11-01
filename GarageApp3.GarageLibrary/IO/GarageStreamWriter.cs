using GarageApp3.GarageLibrary.Interfaces;

namespace GarageApp3.GarageLibrary.IO
{
    public static class GarageStreamWriter<TVehicle>
        where TVehicle : Vehicle
    {
        public static int Write(StreamWriter streamWriter, IGarage<TVehicle> garage)
        {
            var count = 0;

            foreach (var vehicle in garage.Vehicles)
            {
                var x = vehicle.ToString();
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
