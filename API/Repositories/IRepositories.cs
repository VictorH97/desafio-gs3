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
}
