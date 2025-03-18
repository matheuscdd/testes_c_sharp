using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepository) : ControllerBase
    {
        private readonly ICommentRepository _commentRepository = commentRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var commentsModels = await _commentRepository.GetAllAsync();
            var commentsDto = commentsModels.Select(s => s.ToCommentDto());

            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var commentModel = await _commentRepository.GetByIdAsync(id);
            if (commentModel == null)
            {
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        // {
        //     // o nome das chaves no json são case insensitive pro dotnet
        //     var stockModel = stockDto.ToStockFromCreateDto();
        //     await _stockRepository.CreateAsync(stockModel);
        //     // Toda vez que manda um create, no sucesso ele redireciona pra rota de find
        //     return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        // {
        //     var stockModel = await _stockRepository.UpdateAsync(id, stockDto);
        //     if (stockModel == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(stockModel.ToStockDto());
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete([FromRoute] int id)
        // {
        //     var stockModel = await _stockRepository.DeleteAsync(id);
        //     if (stockModel == null)
        //     {
        //         return NotFound();
        //     }

        //     return NoContent();
        // }
    }
}