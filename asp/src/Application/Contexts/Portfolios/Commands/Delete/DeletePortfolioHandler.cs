using Application.Contexts.Portfolios.Repositories;
using Application.Contexts.Stocks.Repositories;
using Domain.Exceptions;
using MediatR;

namespace Application.Contexts.Portfolios.Commands.Delete;

public class DeletePortfolioHandler: IRequestHandler<DeletePortfolioCommand>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IStockRepository _stockRepository;

    public DeletePortfolioHandler(IPortfolioRepository portfolioRepository, IStockRepository stockRepository)
    {
        _portfolioRepository = portfolioRepository;
        _stockRepository = stockRepository;
    }

    public async Task Handle(
        DeletePortfolioCommand request,
        CancellationToken cancellationToken
    )
    {
        var stockExists = await _stockRepository.CheckIdExists(request.StockId);
        if (!stockExists) 
        {
            throw new NotFoundCustomException("Stock not found");
        }

        var entity = await _portfolioRepository.GetByStockWithUserAsync(request.UserId, request.StockId, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundCustomException("User does not have this stock");
        }
        await _portfolioRepository.DeleteAsync(entity, cancellationToken);
    }
}