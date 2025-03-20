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

        public async Task<List<Stock>> GetUserPortfolioAsync(User userModel)
        {
            return await _context.Portfolios.Where(u => u.UserId == userModel.Id)
            .Select(combo => combo.Stock!).ToListAsync();
        }
    }
}

