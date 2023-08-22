﻿using System.ComponentModel.DataAnnotations;
namespace Pharmacy.Models
{
    public class AdminLoginRequest
    {

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required, MinLength(6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }

