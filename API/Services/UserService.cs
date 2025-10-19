using API.Models;
using API.Repositories;
using API.Helpers;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenHelper _tokenHelper;
        
        public UserService(IUserRepository userRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null) return false;
            // Troque para validação de hash em produção
            return "aviation25" == password;
        }

        public async Task<AccessToken> LoginAsync(string username, string password)
        {            
            // Troque para validação de hash em produção
            if (username != "aviadores" || password != "aviation*25")
                throw new UnauthorizedAccessException("Invalid username or password");

            return _tokenHelper.GenerateJwtToken(new User() { Id = new Guid("3840f174-a533-44e1-9cd7-9cef0441892d"), Email = "brazilaviation1@gmail.com", Username = "greece" });
        }
    }
}
