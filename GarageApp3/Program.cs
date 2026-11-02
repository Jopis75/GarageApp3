using GarageApp3.GarageLibrary;
using GarageApp3.GarageLibrary.Helpers;
using GarageApp3.GarageLibrary.IO;
using GarageApp3.Handlers;
using System.Configuration;

try
{
    Console.WriteLine("Welcome to the Garage App 3!");
    Console.WriteLine();

    var numberOfParkingSpaces = ConsoleHelper.ReadInt($"Enter the number of parking spaces (> 0): ", value => value < 1 || value > int.MaxValue);

    var garageHandler = new GarageHandler<Vehicle>(numberOfParkingSpaces);

    var filePath = ConfigurationManager.AppSettings.Get("SampleVehiclesCsvFilePath");
    var sampleVehicles = GarageCsvStreamReader.Read(filePath!);

    garageHandler.HandleCreateGarage(sampleVehicles);

    garageHandler.HandleEnterGarage();
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
