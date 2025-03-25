using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Domain.Entities;
using Application.Contexts.Portfolios.Repositories;
using Domain.Exceptions;

namespace Repository.Repositories.Portfolios;

public class PortfolioRepository(ApplicationDbContext context) : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context = context;

        private async Task<bool> portfolioExistsAsync(string userId, int stockId, CancellationToken cancellationToken = default)
        {
            return await _context.Portfolios
                .AnyAsync(el => el.UserId == userId && el.StockId == stockId, cancellationToken);
        }

        public async Task<List<Stock>> GetUserPortfolioAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Portfolios
                .Where(u => u.UserId == id)
                .Select(combo => combo.Stock!).ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(Portfolio entityRequest, CancellationToken cancellationToken = default)
        {
            var exists = await portfolioExistsAsync(entityRequest.UserId, entityRequest.StockId, cancellationToken);
            if (exists)
            {
                throw new ConflictCustomException("User already has this stock");
            }
            await _context.Portfolios.AddAsync(entityRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string userId, int stockId, CancellationToken cancellationToken = default)
        {
            var portfolioStorage = await _context.Portfolios
                .FirstOrDefaultAsync(el => el.UserId == userId && el.StockId == stockId, cancellationToken);

            if (portfolioStorage == null)
            {
                throw new NotFoundCustomException("User does not have this stock");
            }

            _context.Portfolios.Remove(portfolioStorage);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }