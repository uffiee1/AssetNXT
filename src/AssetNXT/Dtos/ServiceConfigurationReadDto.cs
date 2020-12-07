using System;

namespace AssetNXT.Dtos
{
    public class ServiceConfigurationReadDto
    {
        public string DeviceId { get; set; }

        public bool IsActive { get; set; }

        public string TagId { get; set; }

        public bool Humidity { get; set; }

        public bool Pressure { get; set; }

        public bool Temperature { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
