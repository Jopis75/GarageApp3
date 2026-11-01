using GarageApp3.GarageLibrary;
using GarageApp3.GarageLibrary.Handlers;
using GarageApp3.GarageLibrary.Helpers;
using GarageApp3.GarageLibrary.Interfaces;
using GarageApp3.GarageLibrary.IO;

try
{
    var vehicles = GarageStreamReader.Read(@"C:\Users\johan\source\repos\GarageApp3\GarageApp3\Vehicles.txt");

    var garageHandler = new GarageHandler<Vehicle>();

    garageHandler.HandleCreateGarage(vehicles);
}
catch (Exception ex)
{
    Console.WriteLine();
    ConsoleHelper.WriteErrorLine($"An unexpected error has occurred: {ex.Message}");
    Console.WriteLine();
    Console.Write("Press any key to continue...");
    Console.ReadKey();
}

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

//static IGarage<TVehicle> CreateGarage<TVehicle>(IList<TVehicle> vehicles)
//    where TVehicle : Vehicle
//{
//    Console.Clear();

//    WriteGarageAppWelcomeInfo();

//    var numberOfParkingSpaces = ConsoleHelper.ReadInt($"Enter the number of parking spaces (> 0): ", value => value < 1 || value > int.MaxValue);

//    var garage = new Garage<TVehicle>(numberOfParkingSpaces);

//    Console.WriteLine();

//    var addVehicles = ConsoleHelper.ReadYesNo("Do you want to add some vehicles to the garage ('y', 'n')? ");

//    if (addVehicles == true)
//    {
//        var length = Math.Min(garage.NumberOfParkingSpaces, vehicles.Count);

//        for (var i = 0; i < length; i++)
//        {
//            try
//            {
//                garage.AddVehicle(vehicles[i]);
//            }
//            catch (IndexOutOfRangeException ex)
//            {
//                Console.WriteLine();
//                ConsoleHelper.WriteErrorLine($"An error occurred while adding the vehicle: {ex.Message}");
//                Console.WriteLine();
//                Console.Write("Press any key to continue...");
//                Console.ReadKey();
//            }
//        }
//    }

//    return garage;
//}

//static void HandleAddVehicle<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        var exit = false;

//        while (exit == false)
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("What type of vehicle do you want to add?");
//            Console.WriteLine();

//            Console.WriteLine("1. A bicycle.");
//            Console.WriteLine("2. A motorcycle.");
//            Console.WriteLine("3. A car.");
//            Console.WriteLine("4. A truck.");
//            Console.WriteLine("5. A bus.");
//            Console.WriteLine("6. A boat.");
//            Console.WriteLine("7. An airplane.");
//            Console.WriteLine("8. Exit to Main menu.");
//            Console.WriteLine();
//            Console.Write("Enter your selection: ");

//            var selection = Console.ReadLine();

//            switch (selection)
//            {
//                case "1":
//                    AddVehicle(garage, VehicleType.Bicycle);
//                    break;
//                case "2":
//                    AddVehicle(garage, VehicleType.Motorcycle);
//                    break;
//                case "3":
//                    AddVehicle(garage, VehicleType.Car);
//                    break;
//                case "4":
//                    AddVehicle(garage, VehicleType.Truck);
//                    break;
//                case "5":
//                    AddVehicle(garage, VehicleType.Bus);
//                    break;
//                case "6":
//                    AddVehicle(garage, VehicleType.Boat);
//                    break;
//                case "7":
//                    AddVehicle(garage, VehicleType.Airplane);
//                    break;
//                case "8":
//                    exit = true;
//                    break;
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

//static void HandleFindVehicles<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        var exit = false;

//        while (exit == false)
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("How do you want to find the vehicle(s)?");
//            Console.WriteLine();

