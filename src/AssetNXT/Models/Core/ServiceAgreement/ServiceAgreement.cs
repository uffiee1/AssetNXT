using System;
using AssetNXT.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetNXT.Models.Core.ServiceAgreement
{
    [BsonCollection("service_agreement_configurations")]
    public class ServiceAgreement : Document
    {
        [BsonElement]
        public string DeviceId { get; set; }

        [BsonElement]
        public bool IsActive { get; set; }

        [BsonElement]
        public string TagId { get; set; }

        [BsonElement]
        public bool Humidity { get; set; }

        [BsonElement]
        public bool Pressure { get; set; }

        [BsonElement]
        public bool Temperature { get; set; }

        [BsonElement]
        public DateTime CreateDate { get; set; }
    }
}
