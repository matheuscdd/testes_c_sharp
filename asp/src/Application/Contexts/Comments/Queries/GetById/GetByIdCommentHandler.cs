using Application.Contexts.Comments.Dtos;
using Application.Contexts.Comments.Repositories;
using Mapster;
using MediatR;

namespace Application.Contexts.Comments.Queries.GetById;

public class GetByIdCommentHandler: IRequestHandler<GetByIdCommentQuery, CommentDto?>
{
    private readonly ICommentRepository _commentRepository;
    public GetByIdCommentHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto?> Handle(
        GetByIdCommentQuery request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _commentRepository.GetByIdAsync(request.Id,  cancellationToken);
        var dto = entity.Adapt<CommentDto>();
        return dto;
    }
}