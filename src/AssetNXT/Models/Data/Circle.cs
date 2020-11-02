using AssetNXT.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models.Data
{
    [BsonCollection("boundaries")]
    public class Circle : Boundary
    {
        [BsonElement]
        public double Radius { get; set; }
    }
}
