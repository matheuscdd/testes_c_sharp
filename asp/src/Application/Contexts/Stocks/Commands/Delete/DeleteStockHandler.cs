using Application.Contexts.Stocks.Repositories;
using Domain.Exceptions;
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
        var entity = await _stockRepository.GetByIdCommentsAsync(request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundCustomException("Stock not found");
        }
        await _stockRepository.DeleteAsync(entity, cancellationToken);
    }
}