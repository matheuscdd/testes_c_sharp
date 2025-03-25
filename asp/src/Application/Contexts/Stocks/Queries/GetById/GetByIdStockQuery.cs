using Application.Contexts.Stocks.Dtos;
using MediatR;

namespace Application.Contexts.Stocks.Queries.GetById;

public class GetByIdStockQuery: IRequest<StockDto?>
{
    public int Id { get; set; }
    public GetByIdStockQuery(int id)
    {
        Id = id;
    }

    public GetByIdStockQuery() {}
}