using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssetNXT.Models.Data;

namespace AssetNXT.Dtos
{
    public class RuuviStationCreateDto
    {
        [Required]
        public List<Tag> Tags { get; set; }

        [Required]
        public int BatteryLevel { get; set; }

        [MaxLength(250)]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeviceId { get; set; }

        [MaxLength(250)]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string EventId { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
    }
}
