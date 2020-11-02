using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetNXT.Dtos
{
    public class BoundaryCircleReadDto
    {
        public string Id { get; set; }

        public double Radius { get; set; }

        public string Colour { get; set; }

        public double Accuracy { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
