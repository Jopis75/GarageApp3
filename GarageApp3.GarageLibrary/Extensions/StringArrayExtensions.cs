namespace GarageApp3.GarageLibrary.Extensions
{
    public static class StringArrayExtensions
    {
        public static string[] TrimFields(this string[] fields)
        {
            string[] result = new string[fields.Length];

            for (var i = 0; i < fields.Length; i++)
            {
                result[i] = fields[i].Trim();
            }

            return result;
        }
    }
}
