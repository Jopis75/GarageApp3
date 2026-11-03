using GarageApp3.GarageLibrary.Dtos;
using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.Extensions;
using GarageApp3.GarageLibrary.Validators;
using GarageApp3.GarageLibrary.Vehicles;

namespace GarageApp3.GarageLibrary.IO
{
    public static class VehicleCsvLineParser
    {
        public static IList<Vehicle> Parse(string[] lines)
        {
            List<Vehicle> vehicles = [];

            foreach (var line in lines)
            {
                var vehicle = Parse(line);

                vehicles.Add(vehicle);
            }

            return vehicles;
        }

        private static Airplane ParseAirplane(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("Invalid number of fields in CSV line when parsing Airplane.");
            }

            if (fields[0].Equals("Airplane") == false)
            {
                throw new ArgumentException("Invalid vehicle type value in CSV line when parsing Airplane.");
            }

            var vehicleData = ParseVehicleData(fields, VehicleType.Airplane);

            var motorVehicleData = ParseMotorVehicleData(fields, VehicleType.Airplane);

            if (float.TryParse(fields[7], out var maxTrust) == false)
            {
                throw new ArgumentException("Invalid maximum trust value in CSV line when parsing Airplane.");
            }

            return new Airplane(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, maxTrust);
        }

        private static Bicycle ParseBicycle(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 5)
            {
                throw new ArgumentException("Invalid number of fields in CSV line when parsing Bicycle.");
            }

            if (fields[0].Equals("Bicycle") == false)
            {
                throw new ArgumentException("Invalid vehicle type value in CSV line when parsing Bicycle.");
            }

            var vehicleData = ParseVehicleData(fields, VehicleType.Bicycle);

            if (int.TryParse(fields[4], out var numberOfGears) == false || !(numberOfGears > 0))
            {
                throw new ArgumentException("Invalid number of gears value in CSV line when parsing Bicycle.");
            }

            return new Bicycle(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, numberOfGears);
        }

        private static Boat ParseBoat(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("Invalid number of fields in CSV line when parsing Boat.");
            }

            if (fields[0].Equals("Boat") == false)
            {
                throw new ArgumentException("Invalid vehicle type value in CSV line when parsing Boat.");
            }

            var vehicleData = ParseVehicleData(fields, VehicleType.Boat);

            var motorVehicleData = ParseMotorVehicleData(fields, VehicleType.Boat);

            if (float.TryParse(fields[7], out var draft) == false)
            {
                throw new ArgumentException("Invalid draft value in CSV line when parsing Boat.");
            }

            return new Boat(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, draft);
        }

        private static Bus ParseBus(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("Invalid number of fields in CSV line when parsing Bus.");
            }

            if (fields[0].Equals("Bus") == false)
            {
                throw new ArgumentException("Invalid vehicle type value in CSV line when parsing Bus.");
            }

            var vehicleData = ParseVehicleData(fields, VehicleType.Bus);

            var motorVehicleData = ParseMotorVehicleData(fields, VehicleType.Bus);

            if (int.TryParse(fields[7], out var numberOfPassengerSeats) == false)
            {
                throw new ArgumentException("Invalid number of passenger seats value in CSV line when parsing Bus.");
            }

            return new Bus(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, numberOfPassengerSeats);
        }

        private static Car ParseCar(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("Invalid number of fields in CSV line when parsing Car.");
            }

            if (fields[0].Equals("Car") == false)
            {
                throw new ArgumentException("Invalid vehicle type value in CSV line when parsing Car.");
            }

            var vehicleData = ParseVehicleData(fields, VehicleType.Car);

            var motorVehicleData = ParseMotorVehicleData(fields, VehicleType.Car);

            if (float.TryParse(fields[7], out var maxTrailerWeight) == false)
            {
                throw new ArgumentException("Invalid maximum trailer weight value in CSV line when parsing Car.");
            }

            return new Car(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, maxTrailerWeight);
        }

        public static Vehicle Parse(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (Enum.TryParse<VehicleType>(fields[0], out var vehicleType) == false)
            {
                throw new ArgumentException("Invalid vehicle type value in CSV line.");
            }

            return vehicleType switch
            {
                VehicleType.Bicycle => ParseBicycle(line),
                VehicleType.Motorcycle => ParseMotorcycle(line),
                VehicleType.Car => ParseCar(line),
                VehicleType.Truck => ParseTruck(line),
                VehicleType.Bus => ParseBus(line),
                VehicleType.Boat => ParseBoat(line),
                VehicleType.Airplane => ParseAirplane(line),
                _ => throw new ArgumentException("Invalid vehicle type value in CSV line.")
            };
        }

        private static Motorcycle ParseMotorcycle(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("Invalid number of fields in CSV line when parsing Motorcycle.");
            }

            if (fields[0].Equals("Motorcycle") == false)
            {
                throw new ArgumentException("Invalid vehicle type value in CSV line when parsing Motorcycle.");
            }

            var vehicleData = ParseVehicleData(fields, VehicleType.Motorcycle);

            var motorVehicleData = ParseMotorVehicleData(fields, VehicleType.Motorcycle);

            if (float.TryParse(fields[7], out var seatHeight) == false)
            {
                throw new ArgumentException("Invalid seat height value in CSV line when parsing Motorcycle.");
            }

            return new Motorcycle(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, seatHeight);
        }

        private static MotorVehicleDto ParseMotorVehicleData(string[] fields, VehicleType vehicleType)
        {
            if (int.TryParse(fields[4], out var numberOfEngines) == false)
            {
                throw new ArgumentException($"Invalid number of engines value in CSV line when parsing {vehicleType}.");
            }

            if (float.TryParse(fields[5], out var cylinderVolume) == false)
            {
                throw new ArgumentException($"Invalid cylinder volume value in CSV line when parsing {vehicleType}.");
            }

            if (Enum.TryParse<FuelType>(fields[6], out var fuelType) == false)
            {
                throw new ArgumentException($"Invalid fuel type value in CSV line when parsing {vehicleType}.");
            }

            return new MotorVehicleDto(numberOfEngines, cylinderVolume, fuelType);
        }

        private static Truck ParseTruck(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("Invalid number of fields in CSV line when parsing Truck.");
            }

            if (fields[0].Equals("Truck") == false)
            {
                throw new ArgumentException("Invalid vehicle type value in CSV line when parsing Truck.");
            }

            var vehicleData = ParseVehicleData(fields, VehicleType.Truck);

            var motorVehicleData = ParseMotorVehicleData(fields, VehicleType.Truck);

            if (float.TryParse(fields[7], out var maxLoad) == false)
            {
                throw new ArgumentException("Invalid maximum load value in CSV line when parsing Truck.");
            }

            return new Truck(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, maxLoad);
        }

        private static VehicleDto ParseVehicleData(string[] fields, VehicleType vehicleType)
        {
            string registrationNumber;

            if (RegistrationNumberValidator.Validate(fields[1]) == false)
            {
                throw new ArgumentException($"Invalid registration number in CSV line when parsing {vehicleType}.");
            }

            registrationNumber = fields[1];

            if (float.TryParse(fields[2], out var length) == false)
            {
                throw new ArgumentException($"Invalid length value in CSV line when parsing {vehicleType}.");
            }

            if (int.TryParse(fields[3], out var numberOfSeats) == false)
            {
                throw new ArgumentException($"Invalid number of seats value in CSV line when parsing {vehicleType}.");
            }

            return new VehicleDto(registrationNumber, length, numberOfSeats);
        }
    }
}
