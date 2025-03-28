using MediatR;

namespace Application.Contexts.Stocks.Commands.Delete;

public class DeleteStockCommand: IRequest
{
    public int Id { get; set; }
    public DeleteStockCommand(int id)
    {
        Id = id;
    }

    public DeleteStockCommand() {}
}