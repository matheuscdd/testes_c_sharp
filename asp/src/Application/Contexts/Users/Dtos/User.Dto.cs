using Domain.Entities;

namespace Application.Contexts.Users.Dtos;

public class UserDto
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public UserDto()
    {
    }

    public UserDto(string name, DateTime birthDate, Gender gender)
    {
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
    }

}