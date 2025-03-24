using System.ComponentModel.DataAnnotations;
using Domain.Exceptions.Users;

namespace Domain.Entities;

public abstract class Entity
{
    public int Id { get; protected set; }

    protected void Validate()
    {
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        
        bool isValid = Validator.TryValidateObject(this, context, results, true);
        
        if (!isValid)
        {
            throw new ValidationUserException(string.Join("; ", results.ConvertAll(r => r.ErrorMessage)));
        }
    }
}