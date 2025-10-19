using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.Request;

namespace API.Models
{
    public class AircraftSaveRequest
    {
        public long AircraftId { get; set; }
        public required string Reference { get; set; }
        public string? Status { get; set; }
        public string? TailNumber { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }
        public string? Manufacturer { get; set; }
        public int? Year { get; set; }
        public double? TotalHours { get; set; }
        public double? Price { get; set; }
        public string? Currency { get; set; }
        public int? ShowPrice { get; set; }
        public long ModelId { get; set; }
        public string? Country { get; set; }
        public int FakeDelete { get; set; }
        public int? Seats { get; set; }
        public string? Fuel { get; set; }
        public string? Condition { get; set; }
        public int? Featured { get; set; }
        public string? TechnicalFile { get; set; }
        public List<AircraftImageRequest>? Images { get; set; }
        public List<AircraftDocumentRequest>? Documents { get; set; }
    }
}