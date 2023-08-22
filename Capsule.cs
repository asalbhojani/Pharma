using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Pharmacy.Models
{
    public class Capsule
    {
        public int Capsuleid { get; set; }

        [Required]
        [DisplayName("Machine Name")]
        public string machinename { get; set; } 
         
        
        [DisplayName("Capsule Image")]
        public string? CapsuleImage { get; set; }

        [Required]
        [DisplayName("Output")]
        public string Output { get; set; }

        [Required]
        [DisplayName("Capsule Size")]
        public string capsulesize { get; set; }

        [Required]
        [DisplayName("Machine Dimension")]
        public string machinedimenssion { get; set; }

        [Required]
        [DisplayName("Shipping Weight")]
        public string shippingweight { get; set; }
    }
}
