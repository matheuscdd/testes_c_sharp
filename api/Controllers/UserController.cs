using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]

    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModels = await _userRepository.GetAllAsync();
            var usersDto = userModels.Select(el => el.ToUserDto());

            return Ok(usersDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = await _userRepository.GetByIdAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel.ToUserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            try {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }      

                var (userModel, errors) = await _userRepository.CreateAsync(
                    new User{ UserName = userDto.Username,  Email = userDto.Email!.ToLower()}, 
                    userDto.Password!
                );

                if (userModel == null)
                {
                    return BadRequest(errors);
                }

                return CreatedAtAction(nameof(GetById), new { id = userModel!.Id }, userModel.ToUserDto());

            } catch(Exception e) {
                Console.WriteLine(e);
                return BadRequest(e);
            }
            
        }
    }
}