using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetNXT.Models.Core
{
    public class GeometricConfiguration
    {
        public string DeviceId { get; set; }

        public bool IsActive { get; set; }

        public string TagId { get; set; }

        public bool Boundary { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