//            Console.WriteLine("1. Find by registration number.");
//            Console.WriteLine("2. Find by vehicle type.");
//            Console.WriteLine("3. Find by vehicle type and fuel type.");
//            Console.WriteLine("4. Find by number of seats.");
//            Console.WriteLine("5. Exit to Main menu.");
//            Console.WriteLine();
//            Console.Write("Enter your selection: ");

//            var selection = Console.ReadLine();

//            switch (selection)
//            {
//                case "1":
//                    FindVehicleByRegistrationNumber(garage);
//                    break;
//                case "2":
//                    FindVehiclesByVehicleType(garage);
//                    break;
//                case "3":
//                    FindVehiclesByVehicleTypeAndFuelType(garage);
//                    break;
//                case "4":
//                    FindVehiclesByNumberOfSeats(garage);
//                    break;
//                case "5":
//                    exit = true;
//                    break;
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

//static void HandleListVehicles<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        if (garage.IsEmpty)
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("-----------------------------");
//            Console.WriteLine("   List vehicles in garage   ");
//            Console.WriteLine("-----------------------------");
//            Console.WriteLine();

//            ConsoleHelper.WriteInfoLine("The garage is empty.");

//            Console.WriteLine();
//            Console.Write("Press any key to continue...");
//            Console.ReadKey();
//        }
//        else
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("-----------------------------");
//            Console.WriteLine("   List vehicles in garage   ");
//            Console.WriteLine("-----------------------------");
//            Console.WriteLine();

//            garage.WriteVehicles(Console.Out);

//            Console.WriteLine();
//            Console.Write("Press any key to continue...");
//            Console.ReadKey();
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

//static void HandleListVehicleTypes<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        if (garage.IsEmpty)
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("----------------------------------");
//            Console.WriteLine("   List vehicle types in garage   ");
//            Console.WriteLine("----------------------------------");
//            Console.WriteLine();

//            ConsoleHelper.WriteInfoLine("The garage is empty.");
//            Console.WriteLine();
//            Console.Write("Press any key to continue...");
//            Console.ReadKey();
//        }
//        else
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("----------------------------------");
//            Console.WriteLine("   List vehicle types in garage   ");
//            Console.WriteLine("----------------------------------");
//            Console.WriteLine();

//            garage.WriteVehicleTypeCount(VehicleType.Bicycle);
//            Console.WriteLine();

//            garage.WriteVehicleTypeCount(VehicleType.Motorcycle);
//            Console.WriteLine();

//            garage.WriteVehicleTypeCount(VehicleType.Car);
//            Console.WriteLine();

//            garage.WriteVehicleTypeCount(VehicleType.Truck);
//            Console.WriteLine();

//            garage.WriteVehicleTypeCount(VehicleType.Bus);
//            Console.WriteLine();

//            garage.WriteVehicleTypeCount(VehicleType.Boat);
//            Console.WriteLine();

//            garage.WriteVehicleTypeCount(VehicleType.Airplane);
//            Console.WriteLine();

//            Console.WriteLine();
//            Console.WriteLine();
//            Console.Write("Press any key to continue...");
//            Console.ReadKey();
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

//static void HandleMainMenu<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        var quit = false;

//        while (quit == false)
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("What do you want to do?");
//            Console.WriteLine();
//            Console.WriteLine("1. Add a vehicle to the garage.");
//            Console.WriteLine("2. Remove a vehicle from the garage.");
//            Console.WriteLine("3. Find vehicle(s) in the garage.");
//            Console.WriteLine("4. List all vehicles in the garage.");
//            Console.WriteLine("5. List all vehicle types in the garage.");
//            Console.WriteLine("6. Read vehicles from file.");
//            Console.WriteLine("7. Write vehicles to file.");
//            Console.WriteLine("8. Quit.");
//            Console.WriteLine();
//            Console.Write("Enter your selection: ");

//            var selection = Console.ReadLine();

