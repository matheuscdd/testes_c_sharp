using MediatR;

namespace Application.Contexts.Users.Commands.DeleteById;

public class DeleteUserByIdCommand: IRequest
{
    public int Id { get; set; }
    public DeleteUserByIdCommand(int id)
    {
        Id = id;
    }

    public DeleteUserByIdCommand()
    {
    }
}