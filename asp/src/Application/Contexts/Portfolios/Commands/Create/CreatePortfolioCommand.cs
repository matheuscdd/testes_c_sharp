using MediatR;

namespace Application.Contexts.Portfolios.Commands.Create;

public class CreatePortfolioCommand: IRequest
{
    public required string UserId { get; set; }
    public required int StockId { get; set; }
    public CreatePortfolioCommand(string userId, int stockId)
    {
        UserId = userId;
        StockId = stockId;
    }

    public CreatePortfolioCommand() {}
}