using System;

namespace AssetNXT.Models.Core
{
    public class AgreementConfiguration
    {
        public string DeviceId { get; set; }

        public bool IsActive { get; set; }

        public string TagId { get; set; }

        public bool Humidity { get; set; }

        public bool Pressure { get; set; }

        public bool Temperature { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
