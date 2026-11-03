using GarageApp3.GarageLibrary.Dtos;
using GarageApp3.GarageLibrary.Enums;
using GarageApp3.GarageLibrary.Exceptions;
using GarageApp3.GarageLibrary.Helpers;
using GarageApp3.GarageLibrary.Interfaces;
using GarageApp3.GarageLibrary.IO;
using GarageApp3.GarageLibrary.Vehicles;
using GarageApp3.Interfaces;

namespace GarageApp3.Handlers
{
    public class GarageHandler<TVehicle>(IGarage<TVehicle> garage) : IGarageHandler<TVehicle>
        where TVehicle : Vehicle
    {
        public void AddVehicle()
        {
            var exit = false;

            while (!exit)
            {
                Console.Clear();

                WritePageHeader();

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
                        AddVehicle(VehicleType.Bicycle);
                        break;
                    case "2":
                        AddVehicle(VehicleType.Motorcycle);
                        break;
                    case "3":
                        AddVehicle(VehicleType.Car);
                        break;
                    case "4":
                        AddVehicle(VehicleType.Truck);
                        break;
                    case "5":
                        AddVehicle(VehicleType.Bus);
                        break;
                    case "6":
                        AddVehicle(VehicleType.Boat);
                        break;
                    case "7":
                        AddVehicle(VehicleType.Airplane);
                        break;
                    case "8":
                        exit = true;
                        break;
                }
            }
        }

