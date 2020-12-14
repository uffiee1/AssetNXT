using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetNXT.Models.Core
{
    public class ServiceGeometric
    {
        public string DeviceId { get; set; }

        public string ConstrainName { get; set; }

        public bool Boundary { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
