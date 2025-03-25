using Application.Contexts.Portfolios.Repositories;
using Application.Contexts.Stocks.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Contexts.Portfolios.Commands.Create;

public class CreatePortfolioHandler: IRequestHandler<CreatePortfolioCommand>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IStockRepository _stockRepository;

    public CreatePortfolioHandler(IPortfolioRepository portfolioRepository, IStockRepository stockRepository)
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
        var entity = new Portfolio(request.UserId, request.StockId);
        await _portfolioRepository.CreateAsync(entity, cancellationToken);
    }
}