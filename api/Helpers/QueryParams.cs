using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Helpers
{
    public class QueryParams
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public ESortBy? SortBy { get; set; } = ESortBy.Id;
        public bool IsDescending { get; set; } = false;
    }
}