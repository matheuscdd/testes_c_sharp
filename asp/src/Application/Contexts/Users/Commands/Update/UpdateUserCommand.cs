using Application.Contexts.Users.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Contexts.Users.Commands.Update;

public class UpdateUserCommand: IRequest<UserDto>
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}