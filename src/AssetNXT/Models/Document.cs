using System;
using MongoDB.Bson;

namespace AssetNXT.Models.Data
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [MaxLength(250)]
        public string DeviceId { get; set; }
    }
}
