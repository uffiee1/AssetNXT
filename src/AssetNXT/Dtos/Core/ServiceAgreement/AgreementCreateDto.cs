using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssetNXT.Models.Data;

namespace AssetNXT.Dtos.Core
{
    public class AgreementCreateDto
    {
        [MaxLength(250)]
        [Required]
        public List<Tag> Tags { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

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
