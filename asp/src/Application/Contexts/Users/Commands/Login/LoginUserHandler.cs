using Application.Contexts.Users.Dtos;
using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Users.Commands.Login;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, TokenDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public LoginUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<TokenDto> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new User(request.UserName, request.Password);
        var token = await _userRepository.Login(entity.UserName!, request.Password, cancellationToken);
        if (token == null)
        {
            throw new UnauthorizedCustomException();
        } 
        return new TokenDto(token);
    }
}