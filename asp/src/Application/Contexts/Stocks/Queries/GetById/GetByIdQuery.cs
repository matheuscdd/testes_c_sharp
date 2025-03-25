using Application.Contexts.Stocks.Dtos;
using MediatR;

namespace Application.Contexts.Stocks.Queries.GetById;

public class GetStockByIdQuery: IRequest<StockDto?>
{
    public int Id { get; set; }
    public GetStockByIdQuery(int id)
    {
        Id = id;
    }

    public GetStockByIdQuery() {}
}