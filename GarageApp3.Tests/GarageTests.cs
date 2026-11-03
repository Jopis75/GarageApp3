using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.Garages;
using GarageApp3.GarageLibrary.Vehicles;

namespace GarageApp3.Tests
{
    public class GarageTests
    {
        private readonly Vehicle[] _vehicles =
        {
            new Bicycle("XYZ 123", 1.8f, 1, 3),
            new Motorcycle("HAC 26G", 2.1f, 2, 1, 6.0f, FuelType.Gasoline, 60.0f),
            new Car("ABC 12A", 4.0f, 4, 1, 2.0f, FuelType.Gasoline, 750.0f),
            new Truck("RTH 09T", 9.0f, 2, 1, 6.0f, FuelType.Diesel, 40000.0f)
        };

        [Fact]
        public void AddVehicle_ReturnsTrue()
        {
            // Arrange
            var garage = new Garage<Vehicle>(5);

            // Act
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Assert
            Assert.Equal(4, garage.NumberOfVehicles);
        }

        [Fact]
        public void AddVehicle_IsFull_ThrowsInvalidOperationException()
        {
            // Arrange
            var garage = new Garage<Vehicle>(1);
            garage.AddVehicle(_vehicles[0]);

            // Act and Assert
            var exception = Assert.Throws<InvalidOperationException>(() => garage.AddVehicle(_vehicles[1]));
            Assert.Equal("The garage is full.", exception.Message);
        }

        [Fact]
        public void CountVehicleType_ReturnsCorrectCount()
        {
            // Arrange
            var garage = new Garage<Vehicle>(5);
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Act
            var count = garage.CountVehicleType(VehicleType.Car);

            // Assert
            Assert.Equal(1, count);
        }

        [Fact]
        public void FindVehicleByRegistrationNumber_ReturnsCorrectVehicle()
        {
            // Arrange
            var garage = new Garage<Vehicle>(5);
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Act
            var vehicle = garage.FindVehicleByRegistrationNumber("ABC 12A");

            // Assert
            Assert.Equal(VehicleType.Car.ToString(), vehicle.GetType().Name);
            Assert.Equal("ABC 12A", vehicle.RegistrationNumber);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(4, 1)]
        public void FindVehiclesByNumberOfSeats_ReturnsCorrectCount(int numberOfSeats, int expectedCount)
        {
            // Arrange
            var garage = new Garage<Vehicle>(5);
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Act
            var vehicles = garage.FindVehiclesByNumberOfSeats(numberOfSeats);

            // Assert
            Assert.Equal(expectedCount, vehicles.Count);
        }

        [Fact]
        public void FindVehiclesByVehicleType_ReturnsCorrectVehicle()
        {
            // Arrange
            var garage = new Garage<Vehicle>(5);
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Act
            var vehicles = garage.FindVehiclesByVehicleType(VehicleType.Truck);

            // Assert
            Assert.Single(vehicles);
            Assert.Equal(VehicleType.Truck.ToString(), vehicles[0].GetType().Name);
            Assert.Equal("RTH 09T", vehicles[0].RegistrationNumber);
        }

        [Fact]
        public void FindVehiclesByVehicleTypeAndFuelType_ReturnsCorrectVehicle()
        {
            // Arrange
            var garage = new Garage<Vehicle>(5);
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Act
            var vehicles = garage.FindVehiclesByVehicleTypeAndFuelType(VehicleType.Truck, FuelType.Diesel);

            // Assert
            Assert.Single(vehicles);
            Assert.Equal(VehicleType.Truck.ToString(), vehicles[0].GetType().Name);
            Assert.Equal("RTH 09T", vehicles[0].RegistrationNumber);
        }

        [Fact]
        public void RemoveAll_ReturnsCorrectVehicles()
        {
            // Arrange
            var garage = new Garage<Vehicle>(5);
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Act
            var vehicles = garage.RemoveAll();

            // Assert
            Assert.Equal(0, garage.NumberOfVehicles);
            Assert.Equal(4, vehicles.Count);
        }

        [Fact]
        public void RemoveVehicleByRegistrationNumber_ReturnsCorrectVehicle()
        {
            // Arrange
            var garage = new Garage<Vehicle>(5);
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Act
            var vehicle = garage.RemoveVehicleByRegistrationNumber("ABC 12A");

            // Assert
            Assert.Equal(3, garage.NumberOfVehicles);
            Assert.Equal(VehicleType.Car.ToString(), vehicle.GetType().Name);
            Assert.Equal("ABC 12A", vehicle.RegistrationNumber);
        }

        [Fact]
        public void RemoveVehicleByRegistrationNumber_IsEmpty_ThrowsInvalidOperationException()
        {
            // Arrange
            var garage = new Garage<Vehicle>(5);

            // Act and Assert
            var exception = Assert.Throws<InvalidOperationException>(() => garage.RemoveVehicleByRegistrationNumber("XYZ 123"));
            Assert.Equal("The garage is empty.", exception.Message);
        }

        [Fact]
        public void VehicleExists_ReturnsCorrectResult()
        {
            // Arrange
            var garage = new Garage<Vehicle>(5);
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Act
            var exists = garage.VehicleExists("RTH 09T");

            // Assert
            Assert.True(exists);
        }
    }
}