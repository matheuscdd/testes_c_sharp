using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories.Users;

public class UserRepository: IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public UserRepository(
        UserManager<User> userManager,
        ITokenService tokenService, 
        SignInManager<User> signInManager
    )
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    public async Task<IReadOnlyCollection<User>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<bool> CheckIdExists(string id, CancellationToken cancellationToken = default)
    {
        return await _userManager.Users.AnyAsync(el => el.Id == id, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await Task.Run(() => _userManager.FindByIdAsync(id), cancellationToken);
    }

    public async Task<(User?, string?)> CreateAsync(
        User entityRequest, 
        string password,
        CancellationToken cancellationToken = default
    )
    {
        var queryResult = await Task.Run(() => _userManager.CreateAsync(entityRequest, password), cancellationToken);

        if (queryResult.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(entityRequest, "Common"); // Aqui é a role definida no context
                if (roleResult.Succeeded)
                {
                    return (entityRequest, null);
                }
                else
                {
                    return (null, string.Join(";", roleResult.Errors));
                }
            } 
        else
        {
            return (null, queryResult.ToString());
        }
    }

    public async Task<User> UpdateAsync(User entityRequest, CancellationToken cancellationToken = default)
    {
        await _userManager.UpdateAsync(entityRequest);
        return entityRequest;
    } 

    public async Task<User> DeleteAsync(User entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _userManager.DeleteAsync(entity), cancellationToken);
        return entity;
    }

    public async Task<User?> GetByUserNameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _userManager.Users.FirstOrDefaultAsync(el => el.UserName == username, cancellationToken);
    }

    public async Task<bool> CheckUserNameExists(string username, CancellationToken cancellationToken = default)
    {
        return await _userManager.Users.AnyAsync(el => el.UserName == username, cancellationToken);
    }

    public async Task<string?> Login(string username, string password, CancellationToken cancellationToken = default)
    {
        var entity = await GetByUserNameAsync(username, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(entity, password, false);
        if (!result.Succeeded)
        {
            return null;
        }

        return _tokenService.CreateToken(entity);
    }
}

