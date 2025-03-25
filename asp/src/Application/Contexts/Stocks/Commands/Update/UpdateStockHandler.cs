using Application.Contexts.Stocks.Dtos;
using Application.Contexts.Stocks.Repositories;
using Domain.Entities;
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
        var entity = new Stock(
            request.Symbol,
            request.CompanyName,
            request.Purchase,
            request.LastDiv,
            request.Industry,
            request.MarketCap
        );
        entity = await _stockRepository.UpdateAsync(request.Id, entity, cancellationToken);
        var dto = entity.Adapt<StockDto>();
        return dto;
    }
}