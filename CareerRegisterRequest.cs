using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class CareerRegisterRequest
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string lastname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }

        public string Education { get; set; }

        public string Degree { get; set; }


        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Required, MinLength(6), Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string? Resume { get; set; }

    }
}
