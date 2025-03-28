namespace Application.Contexts.Users.Dtos;

public class TokenDto
{
    public string Token { get; set; }
    public TokenDto() {}

    public TokenDto(string token)
    {
        Token = token;
    }

}