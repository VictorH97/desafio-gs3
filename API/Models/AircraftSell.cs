using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("AeronaveVenda_tb")]
    public class AircraftSell
    {
        [Key]
        [Column("idAeronave")]
        public long AircraftId { get; set; }

        [Required]
        [StringLength(10)]
        [Column("Referencia")]
        public required string Reference { get; set; }

        [StringLength(30)]
        [Column("Status")]
        public string? Status { get; set; }

        [StringLength(6)]
        [Column("Prefixo")]
        public string? TailNumber { get; set; }

        [Column("DataAnuncio")]
        public DateTime? PostedDate { get; set; }

        [Column("DataAlteracao")]
        public DateTime? UpdateDate { get; set; }

        [StringLength(100)]
        [Column("Tipo")]
        public string? Type { get; set; }

        [StringLength(100)]
        [Column("Categoria")]
        public string? Category { get; set; }

        [StringLength(50)]
        [Column("Fabricante")]
        public string? Manufacturer { get; set; }

        [Column("Ano")]
        public int? Year { get; set; }

        [Column("HorasTotais")]
        public double? TotalHours { get; set; }

        [Column("ValorVenda")]
        public double? Price { get; set; }

        [StringLength(4)]
        [Column("Moeda")]
        public string? Currency { get; set; }

        [Column("ExibirPreco")]
        public int? ShowPrice { get; set; }

        [Column("idModelo")]
        public long ModelId { get; set; }

        [Column("Site")]
        public string? Country { get; set; }

        [Required]
        [Column("Fakedelete")]
        public int FakeDelete { get; set; }

        [Column("Assentos")]
        public int? Seats { get; set; }

        [StringLength(20)]
        [Column("Combustivel")]
        public string? Fuel { get; set; }

        [StringLength(20)]
        [Column("Condicao")]
        public string? Condition { get; set; }

        [StringLength(100)]
        [Column("Motorizacao")]
        public string? Engine { get; set; }

        [Column("Destaque")]
        public int? Featured { get; set; }

        [Column("FichaTecnica")]
        public string? TechnicalFile { get; set; }
    }
}