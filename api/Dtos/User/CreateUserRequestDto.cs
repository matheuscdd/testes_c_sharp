using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Data;

namespace api.Dtos.User
{
    public class CreateUserRequestDto
    {
        [Required]
        [MinLength(3)]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(5)]
        public string? Email { get; set; }

        [Required]
        [MinLength(12)]
        public string? Password { get; set; }


    }
}