using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required int StockId { get; set; }
    }
}