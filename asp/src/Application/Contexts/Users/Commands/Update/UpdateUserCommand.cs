using Application.Contexts.Users.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Contexts.Users.Commands.Update;

public class UpdateUserCommand: IRequest<UserDto>
{
    public required string Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
}