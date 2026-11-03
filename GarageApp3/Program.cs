using GarageApp3.GarageLibrary.Garages;
using GarageApp3.GarageLibrary.Helpers;
using GarageApp3.GarageLibrary.IO;
using GarageApp3.GarageLibrary.Vehicles;
using GarageApp3.Handlers;
using System.Configuration;

try
{
    // Get the sample vehicles CSV file path and number of parking spaces from the app.config file.
    var csvFilePath = GetSampleVehiclesCsvFilePath();
    var numberOfParkingSpaces = GetNumberOfParkingSpaces();

    // Read the sample vehicles from the CSV file.
    var sampleVehicles = GarageCsvStreamReader.Read(csvFilePath!);

    var garageHandler = new GarageHandler<Vehicle>(new Garage<Vehicle>(numberOfParkingSpaces));

    // Initialize the garage handler with the sample vehicles and enter the garage.
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

static string? GetSampleVehiclesCsvFilePath()
{
    var filePath = ConfigurationManager.AppSettings.Get("SampleVehiclesCsvFilePath");

    if (string.IsNullOrWhiteSpace(filePath))
    {
        throw new ArgumentException("Invalid file path value. File path cannot be null or whitespace.", nameof(filePath));
    }

    return filePath;
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
