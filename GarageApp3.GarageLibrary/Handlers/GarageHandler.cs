using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.Exceptions;
using GarageApp3.GarageLibrary.Helpers;
using GarageApp3.GarageLibrary.Interfaces;
using GarageApp3.GarageLibrary.IO;

namespace GarageApp3.GarageLibrary.Handlers
{
    public class GarageHandler<TVehicle> : IGarageHandler<TVehicle>
        where TVehicle : Vehicle
    {
        private Garage<TVehicle> _garage;

        public void HandleAddVehicle()
        {
            var exit = false;

            while (exit == false)
            {
                Console.Clear();

                WriteGarageAppHeader();

                Console.WriteLine();
                Console.WriteLine("What type of vehicle do you want to add?");
                Console.WriteLine();

                Console.WriteLine("1. A bicycle.");
                Console.WriteLine("2. A motorcycle.");
                Console.WriteLine("3. A car.");
                Console.WriteLine("4. A truck.");
                Console.WriteLine("5. A bus.");
                Console.WriteLine("6. A boat.");
                Console.WriteLine("7. An airplane.");
                Console.WriteLine("8. Exit to Main menu.");
                Console.WriteLine();
                Console.Write("Enter your selection: ");

                var selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        //AddVehicle(garage, VehicleType.Bicycle);
                        break;
                    case "2":
                        //AddVehicle(garage, VehicleType.Motorcycle);
                        break;
                    case "3":
                        //AddVehicle(garage, VehicleType.Car);
                        break;
                    case "4":
                        //AddVehicle(garage, VehicleType.Truck);
                        break;
                    case "5":
                        //AddVehicle(garage, VehicleType.Bus);
                        break;
                    case "6":
                        //AddVehicle(garage, VehicleType.Boat);
                        break;
                    case "7":
                        //AddVehicle(garage, VehicleType.Airplane);
                        break;
                    case "8":
                        exit = true;
                        break;
                }
            }
        }

        public void HandleCreateGarage(IList<TVehicle> vehicles)
        {
            Console.Clear();

            WriteGarageAppHeader();

            var numberOfParkingSpaces = ConsoleHelper.ReadInt($"Enter the number of parking spaces (> 0): ", value => value < 1 || value > int.MaxValue);

            _garage = new Garage<TVehicle>(numberOfParkingSpaces);

            Console.WriteLine();

            var addVehicles = ConsoleHelper.ReadYesNo("Do you want to add some vehicles to the garage ('y', 'n')? ");

            if (addVehicles == true)
            {
                var vehiclesCount = 0;

                var count = Math.Min(_garage.NumberOfParkingSpaces, vehicles.Count);

                foreach (var vehicle in vehicles.Take(count))
                {
                    if (_garage.VehicleExists(vehicle.RegistrationNumber))
                    {
                        Console.WriteLine();
                        ConsoleHelper.WriteErrorLine($"The registration number '{vehicle.RegistrationNumber}' already exists.");
                    }
                    else
                    {
                        _garage.AddVehicle(vehicle);

                        vehiclesCount++;

                        Console.WriteLine();
                        ConsoleHelper.WriteSuccessLine($"The vehicle with registrationnumber '{vehicle.RegistrationNumber}' was added successfully.");
                    }
                }

                Console.WriteLine();

                if (vehiclesCount > 0)
                {
                    ConsoleHelper.WriteInfoLine($"{vehiclesCount}" + (vehiclesCount > 1 ? " vehicles" : " vehicle") + " was added to the garage.");
                }
                else
                {
                    ConsoleHelper.WriteInfoLine("No vehicles was added to the garage.");
                }
            }
        }

