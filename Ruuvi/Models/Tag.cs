using System;
using System.ComponentModel.DataAnnotations;

namespace Ruuvi.Models
{
    public class Tag
    {
        [Key]
        public int IdTag  { get; set; }

        [Required]
        public double AccelX { get; set; }

        [Required]
        public double AccelY { get; set; }

        [Required]
        public double AccelZ { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        public int DataFormat { get; set; }

        [Required]
        public int DefaultBackground { get; set; }

        [Required]
        public bool Favorite { get; set; }

        [Required]
        public double Humidity { get; set; }

        [Required]
        public double HumidityOffset { get; set; }

        [Required]
        [MaxLength(250)]
        public string Id { get; set;}

        [Required]
        public int MeasurementSequenceNumber {get; set; }

        [Required]
        public int MovementCounter  {get; set; }

        [Required]
        public long Pressure { get; set; }

        [Required]
        public int Rssi { get; set; }

        [Required]
        public double Temperature { get; set; }

        [Required]
        public int txPower { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public  DateTime UpdateAt { get; set; }
        
        [Required]
        public double Voltage { get; set; }
    }
}