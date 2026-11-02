using GarageApp3.GarageLibrary;
using GarageApp3.GarageLibrary.Helpers;
using GarageApp3.GarageLibrary.IO;
using GarageApp3.Handlers;
using System.Configuration;

try
{
    if (int.TryParse(ConfigurationManager.AppSettings.Get("NumberOfParkingSpaces"), out int numberOfParkingSpaces) == false
        || numberOfParkingSpaces <= 0 || numberOfParkingSpaces > int.MaxValue)
    {
        throw new ArgumentOutOfRangeException(nameof(numberOfParkingSpaces), $"Invalid number of parking spaces value. Must be greater than 0 and less than or equal to {int.MaxValue}.");
    }

    var filePath = ConfigurationManager.AppSettings.Get("SampleVehiclesCsvFilePath");

    var sampleVehicles = GarageCsvStreamReader.Read(filePath!);

    var garageHandler = new GarageHandler<Vehicle>(numberOfParkingSpaces);

    garageHandler.InitGarage(sampleVehicles);
    garageHandler.EnterGarage();
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
