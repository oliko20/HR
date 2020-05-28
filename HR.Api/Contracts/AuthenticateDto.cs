﻿using System.ComponentModel.DataAnnotations;

namespace HR.Api.Contracts
{
    public class AuthenticateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
