using Application.Contexts.Users.Commands.Create;
using Application.Contexts.Users.Commands.DeleteById;
using Application.Contexts.Users.Commands.Update;
using Application.Contexts.Users.Queries.GetAll;
using Application.Contexts.Users.Queries.GetById;
using MediatR;
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
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] string id,
        [FromBody] UpdateUserCommand updateUserCommand
    )
    {
        updateUserCommand.Id = id;
        var response = await _mediator.Send(updateUserCommand);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await _mediator.Send(new DeleteUserByIdCommand(id));
        return NoContent();
    }
}

