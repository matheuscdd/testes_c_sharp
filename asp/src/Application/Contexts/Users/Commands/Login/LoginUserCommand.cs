using Application.Contexts.Users.Dtos;
using MediatR;

namespace Application.Contexts.Users.Commands.Login;

public class LoginUserCommand : IRequest<TokenDto> // aqui é a saída
{
    // o corpo é a entrada do dado
    public string? UserName { get; set; }
    public string? Password { get; set; }
}