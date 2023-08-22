using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Pharmacy.Models
{
    public class Tablet
    {
        public int TabletId { get; set; }

        [Required]
        [DisplayName("Machine Name")]
        public string machinename { get; set; }

        
        [DisplayName("Tablet Image")]
        public string? TabletImage { get; set; }

        [Required]
        [DisplayName("Model Number")]
        public string modelnumber { get; set; }

        [Required]
        [DisplayName("Dice")]
        public string dice { get; set; }

        [Required]
        [DisplayName("Max Pressure")]
        public string maxpressure { get; set; }

        [Required]
        [DisplayName("Max Diameter Of Tablet")]
        public string maxdiameteroftablet { get; set; }

        [Required]
        [DisplayName("Max Depth Of Fill")]
        public string Maxdepthoffill { get; set; }

        [Required]
        [DisplayName("Production Capacity")]
        public string productioncapacity { get; set; }

        [Required]
        [DisplayName("Machine Size")]
        public string machinesize { get; set; }

        [Required]
        [DisplayName("Machine Weight")]
        public string machineweight { get; set; }

        [Required]
        [DisplayName("Net Weight")]
        public string NetWeight { get; set; }
    } 
}
