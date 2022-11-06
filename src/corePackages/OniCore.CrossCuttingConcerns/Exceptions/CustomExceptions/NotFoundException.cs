namespace OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string objectName, string key) : base($"The entity \"{objectName}\" ({key}) was not found.") { }
    }
}
