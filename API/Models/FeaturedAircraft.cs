namespace API.Models
{
    public class FeaturedAircraft
    {
        public long Id { get; set; }
        public required string ModelName { get; set; }
        public int? Year { get; set; }
        public double? TotalHours { get; set; }
        public required string Condition { get; set; }
        public AircraftImage? MainImage { get; set; }
    }
}

