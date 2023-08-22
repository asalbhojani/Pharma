using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class AdminRegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MinLength(6), Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string? Role { get; set; }
    }
}
