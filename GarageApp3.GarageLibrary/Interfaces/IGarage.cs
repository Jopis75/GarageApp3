using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.Vehicles;

namespace GarageApp3.GarageLibrary.Interfaces
{
    public interface IGarage<TVehicle> : IEnumerable<TVehicle>
        where TVehicle : Vehicle
    {
        bool IsEmpty { get; }

        bool IsFull { get; }

        int NumberOfParkingSpaces { get; }

        int NumberOfVehicles { get; }

        void AddVehicle(TVehicle vehicle);

        int CountVehicleType(VehicleType vehicleType);

        IList<TVehicle> FindVehiclesByNumberOfSeats(int numberOfSeats);

        IList<TVehicle> FindVehiclesByVehicleType(VehicleType vehicleType);

        IList<TVehicle> FindVehiclesByVehicleTypeAndFuelType(VehicleType vehicleType, FuelType fuelType);

        TVehicle FindVehicleByRegistrationNumber(string registrationNumber);

        string StatusString();

        IList<TVehicle> RemoveAll();

        TVehicle RemoveVehicleByRegistrationNumber(string registrationNumber);

        bool VehicleExists(string registrationNumber);

        void WriteVehicles(TextWriter textWriter);

        void WriteVehicleTypes(TextWriter textWriter);
    }
}
