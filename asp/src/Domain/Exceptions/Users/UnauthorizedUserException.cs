using System.Net;

namespace Domain.Exceptions.Users;

public class UnauthorizedUserException : BaseException
{
    public UnauthorizedUserException(string message = "Unauthorized") 
        : base(
            message, 
            (int) HttpStatusCode.Unauthorized,
            "https://tools.ietf.org/html/rfc9110#section-15.5.2"
          ) { }
}