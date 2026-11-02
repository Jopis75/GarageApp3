using GarageApp3.GarageLibrary;
using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.Exceptions;
using GarageApp3.GarageLibrary.Helpers;
using GarageApp3.GarageLibrary.IO;
using GarageApp3.Interfaces;

namespace GarageApp3.Handlers
{
    public class GarageHandler<TVehicle> : IGarageHandler<TVehicle>
        where TVehicle : Vehicle
    {
        private Garage<TVehicle>? _garage = null;

        public void HandleAddVehicle()
        {
            if (_garage == null)
            {
                throw new InvalidOperationException("The garage has not been created.");
            }

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

            var addVehicles = ConsoleHelper.ReadYesNo("Do you want to add sample vehicles to the garage ('y', 'n')? ");

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

                WritePressAnyKeyToContinue();
            }
        }

        public void HandleFindVehicles()
        {
            if (_garage == null)
            {
                throw new InvalidOperationException("The garage has not been created.");
            }

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

        public void HandleEnterGarage()
        {
            if (_garage == null)
            {
                throw new InvalidOperationException("The garage has not been created.");
            }

            var quit = false;

            while (quit == false)
            {
                Console.Clear();

                WriteGarageAppHeader();

                Console.WriteLine();
                Console.WriteLine("What do you want to do?");
                Console.WriteLine();

                Console.WriteLine("1. Add a vehicle to the garage.");
                Console.WriteLine("2. Remove a vehicle from the garage.");
                Console.WriteLine("3. Find vehicle(s) in the garage.");
                Console.WriteLine("4. List all vehicles in the garage.");
                Console.WriteLine("5. List all vehicle types in the garage.");
                Console.WriteLine("6. Read vehicles from a file.");
                Console.WriteLine("7. Write vehicles to a file.");
                Console.WriteLine("8. Quit.");
                Console.WriteLine();
                Console.Write("Enter your selection: ");

                var selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        HandleAddVehicle();
                        break;
                    case "2":
                        HandleRemoveVehicle();
                        break;
                    case "3":
                        HandleFindVehicles();
                        break;
                    case "4":
                        HandleListVehicles();
                        break;
                    case "5":
                        HandleListVehicleTypes();
                        break;
                    case "6":
                        HandleReadVehicles();
                        break;
                    case "7":
                        HandleWriteVehicles();
                        break;
                    case "8":
                        HandleExitGarage();
                        quit = true;
                        break;
                }
            }
        }

        public void HandleExitGarage()
        {
            if (_garage == null)
            {
                throw new InvalidOperationException("The garage has not been created.");
            }

            Console.WriteLine();

            var saveToFile = ConsoleHelper.ReadYesNo("Do you want to save the garage to a file before quitting ('y', 'n')? ");

            if (saveToFile)
            {
                HandleWriteVehicles();
            }
        }

