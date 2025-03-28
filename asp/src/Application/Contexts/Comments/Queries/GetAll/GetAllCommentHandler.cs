using Application.Contexts.Comments.Dtos;
using Application.Contexts.Comments.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Contexts.Comments.Queries.GetAll;

public class GetAllCommentHandler : IRequestHandler<GetAllCommentQuery,
    IReadOnlyCollection<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;

    public GetAllCommentHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IReadOnlyCollection<CommentDto>> Handle(
        GetAllCommentQuery request, CancellationToken cancellationToken)
    {
        var entities = await _commentRepository.GetAllAsync(cancellationToken);
        var dtos = entities.Adapt<IReadOnlyCollection<CommentDto>>();
        return dtos;
    }
}