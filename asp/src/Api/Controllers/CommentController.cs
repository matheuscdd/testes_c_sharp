using System.Security.Claims;
using Application.Contexts.Comments.Commands.Create;
using Application.Contexts.Comments.Commands.Delete;
using Application.Contexts.Comments.Commands.Update;
using Application.Contexts.Comments.Queries.GetAll;
using Application.Contexts.Comments.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentController: ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllCommentQuery());
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await _mediator.Send(new GetByIdCommentQuery(id));
        return Ok(response);
    }

    [HttpPost("{stockId:int}")]
    [Authorize]
    public async Task<IActionResult> Create(
        [FromRoute] int stockId,
        [FromBody] CreateCommentCommand createCommentCommand
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        createCommentCommand.UserId = userId;
        createCommentCommand.StockId = stockId;
        var response = await _mediator.Send(createCommentCommand);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UpdateCommentCommand updateCommentCommand
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        updateCommentCommand.UserId = userId;
        updateCommentCommand.Id = id;
        var response = await _mediator.Send(updateCommentCommand);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(
        [FromRoute] int id
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _mediator.Send(new DeleteCommentCommand(id, userId!));
        return NoContent();
    }
}