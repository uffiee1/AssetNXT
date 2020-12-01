using System.Collections.Generic;
using AssetNXT.Repository.Service;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models.Data
{
    [BsonCollection("routes")]
    public class Route : Document
    {
        [BsonElement]
        public string Name { get; set; }

        [BsonElement]
        public string Description { get; set; }

        [BsonElement]
        public List<Boundary> Points { get; set; }
    }
}
