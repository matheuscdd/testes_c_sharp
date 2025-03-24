using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.Repositories.Users;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<User>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.User.ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.User.FindAsync(id);
    }

    public async Task<User> AddAsync(User entity,
        CancellationToken cancellationToken = default)
    {
        await _context.User.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    } 

    public async Task<User> DeleteAsync(int id,
        CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        _context.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

}

