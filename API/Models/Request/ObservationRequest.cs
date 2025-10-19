using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class ObservationRequest
    {
        [Required]
        public long AircraftId { get; set; }
        
        [Required]
        [StringLength(4000)]
        public string Description { get; set; } = string.Empty;
    }
} 