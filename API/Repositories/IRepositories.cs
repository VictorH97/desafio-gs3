using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<Usuario?> GetByEmailAsync(string email);
        Task AddAsync(Usuario user);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<Usuario> CreateAsync(Usuario user);
        Task UpdateAsync(Usuario user);
        Task DeleteAsync(Guid id);
    }

    public interface IPerfilRepository
    {
        Task AddAsync(Perfil perfil);
        Task<IEnumerable<Perfil>> GetAllAsync();
        Task<Perfil?> GetByIdAsync(int id);
        Task<Perfil> CreateAsync(Perfil perfil);
        Task UpdateAsync(Perfil perfil);
        Task DeleteAsync(int id);
    }
        
}
