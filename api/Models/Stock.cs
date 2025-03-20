using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml;

namespace api.Models 
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }
        public required string Symbol { get; set; }
        public required string CompanyName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal LastDiv { get; set; }
        public required string Industry { get; set; }
        public required long MarketCap { get; set; }
        public List<Comment> Comments { get; set; } = [];
        public List<Portfolio> Portfolios { get; set; } = [];
    }    
}