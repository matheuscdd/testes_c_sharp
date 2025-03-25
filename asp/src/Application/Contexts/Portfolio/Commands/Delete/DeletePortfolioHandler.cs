using Application.Contexts.Portfolios.Repositories;
using Application.Contexts.Stocks.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Contexts.Portfolios.Commands.Create;

public class DeletePortfolioHandler: IRequestHandler<CreatePortfolioCommand>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IStockRepository _stockRepository;

    public DeletePortfolioHandler(IPortfolioRepository portfolioRepository, IStockRepository stockRepository)
    {
        _portfolioRepository = portfolioRepository;
        _stockRepository = stockRepository;
    }

    public async Task Handle(
        CreatePortfolioCommand request,
        CancellationToken cancellationToken
    )
    {
        var stockExists = await _stockRepository.CheckIdExists(request.StockId);
        if (!stockExists) {
            throw new NotFoundCustomException("Stock does not exist");
        }
        await _portfolioRepository.DeleteAsync(request.UserId, request.StockId, cancellationToken);
    }
}