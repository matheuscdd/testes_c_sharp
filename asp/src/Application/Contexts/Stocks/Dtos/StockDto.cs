using Application.Contexts.Comments.Dtos;
using Domain.Entities;

namespace Application.Contexts.Stocks.Dtos;

public class StockDto
{
    public int Id { get; set; }
    public string Symbol { get; set; }
    public string CompanyName { get; set; }
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; }
    public long MarketCap { get; set; }
    public List<CommentDto> Comments { get; set; } = [];

    public StockDto() {}
    public StockDto(
        string symbol, 
        string companyName, 
        decimal purchase, 
        decimal lastDiv, 
        string industry, 
        long marketCap
    )
    {
        Symbol = symbol;
        CompanyName = companyName;
        Purchase = purchase;
        LastDiv = lastDiv;
        Industry = industry;
        MarketCap = marketCap;
    }
}