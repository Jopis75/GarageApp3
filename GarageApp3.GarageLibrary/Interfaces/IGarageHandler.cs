namespace GarageApp3.GarageLibrary.Interfaces
{
    public interface IGarageHandler<TVehicle>
        where TVehicle : Vehicle
    {
        void HandleAddVehicle();

        void HandleCreateGarage(IList<TVehicle> vehicles);

        void HandleFindVehicles();

        void HandleListVehicleTypes();

        void HandleListVehicles();

        void HandleReadVehicles();

        void HandleRemoveVehicle();

        void HandleWriteVehicles();
    }
}
