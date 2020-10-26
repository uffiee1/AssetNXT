using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using AssetNXT;
using AssetNXT.Models;

namespace AssetNXT.Data
{
    public class RuuviStationReadDto
    {
        public List<RuuviStationTag> Tags { get; set; }

        public int BatteryLevel { get; set; }

        public string DeviceId { get; set; }

        public string EventId { get; set; }

        public Location Location { get; set; }

        public DateTime Time { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
