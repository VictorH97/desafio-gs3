using System;

namespace API.Models.DTO;

public class UserDTO
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Perfil { get; set; }
    public required int Idade { get; set; }
    public required string Sexo { get; set; }
    public required string Nacionalidade { get; set; }
    public required string EstadoCivil { get; set; }
}
