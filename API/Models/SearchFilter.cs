namespace API.Models
{
    public class SearchFilter
    {
        public required string Type { get; set; }
        public required string Category { get; set; }
        public required string Manufacturer { get; set; }
        public required string ModelName { get; set; }
        public required int Year { get; set; }
    }
}