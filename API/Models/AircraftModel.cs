using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tbModelo")]
    public class AircraftModel
    {
        [Key]
        [Column("idModelo")]
        public long ModelId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Fabricante")]
        public required string Manufacturer { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Categoria")]
        public required string Category { get; set; }

        [Required]
        [StringLength(30)]
        [Column("NomeModelo")]
        public required string Name { get; set; }

        [Required]
        [Column("FakeDelete")]
        public int FakeDelete { get; set; }
    }
}
