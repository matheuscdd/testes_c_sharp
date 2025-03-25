using MediatR;

namespace Application.Contexts.Portfolios.Commands.Create;

public class DeletePortfolioCommand: IRequest
{
    public required string UserId { get; set; }
    public required int StockId { get; set; }
    public DeletePortfolioCommand(string userId, int stockId)
    {
        UserId = userId;
        StockId = stockId;
    }

    public DeletePortfolioCommand() {}
}