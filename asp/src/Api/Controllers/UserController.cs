using System.Security.Claims;
using Application.Contexts.Users.Commands.Create;
using Application.Contexts.Users.Commands.Delete;
using Application.Contexts.Users.Commands.Login;
using Application.Contexts.Users.Commands.Update;
using Application.Contexts.Users.Queries.GetAll;
using Application.Contexts.Users.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController: ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;

    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllUserQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var response = await _mediator.Send(new GetByIdUserQuery{Id = id});
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserCommand createUserCommand
    )
    {
        var response = await _mediator.Send(createUserCommand);
        _logger.LogInformation("User Created");
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromBody] UpdateUserCommand updateUserCommand
    )
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        updateUserCommand.Id = id;
        var response = await _mediator.Send(updateUserCommand);
        _logger.LogInformation($"User {id} Updated - UserId: {id}");
        return Ok(response);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _mediator.Send(new DeleteUserCommand(id));
        _logger.LogInformation($"User {id} Deleted - UserId: {id}");
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
    {
        var response = await _mediator.Send(loginUserCommand);
        return Ok(response);
    }
}

