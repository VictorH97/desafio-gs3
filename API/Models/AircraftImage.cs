using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Imagem_tb")]
    public class AircraftImage
    {
        [Key]
        [Column("idImagem")]
        public long ImageId { get; set; }

        [Required]
        [Column("idAeronave")]
        public required long AircraftId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Descricao")]
        public required string Description { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Caminho")]
        public required string Path { get; set; }

        [Required]
        [Column("Imagem")]
        public required byte[] ImageBase64 { get; set; }

        [StringLength(3)]
        [Column("FotoPrincipal")]
        public string? IsMain { get; set; }
    }
}
