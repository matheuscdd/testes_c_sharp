using Application.Contexts.Comments.Dtos;
using Application.Contexts.Comments.Repositories;
using Mapster;
using MediatR;

namespace Application.Contexts.Comments.Queries.GetById;

public class GetUserByIdHandler: IRequestHandler<GetCommentByIdQuery, CommentDto?>
{
    private readonly ICommentRepository _commentRepository;
    public GetUserByIdHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto?> Handle(
        GetCommentByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _commentRepository.GetByIdAsync(request.Id,  cancellationToken);
        var dto = entity.Adapt<CommentDto>();
        return dto;
    }
}