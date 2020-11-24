using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssetNXT.Models.Data;

namespace AssetNXT.Dtos
{
    public class RouteCreateDto
    {
        [MaxLength(250)]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeviceId { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        [Required]
        public Boundary[] Points { get; set; }
    }
}
