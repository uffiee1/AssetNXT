using System;
using AssetNXT.Models.Data;

namespace AssetNXT.Dtos
{
    public class BoundaryCircleReadDto
    {
        public string Id { get; set; }

        public string DeviceId { get; set; }

        public double Radius { get; set; }

        public string Colour { get; set; }

        public Location Location { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
