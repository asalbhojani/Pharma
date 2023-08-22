using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Models
{
    
    public class Quote
    {
        
        public int Id { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required]
        [DisplayName("City")]
        public string City { get; set; }

        [Required]
        [DisplayName("State")]
        public string State { get; set;}

        [Required]
        [DisplayName("PostalCode")]
        [DataType(DataType.PostalCode, ErrorMessage = "PostalCode is not valid")]
        public string PostalCode { get; set;}

        [Required]
        [DisplayName("Country")]
        public string Country { get; set;}

        [Required]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Phone No")]
        public string Phone { get; set; }

        [Required]
        [DisplayName("Comments")]
        public string Comments { get; set;}


    }
}
