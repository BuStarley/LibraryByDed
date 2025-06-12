namespace Application.Exceptions
{
    [Serializable]
    internal class RegistrationException : Exception
    {
        public RegistrationException()
        {
        }

        public RegistrationException(string? message) : base(message)
        {
        }

        public RegistrationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}