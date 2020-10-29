using System.ComponentModel.DataAnnotations;

namespace AssetNXT.Dtos
{
    public class NotificationCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
