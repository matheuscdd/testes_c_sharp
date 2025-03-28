using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Contexts.Users.Repositories;

public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<(User?, string?)> CreateAsync(User entityRequest, string password, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(User entityRequest, CancellationToken cancellationToken = default);
    Task<User> DeleteAsync(User entity, CancellationToken cancellationToken = default);
    Task<User?> GetByUserNameAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> CheckIdExists(string id, CancellationToken cancellationToken = default);
    Task<bool> CheckUserNameExists(string username, CancellationToken cancellationToken = default);
    Task<string?> Login(string username, string password, CancellationToken cancellationToken = default);
}