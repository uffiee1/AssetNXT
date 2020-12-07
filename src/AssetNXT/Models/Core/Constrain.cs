using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace AssetNXT.Models.Core
{
    public class Constrain : IConstrain
    {
        public ObjectId Id { get; set; }

        public int ConstrainId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<string> Devices { get; set; }
    }
}
