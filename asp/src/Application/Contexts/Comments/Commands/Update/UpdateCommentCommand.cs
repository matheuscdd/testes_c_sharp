using Application.Contexts.Comments.Dtos;
using MediatR;

namespace Application.Contexts.Comments.Commands.Update;

public class UpdateCommentCommand : IRequest<CommentDto>
{
    public required int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public required int StockId { get; set; }
    public required string UserId { get; set; }
}