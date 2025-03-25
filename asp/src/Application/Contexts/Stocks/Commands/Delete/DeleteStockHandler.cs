using Application.Contexts.Stocks.Repositories;
using MediatR;

namespace Application.Contexts.Stocks.Commands.Delete;

public class DeleteStockHandler: IRequestHandler<DeleteStockCommand>
{
    private readonly IStockRepository _stockRepository;

    public DeleteStockHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task Handle(
        DeleteStockCommand request,
        CancellationToken cancellationToken
    )
    {
        await _stockRepository.DeleteAsync(request.Id, cancellationToken);
    }
}