using System;
using System.Collections.Generic;
using AssetNXT.Models.Core;
using AssetNXT.Models.Data;

namespace AssetNXT.Dtos
{
    public class RouteReadDto
    {
        public string Id { get; set; }

        public List<Tag> Tags { get; set; }

        public int ConstrainId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Boundary> Points { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
