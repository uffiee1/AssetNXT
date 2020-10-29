using System.ComponentModel.DataAnnotations;

namespace AssetNXT.Dtos
{
    public class NotificationCreateDto
    {
        [Required]
        [DataType(DataType.Date)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string Description { get; set; }
    }
}
