using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.IO;

namespace GarageApp3.Tests
{
    public class VehicleCsvLineParserTests
    {
        private readonly string[] _csvLines =
        {
            "Bicycle;XYZ 123;1,8;1;3",
            "Motorcycle;HAC 26G;2,1;2;1;6,0;Gasoline;60,0",
            "Car;ABC 12A;4,0;4;1;2,0;Gasoline;750,0",
            "Truck;RTH 09T;9,0;2;1;6,0;Diesel;40000,0"
        };

        [Fact]
        public void Parse_MultipleLines_ReturnsCorrectVehicles()
        {
            // Arrange
            var csvLines = _csvLines;

            // Act
            var vehicles = VehicleCsvLineParser.Parse(csvLines);

            // Assert
            Assert.Equal(4, vehicles.Count);

            Assert.Equal(VehicleType.Bicycle.ToString(), vehicles[0].GetType().Name);
            Assert.Equal("XYZ 123", vehicles[0].RegistrationNumber);

            Assert.Equal(VehicleType.Motorcycle.ToString(), vehicles[1].GetType().Name);
            Assert.Equal("HAC 26G", vehicles[1].RegistrationNumber);

            Assert.Equal(VehicleType.Car.ToString(), vehicles[2].GetType().Name);
            Assert.Equal("ABC 12A", vehicles[2].RegistrationNumber);

            Assert.Equal(VehicleType.Truck.ToString(), vehicles[3].GetType().Name);
            Assert.Equal("RTH 09T", vehicles[3].RegistrationNumber);
        }

        [Fact]
        public void Parse_SingleLine_ReturnsCorrectVehicle()
        {
            // Arrange
            var csvLine = _csvLines[2];

            // Act
            var vehicle = VehicleCsvLineParser.Parse(csvLine);

            // Assert
            Assert.Equal(VehicleType.Car.ToString(), vehicle.GetType().Name);
            Assert.Equal("ABC 12A", vehicle.RegistrationNumber);
        }

        [Theory]
        [InlineData("Car;ABC 12A;4,0;4;1;2,0;Gasoline", "Invalid number of fields in CSV line when parsing Car.")]
        [InlineData("Car;ABC 12;4,0;4;1;2,0;Gasoline;750,0", "Invalid registration number in CSV line when parsing Car.")]
        [InlineData("Car;ABC 12A;4a,0;4;1;2,0;Gasoline;750,0", "Invalid length value in CSV line when parsing Car.")]
        [InlineData("Car;ABC 12A;4,0;4a;1;2,0;Gasoline;750,0", "Invalid number of seats value in CSV line when parsing Car.")]
        [InlineData("Car;ABC 12A;4,0;4;1x;2,0;Gasoline;750,0", "Invalid number of engines value in CSV line when parsing Car.")]
        [InlineData("Car;ABC 12A;4,0;4;1;xyz;Gasoline;750,0", "Invalid cylinder volume value in CSV line when parsing Car.")]
        [InlineData("Car;ABC 12A;4,0;4;1;2,0;Gas;750,0", "Invalid fuel type value in CSV line when parsing Car.")]
        [InlineData("Car;ABC 12A;4,0;4;1;2,0;Gasoline;750x0", "Invalid maximum trailer weight value in CSV line when parsing Car.")]
        public void Parse_InvalidLine_ThrowsArgumentException(string line, string expectedExceptionMessage)
        {
            // Arrange
            var lines = new string[] { line };

            // Act and Assert
            var exception = Assert.Throws<ArgumentException>(() => VehicleCsvLineParser.Parse(lines));
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}
