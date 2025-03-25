using Domain.Entities;

namespace Application.Contexts.Users.Repositories;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Comment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Comment> CreateAsync(Comment entityRequest, CancellationToken cancellationToken = default);
    Task<Comment?> UpdateAsync(int id, Comment entityRequest, CancellationToken cancellationToken = default);
    Task<Comment?> DeleteAsync(int id, CancellationToken cancellationToken = default);
}