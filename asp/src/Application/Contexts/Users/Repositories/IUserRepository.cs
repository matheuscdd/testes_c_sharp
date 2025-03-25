using Domain.Entities;

namespace Application.Contexts.Users.Repositories;

public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(User entity, string password, CancellationToken cancellationToken = default);
    // Task<User> UpdateAsync(User entity, CancellationToken cancellationToken = default);
    Task<User> DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<User?> GetByUserNameAsync(string username);
}