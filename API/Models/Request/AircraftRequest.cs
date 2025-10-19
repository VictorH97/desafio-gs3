namespace API.Models.Request
{
    public class AircraftRequest
    {
        public long AircraftId { get; set; }
        public string? TailNumber { get; set; }
        public required string Reference { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        public string? Manufacturer { get; set; }
        public int? Year { get; set; }
        public int? TotalHours { get; set; }
        public double? Price { get; set; }
        public string? Currency { get; set; }
        public int? ShowPrice { get; set; }
        public long ModelId { get; set; }
        public int? Seats { get; set; }
        public string? Fuel { get; set; }
        public string? Condition { get; set; }
        public AircraftImageRequest? Images { get; set; }
    }
}