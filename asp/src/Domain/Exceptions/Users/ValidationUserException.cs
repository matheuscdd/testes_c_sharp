using System.Net;

namespace Domain.Exceptions.Users;

public class ValidationUserException : BaseException
{
    public ValidationUserException(string message = "Validation Error") 
        : base(
            message, 
            (int) HttpStatusCode.BadRequest,
            "https://tools.ietf.org/html/rfc9110#section-15.5.1"
          ) { }
}