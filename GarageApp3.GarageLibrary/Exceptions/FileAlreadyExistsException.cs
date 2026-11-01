namespace GarageApp3.GarageLibrary.Exceptions
{
    public class FileAlreadyExistsException(string path) : IOException($"The file '{path}' already exists.")
    {
    }
}
