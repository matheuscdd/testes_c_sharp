using Application.Contexts.Stocks.Dtos;
using Application.Contexts.Stocks.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Stocks.Commands.Update;

public class UpdateStockHandler : IRequestHandler<UpdateStockCommand, StockDto>
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public UpdateStockHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<StockDto> Handle(
        UpdateStockCommand request,
        CancellationToken cancellationToken
    )
    {
        var entityStorage = await _stockRepository.GetByIdCommentsAsync(request.Id, cancellationToken);
        if (entityStorage == null)
        {
            throw new NotFoundCustomException("Stock not found");
        }

        var entityRequest = new Stock(
            request.Symbol,
            request.CompanyName,
            request.Purchase,
            request.LastDiv,
            request.Industry,
            request.MarketCap
        );
        
        entityRequest = await _stockRepository.UpdateAsync(entityStorage, entityRequest, cancellationToken);
        var dto = entityRequest.Adapt<StockDto>();
        return dto;
    }
}