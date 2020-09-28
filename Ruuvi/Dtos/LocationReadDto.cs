using System;

namespace Ruuvi.Dtos
{
    public class LocationReadDto
    {
    
        public int IdLocation  { get; set; }

        public double Accuracy { get; set; }
        
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    
    }
}