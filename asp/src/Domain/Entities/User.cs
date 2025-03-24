using System.ComponentModel.DataAnnotations;
using Domain.Exceptions.Users;

namespace Domain.Entities;

public class User : Entity
{
    [Required]
    [MinLength(3)]
    public string Name { get; private set; }

    [Required]
    public DateTime BirthDate { get; private set; }

    [Required]
    public Gender Gender { get; private set; }
    public bool IsActive { get; private set; }
    protected User(){ }
    public User(string name, DateTime birthDate, Gender gender)
    {
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
        IsActive = true;
        Validate();
    }

    private void Validate()
    {
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        
        bool isValid = Validator.TryValidateObject(this, context, results, true);
        
        if (!isValid)
        {
            throw new ValidationUserException(string.Join("; ", results.ConvertAll(r => r.ErrorMessage)));
        }
    }

    public void Activate()
    {
        if (IsActive) 
        {
            throw new InvalidOperationException("User is already active");
        }

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("User already is not active");
        }
        IsActive = false;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeBirthDate(DateTime birthDate)
    {
        BirthDate = birthDate;
    }
}