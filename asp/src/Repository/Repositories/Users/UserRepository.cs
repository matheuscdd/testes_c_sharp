using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Domain.Exceptions.Users;
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
        var user = await getUserAsync(id, cancellationToken);
        if (user == null)
        {
            throw new NotFoundUserException();
        }
        return user;
    }

    private async Task<User?> getUserAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.User.FindAsync([id, cancellationToken]);
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
        var user = await getUserAsync(id, cancellationToken);
        if (user == null)
        {
            throw new NotFoundUserException();
        }
        _context.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

}

