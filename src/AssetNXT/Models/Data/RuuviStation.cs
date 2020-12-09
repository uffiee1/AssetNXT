using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssetNXT.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models.Data
{
    [BsonCollection("ruuvi_stations")]
    public class RuuviStation : Document
    {
        [MaxLength(250)]
        public string DeviceId { get; set; }

        [BsonElement]
        public List<Tag> Tags { get; set; }

        [BsonElement]
        public int BatteryLevel { get; set; }

        [BsonElement]
        [MaxLength(250)]
        public string EventId { get; set; }

        [BsonElement]
        public Location Location { get; set; }

        [BsonElement]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
    }
}
