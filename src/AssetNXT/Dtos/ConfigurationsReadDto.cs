using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetNXT.Dtos
{
    public class ConfigurationsReadDto
    {
        public bool IsActive { get; set; }

        public string TagId { get; set; }

        public bool Humidity { get; set; }

        public bool Pressure { get; set; }

        public bool Temperature { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
