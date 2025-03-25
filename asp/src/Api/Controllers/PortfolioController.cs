using System.Security.Claims;
using Application.Contexts.Portfolios.Commands.Create;
using Application.Contexts.Portfolios.Queries.GetUserPortfolio;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/portfolios")]
public class PortfolioController: ControllerBase
{
    private readonly IMediator _mediator;

    public PortfolioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var response = await _mediator.Send(new GetUserPortfolioQuery{UserId = userId});
        return Ok(response);
    }

    [HttpPost("{stockId:int}")]
    [Authorize]
    public async Task<IActionResult> Create(
        [FromRoute] int stockId
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _mediator.Send(new CreatePortfolioCommand{UserId = userId, StockId = stockId});
        return NoContent();
    }

    [HttpDelete("{stockId:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(
        [FromRoute] int stockId
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _mediator.Send(new DeletePortfolioCommand{UserId = userId, StockId = stockId});
        return NoContent();
    }
}