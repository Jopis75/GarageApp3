using GarageApp3.GarageLibrary.Enums;

namespace GarageApp3.GarageLibrary.Dtos
{
    public record MotorVehicleDto(int NumberOfEngines, float CylinderVolume, FuelType FuelType);
}
