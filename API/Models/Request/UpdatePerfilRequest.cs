using System;

namespace API.Models.Request;

public class UpdatePerfilRequest
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Permissoes { get; set; }
}
