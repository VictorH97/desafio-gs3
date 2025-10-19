namespace API.Models.Request
{
    public class AircraftDocumentRequest
    {
        public long DocumentId { get; set; }
        public long AircraftId { get; set; }
        public required string Description { get; set; }
        public required byte[] DocumentBase64 { get; set; }
        public DateTime? Date { get; set; }
    }
}