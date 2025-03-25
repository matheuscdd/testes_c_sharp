using Application.Contexts.Users.Dtos;
using Application.Contexts.Users.Repositories;
using Mapster;
using MediatR;

namespace Application.Contexts.Users.Queries.GetById;

public class GetUserByIdHandler: IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _userRepository.GetByIdAsync(request.Id,  cancellationToken);
        var dto = entity.Adapt<UserDto>();
        return dto;
    }
}
