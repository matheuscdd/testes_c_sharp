using Application.Contexts.Comments.Dtos;
using Application.Contexts.Comments.Repositories;
using Domain.Entities;
using Domain.Exceptions;
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
        var entityStorage = await _commentRepository.GetByIdAndUserAsync(request.Id, request.UserId, cancellationToken);
        if (entityStorage == null)
        {
            throw new NotFoundCustomException("Comment not found");
        }

        var entityRequest = new Comment(request.Title, request.Content, request.UserId, request.StockId);
        entityRequest = await _commentRepository.UpdateAsync(entityStorage, entityRequest, cancellationToken);
        var dto = entityRequest.Adapt<CommentDto>();
        return dto;
    }
}