//            switch (selection)
//            {
//                case "1":
//                    HandleAddVehicle(garage);
//                    break;
//                case "2":
//                    HandleRemoveVehicle(garage);
//                    break;
//                case "3":
//                    HandleFindVehicles(garage);
//                    break;
//                case "4":
//                    HandleListVehicles(garage);
//                    break;
//                case "5":
//                    HandleListVehicleTypes(garage);
//                    break;
//                case "6":
//                    HandleReadFromFile(garage);
//                    break;
//                case "7":
//                    HandleWriteToFile(garage);
//                    break;
//                case "8":
//                    quit = true;
//                    break;
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

//static void HandleReadFromFile<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        Console.Clear();

//        WriteGarageAppWelcomeInfo();
//        ConsoleHelper.WriteInfoLine(garage.StatusString());

//        Console.WriteLine();

//        Console.WriteLine("-----------------------------");
//        Console.WriteLine("   Read vehicles from file   ");
//        Console.WriteLine("-----------------------------");
//        Console.WriteLine();

//        var path = ConsoleHelper.ReadFilePath("Enter file path (blank to exit): ");

//        if (string.IsNullOrEmpty(path) == false)
//        {
//            var vehicles = GarageStreamReader.Read(path!);

//            if (vehicles.Count == 0)
//            {
//                Console.WriteLine();
//                ConsoleHelper.WriteInfoLine($"Could not find any vehicles in the file {path}.");
//                Console.WriteLine();
//            }
//            else
//            {
//                Console.WriteLine();
//                ConsoleHelper.WriteInfoLine($"Found {vehicles.Count}" + (vehicles.Count > 1 ? " vehicles" : " vehicle") + $" in the file {path}.");
//                Console.WriteLine();

//                foreach (var vehicle in vehicles)
//                {
//                    vehicle.Write(Console.Out);
//                    Console.WriteLine();
//                }

//                Console.WriteLine();

//                var addToGarage = ConsoleHelper.ReadYesNo("Do you want to add the vehicles to the garage ('y', 'n')? ");

//                if (addToGarage)
//                {
//                    var vehiclesCount = 0;

//                    foreach (var vehicle in vehicles)
//                    {
//                        if (garage.VehicleExists(vehicle.RegistrationNumber))
//                        {
//                            Console.WriteLine();
//                            ConsoleHelper.WriteErrorLine($"The registration number '{vehicle.RegistrationNumber}' already exists.");
//                        }
//                        else
//                        {
//                            garage.AddVehicle((TVehicle)vehicle);

//                            Console.WriteLine();
//                            ConsoleHelper.WriteSuccessLine($"The vehicle with registrationnumber {vehicle.RegistrationNumber} was added successfully.");

//                            vehiclesCount++;

//                            if (garage.IsFull)
//                            {
//                                Console.WriteLine();
//                                ConsoleHelper.WriteInfoLine("The garage is full.");
//                                break;
//                            }
//                        }
//                    }

//                    Console.WriteLine();

//                    if (vehiclesCount > 0)
//                    {
//                        ConsoleHelper.WriteInfoLine($"{vehiclesCount}" + (vehiclesCount > 1 ? " vehicles" : " vehicle") + " was added to the garage.");
//                    }
//                    else
//                    {
//                        ConsoleHelper.WriteInfoLine("No vehicles was added to the garage.");
//                    }
//                }
//            }

//            Console.WriteLine();
//            Console.Write("Press any key to continue...");
//            Console.ReadKey();
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

//static void HandleRemoveVehicle<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        while (true)
//        {
//            if (garage.IsEmpty)
//            {
//                Console.Clear();

//                WriteGarageAppWelcomeInfo();
//                ConsoleHelper.WriteInfoLine(garage.StatusString());

//                Console.WriteLine();

//                Console.WriteLine("--------------------------------");
//                Console.WriteLine("   Remove vehicle from garage   ");
//                Console.WriteLine("--------------------------------");
//                Console.WriteLine();

