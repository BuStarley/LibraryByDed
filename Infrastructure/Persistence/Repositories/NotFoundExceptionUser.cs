
namespace Infrastructure.Persistence.Repositories
{
    [Serializable]
    internal class NotFoundExceptionUser : Exception
    {
        public NotFoundExceptionUser()
        {
        }

        public NotFoundExceptionUser(string? message) : base(message)
        {
        }

        public NotFoundExceptionUser(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}