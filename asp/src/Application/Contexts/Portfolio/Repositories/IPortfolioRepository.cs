using Domain.Entities;

namespace Application.Contexts.Portfolios.Repositories;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolioAsync(string id, CancellationToken cancellationToken = default);
    Task CreateAsync(Portfolio entityRequest, CancellationToken cancellationToken = default);
    Task DeleteAsync(string userId, int stockId, CancellationToken cancellationToken = default);
}