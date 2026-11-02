using GarageApp3.GarageLibrary;
using GarageApp3.GarageLibrary.Helpers;
using GarageApp3.GarageLibrary.IO;
using GarageApp3.Handlers;
using System.Configuration;

try
{
    var filePath = ConfigurationManager.AppSettings.Get("SampleVehiclesCsvFilePath");
    var sampleVehicles = GarageCsvStreamReader.Read(filePath!);

    var garageHandler = new GarageHandler<Vehicle>();

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
