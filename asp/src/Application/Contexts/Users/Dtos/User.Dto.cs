using Domain.Entities;

namespace Application.Contexts.Users.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public UserDto() {}

    public UserDto(string name, DateTime birthDate, Gender gender, int id)
    {
        Id = id;
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
    }

}