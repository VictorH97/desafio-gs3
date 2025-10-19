using System;

namespace API.Models;

public class Perfil
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Permissoes { get; set; }
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public required DateTime DataCriacao { get; set; }
    public required DateTime DataAtualizacao { get; set; }
}
