using System.Net;

namespace Domain.Exceptions.Users;

public class UnauthorizedUserException : BaseException
{
    public UnauthorizedUserException(string message = "Unauthorized") 
        : base(message, (int)HttpStatusCode.Unauthorized) { }
}