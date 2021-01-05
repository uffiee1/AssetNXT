using System;
using AssetNXT.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models.Core
{
    [BsonCollection("service_geometric_configurations")]
    public class ServiceGeometric : Document
    {
        [BsonElement]
        public string DeviceId { get; set; }

        [BsonElement]
        public string ConstrainName { get; set; }

        [BsonElement]
        public bool Boundary { get; set; }
    }
}
