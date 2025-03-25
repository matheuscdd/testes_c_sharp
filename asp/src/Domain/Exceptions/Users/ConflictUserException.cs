using System.Net;

namespace Domain.Exceptions.Users;

public class ConflictUserException : BaseException
{
    public ConflictUserException(string message = "Conflict") 
        : base(
            message, 
            (int) HttpStatusCode.Conflict, 
            "https://tools.ietf.org/html/rfc9110#section-15.5.10"
        ) { }
}