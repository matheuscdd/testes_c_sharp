using Application.Contexts.Stocks.Dtos;
using Application.Contexts.Stocks.Repositories;
using Mapster;
using MediatR;

namespace Application.Contexts.Stocks.Queries.GetAll;

public class GetAllStockHandler: IRequestHandler<GetAllStockQuery, IReadOnlyCollection<StockDtoWithComments>>
{
    private readonly IStockRepository _stockRepository;
    public GetAllStockHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<IReadOnlyCollection<StockDtoWithComments>> Handle(
        GetAllStockQuery request, CancellationToken cancellationToken
    )
    {
        // TODO tentar usar mapsters
        var queryParams = new GetAllStockQueryParams(request.Symbol, request.CompanyName, request.SortBy, request.IsDescending, request.PageNumber, request.PageSize);
        var entities = await _stockRepository.GetAllAsync(queryParams, cancellationToken);
        var dtos = entities.Adapt<IReadOnlyCollection<StockDtoWithComments>>();
        return dtos;
    }
}