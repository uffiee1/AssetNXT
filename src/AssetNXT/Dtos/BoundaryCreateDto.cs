using System.ComponentModel.DataAnnotations;
using AssetNXT.Models.Data;

namespace AssetNXT.Dtos
{
    public class BoundaryCreateDto
    {
        [MaxLength(250)]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeviceId { get; set; }

        [Required]
        public double Radius { get; set; }

        [Required]
        public string Colour { get; set; }

        [Required]
        public Location Location { get; set; }
    }
}
