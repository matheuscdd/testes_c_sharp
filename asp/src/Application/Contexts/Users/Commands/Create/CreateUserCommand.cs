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
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
}