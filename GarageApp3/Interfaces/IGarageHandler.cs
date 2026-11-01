using GarageApp3.GarageLibrary;

namespace GarageApp3.Interfaces
{
    public interface IGarageHandler<TVehicle>
        where TVehicle : Vehicle
    {
        void HandleAddVehicle();

        void HandleCreateGarage(IList<TVehicle> vehicles);

        void HandleEnterGarage();

        void HandleExitGarage();

        void HandleFindVehicles();

        void HandleListVehicleTypes();

        void HandleListVehicles();

        void HandleReadVehicles();

        void HandleRemoveVehicle();

        void HandleWriteVehicles();
    }
}
