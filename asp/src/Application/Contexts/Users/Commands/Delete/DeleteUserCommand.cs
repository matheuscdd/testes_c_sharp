using MediatR;

namespace Application.Contexts.Users.Commands.Delete;

public class DeleteUserCommand: IRequest
{
    public string Id { get; set; }
    public DeleteUserCommand(string id)
    {
        Id = id;
    }

    public DeleteUserCommand() {}
}