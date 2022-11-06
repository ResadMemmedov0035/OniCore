namespace OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("The entity was not found") { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string objectName, object key) : base($"The \"{objectName}\" ({key}) was not found.") { }
    }
}
