using Application.Contexts.Stocks.Commands.Create;
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
    private readonly IMediator _mediator;

    public StockController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize] // TODO talvez não funcione pois ele não tem as chaves direto
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
        var response = await _mediator.Send(createStockCommand);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UpdateStockCommand updateStockCommand
    )
    {
        updateStockCommand.Id = id;
        var response = await _mediator.Send(updateStockCommand);
        return Ok(response);
    }
}