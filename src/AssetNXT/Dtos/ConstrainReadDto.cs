using System;

namespace AssetNXT.Dtos
{
    public class ConstrainReadDto
    {
        public string Id { get; set; }

        public string DeviceId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double TemperatureMin { get; set; }

        public double TemperatureMax { get; set; }

        public double HumidityMin { get; set; }

        public double HumidityMax { get; set; }

        public double PressureMin { get; set; }

        public double PressureMax { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
