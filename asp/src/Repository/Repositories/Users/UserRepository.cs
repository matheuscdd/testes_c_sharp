using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Domain.Exceptions.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories.Users;

public class UserRepository: IUserRepository
{
    private readonly UserManager<User> _userManager;
    // private readonly SignInManager _signInManager;
    // private readonly ITokenService _tokenService;

    public UserRepository(
        UserManager<User> userManager
        // ITokenService tokenService, 
        // SignInManager<User> signInManager
    )
    {
        _userManager = userManager;
        // _tokenService = tokenService;
        // _signInManager = signInManager;
    }

    public async Task<IReadOnlyCollection<User>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var user = await getUserAsync(id, cancellationToken);
        if (user == null)
        {
            throw new NotFoundUserException();
        }
        return user;
    }

    private async Task<User?> getUserAsync(string id, CancellationToken cancellationToken)
    {
        return await Task.Run(() => _userManager.FindByIdAsync(id), cancellationToken);
    }

    public async Task<User> CreateAsync(
        User entity, 
        string password,
        CancellationToken cancellationToken = default
    )
    {
        var usernameExists = await checkUserNameExists(entity.UserName);
        if (usernameExists) {
            throw new ConflictUserException($"{nameof(entity.UserName)} already exists");
        }

        var queryResult = await Task.Run(() => _userManager.CreateAsync(entity, password), cancellationToken);

        if (queryResult.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(entity, "Common"); // Aqui é a role definida no context
                if (roleResult.Succeeded)
                {
                    return entity;
                }
                else
                {
                    throw new ValidationUserException(string.Join(";", roleResult.Errors));
                }
            } 
        else
        {
            throw new ValidationUserException(string.Join(";", queryResult.ToString()));
        }
    }

    // public async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken = default)
    // {
    //     _context.Update(entity);
    //     await _context.SaveChangesAsync(cancellationToken);
    //     return entity;
    // } 

    public async Task<User> DeleteAsync(string id,
        CancellationToken cancellationToken = default)
    {
        var user = await getUserAsync(id, cancellationToken);
        if (user == null)
        {
            throw new NotFoundUserException();
        }
        // TODO - ajustar
        // _context.Remove(user);
        // await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User?> GetByUserNameAsync(string username)
    {
        return await _userManager.Users.FirstOrDefaultAsync(el => el.UserName == username);
    }

    private async Task<bool> checkUserNameExists(string username)
    {
        return await _userManager.Users.AnyAsync(el => el.UserName == username);
    }
}

