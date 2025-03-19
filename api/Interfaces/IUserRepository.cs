using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;
using Microsoft.AspNetCore.Identity;

// TODO Depois trocar pelo repository de escrita e leitura
namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByUserNameAsync(string username);
        Task<string?> Login(User userModel, string password);
        Task<(User?, IEnumerable<IdentityError>? Errors)> CreateAsync(User userModel, string password);
    }
}
