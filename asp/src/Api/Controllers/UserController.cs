using System.Security.Claims;
using Application.Contexts.Users.Commands.Create;
using Application.Contexts.Users.Commands.Delete;
using Application.Contexts.Users.Commands.Login;
using Application.Contexts.Users.Commands.Update;
using Application.Contexts.Users.Queries.GetAll;
using Application.Contexts.Users.Queries.GetById;
using Domain.Exceptions.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController: ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
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
        var response = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserCommand createUserCommand
    )
    {
        var response = await _mediator.Send(createUserCommand);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromBody] UpdateUserCommand updateUserCommand
    )
    {
        updateUserCommand.Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var response = await _mediator.Send(updateUserCommand);
        return Ok(response);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
    {
        var response = await _mediator.Send(loginUserCommand);
        return Ok(response);
    }
}

