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
        public async Task<IActionResult> AddPortfolio([FromRoute] int stockId)
        {
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
    }
}