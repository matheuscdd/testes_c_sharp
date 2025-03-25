using MediatR;

namespace Application.Contexts.Users.Commands.DeleteById;

public class DeleteUserByIdCommand: IRequest
{
    public string Id { get; set; }
    public DeleteUserByIdCommand(string id)
    {
        Id = id;
    }

    public DeleteUserByIdCommand() {}
}