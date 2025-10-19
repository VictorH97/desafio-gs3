namespace API.Models.Request
{
    public class AircraftImageRequest
    {
        public long ImageId { get; set; }
        public long AircraftId { get; set; }
        public required string Description { get; set; }
        public required string Path { get; set; }
        public required byte[] ImageBase64 { get; set; }
        public string? IsMain { get; set; }
    }
}