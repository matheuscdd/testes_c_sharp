using System.Security.Claims;
using Application.Contexts.Stocks.Commands.Create;
using Application.Contexts.Stocks.Commands.Delete;
using Application.Contexts.Stocks.Commands.Update;
using Application.Contexts.Stocks.Queries.GetAll;
using Application.Contexts.Stocks.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/stocks")]
public class StockController: ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly IMediator _mediator;

    public StockController(ILogger<StockController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetAllStockQuery getAllStockQuery
    )
    {
        var response = await _mediator.Send(getAllStockQuery);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await _mediator.Send(new GetByIdStockQuery(id));
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(
        [FromBody] CreateStockCommand createStockCommand
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var response = await _mediator.Send(createStockCommand);
        _logger.LogInformation($"Stock Created - UserId: {userId}");
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UpdateStockCommand updateStockCommand
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        updateStockCommand.Id = id;
        var response = await _mediator.Send(updateStockCommand);
        _logger.LogInformation($"Stock {id} Updated - UserId: {userId}");
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _mediator.Send(new DeleteStockCommand(id));
        _logger.LogInformation($"Stock {id} Deleted - UserId: {userId}");
        return NoContent();
    }
}