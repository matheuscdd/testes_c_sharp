using Application.Contexts.Comments.Dtos;
using Application.Contexts.Comments.Repositories;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Comments.Commands.Update;

public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public UpdateCommentHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto> Handle(
        UpdateCommentCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new Comment(request.Title, request.Content, request.UserId, request.StockId);
        entity = await _commentRepository.UpdateAsync(request.Id, entity, cancellationToken);
        var dto = entity.Adapt<CommentDto>();
        return dto;
    }
}