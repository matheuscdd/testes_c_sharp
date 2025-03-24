using Application.Contexts.Users.Dtos;
using Application.Contexts.Users.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Contexts.Users.Queries.GetAll;

public class GetAllUserHandler: IRequestHandler<GetAllUserQuery, IReadOnlyCollection<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IReadOnlyCollection<UserDto>> Handle(
        GetAllUserQuery request, CancellationToken cancellationToken
    )
    {
        var entities = await _userRepository.GetAllAsync(cancellationToken);
        // TODO trocar por mapper
        return entities.Select(el => new UserDto(el.Name, el.BirthDate, el.Gender)).ToList();
    }
}