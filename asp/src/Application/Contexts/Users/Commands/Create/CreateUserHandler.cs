using System.Net;
using Application.Contexts.Users.Dtos;
using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Users.Commands.Create;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var usernameExists = await _userRepository.CheckUserNameExists(request.UserName, cancellationToken);
        if (usernameExists)
        {
            throw new ConflictCustomException($"{nameof(request.UserName)} already exists");
        }
        var (entity, errors) = await _userRepository.CreateAsync(
            new User(request.UserName, request.Email, request.Password),
            request.Password,
            cancellationToken
        );
        if (entity == null) 
        {
            throw new ValidationCustomException(string.Join(";", errors!));
        }
        var dto = entity.Adapt<UserDto>();
        return dto;
    }
}