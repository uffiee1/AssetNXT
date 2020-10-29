using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetNXT.Dtos
{
    public class ConstrainReadDto
    {
        public string Id { get; set; }

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
