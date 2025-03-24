using System.Net;

namespace Domain.Exceptions.Users;

public class ValidationUserException : BaseException
{
    public ValidationUserException(string message = "Validation Error") 
        : base(message, (int)HttpStatusCode.BadRequest) { }
}