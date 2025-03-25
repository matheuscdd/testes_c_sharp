using System.Net;
using Application.Contexts.Users.Dtos;
using Application.Contexts.Users.Repositories;
using Domain.Entities;
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
        var entity = new User(request.UserName, request.Email, request.Password);
        entity = await _userRepository.CreateAsync(entity, request.Password, cancellationToken);        
        var dto = entity.Adapt<UserDto>();
        return dto;
    }
}