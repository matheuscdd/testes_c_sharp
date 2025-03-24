namespace Domain.Entities;

public class User : Entity
{
    public string Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public bool IsActive { get; private set; }
    protected User(){ }
    public User(string name, DateTime birthDate, Gender gender)
    {
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
        IsActive = true;
    }

    public void Activate()
    {
        if (IsActive) 
        {
            throw new InvalidOperationException("User is already active");
        }

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("User already is not active");
        }
        IsActive = false;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeBirthDate(DateTime birthDate)
    {
        BirthDate = birthDate;
    }
}