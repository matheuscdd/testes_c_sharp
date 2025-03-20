using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Enums;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<bool> PortfolioExistsAsync(User userModel, Stock stockModel)
        {
            return await _context.Portfolios
                .AnyAsync(el => el.UserId == userModel.Id && el.StockId == stockModel.Id);
        }

        public async Task<List<Stock>> GetUserPortfolioAsync(User userModel)
        {
            return await _context.Portfolios
                .Where(u => u.UserId == userModel.Id)
                .Select(combo => combo.Stock!).ToListAsync();
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolioRequest)
        {
            await _context.Portfolios.AddAsync(portfolioRequest);
            await _context.SaveChangesAsync();

            return portfolioRequest;
        }

        public async Task<Portfolio?> DeleteAsync(User userModel, Stock stockModel)
        {
            var portfolioStorage = await _context.Portfolios
                .FirstOrDefaultAsync(el => el.UserId == userModel.Id && el.StockId == stockModel.Id);

            if (portfolioStorage == null)
            {
                return null;
            }

            _context.Portfolios.Remove(portfolioStorage);
            await _context.SaveChangesAsync();
            
            return portfolioStorage;
        }
    }
}

