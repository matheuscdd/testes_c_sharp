using Microsoft.EntityFrameworkCore;
using Application.Contexts.Users.Repositories;
using Repository.Context;
using Domain.Entities;

namespace Repository.Repositories.Portfolios;

public class PortfolioRepository(ApplicationDbContext context) : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> PortfolioExistsAsync(User userModel, Stock stockModel, CancellationToken cancellationToken = default)
        {
            return await _context.Portfolios
                .AnyAsync(el => el.UserId == userModel.Id && el.StockId == stockModel.Id, cancellationToken);
        }

        public async Task<List<Stock>> GetUserPortfolioAsync(User userModel, CancellationToken cancellationToken = default)
        {
            return await _context.Portfolios
                .Where(u => u.UserId == userModel.Id)
                .Select(combo => combo.Stock!).ToListAsync(cancellationToken);
        }

        public async Task<Portfolio> CreateAsync(Portfolio entityRequest, CancellationToken cancellationToken = default)
        {
            await _context.Portfolios.AddAsync(entityRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entityRequest;
        }

        public async Task<Portfolio?> DeleteAsync(string userId, int stockId, CancellationToken cancellationToken = default)
        {
            var portfolioStorage = await _context.Portfolios
                .FirstOrDefaultAsync(el => el.UserId == userId && el.StockId == stockId, cancellationToken);

            if (portfolioStorage == null)
            {
                return null;
            }

            _context.Portfolios.Remove(portfolioStorage);
            await _context.SaveChangesAsync(cancellationToken);
            
            return portfolioStorage;
        }
    }