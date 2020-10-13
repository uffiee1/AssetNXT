using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using AssetNXT;
using AssetNXT.Models;

namespace AssetNXT.Data
{
    public class RuuviStationCreateDto
    {
        [Required]
        public List<RuuviStationTag> Tags { get; set; }

        [Required]
        public int BatteryLevel { get; set; }

        [Required]
        [MaxLength(250)]
        public string DeviceId { get; set; }

        [Required]
        [MaxLength(250)]
        public string EventId { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
    }
}
