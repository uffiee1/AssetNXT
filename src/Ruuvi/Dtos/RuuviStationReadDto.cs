using System;
using Ruuvi.Models.Data;
using System.Collections.Generic;


namespace Ruuvi.Dtos
{
    public class RuuviStationReadDto
    {
        public string Id { get; set; }
        public List<Tag> Tags { get; set; }

        public int BatteryLevel { get; set; }

        public string DeviceId { get; set; }

        public string EventId { get; set; }
        
        public Location Location { get; set; }

        public DateTime Time { get; set;}

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}