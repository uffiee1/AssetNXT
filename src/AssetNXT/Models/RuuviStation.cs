using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

using AssetNXT.Models;
using AssetNXT.Models.Data;
using AssetNXT.Repositories;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models
{
    [BsonCollection("ruuvistations")]
    public class RuuviStation : Document
    {
        [BsonElement]
        public List<RuuviStationTag> Tags { get; set; }

        [BsonElement]
        public int BatteryLevel { get; set; }

        [BsonElement]
        [MaxLength(250)]
        public string DeviceId { get; set; }

        [BsonElement]
        [MaxLength(250)]
        public string EventId { get; set; }

        [BsonElement]
        public Location Location { get; set; }

        [BsonElement]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
