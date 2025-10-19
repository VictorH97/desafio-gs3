using API.Models;
using API.Models.Request;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IUserService
    {
        Task<AccessToken> LoginAsync(string username, string password);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<Usuario> CreateAsync(CreateUserRequest request);
        Task UpdateUserAsync(UpdateUserRequest request);
        Task DeleteUserAsync(Guid id);
    }
}
