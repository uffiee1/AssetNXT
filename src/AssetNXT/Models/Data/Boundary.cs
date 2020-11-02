using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models.Data
{
    public class Boundary : Document
    {
        [BsonElement]
        public string Colour { get; set; }

        [BsonElement]
        public Location Location { get; set; }
    }
}
