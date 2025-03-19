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
using Microsoft.OpenApi.Any;

namespace api.Repositories
{
    public class StockRepository(ApplicationDBContext context) : IStockRepository
    {
        private readonly ApplicationDBContext _context = context;

        private static IQueryable<Stock> insertSort(IQueryable<Stock> stocks, QueryParams queryParams)
        {
            return queryParams.SortBy switch
            {
                ESortBy.Id => queryParams.IsDescending ? stocks.OrderByDescending(el => el.Id) : stocks.OrderBy(el => el.Id),
                ESortBy.Symbol => queryParams.IsDescending ? stocks.OrderByDescending(el => el.Symbol) : stocks.OrderBy(el => el.Symbol),
                ESortBy.CompanyName => queryParams.IsDescending ? stocks.OrderByDescending(el => el.CompanyName) : stocks.OrderBy(el => el.CompanyName),
                ESortBy.Industry => queryParams.IsDescending ? stocks.OrderByDescending(el => el.Industry) : stocks.OrderBy(el => el.Industry),
                ESortBy.LastDiv => queryParams.IsDescending ? stocks.OrderByDescending(el => el.LastDiv) : stocks.OrderBy(el => el.LastDiv),
                ESortBy.MarketCap => queryParams.IsDescending ? stocks.OrderByDescending(el => el.MarketCap) : stocks.OrderBy(el => el.MarketCap),
                ESortBy.Purchase => queryParams.IsDescending ? stocks.OrderByDescending(el => el.Purchase) : stocks.OrderBy(el => el.Purchase),
                _ => stocks,
            };
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockStorage = await _context.Stocks.FindAsync(id);
            if (stockStorage == null) 
            {
                return null;
            }

            _context.Stocks.Remove(stockStorage);
            await _context.SaveChangesAsync();

            return stockStorage;
        }

        public async Task<List<Stock>> GetAllAsync(QueryParams queryParams)
        {
            var stocks = _context.Stocks.Include(el => el.Comments).AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(queryParams.CompanyName)) 
            {
                stocks = stocks.Where(el => el.CompanyName.Contains(queryParams.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(queryParams.Symbol))
            {
                stocks = stocks.Where(el => el.Symbol.Contains(queryParams.Symbol));
            }

            stocks = insertSort(stocks, queryParams);
    
            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(el => el.Comments).FirstOrDefaultAsync(el => el.Id == id);
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(el => el.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, Stock stockRequest)
        {
            var stockStorage = await _context.Stocks.FindAsync(id);
            if (stockStorage == null) 
            {
                return null;
            }

            stockStorage.Symbol = stockRequest.Symbol;
            stockStorage.CompanyName = stockRequest.CompanyName;
            stockStorage.Purchase = stockRequest.Purchase;
            stockStorage.LastDiv = stockRequest.LastDiv;
            stockStorage.Industry = stockRequest.Industry;
            stockStorage.MarketCap = stockRequest.MarketCap;

            await _context.SaveChangesAsync();

            return stockStorage;
        }
    }
}

