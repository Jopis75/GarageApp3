using GarageApp3.GarageLibrary.Enums;

namespace GarageApp3.GarageLibrary.Interfaces
{
    public interface IGarage<TVehicle>
    {
        bool IsEmpty { get; }

        bool IsFull { get; }

        int NumberOfParkingSpaces { get; }

        int NumberOfVehicles { get; }

        IReadOnlyList<TVehicle> Vehicles { get; }

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

        void WriteVehicleTypeCount(VehicleType vehicleType);
    }
}
