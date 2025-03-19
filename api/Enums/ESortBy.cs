using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using api.Models;

namespace api.Enums
{
    public enum ESortBy
    {
        Id,
        Symbol,
        CompanyName,
        Purchase,
        LastDiv,
        Industry,
        MarketCap,
    }
}