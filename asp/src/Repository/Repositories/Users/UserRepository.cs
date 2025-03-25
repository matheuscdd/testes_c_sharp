using Application.Contexts.Users.Dtos;
using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Domain.Exceptions.Users;
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

    public async Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var user = await getUserAsync(id, cancellationToken);
        if (user == null)
        {
            throw new NotFoundUserException();
        }
        return user;
    }

    public async Task<bool> CheckIdExists(string id, CancellationToken cancellationToken = default)
    {
        return await _userManager.Users.AnyAsync(el => el.Id == id, cancellationToken);
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
        var usernameExists = await checkUserNameExists(entity.UserName!);
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

    public async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        var usernameExists = await checkUserNameExists(entity.UserName!);
        if (usernameExists) {
            throw new ConflictUserException($"{nameof(entity.UserName)} already exists");
        }
        await _userManager.UpdateAsync(entity);
        return entity;
    } 

    public async Task<User> DeleteAsync(string id,
        CancellationToken cancellationToken = default)
    {
        var entity = await getUserAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundUserException();
        }
        await Task.Run(() => _userManager.DeleteAsync(entity), cancellationToken);
        return entity;
    }

    public async Task<User?> GetByUserNameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _userManager.Users.FirstOrDefaultAsync(el => el.UserName == username, cancellationToken);
    }

    private async Task<bool> checkUserNameExists(string username)
    {
        return await _userManager.Users.AnyAsync(el => el.UserName == username);
    }

    public async Task<string> Login(string username, string password, CancellationToken cancellationToken = default)
    {
        var entity = await GetByUserNameAsync(username, cancellationToken);
        if (entity == null)
        {
            throw new UnauthorizedUserException();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(entity, password, false);
        if (!result.Succeeded)
        {
            throw new UnauthorizedUserException();
        }

        return _tokenService.CreateToken(entity);
    }
}