//                ConsoleHelper.WriteInfoLine("The garage is empty.");
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

//                Console.WriteLine("--------------------------------");
//                Console.WriteLine("   Remove vehicle from garage   ");
//                Console.WriteLine("--------------------------------");
//                Console.WriteLine();

//                var registrationNumber = ConsoleHelper.ReadRegistrationNumber("Enter the registration number: ");

//                Vehicle? removedVehicle = null;

//                try
//                {
//                    removedVehicle = garage.RemoveVehicleByRegistrationNumber(registrationNumber);

//                    Console.WriteLine();
//                    ConsoleHelper.WriteSuccessLine("The vehicle was removed successfully.");
//                }
//                catch (VehicleNotFoundException ex)
//                {
//                    Console.WriteLine();
//                    ConsoleHelper.WriteErrorLine($"The vehicle was not found: {ex.Message}");
//                }

//                Console.WriteLine();

//                var removeAnother = ConsoleHelper.ReadYesNo("Do you want to remove another ('y', 'n')? ");

//                if (removeAnother == false)
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

//static void HandleWriteToFile<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        Console.Clear();

//        WriteGarageAppWelcomeInfo();
//        ConsoleHelper.WriteInfoLine(garage.StatusString());

//        Console.WriteLine();

//        Console.WriteLine("----------------------------");
//        Console.WriteLine("   Write vehicles to file   ");
//        Console.WriteLine("----------------------------");
//        Console.WriteLine();

//        var (path, streamWriter) = ConsoleHelper.ReadCreateTextFile("Enter file path (blank to exit): ");

//        if (string.IsNullOrEmpty(path) == false)
//        {
//            if (streamWriter != null)
//            {
//                Console.WriteLine();

//                var vehiclesCount = GarageStreamWriter<TVehicle>.Write(streamWriter, garage);

//                if (vehiclesCount > 0)
//                {
//                    ConsoleHelper.WriteInfoLine($"{vehiclesCount}" + (vehiclesCount > 1 ? " vehicles" : " vehicle") + $" was written to the file {path}.");
//                }
//                else
//                {
//                    ConsoleHelper.WriteInfoLine($"No vehicles was written to the file {path}.");
//                }

//                streamWriter.Dispose();
//            }

//            Console.WriteLine();
//            Console.Write("Press any key to continue...");
//            Console.ReadKey();
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

//static void FindVehicleByRegistrationNumber<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        if (garage.IsEmpty)
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("-----------------------------------------");
//            Console.WriteLine("   Find vehicle by registration number   ");
//            Console.WriteLine("-----------------------------------------");
//            Console.WriteLine();

//            ConsoleHelper.WriteInfoLine("The garage is empty.");
//            Console.WriteLine();
//            Console.WriteLine();
//            Console.Write("Press any key to continue...");
//            Console.ReadKey();
//        }
//        else
//        {
//            while (true)
//            {
//                Console.Clear();

//                WriteGarageAppWelcomeInfo();
//                ConsoleHelper.WriteInfoLine(garage.StatusString());

//                Console.WriteLine();

//                Console.WriteLine("-----------------------------------------");
//                Console.WriteLine("   Find vehicle by registration number   ");
//                Console.WriteLine("-----------------------------------------");
//                Console.WriteLine();

//                var registrationNumber = ConsoleHelper.ReadRegistrationNumber("Enter the registration number: ");

//                try
//                {
//                    var foundVehicle = garage.FindVehicleByRegistrationNumber(registrationNumber);

//                    Console.WriteLine();

//                    foundVehicle.Write(Console.Out);
//                }
//                catch (VehicleNotFoundException ex)
//                {
//                    Console.WriteLine();
//                    ConsoleHelper.WriteErrorLine($"The vehicle was not found: {ex.Message}");
//                }

//                Console.WriteLine();

//                var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

//                if (findAnother == false)
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

