using System.ComponentModel.DataAnnotations;
using Domain.Exceptions.Users;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = [];
    // [Required]
    // public Gender Gender { get; private set; }
    // public bool IsActive { get; private set; }
    protected User(){ }

    // TODO ajustar construtor
    public User(string username, string email)
    {
        Email = email;
        UserName = username;
    }

    public void ChangeUsername(string username)
    {
        UserName = username;
    }

    public void ChangeEmail(string email)
    {
        Email = email;
    }

    // TODO - trocar password
    // public void ChangeEmail(string email)
    // {
    //     Email = email;
    // }

    // public void Activate()
    // {
    //     if (IsActive) 
    //     {
    //         throw new InvalidOperationException("User is already active");
    //     }

    //     IsActive = true;
    // }

    // public void Deactivate()
    // {
    //     if (!IsActive)
    //     {
    //         throw new InvalidOperationException("User already is not active");
    //     }
    //     IsActive = false;
    // }
}