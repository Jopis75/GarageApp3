using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.Extensions;
using GarageApp3.GarageLibrary.Validators;

namespace GarageApp3.GarageLibrary.IO
{
    public static class VehicleLineParser
    {
        private record VehicleData(string RegistrationNumber, float Length, int NumberOfSeats);

        private record MotorVehicleData(int NumberOfEngines, float CylinderVolume, FuelType FuelType);

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
                throw new ArgumentException("");
            }

            if (fields[0].Equals("Airplane") == false)
            {
                throw new ArgumentException("");
            }

            var vehicleData = ParseVehicleData(fields);

            var motorVehicleData = ParseMotorVehicleData(fields);

            if (float.TryParse(fields[7], out var maxTrust) == false)
            {
                throw new ArgumentException("");
            }

            return new Airplane(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, maxTrust);
        }

        private static Bicycle ParseBicycle(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 5)
            {
                throw new ArgumentException("");
            }

            if (fields[0].Equals("Bicycle") == false)
            {
                throw new ArgumentException("");
            }

            var vehicleData = ParseVehicleData(fields);

            if (int.TryParse(fields[4], out var numberOfGears) == false || !(numberOfGears > 0))
            {
                throw new ArgumentException("");
            }

            return new Bicycle(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, numberOfGears);
        }

        private static Boat ParseBoat(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("");
            }

            if (fields[0].Equals("Boat") == false)
            {
                throw new ArgumentException("");
            }

            var vehicleData = ParseVehicleData(fields);

            var motorVehicleData = ParseMotorVehicleData(fields);

            if (float.TryParse(fields[7], out var draft) == false)
            {
                throw new ArgumentException("");
            }

            return new Boat(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, draft);
        }

        private static Bus ParseBus(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("");
            }

            if (fields[0].Equals("Bus") == false)
            {
                throw new ArgumentException("");
            }

            var vehicleData = ParseVehicleData(fields);

            var motorVehicleData = ParseMotorVehicleData(fields);

            if (int.TryParse(fields[7], out var numberOfPassengerSeats) == false)
            {
                throw new ArgumentException("");
            }

            return new Bus(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, numberOfPassengerSeats);
        }

        private static Car ParseCar(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("");
            }

            if (fields[0].Equals("Car") == false)
            {
                throw new ArgumentException("");
            }

            var vehicleData = ParseVehicleData(fields);

            var motorVehicleData = ParseMotorVehicleData(fields);

            if (float.TryParse(fields[7], out var maxTrailerWeight) == false)
            {
                throw new ArgumentException("");
            }

            return new Car(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, maxTrailerWeight);
        }

        public static Vehicle Parse(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (Enum.TryParse<VehicleType>(fields[0], out var vehicleType) == false)
            {
                throw new ArgumentException("");
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
                _ => throw new ArgumentException("")
            };
        }

        private static Motorcycle ParseMotorcycle(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("");
            }

            if (fields[0].Equals("Motorcycle") == false)
            {
                throw new ArgumentException("");
            }

            var vehicleData = ParseVehicleData(fields);

            var motorVehicleData = ParseMotorVehicleData(fields);

            if (float.TryParse(fields[7], out var seatHeight) == false)
            {
                throw new ArgumentException("");
            }

            return new Motorcycle(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, seatHeight);
        }

        private static MotorVehicleData ParseMotorVehicleData(string[] fields)
        {
            if (int.TryParse(fields[4], out var numberOfEngines) == false)
            {
                throw new ArgumentException("");
            }

            if (float.TryParse(fields[5], out var cylinderVolume) == false)
            {
                throw new ArgumentException("");
            }

            if (Enum.TryParse<FuelType>(fields[6], out var fuelType) == false)
            {
                throw new ArgumentException("");
            }

            return new MotorVehicleData(numberOfEngines, cylinderVolume, fuelType);
        }

        private static Truck ParseTruck(string line)
        {
            string[] fields = line.Split(";", StringSplitOptions.RemoveEmptyEntries).TrimFields();

            if (fields.Length != 8)
            {
                throw new ArgumentException("");
            }

            if (fields[0].Equals("Truck") == false)
            {
                throw new ArgumentException("");
            }

            var vehicleData = ParseVehicleData(fields);

            var motorVehicleData = ParseMotorVehicleData(fields);

            if (float.TryParse(fields[7], out var maxLoad) == false)
            {
                throw new ArgumentException("");
            }

            return new Truck(vehicleData.RegistrationNumber, vehicleData.Length, vehicleData.NumberOfSeats, motorVehicleData.NumberOfEngines, motorVehicleData.CylinderVolume, motorVehicleData.FuelType, maxLoad);
        }

        private static VehicleData ParseVehicleData(string[] fields)
        {
            if (RegistrationNumberValidator.Validate(fields[1]) == false)
            {
                throw new ArgumentException("");
            }

            var registrationNumber = fields[1];

            if (float.TryParse(fields[2], out var length) == false)
            {
                throw new ArgumentException("");
            }

            if (int.TryParse(fields[3], out var numberOfSeats) == false)
            {
                throw new ArgumentException("");
            }

            return new VehicleData(registrationNumber, length, numberOfSeats);
        }
    }
}
