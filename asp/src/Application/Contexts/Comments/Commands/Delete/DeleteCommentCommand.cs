using MediatR;

namespace Application.Contexts.Comments.Commands.Delete;

public class DeleteCommentCommand: IRequest
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DeleteCommentCommand(int id, string userId)
    {
        Id = id;
        UserId = userId;
    }

    public DeleteCommentCommand() {}
}