        public void FindVehicles()
        {
            var exit = false;

            while (!exit)
            {
                Console.Clear();

                WritePageHeader();

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

        public void EnterGarage()
        {
            var quit = false;

            while (!quit)
            {
                Console.Clear();

                WritePageHeader();

                Console.WriteLine();
                Console.WriteLine("What do you want to do?");
                Console.WriteLine();

                Console.WriteLine("1. Add a vehicle to the garage.");
                Console.WriteLine("2. Remove a vehicle from the garage.");
                Console.WriteLine("3. Remove all vehicles from the garage.");
                Console.WriteLine("4. Find vehicles in the garage.");
                Console.WriteLine("5. List all vehicles in the garage.");
                Console.WriteLine("6. List all vehicle types in the garage.");
                Console.WriteLine("7. Read vehicles from a file.");
                Console.WriteLine("8. Write vehicles to a file.");
                Console.WriteLine("9. Quit.");
                Console.WriteLine();
                Console.Write("Enter your selection: ");

                var selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        AddVehicle();
                        break;
                    case "2":
                        RemoveVehicle();
                        break;
                    case "3":
                        RemoveAllVehicles();
                        break;
                    case "4":
                        FindVehicles();
                        break;
                    case "5":
                        ListVehicles();
                        break;
                    case "6":
                        ListVehicleTypes();
                        break;
                    case "7":
                        ReadVehicles();
                        break;
                    case "8":
                        WriteVehicles();
                        break;
                    case "9":
                        ExitGarage();
                        quit = true;
                        break;
                }
            }
        }

        public void ExitGarage()
        {
            Console.WriteLine();

            var saveToFile = ConsoleHelper.ReadYesNo("Do you want to save the garage to a file before quitting ('y', 'n')? ");

            if (saveToFile)
            {
                WriteVehicles();
            }
        }

        public void InitGarage(IList<TVehicle> vehicles)
        {
            Console.Clear();

            WritePageHeader();

            Console.WriteLine();

            // Ask the user if they want to add sample vehicles to the garage and add them if they choose to do so.
            var addVehicles = ConsoleHelper.ReadYesNo("Do you want to add sample vehicles to the garage ('y', 'n')? ");

            if (addVehicles)
            {
                var vehiclesCount = 0;

                // Limit the number of vehicles to add to the garage based on the number of parking spaces available and the number of vehicles in the list.
                var count = Math.Min(garage.NumberOfParkingSpaces, vehicles.Count);

                foreach (var vehicle in vehicles.Take(count))
                {
                    if (garage.VehicleExists(vehicle.RegistrationNumber))
                    {
                        Console.WriteLine();
                        ConsoleHelper.WriteErrorLine($"A vehicle with registration number '{vehicle.RegistrationNumber}' already exists in the garage.");
                    }
                    else
                    {
                        garage.AddVehicle(vehicle);

                        vehiclesCount++;

                        Console.WriteLine();
                        ConsoleHelper.WriteSuccessLine($"The vehicle with registrationnumber '{vehicle.RegistrationNumber}' has been added to the garage.");
                    }
                }

                Console.WriteLine();

                if (vehiclesCount > 0)
                {
                    ConsoleHelper.WriteInfoLine($"{vehiclesCount}" + (vehiclesCount > 1 ? " vehicles" : " vehicle") + " has been added to the garage.");
                }
                else
                {
                    ConsoleHelper.WriteInfoLine("No vehicles has been added to the garage.");
                }

                WritePressAnyKeyToContinue();
            }
        }

        public void ListVehicles()
        {
            if (garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   List vehicles in garage   ");
            }
            else
            {
                Console.Clear();

                WritePageHeader();

                WritePageTitle("   List vehicles in garage   ");

                ConsoleHelper.WriteInfoLine($"Found {garage.NumberOfVehicles}" + (garage.NumberOfVehicles > 1 ? " vehicles" : " vehicle") + " in the garage.");
                Console.WriteLine();

                garage.WriteVehicles(Console.Out);
                Console.WriteLine();

                WritePressAnyKeyToContinue();
            }
        }

        public void ListVehicleTypes()
        {
            if (garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   List vehicle types in garage   ");
            }
            else
            {
                Console.Clear();

                WritePageHeader();

                WritePageTitle("   List vehicle types in garage   ");

                garage.WriteVehicleTypes(Console.Out);
                Console.WriteLine();

                WritePressAnyKeyToContinue();
            }
        }

        public void ReadVehicles()
        {
            Console.Clear();

            WritePageHeader();

            WritePageTitle("   Read vehicles from file   ");

            // Read the file path from the console.
            var path = ConsoleHelper.ReadFilePath("Enter the file path (blank to exit): ");

            if (!string.IsNullOrEmpty(path))
            {
                // Read the vehicles from the specified CSV file and display the results.
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

                    // Ask the user if they want to add the vehicles to the garage and add them if they choose to do so.
                    var addToGarage = ConsoleHelper.ReadYesNo("Do you want to add the vehicles to the garage ('y', 'n')? ");

                    if (addToGarage)
                    {
                        if (garage.IsFull)
                        {
                            Console.WriteLine();
                            ConsoleHelper.WriteInfoLine("The garage is full.");
                        }
                        else
                        {
                            var vehiclesCount = 0;

                            foreach (var vehicle in vehicles)
                            {
                                if (garage.VehicleExists(vehicle.RegistrationNumber))
                                {
                                    Console.WriteLine();
                                    ConsoleHelper.WriteErrorLine($"A vehicle with registration number '{vehicle.RegistrationNumber}' already exists in the garage.");
                                }
                                else
                                {
                                    if (garage.IsFull)
                                    {
                                        Console.WriteLine();
                                        ConsoleHelper.WriteInfoLine("The garage is full.");
                                        break;
                                    }

                                    garage.AddVehicle((TVehicle)vehicle);

                                    vehiclesCount++;

                                    Console.WriteLine();
                                    ConsoleHelper.WriteSuccessLine($"The vehicle with registration number '{vehicle.RegistrationNumber}' has been added to the garage.");
                                }
                            }

                            Console.WriteLine();

                            if (vehiclesCount > 0)
                            {
                                ConsoleHelper.WriteInfoLine($"{vehiclesCount}" + (vehiclesCount > 1 ? " vehicles" : " vehicle") + " has been added to the garage.");
                            }
                            else
                            {
                                ConsoleHelper.WriteInfoLine("No vehicles has been added to the garage.");
                            }
                        }
                    }
                }

                WritePressAnyKeyToContinue();
            }
        }

        public void RemoveAllVehicles()
        {
            if (garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   Remove all vehicles from garage   ");
            }
            else
            {
                Console.Clear();

                WritePageHeader();

                WritePageTitle("   Remove all vehicles from garage   ");

                var removeAll = ConsoleHelper.ReadYesNo("Are you sure you want to remove all vehicles from the garage ('y', 'n')? ");

                if (removeAll)
                {
                    garage.RemoveAllVehicles();

                    Console.WriteLine();
                    ConsoleHelper.WriteSuccessLine("All vehicles have been removed from the garage.");

                    Console.WriteLine();
                    WritePressAnyKeyToContinue();
                }
            }
        }

