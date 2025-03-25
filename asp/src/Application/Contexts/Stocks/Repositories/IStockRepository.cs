using Application.Contexts.Stocks.Queries.GetAll;
using Domain.Entities;

namespace Application.Contexts.Stocks.Repositories;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(GetAllStockQueryParams queryParams, CancellationToken cancellationToken = default);
    Task<Stock?> GetByIdWithCommentsAsync(int id, CancellationToken cancellationToken = default);
    Task<Stock?> GetByIdWithoutCommentsAsync(int id, CancellationToken cancellationToken = default);
    Task<Stock> CreateAsync(Stock entityRequest, CancellationToken cancellationToken = default);
    Task<Stock?> UpdateAsync(int id, Stock entityRequest, CancellationToken cancellationToken = default);
    Task<Stock?> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> CheckIdExists(int id, CancellationToken cancellationToken = default);
}