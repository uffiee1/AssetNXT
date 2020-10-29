using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models.Data
{
    public class Tag
    {
        [BsonElement]
        public double AccelX { get; set; }

        [BsonElement]
        public double AccelY { get; set; }

        [BsonElement]
        public double AccelZ { get; set; }

        [BsonElement]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        public int DataFormat { get; set; }

        [BsonElement]
        public int DefaultBackground { get; set; }

        [BsonElement]
        public bool Favorite { get; set; }

        [BsonElement]
        public double Humidity { get; set; }

        [BsonElement]
        public double HumidityOffset { get; set; }

        [BsonElement]
        [MaxLength(250)]
        public string Id { get; set; }

        [BsonElement]
        public int MeasurementSequenceNumber { get; set; }

        [BsonElement]
        public int MovementCounter { get; set; }

        [BsonElement]
        public long Pressure { get; set; }

        [BsonElement]
        public int Rssi { get; set; }

        [BsonElement]
        public double Temperature { get; set; }

        [BsonElement]
        public int txPower { get; set; }

        [BsonElement]
        [DataType(DataType.Date)]
        public DateTime UpdateAt { get; set; }

        [BsonElement]
        public double Voltage { get; set; }
    }
}
