using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Domain.Entities;

public class User : IdentityUser
{

    public List<Portfolio> Portfolios { get; set; } = [];
    protected User(){ }

    public User(string username, string password)
    {
        validatePassword(password);
        validateUserName(username);
        SetUsername(username);
    }

    public User(string username, string email, string password)
    {
        validatePassword(password);
        validateUserName(username);
        validateEmail(email);
        SetUsername(username);
        SetEmail(email);
    }

    public void SetUsername(string? username)
    {
        validateUserName(username);
        UserName = username;
    }

    public void SetEmail(string email)
    {
        validateEmail(email);
        Email = email.ToLower();
    }

    private void validateEmpty(string? value, string name)
    {
        if (value.IsNullOrEmpty()) 
        {
            throw new ValidationCustomException($"{name} cannot be empty");
        }
    }

    private void validateLength(string value, string name, int min, int max)
    {
        if (value.Length > max) {
            throw new ValidationCustomException($"{name} cannot be greater than {max} characters");
        } else if (value.Length < min) {
            throw new ValidationCustomException($"{name} cannot be smaller than {min} characters");
        }
    }

    private void validateEmailFormat(string email, string name)
    {
        if (new EmailAddressAttribute().IsValid(email)) return;
        throw new ValidationCustomException($"{name} is invalid");
    }

    private void validatePasswordFormat(string password, string name)
    {
        if (!password.Any(char.IsNumber))
        {
            throw new ValidationCustomException($"{name} must have at least one digit");
        }
        else if (!password.Any(char.IsLetter))
        {
            throw new ValidationCustomException($"{name} must have at least one letter");
        }
        else if (!password.Any(char.IsUpper))
        {
            throw new ValidationCustomException($"{name} must have at least one uppercase letter");
        }
        else if (!password.Any(char.IsLower))
        {
            throw new ValidationCustomException($"{name} must have at least one lowercase letter");
        }
        else if (!password.Any(c => !char.IsLetterOrDigit(c)))
        {
            throw new ValidationCustomException($"{name} must have at least one non alphanumeric character");
        }
    }

    private void validatePassword(string? password)
    {
        const string name = "Password";
        validateEmpty(password, name);
        validateLength(password!, name, 12, 150);
        validatePasswordFormat(password!, name);
    }
    
    private void validateEmail(string? email)
    {
        const string name = nameof(Email);
        validateEmpty(email, name);
        validateLength(email!, name, 5, 100);
        validateEmailFormat(email!, name);
    }

    private void validateUserName(string? username)
    {
        const string name = nameof(UserName);
        validateEmpty(username, name);
        validateLength(username!, name, 6, 30);
    }
}
