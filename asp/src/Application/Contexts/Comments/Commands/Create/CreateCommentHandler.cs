using Application.Contexts.Comments.Dtos;
using Application.Contexts.Comments.Repositories;
using Application.Contexts.Stocks.Commands.Create;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Comments.Commands.Create;

public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CreateCommentHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto> Handle(
        CreateCommentCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new Comment(request.Title, request.Content, request.UserId, request.StockId);
        entity = await _commentRepository.CreateAsync(entity, cancellationToken);
        var dto = entity.Adapt<CommentDto>();
        return dto;
    }
}