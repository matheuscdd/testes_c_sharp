using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Domain.Entities;
using Application.Contexts.Comments.Repositories;

namespace Repository.Repositories.Comments;

public class CommentRepository(ApplicationDbContext context) : ICommentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Comment> CreateAsync(Comment entityRequest, CancellationToken cancellationToken = default)
    {
        await _context.Comments.AddAsync(entityRequest, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entityRequest;
    }

    public async Task<Comment?> DeleteAsync(Comment entity, CancellationToken cancellationToken = default)
    {
        _context.Comments.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<List<Comment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Comments.Include(el => el.User).ToListAsync(cancellationToken);
    }

    public async Task<Comment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Comments.Include(el => el.User).FirstOrDefaultAsync(el => el.Id == id, cancellationToken);
    }

    public async Task<Comment?> GetByIdAndUserAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Comments.FirstOrDefaultAsync(el => el.Id == id && el.UserId == userId, cancellationToken);
    }

    public async Task<Comment?> UpdateAsync(Comment entityStorage, Comment entityRequest, CancellationToken cancellationToken = default)
    {
        entityStorage.SetTitle(entityRequest.Title);
        entityStorage.SetContent(entityRequest.Content);

        await _context.SaveChangesAsync(cancellationToken);

        return entityStorage;
    }
}