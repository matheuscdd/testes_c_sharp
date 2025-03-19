using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserRepository(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager) : IUserRepository
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly ITokenService _tokenService = tokenService;


        public async Task<string?> Login(User userModel, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(userModel, password, false);
            if (!result.Succeeded)
            {
                return null;
            }

            return _tokenService.CreateToken(userModel);
        }
        public async Task<(User?, IEnumerable<IdentityError>? Errors)> CreateAsync(User userModel, string password)
        {
            var queryResult = await _userManager.CreateAsync(userModel, password);

            if (queryResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(userModel, "Common"); // Aqui é a role definida no context
                    if (roleResult.Succeeded)
                    {
                        return (userModel, null);
                    }
                    else
                    {
                        return (null, roleResult.Errors);
                    }
                } 
            else
            {
                return (null, queryResult.Errors);
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User?> GetByUserNameAsync(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(el => el.UserName == username);
        }
    }
}

