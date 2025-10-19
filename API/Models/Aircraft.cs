namespace API.Models
{
    public class Aircraft
    {
        public long Id { get; set; }
        public required string Reference { get; set; }
        public required string TailNumber { get; set; }
        public required string Type { get; set; }
        public required string Category { get; set; }
        public required string Manufacturer { get; set; }
        public required long ModelId { get; set; }
        public required string ModelName { get; set; }
        public int? Year { get; set; }
        public double? TotalHours { get; set; }
        public string? Country { get; set; }
        public double? Price { get; set; }
        public string? Currency { get; set; }
        public bool ShowPrice { get; set; }
        public required string Condition { get; set; }
        public int? Seats { get; set; }
        public required string Fuel { get; set; }
        public required string Status { get; set; }
        public bool Featured { get; set; }
        public string? TechnicalFile { get; set; }
        public DateTime? PostedDate { get; set; }
        public IEnumerable<AircraftDocument>? Documents { get; set; }
        public IEnumerable<AircraftImage>? Images { get; set; }
    }
}