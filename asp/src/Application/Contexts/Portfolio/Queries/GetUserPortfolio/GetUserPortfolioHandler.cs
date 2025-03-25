using Application.Contexts.Portfolios.Repositories;
using Application.Contexts.Stocks.Dtos;
using Application.Contexts.Users.Dtos;
using Mapster;
using MediatR;

namespace Application.Contexts.Portfolios.Queries.GetUserPortfolio;

public class GetUserPortfolioHandler: IRequestHandler<GetUserPortfolioQuery, IReadOnlyCollection<StockDto>>
{
    private readonly IPortfolioRepository _portfolioRepository;
    public GetUserPortfolioHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<IReadOnlyCollection<StockDto>> Handle(
        GetUserPortfolioQuery request,
        CancellationToken cancellationToken
    )
    {
        // TODO - depois tentar fazer tratativa para ocultar comentários
        var entities = await _portfolioRepository.GetUserPortfolioAsync(request.UserId,  cancellationToken);
        var dtos = entities.Adapt<IReadOnlyCollection<StockDto>>();
        return dtos;
    }
}
