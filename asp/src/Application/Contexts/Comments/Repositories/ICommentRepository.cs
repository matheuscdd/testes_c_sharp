using Domain.Entities;

namespace Application.Contexts.Comments.Repositories;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Comment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Comment?> GetByIdAndUserAsync(int id, string userId, CancellationToken cancellationToken = default);
    Task<Comment> CreateAsync(Comment entityRequest, CancellationToken cancellationToken = default);
    Task<Comment?> UpdateAsync(Comment entityStorage, Comment entityRequest, CancellationToken cancellationToken = default);
    Task<Comment?> DeleteAsync(Comment entity, CancellationToken cancellationToken = default);
}