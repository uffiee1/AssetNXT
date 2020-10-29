using System.ComponentModel.DataAnnotations;

namespace AssetNXT.Dtos
{
    public class ConstrainCreateDto
    {
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
