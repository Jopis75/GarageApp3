namespace GarageApp3.GarageLibrary.Exceptions
{
    public class VehicleNotFoundException(string registrationNumber) : Exception($"Could not find vehicle with registration number '{registrationNumber}'.")
    {
    }
}
