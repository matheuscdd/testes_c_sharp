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

    public async Task<Comment?> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var commentStorage = await _context.Comments.FindAsync(id, cancellationToken);
        if (commentStorage == null) 
        {
            return null;
        }

        _context.Comments.Remove(commentStorage);
        await _context.SaveChangesAsync(cancellationToken);

        return commentStorage;
    }

    public async Task<List<Comment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Comments.Include(el => el.User).ToListAsync(cancellationToken);
    }

    public async Task<Comment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Comments.Include(el => el.User).FirstOrDefaultAsync(el => el.Id == id, cancellationToken);
    }

    public async Task<Comment?> UpdateAsync(int id, Comment entityRequest, CancellationToken cancellationToken = default)
    {
        var commentStorage = await _context.Comments.Include(el => el.User).FirstOrDefaultAsync(el => el.Id == id, cancellationToken);
        if (commentStorage == null) 
        {
            return null;
        }

        commentStorage.SetTitle(entityRequest.Title);
        commentStorage.SetContent(entityRequest.Content);

        await _context.SaveChangesAsync(cancellationToken);

        return commentStorage;
    }
}