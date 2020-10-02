using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ruuvi.Models
{
   public class RuuviStation 
   {
        [Key]
        public int IdStation  { get; set; }
        
        [Required]
        public List<Tag> Tags { get; set; }

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
        public DateTime Time { get; set;}

   }

}