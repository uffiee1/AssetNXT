using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ruuvi.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Ruuvi.Models.Data
{
   [BsonCollection("ruuvistations")]
   public class RuuviStation : Document
   {
        
        [BsonElement]
        public List<Tag> Tags { get; set; }

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
        [DataType(DataType.Date)]
        public DateTime Time { get; set;}

   }

}