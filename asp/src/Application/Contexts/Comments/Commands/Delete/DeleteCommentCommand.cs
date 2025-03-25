using MediatR;

namespace Application.Contexts.Comments.Commands.Delete;

public class DeleteCommentCommand: IRequest
{
    public int Id { get; set; }
    public DeleteCommentCommand(int id)
    {
        Id = id;
    }

    public DeleteCommentCommand() {}
}