using System;
using System.Collections.Generic;
using AssetNXT.Models.Core;

namespace AssetNXT.Dtos.Core
{
    public class RouteReadDto
    {
        public string Id { get; set; }

        public List<string> Devices { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Boundary> Points { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
