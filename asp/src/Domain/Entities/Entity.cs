using Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Entities;

public abstract class Entity
{
    public int Id { get; protected set; }

    protected void validateEmpty(string? value, string name)
    {
        if (value.IsNullOrEmpty()) 
        {
            throw new ValidationCustomException($"{name} cannot be empty");
        }
    }

    protected void validateLength(string value, string name, int min, int max)
    {
        if (value.Length > max) {
            throw new ValidationCustomException($"{name} cannot be greater than {max} characters");
        } else if (value.Length < min) {
            throw new ValidationCustomException($"{name} cannot be smaller than {min} characters");
        }
    }
}