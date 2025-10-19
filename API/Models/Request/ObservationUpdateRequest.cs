using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class ObservationUpdateRequest
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        [StringLength(4000)]
        public string Description { get; set; } = string.Empty;
    }
} 