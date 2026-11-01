using GarageApp3.GarageLibrary;
using GarageApp3.GarageLibrary.Helpers;
using GarageApp3.GarageLibrary.IO;
using GarageApp3.Handlers;

try
{
    var vehicles = GarageCsvStreamReader.Read(@"C:\Users\johan\source\repos\GarageApp3\GarageApp3\SampleVehicles.txt");

    var garageHandler = new GarageHandler<Vehicle>();

    garageHandler.HandleCreateGarage(vehicles);
    garageHandler.HandleEnterGarage();
}
catch (Exception ex)
{
    Console.WriteLine();
    ConsoleHelper.WriteErrorLine($"An unexpected error has occurred: {ex.Message}");
    Console.WriteLine();
    Console.Write("Press any key to continue...");
    Console.ReadKey();
}
