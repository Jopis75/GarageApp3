using GarageApp3.GarageLibrary.Validators;

namespace GarageApp3.GarageLibrary.Helpers
{
    public static class ConsoleHelper
    {
        public static (string?, StreamWriter?) ReadCreateTextFile(string prompt)
        {
            StreamWriter? streamWriter = null;

            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    var path = Console.ReadLine();

                    if (string.IsNullOrEmpty(path))
                    {
                        return (null, null);
                    }

                    if (Path.Exists(path))
                    {
                        Console.WriteLine($"The file {path} already exists.");

                        var replaceContents = ReadYesNo("Do you want to replace its contents ('y', 'n')? ");

                        if (replaceContents)
                        {
                            streamWriter = File.CreateText(path);
                        }
                    }
                    else
                    {
                        streamWriter = File.CreateText(path);
                    }

                    return (path, streamWriter);
                }
                catch (IOException ex)
                {
                    WriteErrorLine(ex.Message);
                }
            }
        }

        public static TEnum ReadEnum<TEnum>(string prompt) where TEnum : struct
        {
            string? valueInput;
            TEnum value;

            do
            {
                Console.Write(prompt);
                valueInput = Console.ReadLine();
            }
            while (Enum.TryParse<TEnum>(valueInput, out value) == false);

            return value;
        }

        public static string? ReadFilePath(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    var path = Console.ReadLine();

                    if (string.IsNullOrEmpty(path))
                    {
                        return null;
                    }

                    //var fileName = Path.GetFileName(path);
                    var directoryName = Path.GetDirectoryName(path);

                    if (Directory.Exists(directoryName) == false)
                    {
                        throw new DirectoryNotFoundException($"Could not find the directory '{directoryName}'.");
                    }

                    if (Path.Exists(path) == false)
                    {
                        throw new FileNotFoundException($"Could not find the file '{path}'.");
                    }

                    return path;
                }
                catch (DirectoryNotFoundException ex)
                {
                    WriteErrorLine(ex.Message);
                }
                catch (FileNotFoundException ex)
                {
                    WriteErrorLine(ex.Message);
                }
            }
        }

        public static float ReadFloat(string prompt, Func<float, bool> func)
        {
            string? valueInput;
            float value;

            do
            {
                Console.Write(prompt);
                valueInput = Console.ReadLine();
            }
            while (float.TryParse(valueInput, out value) == false || func(value));

            return value;
        }

        public static int ReadInt(string prompt, Func<float, bool> func)
        {
            string? valueInput;
            int value;

            do
            {
                Console.Write(prompt);
                valueInput = Console.ReadLine();
            }
            while (int.TryParse(valueInput, out value) == false || func(value));

            return value;
        }

        public static string ReadRegistrationNumber(string prompt)
        {
            string? registrationNumber;

            do
            {
                Console.Write(prompt);
                registrationNumber = Console.ReadLine();
            }
            while (registrationNumber != null && RegistrationNumberValidator.Validate(registrationNumber.ToUpper()) == false);

            return registrationNumber!;
        }

        public static bool ReadYesNo(string prompt)
        {
            string? answerInput;

            do
            {
                Console.Write(prompt);
                answerInput = Console.ReadLine();
                answerInput = answerInput?.Trim().ToLower();
            }
            while (answerInput != null
                && answerInput.Equals("y", StringComparison.CurrentCultureIgnoreCase) == false
                && answerInput.Equals("yes", StringComparison.CurrentCultureIgnoreCase) == false
                && answerInput.Equals("n", StringComparison.CurrentCultureIgnoreCase) == false
                && answerInput.Equals("no", StringComparison.CurrentCultureIgnoreCase) == false);

            return answerInput!.Equals("y", StringComparison.CurrentCultureIgnoreCase)
                || answerInput!.Equals("yes", StringComparison.CurrentCultureIgnoreCase);
        }

        public static void WriteErrorLine(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WriteInfoLine(string message)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WriteSuccessLine(string message)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
