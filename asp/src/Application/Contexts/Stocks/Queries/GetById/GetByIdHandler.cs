using Application.Contexts.Stocks.Dtos;
using Application.Contexts.Stocks.Repositories;
using Mapster;
using MediatR;

namespace Application.Contexts.Stocks.Queries.GetById;

public class GetUserByIdHandler: IRequestHandler<GetStockByIdQuery, StockDto?>
{
    private readonly IStockRepository _stockRepository;
    public GetUserByIdHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<StockDto?> Handle(
        GetStockByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _stockRepository.GetByIdWithCommentsAsync(request.Id,  cancellationToken);
        var dto = entity.Adapt<StockDto>();
        return dto;
    }
}