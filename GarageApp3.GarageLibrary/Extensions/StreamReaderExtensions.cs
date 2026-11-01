namespace GarageApp3.GarageLibrary.Extensions
{
    public static class StreamReaderExtensions
    {
        public static string[] ReadAllLines(this StreamReader streamReader)
        {
            var lines = new List<string>();

            var line = string.Empty;
            
            while ((line = streamReader.ReadLine()) != null)
            {
                lines.Add(line);
            }

            return lines.ToArray();
        }
    }
}
