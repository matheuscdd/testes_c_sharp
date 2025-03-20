using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolios")]
    public class PortfolioController(IUserRepository userRepository, IStockRepository stockRepository, IPortfolioRepository portfolioRepository) : ControllerBase
    {
        private readonly IStockRepository _stockRepository = stockRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;

        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var userModel = await _userRepository.GetByIdAsync(userId);
            if (userModel == null)
            {
                return Unauthorized();
            }

            var portfolioModel = await _portfolioRepository.GetUserPortfolioAsync(userModel);
            return Ok(portfolioModel);
        }

        [HttpPost("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int stockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var userModel = await _userRepository.GetByIdAsync(userId);
            if (userModel == null)
            {
                return Unauthorized();
            }

            var stockModel = await _stockRepository.GetByIdWithoutCommentsAsync(stockId);
            if (stockModel == null)
            {
                return NotFound();
            }

            if (await _portfolioRepository.PortfolioExistsAsync(userModel, stockModel))
            {
                return Problem(title: "User already has this stock", statusCode: StatusCodes.Status409Conflict);
            }

            var portfolioModel = new Portfolio
            {
                StockId = stockModel.Id,
                UserId = userModel.Id,
            };

            await _portfolioRepository.CreateAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return Problem(title: "Could not create", statusCode: StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int stockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var userModel = await _userRepository.GetByIdAsync(userId);
            if (userModel == null)
            {
                return Unauthorized();
            }

            var stockModel = await _stockRepository.GetByIdWithoutCommentsAsync(stockId);
            if (stockModel == null)
            {
                return Problem(title: "This stock does not exists", statusCode: StatusCodes.Status404NotFound);
            }

            var portfolioModel = await _portfolioRepository.DeleteAsync(userModel, stockModel);
            if (portfolioModel == null)
            {
                return Problem(title: "This user does not have this stock in his portfolio", statusCode: StatusCodes.Status404NotFound);
            }

            return NoContent();
        }
    }
}