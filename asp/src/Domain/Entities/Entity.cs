using System.ComponentModel.DataAnnotations;
using Domain.Exceptions.Users;

namespace Domain.Entities;

public abstract class Entity
{
    public int Id { get; protected set; }
}