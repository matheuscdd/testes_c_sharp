using Application.Contexts.Portfolios.Repositories;
using Application.Contexts.Stocks.Dtos;
using Application.Contexts.Users.Dtos;
using Mapster;
using MediatR;

namespace Application.Contexts.Portfolios.Queries.GetUserPortfolio;

public class GetUserPortfolioHandler: IRequestHandler<GetUserPortfolioQuery, IReadOnlyCollection<StockDtoWithComments>>
{
    private readonly IPortfolioRepository _portfolioRepository;
    public GetUserPortfolioHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<IReadOnlyCollection<StockDtoWithComments>> Handle(
        GetUserPortfolioQuery request,
        CancellationToken cancellationToken
    )
    {
        var entities = await _portfolioRepository.GetUserPortfolioAsync(request.UserId,  cancellationToken);
        var dtos = entities.Adapt<IReadOnlyCollection<StockDtoWithComments>>();
        return dtos;
    }
}
