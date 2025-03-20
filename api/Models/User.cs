using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml;
using Microsoft.AspNetCore.Identity;

namespace api.Models 
{
    public class User : IdentityUser
    {
        public int Risk { get; set; }
        public List<Portfolio> Portfolios { get; set; } = [];
    }
}