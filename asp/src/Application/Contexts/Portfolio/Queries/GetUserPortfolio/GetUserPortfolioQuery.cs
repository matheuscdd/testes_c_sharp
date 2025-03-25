using Application.Contexts.Stocks.Dtos;
using Application.Contexts.Users.Dtos;
using MediatR;

namespace Application.Contexts.Portfolios.Queries.GetUserPortfolio;

public class GetUserPortfolioQuery: IRequest<IReadOnlyCollection<StockDto>>
{
    public required string UserId { get; set; }
    public GetUserPortfolioQuery() {}
    public GetUserPortfolioQuery(string userId)
    {
        UserId = userId;
    }

}