using System;
using API.Models;
using API.Models.Request;

namespace API.Services;

public interface IPerfilService
{
    Task<IEnumerable<Perfil>> GetAllAsync();
    Task<Perfil?> GetByIdAsync(int id);
    Task<Perfil> CreateAsync(CreatePerfilRequest request);
    Task UpdateAsync(UpdatePerfilRequest request);
    Task DeleteAsync(int id);
}
