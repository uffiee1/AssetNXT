using System;
using System.Collections.Generic;
using AssetNXT.Models.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models
{
    public interface IConstrain
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        ObjectId Id { get; set; }

        [BsonElement]
        int ConstrainId { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }

        [BsonElement]
        List<Tag> Tags { get; set; }
    }
}
