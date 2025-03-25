using Application.Contexts.Stocks.Dtos;
using MediatR;

namespace Application.Contexts.Stocks.Commands.Create;

public class CreateStockCommand : IRequest<StockDto>
{
    public string? Symbol { get; set; }
    public string? CompanyName { get; set; }
    public decimal? Purchase { get; set; }
    public decimal? LastDiv { get; set; }
    public string? Industry { get; set; }
    public long? MarketCap { get; set; }
}