using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }

        [ForeignKey("Perfil")]
        public required int PerfilId { get; set; }
        public required int Idade { get; set; }
        public required string Sexo { get; set; }
        public required string Nacionalidade { get; set; }
        public required string EstadoCivil { get; set; }
        public required DateTime DataCriacao { get; set; }
        public required DateTime DataAtualizacao { get; set; }

        public Perfil? Perfil { get; set; }
    }
}