//static void FindVehiclesByNumberOfSeats<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        if (garage.IsEmpty)
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("----------------------------------------");
//            Console.WriteLine("   Find vehicle(s) by number of seats   ");
//            Console.WriteLine("----------------------------------------");
//            Console.WriteLine();

//            ConsoleHelper.WriteInfoLine("The garage is empty.");
//            Console.WriteLine();
//            Console.WriteLine();
//            Console.Write("Press any key to continue...");
//            Console.ReadKey();
//        }
//        else
//        {
//            while (true)
//            {
//                Console.Clear();

//                WriteGarageAppWelcomeInfo();
//                ConsoleHelper.WriteInfoLine(garage.StatusString());

//                Console.WriteLine();

//                Console.WriteLine("----------------------------------------");
//                Console.WriteLine("   Find vehicle(s) by number of seats   ");
//                Console.WriteLine("----------------------------------------");
//                Console.WriteLine();

//                var numberOfSeats = ConsoleHelper.ReadInt("Enter the number of seats (> 0): ", value => value < 1 || value > int.MaxValue);

//                var foundVehicles = garage.FindVehiclesByNumberOfSeats(numberOfSeats);

//                Console.WriteLine();

//                if (foundVehicles.Count > 0)
//                {
//                    ConsoleHelper.WriteInfoLine($"Found {foundVehicles.Count}" + (foundVehicles.Count == 1 ? " vehicle" : " vehicles") + $" with number of seats '{numberOfSeats}'.");
//                    Console.WriteLine();

//                    foreach (var foundVehicle in foundVehicles)
//                    {
//                        foundVehicle.Write(Console.Out);
//                        Console.WriteLine();
//                    }
//                }
//                else
//                {
//                    ConsoleHelper.WriteErrorLine($"Could not find any vehicles with number of seats '{numberOfSeats}'.");
//                    Console.WriteLine();
//                }

//                var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

//                if (findAnother == false)
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

//static void FindVehiclesByVehicleType<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        if (garage.IsEmpty)
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("-------------------------------------");
//            Console.WriteLine("   Find vehicle(s) by vehicle type   ");
//            Console.WriteLine("-------------------------------------");
//            Console.WriteLine();

//            ConsoleHelper.WriteInfoLine("The garage is empty.");
//            Console.WriteLine();
//            Console.WriteLine();
//            Console.Write("Press any key to continue...");
//            Console.ReadKey();
//        }
//        else
//        {
//            while (true)
//            {
//                Console.Clear();

//                WriteGarageAppWelcomeInfo();
//                ConsoleHelper.WriteInfoLine(garage.StatusString());

//                Console.WriteLine();

//                Console.WriteLine("-------------------------------------");
//                Console.WriteLine("   Find vehicle(s) by vehicle type   ");
//                Console.WriteLine("-------------------------------------");
//                Console.WriteLine();

//                var vehicleType = ConsoleHelper.ReadEnum<VehicleType>("Enter the vehicle type ('Bicycle', 'Motorcycle', 'Car', 'Truck', 'Bus', 'Boat', 'Airplane'): ");

//                var foundVehicles = garage.FindVehiclesByVehicleType(vehicleType);

//                Console.WriteLine();

//                if (foundVehicles.Count > 0)
//                {
//                    ConsoleHelper.WriteInfoLine($"Found {foundVehicles.Count}" + (foundVehicles.Count == 1 ? " vehicle" : " vehicles") + $" with vehicle type '{vehicleType}'.");
//                    Console.WriteLine();

//                    foreach (var foundVehicle in foundVehicles)
//                    {
//                        foundVehicle.Write(Console.Out);
//                        Console.WriteLine();
//                    }
//                }
//                else
//                {
//                    ConsoleHelper.WriteErrorLine($"Could not find any vehicles with vehicle type '{vehicleType}'.");
//                    Console.WriteLine();
//                }

//                var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

//                if (findAnother == false)
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