        public void HandleListVehicles()
        {
            if (_garage == null)
            {
                throw new InvalidOperationException("The garage has not been created.");
            }

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
            if (_garage == null)
            {
                throw new InvalidOperationException("The garage has not been created.");
            }

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
            if (_garage == null)
            {
                throw new InvalidOperationException("The garage has not been created.");
            }

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
                var vehicles = GarageCsvStreamReader.Read(path!);

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
            if (_garage == null)
            {
                throw new InvalidOperationException("The garage has not been created.");
            }

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
                        ConsoleHelper.WriteSuccessLine($"The vehicle with registration number '{registrationNumber}' was removed successfully.");
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
            if (_garage == null)
            {
                throw new InvalidOperationException("The garage has not been created.");
            }

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

                    var vehiclesCount = GarageCsvStreamWriter<TVehicle>.Write(streamWriter, _garage);

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

        #region Private helper methods

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

        //static void AddVehicle<TVehicle>(IGarage<TVehicle> garage, VehicleType vehicleType)
        //    where TVehicle : Vehicle
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            var infoString = $"   Add {vehicleType} vehicle to garage   ";
        //            var infoStringUnderline = new string('-', infoString.Length);

        //            if (garage.IsFull)
        //            {
        //                Console.Clear();

        //                WriteGarageAppWelcomeInfo();
        //                ConsoleHelper.WriteInfoLine(garage.StatusString());

        //                Console.WriteLine();

        //                Console.WriteLine(infoStringUnderline);
        //                Console.WriteLine(infoString);
        //                Console.WriteLine(infoStringUnderline);
        //                Console.WriteLine();

        //                ConsoleHelper.WriteInfoLine("The garage is full.");
        //                Console.WriteLine();
        //                Console.WriteLine();
        //                Console.Write("Press any key to continue...");
        //                Console.ReadKey();
        //                break;
        //            }
        //            else
        //            {
        //                Console.Clear();

        //                WriteGarageAppWelcomeInfo();
        //                ConsoleHelper.WriteInfoLine(garage.StatusString());

        //                Console.WriteLine();

        //                Console.WriteLine(infoStringUnderline);
        //                Console.WriteLine(infoString);
        //                Console.WriteLine(infoStringUnderline);
        //                Console.WriteLine();

        //                var registrationNumber = string.Empty;

        //                while (true)
        //                {
        //                    registrationNumber = ConsoleHelper.ReadRegistrationNumber("Enter the registration number: ");

        //                    if (garage.VehicleExists(registrationNumber))
        //                    {
        //                        Console.WriteLine();
        //                        ConsoleHelper.WriteErrorLine($"The registration number '{registrationNumber}' already exists.");
        //                        Console.WriteLine();
        //                    }
        //                    else
        //                    {
        //                        break;
        //                    }
        //                }

        //                Vehicle? vehicle = null;

        //                var length = ConsoleHelper.ReadFloat("Enter the length (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);
        //                var numberOfSeats = ConsoleHelper.ReadInt("Enter the number of seats (> 0): ", value => value < 1 || value > int.MaxValue);

        //                if (vehicleType == VehicleType.Bicycle)
        //                {
        //                    var numberOfGears = ConsoleHelper.ReadInt("Enter the number of gears (> 0): ", value => value < 1 || value > int.MaxValue);

        //                    vehicle = new Bicycle(registrationNumber, length, numberOfSeats, numberOfGears);
        //                }
        //                else
        //                {
        //                    var numberOfEngines = ConsoleHelper.ReadInt("Enter the number of engines (> 0): ", value => value < 1 || value > int.MaxValue);
        //                    var cylinderVolume = ConsoleHelper.ReadFloat("Enter the cylinder volume (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);
        //                    var fuelType = ConsoleHelper.ReadEnum<FuelType>("Enter the fuel type ('Gasoline', 'Diesel'): ");

        //                    if (vehicleType == VehicleType.Motorcycle)
        //                    {
        //                        var seatHeight = ConsoleHelper.ReadFloat("Enter the seat height (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);

        //                        vehicle = new Motorcycle(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType, seatHeight);
        //                    }
        //                    else if (vehicleType == VehicleType.Car)
        //                    {
        //                        var maxTrailerWeight = ConsoleHelper.ReadFloat("Enter the maximum trailer weight (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);

        //                        vehicle = new Car(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType, maxTrailerWeight);
        //                    }
        //                    else if (vehicleType == VehicleType.Truck)
        //                    {
        //                        var maximumLoad = ConsoleHelper.ReadFloat("Enter the maximum load (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);

        //                        vehicle = new Truck(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType, maximumLoad);
        //                    }
        //                    else if (vehicleType == VehicleType.Bus)
        //                    {
        //                        var numberOfPassengerSeats = ConsoleHelper.ReadInt("Enter the number of passenger seats (> 0): ", value => value < 1 || value > int.MaxValue);

        //                        vehicle = new Bus(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType, numberOfPassengerSeats);
        //                    }
        //                    else if (vehicleType == VehicleType.Boat)
        //                    {
        //                        var draft = ConsoleHelper.ReadFloat("Enter the draft (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);

        //                        vehicle = new Boat(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType, draft);
        //                    }
        //                    else if (vehicleType == VehicleType.Airplane)
        //                    {
        //                        var maxThrust = ConsoleHelper.ReadFloat("Enter the Max thrust (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);

        //                        vehicle = new Airplane(registrationNumber, length, numberOfSeats, numberOfEngines, cylinderVolume, fuelType, maxThrust);
        //                    }
        //                }

        //                try
        //                {
        //                    garage.AddVehicle((TVehicle)vehicle!);

        //                    Console.WriteLine();
        //                    ConsoleHelper.WriteSuccessLine($"The vehicle with registrationnumber {registrationNumber} was added successfully.");
        //                    Console.WriteLine();
        //                }
        //                catch (IndexOutOfRangeException ex)
        //                {
        //                    Console.WriteLine();
        //                    ConsoleHelper.WriteErrorLine($"An error occurred while adding the vehicle: {ex.Message}");
        //                    Console.WriteLine();
        //                    Console.Write("Press any key to continue...");
        //                    Console.ReadKey();
        //                    break;
        //                }

        //                var addAnother = ConsoleHelper.ReadYesNo("Do you want to add another ('y', 'n')? ");

        //                if (addAnother == false)
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine();
        //        ConsoleHelper.WriteErrorLine($"An unexpected error has occurred: {ex.Message}");
        //        Console.WriteLine();
        //        Console.Write("Press any key to continue...");
        //        Console.ReadKey();
        //        throw;
        //    }
        //}

    }
}
