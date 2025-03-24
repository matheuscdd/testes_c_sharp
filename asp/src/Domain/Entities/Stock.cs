using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Stocks")]
public class Stock: Entity
{
    public string Symbol { get; private set; }
    public string CompanyName { get; private set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Purchase { get; private set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LastDiv { get; private set; }

    public string Industry { get; private set; }
    public long MarketCap { get; private set; }
    public List<Comment> Comments { get; set; } = [];
    public List<Portfolio> Portfolios { get; set; } = [];

    protected Stock(
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