using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    #region UserRepository

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuario
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(Usuario user)
        {
            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuario.Include(u => u.Perfil).ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        {
            return await _context.Usuario.Include(u => u.Perfil).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> CreateAsync(Usuario user)
        {
            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(Usuario user)
        {
            _context.Usuario.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Usuario.FindAsync(id);
            if (user != null)
            {
                _context.Usuario.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }

    #endregion

    #region PerfilRepository

    public class PerfilRepository : IPerfilRepository
    {
        private readonly AppDbContext _context;

        public PerfilRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Perfil perfil)
        {
            _context.Perfil.Add(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task<Perfil?> GetByIdAsync(int id)
        {
            return await _context.Perfil.FindAsync(id);
        }

        public async Task<IEnumerable<Perfil>> GetAllAsync()
        {
            return await _context.Perfil.ToListAsync();
        }

        public async Task<Perfil> CreateAsync(Perfil perfil)
        {
            _context.Perfil.Add(perfil);
            await _context.SaveChangesAsync();
            return perfil;
        }

        public async Task UpdateAsync(Perfil perfil)
        {
            _context.Perfil.Update(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var perfil = await _context.Perfil.FindAsync(id);
            if (perfil != null)
            {
                _context.Perfil.Remove(perfil);
                await _context.SaveChangesAsync();
            }
        }

        #endregion
    }
}