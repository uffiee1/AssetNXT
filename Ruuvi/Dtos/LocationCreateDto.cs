using System;
using System.ComponentModel.DataAnnotations;

namespace Ruuvi.Dtos
{
    public class LocationCreateDto
    {

        [Required]
        public double Accuracy { get; set; }
        
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}