using GarageApp3.GarageLibrary;

namespace GarageApp3.Interfaces
{
    public interface IGarageHandler<TVehicle>
        where TVehicle : Vehicle
    {
        void AddVehicle();

        void EnterGarage();

        void ExitGarage();

        void FindVehicles();

        void InitGarage(IList<TVehicle> vehicles);

        void ListVehicleTypes();

        void ListVehicles();

        void ReadVehicles();

        void RemoveVehicle();

        void WriteVehicles();
    }
}
