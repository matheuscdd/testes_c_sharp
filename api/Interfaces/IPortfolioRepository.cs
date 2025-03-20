using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolioAsync(User userModel);
        // Task<Stock?> GetByIdAsync(int id);
        // Task<Stock> CreateAsync(Stock stockModel);
        // Task<Stock?> UpdateAsync(int id, Stock stockRequest);
        // Task<Stock?> DeleteAsync(int id);
        // Task<bool> StockExists(int id);
    }
}
