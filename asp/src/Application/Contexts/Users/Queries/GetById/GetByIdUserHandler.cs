using Application.Contexts.Users.Dtos;
using Application.Contexts.Users.Repositories;
using Mapster;
using MediatR;

namespace Application.Contexts.Users.Queries.GetById;

public class GetByIdUserHandler: IRequestHandler<GetByIdUserQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;
    public GetByIdUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(
        GetByIdUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _userRepository.GetByIdAsync(request.Id,  cancellationToken);
        var dto = entity.Adapt<UserDto>();
        return dto;
    }
}
