using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Dtos.Stock
{
    public class StockDto
    {
        public required int Id { get; set; }
        public required string Symbol { get; set; }
        public required string CompanyName { get; set; }
        public required decimal Purchase { get; set; }
        public required decimal LastDiv { get; set; }
        public required string Industry { get; set; }
        public required long MarketCap { get; set; }
        public List<CommentDto> Comments { get; set; } = [];
    }
}