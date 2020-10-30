using System.ComponentModel.DataAnnotations;

namespace AssetNXT.Dtos
{
    public class NotificationCreateDto
    {
        [MaxLength(250)]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeviceId { get; set; }

        [MaxLength(250)]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }
    }
}