        public void RemoveVehicle()
        {
            while (true)
            {
                if (garage.IsEmpty)
                {
                    WriteGarageIsEmptyPage("   Remove vehicle from garage   ");
                    break;
                }
                else
                {
                    Console.Clear();

                    WritePageHeader();

                    WritePageTitle("   Remove vehicle from garage   ");

                    // Read the registration number from the console and attempt to remove the vehicle from the garage.
                    var registrationNumber = ConsoleHelper.ReadRegistrationNumber("Enter the registration number: ");

                    Vehicle? vehicle;

                    try
                    {
                        // Remove the vehicle from the garage by the specified registration number and display a success message.
                        vehicle = garage.RemoveVehicleByRegistrationNumber(registrationNumber);

                        Console.WriteLine();
                        ConsoleHelper.WriteSuccessLine($"The vehicle with registration number '{registrationNumber}' has been removed from the garage.");
                    }
                    catch (VehicleNotFoundException ex)
                    {
                        Console.WriteLine();
                        ConsoleHelper.WriteErrorLine(ex.Message);
                    }

                    Console.WriteLine();

                    var removeAnother = ConsoleHelper.ReadYesNo("Do you want to remove another ('y', 'n')? ");

                    if (!removeAnother)
                    {
                        break;
                    }
                }
            }
        }

        public void WriteVehicles()
        {
            Console.Clear();

            WritePageHeader();

            WritePageTitle("   Write vehicles to file   ");

            // Read the file path from the console.
            var createTextFileDto = ConsoleHelper.ReadCreateTextFile("Enter the file path (blank to exit): ");

            if (!string.IsNullOrEmpty(createTextFileDto.FilePath))
            {
                if (createTextFileDto.StreamWriter != null)
                {
                    Console.WriteLine();

                    // Write the vehicles in the garage to the specified CSV file.
                    var vehiclesCount = GarageCsvStreamWriter<TVehicle>.Write(createTextFileDto.StreamWriter, garage);

                    if (vehiclesCount > 0)
                    {
                        ConsoleHelper.WriteInfoLine($"{vehiclesCount}" + (vehiclesCount > 1 ? " vehicles" : " vehicle") + $" was written to the file {createTextFileDto.FilePath}.");
                    }
                    else
                    {
                        ConsoleHelper.WriteInfoLine($"No vehicles was written to the file {createTextFileDto.FilePath}.");
                    }

                    createTextFileDto.StreamWriter.Dispose();
                }

                WritePressAnyKeyToContinue();
            }
        }

        #region Private helper methods

        private void AddVehicle(VehicleType vehicleType)
        {
            var title = $"   Add {vehicleType} vehicle to garage   ";
            var dashLine = new string('-', title.Length);

            while (true)
            {
                if (garage.IsFull)
                {
                    WriteGarageIsFullPage(title);
                    break;
                }
                else
                {
                    Console.Clear();

                    WritePageHeader();

                    Console.WriteLine();
                    Console.WriteLine(dashLine);
                    Console.WriteLine(title);
                    Console.WriteLine(dashLine);
                    Console.WriteLine();

                    // Read the vehicle fields from the console based on the vehicle type and create a new vehicle instance.
                    var vehicle = ReadVehicle(vehicleType);

                    try
                    {
                        // Add the vehicle to the garage and display a success message.
                        garage.AddVehicle((TVehicle)vehicle!);

                        Console.WriteLine();
                        ConsoleHelper.WriteSuccessLine($"The vehicle with registrationnumber '{vehicle.RegistrationNumber}' has been added to the garage.");
                        Console.WriteLine();
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine();
                        ConsoleHelper.WriteErrorLine($"An error occurred while adding the vehicle: {ex.Message}");
                        Console.WriteLine();
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    }

                    var addAnother = ConsoleHelper.ReadYesNo("Do you want to add another ('y', 'n')? ");

                    if (!addAnother)
                    {
                        break;
                    }
                }
            }
        }

