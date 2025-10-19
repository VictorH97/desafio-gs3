namespace API.Models
{
    public class AircraftSummary
    {
        public long Id { get; set; }
        public required string Reference { get; set; }
        public required string Category { get; set; }
        public required string Manufacturer { get; set; }
        public required string ModelName { get; set; }
        public int? Year { get; set; }
        public double? TotalHours { get; set; }
        public required string Fuel { get; set; }
        public required string Engine { get; set; }
        public int? Seats { get; set; }
        public AircraftImage? MainImage { get; set; }
    }
}