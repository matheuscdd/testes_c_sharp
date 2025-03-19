using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Dtos.Comment;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]

    public class UserController(UserManager<User> userManager) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            try {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }      

                var userModel = new User
                {
                    UserName = userDto.Username,
                    Email = userDto.Email,
                };

                var queryResult = await _userManager.CreateAsync(userModel, userDto.Password!);

                if (queryResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(userModel, "Common"); // Aqui é a role definida no context
                    if (roleResult.Succeeded)
                    {
                        return Ok("TODO - change");
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }
                } 
                else
                {
                    return BadRequest(queryResult.Errors);
                }

            } catch(Exception e) {
                return BadRequest(e);
            }
            
        }
    }
}