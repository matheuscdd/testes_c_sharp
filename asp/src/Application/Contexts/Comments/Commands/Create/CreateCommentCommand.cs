using Application.Contexts.Comments.Dtos;
using MediatR;

namespace Application.Contexts.Comments.Commands.Create;

public class CreateCommentCommand : IRequest<CommentDto>
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int StockId { get; set; }
    public string? UserId { get; set; }
}