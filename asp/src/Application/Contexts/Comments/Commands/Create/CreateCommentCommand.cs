using Application.Contexts.Comments.Dtos;
using MediatR;

namespace Application.Contexts.Comments.Commands.Create;

public class CreateCommentCommand : IRequest<CommentDto>
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public required int StockId { get; set; }
    public required string UserId { get; set; }
}