//static void FindVehiclesByVehicleTypeAndFuelType<TVehicle>(IGarage<TVehicle> garage)
//    where TVehicle : Vehicle
//{
//    try
//    {
//        if (garage.IsEmpty)
//        {
//            Console.Clear();

//            WriteGarageAppWelcomeInfo();
//            ConsoleHelper.WriteInfoLine(garage.StatusString());

//            Console.WriteLine();

//            Console.WriteLine("---------------------------------------------------");
//            Console.WriteLine("   Find vehicle(s) by vehicle type and fuel type   ");
//            Console.WriteLine("---------------------------------------------------");
//            Console.WriteLine();

//            ConsoleHelper.WriteInfoLine("The garage is empty.");
//            Console.WriteLine();
//            Console.WriteLine();
//            Console.Write("Press any key to continue...");
//            Console.ReadKey();
//        }
//        else
//        {
//            while (true)
//            {
//                Console.Clear();

//                WriteGarageAppWelcomeInfo();
//                ConsoleHelper.WriteInfoLine(garage.StatusString());

//                Console.WriteLine();

//                Console.WriteLine("---------------------------------------------------");
//                Console.WriteLine("   Find vehicle(s) by vehicle type and fuel type   ");
//                Console.WriteLine("---------------------------------------------------");
//                Console.WriteLine();

//                var vehicleType = ConsoleHelper.ReadEnum<VehicleType>("Enter the vehicle type ('Bicycle', 'Motorcycle', 'Car', 'Truck', 'Bus', 'Boat', 'Airplane'): ");
//                var fuelType = ConsoleHelper.ReadEnum<FuelType>("Enter the fuel type ('Gasoline', 'Diesel'): ");

//                var foundVehicles = garage.FindVehiclesByVehicleTypeAndFuelType(vehicleType, fuelType);

//                Console.WriteLine();

//                if (foundVehicles.Count > 0)
//                {
//                    ConsoleHelper.WriteInfoLine($"Found {foundVehicles.Count}" + (foundVehicles.Count == 1 ? " vehicle" : " vehicles") + $" with vehicle type '{vehicleType}' and fuel type '{fuelType}'.");
//                    Console.WriteLine();

//                    foreach (var foundVehicle in foundVehicles)
//                    {
//                        foundVehicle.Write(Console.Out);
//                        Console.WriteLine();
//                    }
//                }
//                else
//                {
//                    ConsoleHelper.WriteErrorLine($"Could not find any vehicles with vehicle type '{vehicleType}' and fuel type '{fuelType}'.");
//                    Console.WriteLine();
//                }

//                var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

//                if (findAnother == false)
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

//static void WriteGarageAppWelcomeInfo()
//{
//    Console.WriteLine(" ▄██████ ██████▄  ██████▄  ██████▄   ▄██████  ▄██████      ██████▄  ██████▄  ██████▄       ██████▄ \r\n▒▒▒  ▀▀▀ ▄▄▄  ▒▒▒ ▄▄▄  ▒▒▀ ▄▄▄  ▒▒▒ ▒▒▒  ▀▀▀ ▒▒▒  ▀▀▀      ▄▄▄  ▒▒▒ ▄▄▄  ▒▒▒ ▄▄▄  ▒▒▒      ▀▀▀  ▒▒▒\r\n███ ████ ████████ ██████▄  ████████ ███ ████ █████▀        ████████ ██████▀  ██████▀         ▀█████\r\n▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▄▄▄      ▒▒▒  ▒▒▒ ▒▒▒      ▒▒▒           ▄▄▄  ▒▒▒\r\n ▀██████ ███  ███ ███  ███ ███  ███  ▀██████  ▀██████      ███  ███ ███      ███           ██████▀ ");
//    Console.WriteLine();
//    Console.WriteLine("Welcome to the Garage App 3!");
//    Console.WriteLine();
//    Console.WriteLine();
//}