using Application.Contexts.Stocks.Dtos;
using Application.Contexts.Stocks.Repositories;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.Contexts.Stocks.Queries.GetById;

public class GetByIdStockHandler: IRequestHandler<GetByIdStockQuery, StockDtoWithComments?>
{
    private readonly IStockRepository _stockRepository;
    public GetByIdStockHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<StockDtoWithComments?> Handle(
        GetByIdStockQuery request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _stockRepository.GetByIdWithCommentsAsync(request.Id,  cancellationToken);
        if (entity == null)
        {
            throw new NotFoundCustomException("Stock not found");
        }
        var dto = entity.Adapt<StockDtoWithComments>();
        return dto;
    }
}