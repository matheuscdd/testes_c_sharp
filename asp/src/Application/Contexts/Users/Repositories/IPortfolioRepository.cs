using Domain.Entities;

namespace Application.Contexts.Users.Repositories;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolioAsync(User userModel, CancellationToken cancellationToken = default);
    Task<bool> PortfolioExistsAsync(User userModel, Stock stockModel, CancellationToken cancellationToken = default);
    Task<Portfolio> CreateAsync(Portfolio entityRequest, CancellationToken cancellationToken = default);
    Task<Portfolio?> DeleteAsync(string userId, int stockId, CancellationToken cancellationToken = default);
}