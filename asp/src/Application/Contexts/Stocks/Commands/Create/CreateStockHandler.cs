using Application.Contexts.Stocks.Dtos;
using Application.Contexts.Stocks.Repositories;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Stocks.Commands.Create;

public class CreateStockHandler : IRequestHandler<CreateStockCommand, StockDto>
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public CreateStockHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<StockDto> Handle(
        CreateStockCommand request,
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
        entity = await _stockRepository.CreateAsync(entity, cancellationToken);
        var dto = entity.Adapt<StockDto>();
        return dto;
    }
}