using System;
using System.Collections.Generic;
using AssetNXT.Models.Data;
using MongoDB.Bson;

namespace AssetNXT.Models.Core
{
    public class Constrain : IConstrain
    {
        public ObjectId Id { get; set; }

        public int ConstrainId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