        private void FindVehiclesByNumberOfSeats()
        {
            if (garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   Find vehicle(s) by number of seats   ");
            }
            else
            {
                while (true)
                {
                    Console.Clear();

                    WritePageHeader();

                    Console.WriteLine();
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("   Find vehicle(s) by number of seats   ");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine();

                    // Read the number of seats from the console.
                    var numberOfSeats = ConsoleHelper.ReadInt("Enter the number of seats (> 0): ", value => value < 1 || value > int.MaxValue);

                    // Find vehicles in the garage by the specified number of seats and display the results.
                    var vehicles = garage.FindVehiclesByNumberOfSeats(numberOfSeats);

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
                        ConsoleHelper.WriteErrorLine($"Could not find any vehicles with number of seats '{numberOfSeats}'.");
                        Console.WriteLine();
                    }

                    var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

                    if (!findAnother)
                    {
                        break;
                    }
                }
            }
        }

        private void FindVehicleByRegistrationNumber()
        {
            if (garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   Find vehicle by registration number   ");
            }
            else
            {
                while (true)
                {
                    Console.Clear();

                    WritePageHeader();

                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("   Find vehicle by registration number   ");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine();

                    // Read the registration number from the console.
                    var registrationNumber = ConsoleHelper.ReadRegistrationNumber("Enter the registration number: ");

                    try
                    {
                        // Find the vehicle in the garage by the specified registration number and display the result.
                        var vehicle = garage.FindVehicleByRegistrationNumber(registrationNumber);

                        Console.WriteLine();
                        ConsoleHelper.WriteInfoLine($"Found vehicle with registration number '{registrationNumber}'.");
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

                    if (!findAnother)
                    {
                        break;
                    }
                }
            }
        }

        private void FindVehiclesByVehicleType()
        {
            if (garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   Find vehicle(s) by vehicle type   ");
            }
            else
            {
                while (true)
                {
                    Console.Clear();

                    WritePageHeader();

                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("   Find vehicle(s) by vehicle type   ");
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine();

                    // Read the vehicle type from the console.
                    var vehicleType = ConsoleHelper.ReadEnum<VehicleType>("Enter the vehicle type ('Bicycle', 'Motorcycle', 'Car', 'Truck', 'Bus', 'Boat', 'Airplane'): ");

                    // Find vehicles in the garage by the specified vehicle type and display the results.
                    var vehicles = garage.FindVehiclesByVehicleType(vehicleType);

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
                        ConsoleHelper.WriteErrorLine($"Could not find any vehicles with vehicle type '{vehicleType}'.");
                        Console.WriteLine();
                    }

                    var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

                    if (!findAnother)
                    {
                        break;
                    }
                }
            }
        }

        private void FindVehiclesByVehicleTypeAndFuelType()
        {
            if (garage.IsEmpty)
            {
                WriteGarageIsEmptyPage("   Find vehicle(s) by vehicle type and fuel type   ");
            }
            else
            {
                while (true)
                {
                    Console.Clear();

                    WritePageHeader();

                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("   Find vehicle(s) by vehicle type and fuel type   ");
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine();

                    // Read the vehicle type and fuel type from the console.
                    var vehicleType = ConsoleHelper.ReadEnum<VehicleType>("Enter the vehicle type ('Bicycle', 'Motorcycle', 'Car', 'Truck', 'Bus', 'Boat', 'Airplane'): ");
                    var fuelType = ConsoleHelper.ReadEnum<FuelType>("Enter the fuel type ('Gasoline', 'Diesel'): ");

                    // Find vehicles in the garage by the specified vehicle type and fuel type and display the results.
                    var vehicles = garage.FindVehiclesByVehicleTypeAndFuelType(vehicleType, fuelType);

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
                        ConsoleHelper.WriteErrorLine($"Could not find any vehicles with vehicle type '{vehicleType}' and fuel type '{fuelType}'.");
                        Console.WriteLine();
                    }

                    var findAnother = ConsoleHelper.ReadYesNo("Do you want to find another ('y', 'n')? ");

                    if (!findAnother)
                    {
                        break;
                    }
                }
            }
        }

        private Airplane ReadAirplaneFields()
        {
            var vehicleDto = ReadVehicleFields();
            var motorVehicleDto = ReadMotorVehicleFields();
            var maxThrust = ConsoleHelper.ReadFloat("Enter the Max thrust (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);

            return new Airplane(vehicleDto.RegistrationNumber, vehicleDto.Length, vehicleDto.NumberOfSeats, motorVehicleDto.NumberOfEngines, motorVehicleDto.CylinderVolume, motorVehicleDto.FuelType, maxThrust);
        }

        private Bicycle ReadBicycleFields()
        {
            var vehicleDto = ReadVehicleFields();
            var numberOfGears = ConsoleHelper.ReadInt("Enter the number of gears (> 0): ", value => value < 1 || value > int.MaxValue);

            return new Bicycle(vehicleDto.RegistrationNumber, vehicleDto.Length, vehicleDto.NumberOfSeats, numberOfGears);
        }

        private Boat ReadBoatFields()
        {
            var vehicleDto = ReadVehicleFields();
            var motorVehicleDto = ReadMotorVehicleFields();
            var draft = ConsoleHelper.ReadFloat("Enter the draft (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);

            return new Boat(vehicleDto.RegistrationNumber, vehicleDto.Length, vehicleDto.NumberOfSeats, motorVehicleDto.NumberOfEngines, motorVehicleDto.CylinderVolume, motorVehicleDto.FuelType, draft);
        }

        private Bus ReadBusFields()
        {
            var vehicleDto = ReadVehicleFields();
            var motorVehicleDto = ReadMotorVehicleFields();
            var numberOfPassengerSeats = ConsoleHelper.ReadInt("Enter the number of passenger seats (> 0): ", value => value < 1 || value > int.MaxValue);

            return new Bus(vehicleDto.RegistrationNumber, vehicleDto.Length, vehicleDto.NumberOfSeats, motorVehicleDto.NumberOfEngines, motorVehicleDto.CylinderVolume, motorVehicleDto.FuelType, numberOfPassengerSeats);
        }

        private Car ReadCarFields()
        {
            var vehicleDto = ReadVehicleFields();
            var motorVehicleDto = ReadMotorVehicleFields();
            var maxTrailerWeight = ConsoleHelper.ReadFloat("Enter the maximum trailer weight (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);

            return new Car(vehicleDto.RegistrationNumber, vehicleDto.Length, vehicleDto.NumberOfSeats, motorVehicleDto.NumberOfEngines, motorVehicleDto.CylinderVolume, motorVehicleDto.FuelType, maxTrailerWeight);
        }

        private Motorcycle ReadMotorcycleFields()
        {
            var vehicleDto = ReadVehicleFields();
            var motorVehicleDto = ReadMotorVehicleFields();
            var seatHeight = ConsoleHelper.ReadFloat("Enter the seat height (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);

            return new Motorcycle(vehicleDto.RegistrationNumber, vehicleDto.Length, vehicleDto.NumberOfSeats, motorVehicleDto.NumberOfEngines, motorVehicleDto.CylinderVolume, motorVehicleDto.FuelType, seatHeight);
        }

        private MotorVehicleDto ReadMotorVehicleFields()
        {
            var numberOfEngines = ConsoleHelper.ReadInt("Enter the number of engines (> 0): ", value => value < 1 || value > int.MaxValue);
            var cylinderVolume = ConsoleHelper.ReadFloat("Enter the cylinder volume (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);
            var fuelType = ConsoleHelper.ReadEnum<FuelType>("Enter the fuel type ('Gasoline', 'Diesel'): ");

            return new MotorVehicleDto(numberOfEngines, cylinderVolume, fuelType);
        }

        private Truck ReadTruckFields()
        {
            var vehicleDto = ReadVehicleFields();
            var motorVehicleDto = ReadMotorVehicleFields();
            var maximumLoad = ConsoleHelper.ReadFloat("Enter the maximum load (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);

            return new Truck(vehicleDto.RegistrationNumber, vehicleDto.Length, vehicleDto.NumberOfSeats, motorVehicleDto.NumberOfEngines, motorVehicleDto.CylinderVolume, motorVehicleDto.FuelType, maximumLoad);
        }

        private Vehicle ReadVehicle(VehicleType vehicleType)
        {
            return vehicleType switch
            {
                VehicleType.Bicycle => ReadBicycleFields(),
                VehicleType.Motorcycle => ReadMotorcycleFields(),
                VehicleType.Car => ReadCarFields(),
                VehicleType.Truck => ReadTruckFields(),
                VehicleType.Bus => ReadBusFields(),
                VehicleType.Boat => ReadBoatFields(),
                VehicleType.Airplane => ReadAirplaneFields(),
                _ => throw new ArgumentException($"Invalid vehicle type value '{vehicleType}'.")
            };
        }

        private VehicleDto ReadVehicleFields()
        {
            var registrationNumber = string.Empty;

            // Read the registration number from the console and check if it already exists in the garage.
            // If it does, prompt the user to enter a different registration number until a unique one is provided.
            var uniqueRegistrationNumber = false;

            while (!uniqueRegistrationNumber)
            {
                registrationNumber = ConsoleHelper.ReadRegistrationNumber("Enter the registration number: ");

                if (garage.VehicleExists(registrationNumber))
                {
                    Console.WriteLine();
                    ConsoleHelper.WriteErrorLine($"A vehicle with registration number '{registrationNumber}' already exists in the garage.");
                    Console.WriteLine();
                }
                else
                {
                    uniqueRegistrationNumber = true;
                }
            }

            var length = ConsoleHelper.ReadFloat("Enter the length (> 0.0): ", value => !(value > 0.0f) || value > float.MaxValue);
            var numberOfSeats = ConsoleHelper.ReadInt("Enter the number of seats (> 0): ", value => value < 1 || value > int.MaxValue);

            return new VehicleDto(registrationNumber, length, numberOfSeats);
        }

        private void WriteGarageIsEmptyPage(string title)
        {
            var dashLine = new string('-', title.Length);

            Console.Clear();

            WritePageHeader();

            Console.WriteLine();
            Console.WriteLine(dashLine);
            Console.WriteLine(title);
            Console.WriteLine(dashLine);
            Console.WriteLine();

            ConsoleHelper.WriteInfoLine("The garage is empty.");
            Console.WriteLine();

            WritePressAnyKeyToContinue();
        }

        private void WriteGarageIsFullPage(string title)
        {
            var dashLine = new string('-', title.Length);

            Console.Clear();

            WritePageHeader();

            Console.WriteLine();
            Console.WriteLine(dashLine);
            Console.WriteLine(title);
            Console.WriteLine(dashLine);
            Console.WriteLine();

            ConsoleHelper.WriteInfoLine("The garage is full.");
            Console.WriteLine();

            WritePressAnyKeyToContinue();
        }

        private void WritePageHeader()
        {
            Console.WriteLine(" ▄██████ ██████▄  ██████▄  ██████▄   ▄██████  ▄██████      ██████▄  ██████▄  ██████▄       ██████▄ \r\n▒▒▒  ▀▀▀ ▄▄▄  ▒▒▒ ▄▄▄  ▒▒▀ ▄▄▄  ▒▒▒ ▒▒▒  ▀▀▀ ▒▒▒  ▀▀▀      ▄▄▄  ▒▒▒ ▄▄▄  ▒▒▒ ▄▄▄  ▒▒▒      ▀▀▀  ▒▒▒\r\n███ ████ ████████ ██████▄  ████████ ███ ████ █████▀        ████████ ██████▀  ██████▀         ▀█████\r\n▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▒▒▒ ▒▒▒  ▄▄▄      ▒▒▒  ▒▒▒ ▒▒▒      ▒▒▒           ▄▄▄  ▒▒▒\r\n ▀██████ ███  ███ ███  ███ ███  ███  ▀██████  ▀██████      ███  ███ ███      ███           ██████▀ ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            ConsoleHelper.WriteInfoLine(garage.StatusString());
        }

        private static void WritePageTitle(string title)
        {
            var dashLine = new string('-', title.Length);
           
            Console.WriteLine();
            Console.WriteLine(dashLine);
            Console.WriteLine(title);
            Console.WriteLine(dashLine);
            Console.WriteLine();
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
