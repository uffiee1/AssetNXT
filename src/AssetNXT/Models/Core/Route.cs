using System.Collections.Generic;
using AssetNXT.Repository.Service;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models.Core
{
    [BsonCollection("geometric_constrains")]
    public class Route : Constrain
    {
        [BsonElement]
        public string Name { get; set; }

        [BsonElement]
        public string Description { get; set; }

        [BsonElement]
        public List<Boundary> Points { get; set; }
    }
}
