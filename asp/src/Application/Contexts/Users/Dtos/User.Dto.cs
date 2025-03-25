namespace Application.Contexts.Users.Dtos;

public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserDto() {}

    public UserDto(string username, string email)
    {
        UserName = username;
        Email = email;
    }

}