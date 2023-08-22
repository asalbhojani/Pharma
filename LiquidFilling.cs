using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Pharmacy.Models
{
    public class LiquidFilling
    {
        public int LiquidFillingId { get; set; }

        [Required]
        [DisplayName("Machine Name")]
        public string machinename { get; set; }

        
        [DisplayName("Liquid Fill Image")]
        public string? liquidImage { get; set; }

        [Required]
        [DisplayName("Air Pressure")]
        public string airpressure { get; set; }

        [Required]
        [DisplayName("Air Volume")]
        public string airvolume { get; set; }

        [Required]
        [DisplayName("Filling Speed")]
        public string fillingspeed { get; set; }

        [Required]
        [DisplayName("Filling Range")]
        public string fillingrange { get; set; }
    }
}
