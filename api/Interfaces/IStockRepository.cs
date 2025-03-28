using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

// TODO Depois trocar pelo repository de escrita e leitura
namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryParams queryParams);
        Task<Stock?> GetByIdWithCommentsAsync(int id);
        Task<Stock?> GetByIdWithoutCommentsAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, Stock stockRequest);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
