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
        Task<bool> PortfolioExistsAsync(User userModel, Stock stockModel);
        Task<Portfolio> CreateAsync(Portfolio portfolioRequest);
        Task<Portfolio?> DeleteAsync(User userModel, Stock stockModel);
    }
}
