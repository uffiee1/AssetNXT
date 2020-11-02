using System.ComponentModel.DataAnnotations;

namespace AssetNXT.Dtos
{
    public class BoundaryCircleCreateDto
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
        public double Accuracy { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
}
}
