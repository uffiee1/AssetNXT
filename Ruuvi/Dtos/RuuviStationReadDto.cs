using System;
using Ruuvi.Models;
using System.Collections.Generic;


namespace Ruuvi.Dtos
{
    public class RuuviStationReadDto
    {
        public int IdStation  { get; set; }

        public List<Tag> Tags { get; set; }

        public int BatteryLevel { get; set; }

        public string DeviceId { get; set; }

        public string EventId { get; set; }
        
        public Location Location { get; set; }

        public DateTime Time { get; set;}
    }
}