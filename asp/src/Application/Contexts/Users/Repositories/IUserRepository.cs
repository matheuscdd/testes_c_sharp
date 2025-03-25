using Domain.Entities;

namespace Application.Contexts.Users.Repositories;

public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(User entityRequest, string password, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(User entityRequest, CancellationToken cancellationToken = default);
    Task<User> DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<User?> GetByUserNameAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> CheckIdExists(string id, CancellationToken cancellationToken = default);
    Task<string> Login(string username, string password, CancellationToken cancellationToken = default);
}