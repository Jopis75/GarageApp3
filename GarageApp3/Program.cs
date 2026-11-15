using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.Garages;
using GarageApp3.GarageLibrary.Helpers;
using GarageApp3.GarageLibrary.IO;
using GarageApp3.GarageLibrary.Vehicles;
using GarageApp3.Handlers;
using System.Configuration;

try
{
    // Get the number of parking spaces from the App.config file and create a GarageHandler instance.
    var numberOfParkingSpaces = GetNumberOfParkingSpaces();
    var garageHandler = new GarageHandler<Vehicle>(new Garage<Vehicle>(numberOfParkingSpaces));

    // Initialize the garage with sample vehicles and enter the garage.
    var sampleVehicles = GetSampleVehicles();
    garageHandler.InitGarage(sampleVehicles);
    garageHandler.EnterGarage();
}
catch (ArgumentException ex)
{
    Console.WriteLine();
    ConsoleHelper.WriteErrorLine(ex.Message);
    Console.WriteLine();
    Console.Write("Press any key to continue...");
    Console.ReadKey();
}
catch (FileNotFoundException ex)
{
    Console.WriteLine();
    ConsoleHelper.WriteErrorLine(ex.Message);
    Console.WriteLine("Please ensure that the sample vehicles CSV file exists and that the file path is correct in the App.config file.");
    Console.WriteLine();
    Console.Write("Press any key to continue...");
    Console.ReadKey();
}
catch (DirectoryNotFoundException ex)
{
    Console.WriteLine();
    ConsoleHelper.WriteErrorLine(ex.Message);
    Console.WriteLine("Please ensure that the directory for the sample vehicles CSV file exists and that the file path is correct in the App.config file.");
    Console.WriteLine();
    Console.Write("Press any key to continue...");
    Console.ReadKey();

}
catch (IOException ex)
{
    Console.WriteLine();
    ConsoleHelper.WriteErrorLine(ex.Message);
    Console.WriteLine();
    Console.Write("Press any key to continue...");
    Console.ReadKey();
}
catch (Exception ex)
{
    Console.WriteLine();
    ConsoleHelper.WriteErrorLine($"An unexpected error has occurred: {ex.Message}");
    Console.WriteLine();
    Console.Write("Press any key to continue...");
    Console.ReadKey();
}

static int GetNumberOfParkingSpaces()
{
    var numberOfParkingSpacesString = ConfigurationManager.AppSettings.Get("NumberOfParkingSpaces");

    if (int.TryParse(numberOfParkingSpacesString, out int numberOfParkingSpaces) == false
        || numberOfParkingSpaces <= 0 || numberOfParkingSpaces > int.MaxValue)
    {
        throw new ArgumentOutOfRangeException(nameof(numberOfParkingSpaces), $"Invalid number of parking spaces value. Must be greater than 0 and less than or equal to {int.MaxValue}.");
    }

    return numberOfParkingSpaces;
}

static IList<Vehicle> GetSampleVehicles()
{
    var csvFilePath = ConfigurationManager.AppSettings.Get("SampleVehiclesCsvFilePath");

    if (string.IsNullOrWhiteSpace(csvFilePath))
    {
        return
        [
            new Bicycle("XYZ 123", 1.8f, 1, 3),
            new Motorcycle("HAC 26G", 2.1f, 2, 1, 6.0f, FuelType.Gasoline, 60.0f),
            new Car("ABC 12A", 4.0f, 4, 1, 2.0f, FuelType.Gasoline, 750.0f),
            new Truck("RTH 09T", 9.0f, 2, 1, 6.0f, FuelType.Diesel, 40000.0f)
        ];
    }

    // Read the sample vehicles from the CSV file.
    var sampleVehicles = GarageCsvStreamReader.Read(csvFilePath!);

    return sampleVehicles;
}
