using Application.Contexts.Comments.Dtos;
using MediatR;

namespace Application.Contexts.Comments.Queries.GetById;

public class GetCommentByIdQuery: IRequest<CommentDto?>
{
    public int Id { get; set; }
    public GetCommentByIdQuery(int id)
    {
        Id = id;
    }

    public GetCommentByIdQuery() {}
}