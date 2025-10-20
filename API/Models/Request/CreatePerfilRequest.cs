using System;

namespace API.Models.Request;

public class CreatePerfilRequest
{
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Permissoes { get; set; }
}
