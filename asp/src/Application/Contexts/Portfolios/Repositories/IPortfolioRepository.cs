using Domain.Entities;

namespace Application.Contexts.Portfolios.Repositories;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolioAsync(string userId, CancellationToken cancellationToken = default);
    Task CreateAsync(Portfolio entityRequest, CancellationToken cancellationToken = default);
    Task DeleteAsync(Portfolio entity, CancellationToken cancellationToken = default);
    Task<bool> CheckUserWithStockExistsAsync(string userId, int stockId, CancellationToken cancellationToken = default);
    Task<Portfolio?> GetByStockWithUserAsync(string userId, int stockId, CancellationToken cancellationToken = default);
}