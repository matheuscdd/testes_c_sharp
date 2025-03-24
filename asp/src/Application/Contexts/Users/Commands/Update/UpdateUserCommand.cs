using Application.Contexts.Users.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Contexts.Users.Commands.Update;

public class UpdateUserCommand: IRequest<UserDto>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
}