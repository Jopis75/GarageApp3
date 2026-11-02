namespace GarageApp3.GarageLibrary.Interfaces
{
    public interface IVehicle
    {
        float Length { get; }

        int NumberOfSeats { get; }

        string RegistrationNumber { get; }

        void Write(TextWriter textWriter);
    }
}
