using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class ContactRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string? Company { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Phone { get; set; } = string.Empty;
        
        [Required]
        public string Interest { get; set; } = string.Empty; // Sell, Buy, Lease, Other
        
        [Required]
        public string Model { get; set; } = string.Empty;
        
        [Required]
        public int Year { get; set; }
        
        [Required]
        public int TotalHours { get; set; }
        
        [Required]
        public string Message { get; set; } = string.Empty;
        
        [Required]
        public List<string> Files { get; set; } = new List<string>(); // Base64 PDF files
    }
} 