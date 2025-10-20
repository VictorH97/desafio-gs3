using System;

namespace API.Models.Request;

public class CreateUserRequest
{
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required int PerfilId { get; set; }
    public required int Idade { get; set; }
    public required string Sexo { get; set; }
    public required string Nacionalidade { get; set; }
    public required string EstadoCivil { get; set; }
}
