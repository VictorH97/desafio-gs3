using API.Models;
using API.Repositories;
using API.Helpers;
using System.Threading.Tasks;
using API.Models.Request;

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

        public async Task<AccessToken> LoginAsync(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos");
            }

            var user = await _userRepository.GetByEmailAsync(email);

            if (user != null)
            {
                if (_tokenHelper.VerifyHash(senha, user.Senha))
                {
                    return _tokenHelper.GenerateJwtToken(user);
                }
            }

            throw new UnauthorizedAccessException("Usuário ou senha inválidos");
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<Usuario> CreateAsync(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
