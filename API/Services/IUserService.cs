using API.Models;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IUserService
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> ValidateLoginAsync(string username, string password);
        Task<AccessToken> LoginAsync(string username, string password);
    }
}
