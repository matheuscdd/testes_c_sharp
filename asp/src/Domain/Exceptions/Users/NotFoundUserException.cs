using System.Net;

namespace Domain.Exceptions.Users;

public class NotFoundUserException : BaseException
{
    public NotFoundUserException(string message = "User Not Found") 
        : base(message, (int)HttpStatusCode.NotFound) { }
}