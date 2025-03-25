using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Portfolios")]
public class Portfolio: Entity
{
    public string UserId { get; private set; }
    public int StockId { get; private set; }
    public Stock? Stock { get; set; }
    public User? User { get; set; }

    // TODO - inserir validações no construtor
    public Portfolio(string userId, int stockId)
    {
        UserId = userId;
        StockId = stockId;
    }
}