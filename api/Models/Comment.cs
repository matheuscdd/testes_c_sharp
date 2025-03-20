using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml;

namespace api.Models 
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int StockId { get; set; }
        
        // Navigation property serve para ajudar nas queries 
        public Stock? Stock { get; set; } 
    }
}