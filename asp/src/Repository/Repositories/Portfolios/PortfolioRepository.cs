using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Domain.Entities;
using Application.Contexts.Portfolios.Repositories;

namespace Repository.Repositories.Portfolios;

public class PortfolioRepository(ApplicationDbContext context) : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> CheckUserWithStockExistsAsync(string userId, int stockId, CancellationToken cancellationToken = default)
        {
            return await _context.Portfolios
                .AnyAsync(el => el.UserId == userId && el.StockId == stockId, cancellationToken);
        }

        public async Task<Portfolio?> GetByStockWithUserAsync(string userId, int stockId, CancellationToken cancellationToken = default)
        {
            return await _context.Portfolios
                .FirstOrDefaultAsync(el => el.UserId == userId && el.StockId == stockId, cancellationToken);
        }

        public async Task<List<Stock>> GetUserPortfolioAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Portfolios
                .Where(u => u.UserId == userId)
                .Select(combo => combo.Stock!).ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(Portfolio entityRequest, CancellationToken cancellationToken = default)
        {
            await _context.Portfolios.AddAsync(entityRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Portfolio entity, CancellationToken cancellationToken = default)
        {
            _context.Portfolios.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
}