using Domain.Entities;

namespace Application.Contexts.Users.Dtos;

public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    // TODO - precisar a questão da senha
    public UserDto() {}

    public UserDto(string username, string email)
    {
        UserName = username;
        Email = email;
    }

}