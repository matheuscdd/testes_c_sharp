using Application.Contexts.Users.Dtos;
using Application.Contexts.Users.Repositories;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.Contexts.Users.Commands.Update;

public class UpdateUserHandler: IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, 
        CancellationToken cancellationToken)
    {
        var usernameExists = await _userRepository.CheckUserNameExists(request.UserName, cancellationToken);
        if (usernameExists)
        {
            throw new ConflictCustomException($"{nameof(request.UserName)} already exists");
        }
        var entity = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        entity.SetEmail(request.Email);
        entity.SetUsername(request.UserName);
        entity = await _userRepository.UpdateAsync(entity, cancellationToken);
        var dto = entity.Adapt<UserDto>();
        return dto;
    }
}