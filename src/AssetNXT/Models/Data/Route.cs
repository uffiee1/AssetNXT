using System.Collections.Generic;
using AssetNXT.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models.Data
{
    [BsonCollection("routes")]
    public class Route : Document
    {
        [BsonElement]
        public List<Boundary> Points { get; set; }
    }
}
