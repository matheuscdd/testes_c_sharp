using Application.Contexts.Comments.Dtos;
using Application.Contexts.Comments.Repositories;
using Application.Contexts.Stocks.Commands.Create;
using Application.Contexts.Stocks.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Comments.Commands.Create;

public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public CreateCommentHandler(
        ICommentRepository commentRepository,
        IStockRepository stockRepository
    )
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }

    public async Task<CommentDto> Handle(
        CreateCommentCommand request,
        CancellationToken cancellationToken
    )
    {
        var stockModel = await _stockRepository.GetByIdAsync(request.StockId, cancellationToken);
        if (stockModel == null)
        {
            throw new NotFoundCustomException("Stock not found");
        }
        var entity = new Comment(request.Title, request.Content, request.UserId, request.StockId);
        entity = await _commentRepository.CreateAsync(entity, cancellationToken);
        var dto = entity.Adapt<CommentDto>();
        return dto;
    }
}