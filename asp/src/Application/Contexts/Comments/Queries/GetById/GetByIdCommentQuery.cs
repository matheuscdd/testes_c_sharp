using Application.Contexts.Comments.Dtos;
using MediatR;

namespace Application.Contexts.Comments.Queries.GetById;

public class GetByIdCommentQuery: IRequest<CommentDto?>
{
    public int Id { get; set; }
    public GetByIdCommentQuery(int id)
    {
        Id = id;
    }

    public GetByIdCommentQuery() {}
}