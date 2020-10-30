using System.ComponentModel.DataAnnotations;

namespace AssetNXT.Dtos
{
    public class ConstrainCreateDto
    {
        [MaxLength(250)]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeviceId { get; set; }

        [Required]
        public double TemperatureMin { get; set; }

        [Required]
        public double TemperatureMax { get; set; }

        [Required]
        public double HumidityMin { get; set; }

        [Required]
        public double HumidityMax { get; set; }

        [Required]
        public double PressureMin { get; set; }

        [Required]
        public double PressureMax { get; set; }
    }
}
