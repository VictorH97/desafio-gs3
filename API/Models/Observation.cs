using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Observacao_tb")]
    public class Observation
    {
        [Key]
        [Column("idObservacao")]
        public int Id { get; set; }
        
        [Required]
        [Column("idRegistro")]
        public long AircraftId { get; set; }
        
        [Required]
        [Column("Data")]
        public DateTime Date { get; set; }
        
        [Required]
        [Column("Historico")]
        [StringLength(4000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [Column("FakeDelete")]
        public int FakeDelete { get; set; }
    }
} 