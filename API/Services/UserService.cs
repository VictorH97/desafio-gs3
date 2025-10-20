using API.Models;
using API.Repositories;
using API.Helpers;
using System.Threading.Tasks;
using API.Models.Request;
using API.Models.DTO;

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

        public async Task<(AccessToken, UserDTO)> LoginAsync(string email, string senha)
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
                    return (_tokenHelper.GenerateJwtToken(user), new UserDTO()
                    {
                        Nome = user.Nome,
                        Email = user.Email,
                        PerfilId = user.PerfilId,
                        EstadoCivil = user.EstadoCivil,
                        Idade = user.Idade,
                        Sexo = user.Sexo,
                        Nacionalidade = user.Nacionalidade
                    });
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
            var user = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                Senha = _tokenHelper.GenerateHash("mudar@123"),
                PerfilId = request.PerfilId,
                Idade = request.Idade,
                Sexo = request.Sexo,
                Nacionalidade = request.Nacionalidade,
                EstadoCivil = request.EstadoCivil,
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            user.Nome = request.Nome;
            user.Email = request.Email;
            user.PerfilId = request.PerfilId;
            user.Idade = request.Idade;
            user.Sexo = request.Sexo;
            user.Nacionalidade = request.Nacionalidade;
            user.EstadoCivil = request.EstadoCivil;
            user.DataAtualizacao = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            await _userRepository.DeleteAsync(id);
        }
    }
}
