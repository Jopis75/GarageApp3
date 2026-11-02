using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.Exceptions;
using GarageApp3.GarageLibrary.Interfaces;
using System.Collections;

namespace GarageApp3.GarageLibrary
{
    public class Garage<TVehicle>(int numberOfParkingSpaces) : IGarage<TVehicle>, IEnumerable<TVehicle>
        where TVehicle : Vehicle
    {
        private readonly List<TVehicle> _vehicles = [];

        public bool IsEmpty => NumberOfVehicles == 0;

        public bool IsFull => NumberOfVehicles == numberOfParkingSpaces;

        public int NumberOfParkingSpaces => numberOfParkingSpaces;

        public int NumberOfVehicles => _vehicles.Count;

        public void AddVehicle(TVehicle vehicle)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("The garage is full.");
            }

            _vehicles.Add(vehicle);
        }

        public int CountVehicleType(VehicleType vehicleType)
        {
            var count = _vehicles.Count(v => v.GetType().Name.Equals(vehicleType.ToString()));

            return count;
        }

        public IList<TVehicle> FindVehiclesByNumberOfSeats(int numberOfSeats)
        {
            var vehicles = _vehicles
                .Where(v => v.NumberOfSeats == numberOfSeats)
                .ToList();

            return vehicles;
        }

        public TVehicle FindVehicleByRegistrationNumber(string registrationNumber)
        {
            var vehicle = _vehicles
                .Where(v => v.RegistrationNumber.Equals(registrationNumber, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();

            if (vehicle == null)
            {
                throw new VehicleNotFoundException(registrationNumber);
            }

            return vehicle;
        }

        public IList<TVehicle> FindVehiclesByVehicleType(VehicleType vehicleType)
        {
            var vehicles = _vehicles
                .Where(v => v.GetType().Name.Equals(vehicleType.ToString()))
                .ToList();

            return vehicles;
        }

        public IList<TVehicle> FindVehiclesByVehicleTypeAndFuelType(VehicleType vehicleType, FuelType fuelType)
        {
            var vehicles = _vehicles
                .Where(v => v.GetType().Name.Equals(vehicleType.ToString())
                    && v is MotorVehicle
                    && (v as MotorVehicle)!.FuelType == fuelType)
                .ToList();

            return vehicles;
        }

        public IEnumerator<TVehicle> GetEnumerator()
        {
            foreach (var vehicle in _vehicles)
            {
                yield return vehicle;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string StatusString()
        {
            var statusString = $"The garage has a total of {numberOfParkingSpaces} parking spaces.";

            if (IsEmpty)
            {
                statusString += " The garage is empty.";
            }
            else if (IsFull)
            {
                statusString += " The garage is full.";
            }
            else
            {
                statusString += $" There are currently {_vehicles.Count}" + (_vehicles.Count == 1 ? " vehicle" : " vehicles") + " in the garage.";
            }

            return statusString;
        }

        public IList<TVehicle> RemoveAll()
        {
            var vehilces = _vehicles.ToList();

            _vehicles.RemoveAll(_ => true);

            return vehilces;
        }

        public TVehicle RemoveVehicleByRegistrationNumber(string registrationNumber)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The garage is empty.");
            }

            var vehicle = FindVehicleByRegistrationNumber(registrationNumber);

            _vehicles.Remove(vehicle);

            return vehicle;
        }

        public bool VehicleExists(string registrationNumber)
        {
            var vehicle = _vehicles
                .Where(v => v.RegistrationNumber.Equals(registrationNumber, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();

            return vehicle != null;
        }

        public void WriteVehicles(TextWriter textWriter)
        {
            foreach (var vehicle in _vehicles)
            {
                vehicle.Write(textWriter);
                textWriter.WriteLine();
            }
        }

        public void WriteVehicleTypes(TextWriter textWriter)
        {
            var vehicleTypes = Enum.GetValues<VehicleType>().Cast<VehicleType>();

            foreach (var vehicleType in vehicleTypes)
            {
                var count = CountVehicleType(vehicleType);

                textWriter.WriteLine($"Vehicle type: {vehicleType}");
                textWriter.WriteLine($"Count: {count}");
                textWriter.WriteLine();
            }
        }
    }
}
