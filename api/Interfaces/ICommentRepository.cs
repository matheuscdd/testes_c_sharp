using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

// TODO Depois trocar pelo repository de escrita e leitura
namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        // Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto);
        // Task<Comment?> DeleteAsync(int id);
    }
}
