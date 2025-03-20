using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
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
            var user = User.GetUsername();
            var userModel = await _userRepository.GetByUserNameAsync(user);
            var portfolioModel = await _portfolioRepository.GetUserPortfolioAsync(userModel);
            return Ok(portfolioModel);
        }
    }
}