using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssetNXT.Models.Core;

namespace AssetNXT.Dtos.Core
{
    public class RouteCreateDto
    {
        [MaxLength(250)]
        [Required]
        public List<string> Devices { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        [Required]
        public Boundary[] Points { get; set; }
    }
}
