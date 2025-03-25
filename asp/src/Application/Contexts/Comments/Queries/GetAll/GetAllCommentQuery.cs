using Application.Contexts.Comments.Dtos;
using MediatR;

namespace Application.Contexts.Comments.Queries.GetAll;

public class GetAllCommentQuery: IRequest<IReadOnlyCollection<CommentDto>> 
{
    public GetAllCommentQuery() {}
}