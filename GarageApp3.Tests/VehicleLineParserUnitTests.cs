using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.IO;

namespace GarageApp3.Tests
{
    public class VehicleLineParserUnitTests
    {
        private readonly string[] _lines =
        {
            "Bicycle;XYZ 123;1,8;1;3",
            "Motorcycle;HAC 26G;2,1;2;1;6,0;Gasoline;60,0",
            "Car;ABC 12A;4,0;4;1;2,0;Gasoline;750.0f",
            "Truck;RTH 09T;9,0;2;1;6,0;Diesel;40000,0"
        };

        [Fact]
        public void TestParseMultipleLines()
        {
            // Arrange
            var lines = _lines;

            // Act
            var vehicles = VehicleCsvLineParser.Parse(_lines);

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
        public void TestParseSingleLine()
        {
            // Arrange
            var line = _lines[2];

            // Act
            var vehicle = VehicleCsvLineParser.Parse(line);

            // Assert
            Assert.Equal(VehicleType.Car.ToString(), vehicle.GetType().Name);
            Assert.Equal("ABC 12A", vehicle.RegistrationNumber);
        }

        //[Theory]
        //[InlineData("Car;RTH 23T;4,2;4;1;6,0;Gasoline", "")]
        //public void TestThrowArgumentException(string line, string expectedExceptionMessage)
        //{
        //    // Arrange
        //    var lines = new string[] { line };

        //    // Act and Assert
        //    var exception = Assert.Throws<ArgumentException>(() => VehicleLineParser.Parse(line));
        //    Assert.Equal(expectedExceptionMessage, exception.Message);
        //}
    }
}
