using Application.Contexts.Stocks.GetAll;
using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Domain.Enums.Stocks;
using Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories.Stocks;

public class StockRepository: IStockRepository
{
    private readonly ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    private IQueryable<Stock> insertSort(IQueryable<Stock> stocks, GetAllStockQueryParams queryParams)
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

    public async Task<Stock> CreateAsync(Stock entityRequest, CancellationToken cancellationToken = default)
    {
        await _context.Stocks.AddAsync(entityRequest, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entityRequest;
    }

    public async Task<Stock?> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var stockStorage = await _context.Stocks.FindAsync(id, cancellationToken);
        if (stockStorage == null) 
        {
            return null;
        }

        _context.Stocks.Remove(stockStorage);
        await _context.SaveChangesAsync(cancellationToken);

        return stockStorage;
    }

    public async Task<List<Stock>> GetAllAsync(GetAllStockQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        var stocks = _context.Stocks.Include(el => el.Comments).ThenInclude(el => el.User).AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(queryParams.CompanyName)) 
        {
            stocks = stocks.Where(el => el.CompanyName.Contains(queryParams.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Symbol))
        {
            stocks = stocks.Where(el => el.Symbol.Contains(queryParams.Symbol));
        }

        stocks = insertSort(stocks, queryParams);

        var skipNumber = (queryParams.PageNumber - 1) * queryParams.PageSize;

        return await stocks.Skip(skipNumber).Take(queryParams.PageSize).ToListAsync(cancellationToken);
    }

    public async Task<Stock?> GetByIdWithCommentsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Stocks
            .Include(el => el.Comments)
            .ThenInclude(el => el.User)
            .FirstOrDefaultAsync(el => el.Id == id, cancellationToken);
    }

    public async Task<Stock?> GetByIdWithoutCommentsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Stocks.FindAsync(id, cancellationToken);
    }

    public async Task<bool> CheckIdExists(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Stocks.AnyAsync(el => el.Id == id, cancellationToken);
    }

    public async Task<Stock?> UpdateAsync(int id, Stock entityRequest, CancellationToken cancellationToken = default)
    {
        var stockStorage = await _context.Stocks
            .Include(el => el.Comments)
            .ThenInclude(el => el.User)
            .FirstOrDefaultAsync(el => el.Id == id, cancellationToken);
        if (stockStorage == null) 
        {
            return null;
        }
        
        stockStorage.SetSymbol(entityRequest.Symbol);
        stockStorage.SetCompanyName(entityRequest.CompanyName);
        stockStorage.SetPurchase(entityRequest.Purchase);
        stockStorage.SetLastDiv(entityRequest.LastDiv);
        stockStorage.SetIndustry(entityRequest.Industry);
        stockStorage.SetMarketCap(entityRequest.MarketCap);

        await _context.SaveChangesAsync(cancellationToken);

        return stockStorage;
    }
}