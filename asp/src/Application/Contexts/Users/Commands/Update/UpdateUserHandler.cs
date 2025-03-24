using Application.Contexts.Users.Dtos;
using Application.Contexts.Users.Repositories;
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
        var entity = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
        {
            throw new ArgumentException("Entity not found");
        }

        if (request.Gender != entity.Gender)
        {
            throw new ArgumentException("Gender not match");
        }

        if (!entity.IsActive)
        {
            throw new ArgumentException("User is not active");
        }

        entity.ChangeName(request.Name);
        entity.ChangeBirthDate(request.BirthDate);
        entity = await _userRepository.UpdateAsync(entity, cancellationToken);
        var dto = entity.Adapt<UserDto>();
        return dto;
    }
}