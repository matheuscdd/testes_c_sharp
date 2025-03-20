using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml;

namespace api.Models 
{
    [Table("Portfolio")]
    public class Portfolio
    {
        public required string UserId { get; set; }
        public required int StockId { get; set; }
        public Stock? Stock { get; set; }
        public User? User { get; set; }
    }    
}