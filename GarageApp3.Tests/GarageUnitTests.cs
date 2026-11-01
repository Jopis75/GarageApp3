using GarageApp3.GarageLibrary;
using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.Interfaces;

namespace GarageApp3.Tests
{
    public class GarageUnitTests
    {
        private readonly Vehicle[] _vehicles =  
        {
            new Bicycle("XYZ 123", 1.8f, 1, 3),
            new Motorcycle("HAC 26G", 2.1f, 2, 1, 6.0f, FuelType.Gasoline, 60.0f),
            new Car("ABC 12A", 4.0f, 4, 1, 2.0f, FuelType.Gasoline, 750.0f),
            new Truck("RTH 09T", 9.0f, 2, 1, 6.0f, FuelType.Diesel, 40000.0f)
        };

        [Fact]
        public void TestAddVehicle()
        {
            // Arrange
            IGarage<Vehicle> garage = new Garage<Vehicle>(5);

            // Act
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Assert
            Assert.Equal(4, garage.NumberOfVehicles);
        }

        [Fact]
        public void TestCountVehicleType()
        {
            // Arrange
            IGarage<Vehicle> garage = new Garage<Vehicle>(5);
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
        public void TestFindVehicleByRegistrationNumber()
        {
            // Arrange
            IGarage<Vehicle> garage = new Garage<Vehicle>(5);
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

        [Fact]
        public void TestFindVehiclesByNumberOfSeats()
        {
            // Arrange
            IGarage<Vehicle> garage = new Garage<Vehicle>(5);
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Act
            var vehicles = garage.FindVehiclesByNumberOfSeats(4);

            // Assert
            Assert.Single(vehicles);
            Assert.Equal(VehicleType.Car.ToString(), vehicles[0].GetType().Name);
            Assert.Equal("ABC 12A", vehicles[0].RegistrationNumber);
        }

        [Fact]
        public void TestFindVehiclesByVehicleType()
        {
            // Arrange
            IGarage<Vehicle> garage = new Garage<Vehicle>(5);
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
        public void TestFindVehiclesByVehicleTypeAndFuelType()
        {
            // Arrange
            IGarage<Vehicle> garage = new Garage<Vehicle>(5);
            garage.AddVehicle(_vehicles[0]);
            garage.AddVehicle(_vehicles[1]);
            garage.AddVehicle(_vehicles[2]);
            garage.AddVehicle(_vehicles[3]);

            // Act
            var vehicles = garage.FindVehiclesByVehicleTypeAndFuelType(VehicleType.Truck, FuelType.Gasoline);

            // Assert
            Assert.Single(vehicles);
            Assert.Equal(VehicleType.Truck.ToString(), vehicles[0].GetType().Name);
            Assert.Equal("RTH 09T", vehicles[0].RegistrationNumber);
        }

        //[Fact]
        //public void TestIsEmpty()
        //{
        //    // Arrange
        //    IGarage<Vehicle> garage = new Garage<Vehicle>(5);
        //    garage.AddVehicle(_vehicles[0]);

        //    // Act
        //    garage.RemoveVehicleByRegistrationNumber("XYZ 123");

        //    // Assert
        //    Assert.True(garage.IsEmpty);
        //}

        //[Fact]
        //public void TestIsFull()
        //{
        //    // Arrange
        //    IGarage<Vehicle> garage = new Garage<Vehicle>(3);

        //    // Act
        //    garage.AddVehicle(_vehicles[0]);
        //    garage.AddVehicle(_vehicles[1]);
        //    garage.AddVehicle(_vehicles[2]);

        //    // Assert
        //    Assert.True(garage.IsFull);
        //}

        [Fact]
        public void TestRemoveAll()
        {
            // Arrange
            IGarage<Vehicle> garage = new Garage<Vehicle>(5);
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
        public void TestRemoveVehicleByRegistrationNumber()
        {
            // Arrange
            IGarage<Vehicle> garage = new Garage<Vehicle>(5);
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
        public void TestVehicleExists()
        {
            // Arrange
            IGarage<Vehicle> garage = new Garage<Vehicle>(5);
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