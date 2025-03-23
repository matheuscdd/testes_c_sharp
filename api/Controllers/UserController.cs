using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModels = await _userRepository.GetAllAsync();
            var usersDto = userModels.Select(el => el.ToUserDto()).ToList();

            return Ok(usersDto);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto userDto)
        {
            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }  

                var userModel = await _userRepository.GetByUserNameAsync(userDto.UserName!.ToLower());
                if (userModel == null)
                {
                    return Unauthorized();
                }

                var token = await _userRepository.Login(userModel, userDto.Password!);
                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(new Dictionary<string, string>
                {
                    { "token", token }
                });
            }
            catch(Exception e) 
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            _logger.LogWarning("Keep request");
            _logger.LogInformation("Keep request");

            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }      

                var (userModel, errors) = await _userRepository.CreateAsync(
                    new User{ UserName = userDto.UserName!.ToLower(),  Email = userDto.Email!.ToLower()}, 
                    userDto.Password!
                );

                if (userModel == null)
                {
                    return BadRequest(errors);
                }

                _logger.LogInformation("User created");
                return CreatedAtAction(nameof(GetById), new { id = userModel!.Id }, userModel.ToUserDto());

            } 
            catch(Exception e) 
            {
                _logger.LogError("Unespected error");
                return BadRequest(e);
            }
        }
    }
}