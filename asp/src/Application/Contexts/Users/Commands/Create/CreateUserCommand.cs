using System.ComponentModel.DataAnnotations;
using System.Formats.Tar;
using Application.Contexts.Users.Dtos;
using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Users.Commands.Create;

public class CreateUserCommand : IRequest<UserDto>
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}