        public void HandleFindVehicles()
        {
            var exit = false;

            while (exit == false)
            {
                Console.Clear();

                WriteGarageAppHeader();

                Console.WriteLine();
                Console.WriteLine("How do you want to find the vehicle(s)?");
                Console.WriteLine();

                Console.WriteLine("1. Find by registration number.");
                Console.WriteLine("2. Find by vehicle type.");
                Console.WriteLine("3. Find by vehicle type and fuel type.");
                Console.WriteLine("4. Find by number of seats.");
                Console.WriteLine("5. Exit to Main menu.");
                Console.WriteLine();
                Console.Write("Enter your selection: ");

                var selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        FindVehicleByRegistrationNumber();
                        break;
                    case "2":
                        FindVehiclesByVehicleType();
                        break;
                    case "3":
                        FindVehiclesByVehicleTypeAndFuelType();
                        break;
                    case "4":
                        FindVehiclesByNumberOfSeats();
                        break;
                    case "5":
                        exit = true;
                        break;
                }
            }
        }

        public void HandleListVehicles()
        {
            if (_garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   List vehicles in garage   ");
            }
            else
            {
                Console.Clear();

                WriteGarageAppHeader();

                Console.WriteLine();
                Console.WriteLine("-----------------------------");
                Console.WriteLine("   List vehicles in garage   ");
                Console.WriteLine("-----------------------------");
                Console.WriteLine();

                _garage.WriteVehicles(Console.Out);
                Console.WriteLine();

                WritePressAnyKeyToContinue();
            }
        }

        public void HandleListVehicleTypes()
        {
            if (_garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   List vehicle types in garage   ");
            }
            else
            {
                Console.Clear();

                WriteGarageAppHeader();

                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine("   List vehicle types in garage   ");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();

                _garage.WriteVehicleTypes(Console.Out);
                Console.WriteLine();

                WritePressAnyKeyToContinue();
            }
        }

        public void HandleReadVehicles()
        {
            Console.Clear();

            WriteGarageAppHeader();

            Console.WriteLine();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("   Read vehicles from file   ");
            Console.WriteLine("-----------------------------");
            Console.WriteLine();

            var path = ConsoleHelper.ReadFilePath("Enter the file path (blank to exit): ");

            if (string.IsNullOrEmpty(path) == false)
            {
                var vehicles = GarageStreamReader.Read(path!);

                if (vehicles.Count == 0)
                {
                    Console.WriteLine();
                    ConsoleHelper.WriteInfoLine($"Could not find any vehicles in the file {path}.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    ConsoleHelper.WriteInfoLine($"Found {vehicles.Count}" + (vehicles.Count > 1 ? " vehicles" : " vehicle") + $" in the file {path}.");
                    Console.WriteLine();

                    foreach (var vehicle in vehicles)
                    {
                        vehicle.Write(Console.Out);
                        Console.WriteLine();
                    }

                    Console.WriteLine();

                    var addToGarage = ConsoleHelper.ReadYesNo("Do you want to add the vehicles to the garage ('y', 'n')? ");

                    if (addToGarage)
                    {
                        var vehiclesCount = 0;

                        foreach (var vehicle in vehicles)
                        {
                            if (_garage.VehicleExists(vehicle.RegistrationNumber))
                            {
                                Console.WriteLine();
                                ConsoleHelper.WriteErrorLine($"The registration number '{vehicle.RegistrationNumber}' already exists.");
                            }
                            else
                            {
                                _garage.AddVehicle((TVehicle)vehicle);

                                vehiclesCount++;

                                Console.WriteLine();
                                ConsoleHelper.WriteSuccessLine($"The vehicle with registrationnumber {vehicle.RegistrationNumber} was added successfully.");

                                if (_garage.IsFull)
                                {
                                    Console.WriteLine();
                                    ConsoleHelper.WriteInfoLine("The garage is full.");
                                    break;
                                }
                            }
                        }

                        Console.WriteLine();

                        if (vehiclesCount > 0)
                        {
                            ConsoleHelper.WriteInfoLine($"{vehiclesCount}" + (vehiclesCount > 1 ? " vehicles" : " vehicle") + " was added to the garage.");
                        }
                        else
                        {
                            ConsoleHelper.WriteInfoLine("No vehicles was added to the garage.");
                        }
                    }
                }

                WritePressAnyKeyToContinue();
            }
        }

        public void HandleRemoveVehicle()
        {
            while (true)
            {
                if (_garage.IsEmpty)
                {
                    WriteGarageIsEmptyPage("   Remove vehicle from garage   ");
                    break;
                }
                else
                {
                    Console.Clear();

                    WriteGarageAppHeader();

                    Console.WriteLine();
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("   Remove vehicle from garage   ");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine();

                    var registrationNumber = ConsoleHelper.ReadRegistrationNumber("Enter the registration number: ");

                    Vehicle? removedVehicle;

                    try
                    {
                        removedVehicle = _garage.RemoveVehicleByRegistrationNumber(registrationNumber);

                        Console.WriteLine();
                        ConsoleHelper.WriteSuccessLine("The vehicle was removed successfully.");
                    }
                    catch (VehicleNotFoundException ex)
                    {
                        Console.WriteLine();
                        ConsoleHelper.WriteErrorLine(ex.Message);
                    }

                    Console.WriteLine();

                    var removeAnother = ConsoleHelper.ReadYesNo("Do you want to remove another ('y', 'n')? ");

                    if (removeAnother == false)
                    {
                        break;
                    }
                }
            }
        }

        public void HandleWriteVehicles()
        {
            Console.Clear();

            WriteGarageAppHeader();

            Console.WriteLine();
            Console.WriteLine("----------------------------");
            Console.WriteLine("   Write vehicles to file   ");
            Console.WriteLine("----------------------------");
            Console.WriteLine();

            var (path, streamWriter) = ConsoleHelper.ReadCreateTextFile("Enter the file path (blank to exit): ");

            if (string.IsNullOrEmpty(path) == false)
            {
                if (streamWriter != null)
                {
                    Console.WriteLine();

                    var vehiclesCount = GarageStreamWriter<TVehicle>.Write(streamWriter, _garage);

                    if (vehiclesCount > 0)
                    {
                        ConsoleHelper.WriteInfoLine($"{vehiclesCount}" + (vehiclesCount > 1 ? " vehicles" : " vehicle") + $" was written to the file {path}.");
                    }
                    else
                    {
                        ConsoleHelper.WriteInfoLine($"No vehicles was written to the file {path}.");
                    }

                    streamWriter.Dispose();
                }

                WritePressAnyKeyToContinue();
            }
        }

        #region Private Methods

        private void FindVehiclesByNumberOfSeats()
        {
            if (_garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   Find vehicle(s) by number of seats   ");
            }
            else
            {
                while (true)
                {
                    Console.Clear();

                    WriteGarageAppHeader();

                    Console.WriteLine();
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("   Find vehicle(s) by number of seats   ");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine();

                    var numberOfSeats = ConsoleHelper.ReadInt("Enter the number of seats (> 0): ", value => value < 1 || value > int.MaxValue);

                    var vehicles = _garage.FindVehiclesByNumberOfSeats(numberOfSeats);

                    Console.WriteLine();

                    if (vehicles.Count > 0)
                    {
                        ConsoleHelper.WriteInfoLine($"Found {vehicles.Count}" + (vehicles.Count == 1 ? " vehicle" : " vehicles") + $" with number of seats '{numberOfSeats}'.");
                        Console.WriteLine();

                        foreach (var vehicle in vehicles)
                        {
                            vehicle.Write(Console.Out);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        ConsoleHelper.WriteInfoLine($"Could not find any vehicles with number of seats '{numberOfSeats}'.");
                        Console.WriteLine();
                    }

                    var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

                    if (findAnother == false)
                    {
                        break;
                    }
                }
            }
        }

        private void FindVehicleByRegistrationNumber()
        {
            if (_garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   Find vehicle by registration number   ");
            }
            else
            {
                while (true)
                {
                    Console.Clear();

                    WriteGarageAppHeader();

                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("   Find vehicle by registration number   ");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine();

                    var registrationNumber = ConsoleHelper.ReadRegistrationNumber("Enter the registration number: ");

                    try
                    {
                        var vehicle = _garage.FindVehicleByRegistrationNumber(registrationNumber);

                        Console.WriteLine();
                        vehicle.Write(Console.Out);
                    }
                    catch (VehicleNotFoundException ex)
                    {
                        Console.WriteLine();
                        ConsoleHelper.WriteErrorLine(ex.Message);
                    }

                    Console.WriteLine();

                    var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

                    if (findAnother == false)
                    {
                        break;
                    }
                }
            }
        }

        private void FindVehiclesByVehicleType()
        {
            if (_garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   Find vehicle(s) by vehicle type   ");
            }
            else
            {
                while (true)
                {
                    Console.Clear();

                    WriteGarageAppHeader();

                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("   Find vehicle(s) by vehicle type   ");
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine();

                    var vehicleType = ConsoleHelper.ReadEnum<VehicleType>("Enter the vehicle type ('Bicycle', 'Motorcycle', 'Car', 'Truck', 'Bus', 'Boat', 'Airplane'): ");

                    var vehicles = _garage.FindVehiclesByVehicleType(vehicleType);

                    Console.WriteLine();

                    if (vehicles.Count > 0)
                    {
                        ConsoleHelper.WriteInfoLine($"Found {vehicles.Count}" + (vehicles.Count == 1 ? " vehicle" : " vehicles") + $" with vehicle type '{vehicleType}'.");
                        Console.WriteLine();

                        foreach (var vehicle in vehicles)
                        {
                            vehicle.Write(Console.Out);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        ConsoleHelper.WriteInfoLine($"Could not find any vehicles with vehicle type '{vehicleType}'.");
                        Console.WriteLine();
                    }

                    var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

                    if (findAnother == false)
                    {
                        break;
                    }
                }
            }
        }

        private void FindVehiclesByVehicleTypeAndFuelType()
        {
            if (_garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   Find vehicle(s) by vehicle type and fuel type   ");
            }
            else
            {
                while (true)
                {
                    Console.Clear();

                    WriteGarageAppHeader();

                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("   Find vehicle(s) by vehicle type and fuel type   ");
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine();

                    var vehicleType = ConsoleHelper.ReadEnum<VehicleType>("Enter the vehicle type ('Bicycle', 'Motorcycle', 'Car', 'Truck', 'Bus', 'Boat', 'Airplane'): ");
                    var fuelType = ConsoleHelper.ReadEnum<FuelType>("Enter the fuel type ('Gasoline', 'Diesel'): ");

                    var vehicles = _garage.FindVehiclesByVehicleTypeAndFuelType(vehicleType, fuelType);

                    Console.WriteLine();

                    if (vehicles.Count > 0)
                    {
                        ConsoleHelper.WriteInfoLine($"Found {vehicles.Count}" + (vehicles.Count == 1 ? " vehicle" : " vehicles") + $" with vehicle type '{vehicleType}' and fuel type '{fuelType}'.");
                        Console.WriteLine();

                        foreach (var vehicle in vehicles)
                        {
                            vehicle.Write(Console.Out);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        ConsoleHelper.WriteInfoLine($"Could not find any vehicles with vehicle type '{vehicleType}' and fuel type '{fuelType}'.");
                        Console.WriteLine();
                    }

                    var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

                    if (findAnother == false)
                    {
                        break;
                    }
                }
            }
        }

        private void WriteGarageAppHeader()
        {
            Console.WriteLine(" ▄██████ ██████▄  ██████▄  ██████▄   ▄██████  ▄██████      ██████▄  ██████▄  ██████▄       ██████▄ \r\n▒▒▒  ▀▀▀ ▄▄▄  ▒▒▒ ▄▄▄  ▒▒▀ ▄▄▄  ▒▒▒ ▒▒▒  ▀▀▀ ▒▒▒  ▀▀▀      ▄▄▄  ▒▒▒ ▄▄▄  ▒▒▒ ▄▄▄  ▒▒▒      ▀▀▀  ▒▒▒\r\n███ ████ ████████ ██████▄  ████████ ███ ████ █████▀        ████████ ██████▀  ██████▀         ▀█████\r\n▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▄▄▄      ▒▒▒  ▒▒▒ ▒▒▒      ▒▒▒           ▄▄▄  ▒▒▒\r\n ▀██████ ███  ███ ███  ███ ███  ███  ▀██████  ▀██████      ███  ███ ███      ███           ██████▀ ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            if (_garage != null)
            {
                ConsoleHelper.WriteInfoLine(_garage.StatusString());
            }
        }

        private void WriteGarageIsEmptyPage(string title)
        {
            var titleUnderline = new string('-', title.Length);

            Console.Clear();

            WriteGarageAppHeader();

            Console.WriteLine();
            Console.WriteLine(titleUnderline);
            Console.WriteLine(title);
            Console.WriteLine(titleUnderline);
            Console.WriteLine();

            ConsoleHelper.WriteInfoLine("The garage is empty.");
            Console.WriteLine();

            WritePressAnyKeyToContinue();
        }

        private static void WritePressAnyKeyToContinue()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        #endregion

    }
}
