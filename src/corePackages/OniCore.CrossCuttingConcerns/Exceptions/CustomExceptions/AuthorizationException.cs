namespace OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message) { }
    }
}
