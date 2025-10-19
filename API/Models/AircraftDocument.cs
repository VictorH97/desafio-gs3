using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("ImagemDoc_tb")]
    public class AircraftDocument
    {
        [Key]
        [Column("idLancamentoDoc")]
        public long DocumentId { get; set; }

        [Required]
        [Column("idLancamento")]
        public required long AircraftId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Descricao")]
        public required string Description { get; set; }

        [Required]
        [Column("Imagem")]
        public required byte[] DocumentBase64 { get; set; }

        [Required]
        [Column("DataImagem")]
        public DateTime? Date { get; set; }
    }
}
