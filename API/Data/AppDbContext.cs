using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Helpers;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        private readonly TokenHelper _tokenHelper;

        public AppDbContext(DbContextOptions<AppDbContext> options, TokenHelper tokenHelper) : base(options)
        {
            _tokenHelper = tokenHelper;
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateTimeNow = DateTime.UtcNow;
            var hashedPassword = _tokenHelper.GenerateHash("admin123");
            var userAdminId = new Guid("11111111-1111-1111-1111-111111111111");
            var userId = new Guid("22222222-2222-2222-2222-222222222222");
            var dataFixa = new DateTime(2025, 10, 19, 0, 0, 0, DateTimeKind.Utc);
            var adminPermissions = "Criar, Ler, Atualizar, Deletar";
            var userPermissions = "Ler";

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Senha).IsRequired();
                entity.Property(e => e.PerfilId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Idade).IsRequired();
                entity.Property(e => e.Sexo).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Nacionalidade).IsRequired().HasMaxLength(50);
                entity.Property(e => e.EstadoCivil).IsRequired().HasMaxLength(20);
                entity.Property(e => e.DataCriacao).IsRequired();
                entity.Property(e => e.DataAtualizacao).IsRequired();
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(250);
                entity.Property(e => e.Permissoes).IsRequired();
                entity.Property(e => e.DataCriacao).IsRequired();
                entity.Property(e => e.DataAtualizacao).IsRequired();
            });

            modelBuilder.Entity<Perfil>().HasData(
                new Perfil
                {
                    Id = 1,
                    Nome = "Administrador",
                    Descricao = "Perfil com acesso total ao sistema",
                    Permissoes = adminPermissions,
                    DataCriacao = dataFixa,
                    DataAtualizacao = dataFixa
                },
                new Perfil
                {
                    Id = 2,
                    Nome = "Usuário",
                    Descricao = "Perfil padrão para usuários comuns",
                    Permissoes = userPermissions,
                    DataCriacao = dataFixa,
                    DataAtualizacao = dataFixa
                }
            );
            
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = userAdminId,
                    Nome = "Admin",
                    Email = "admin@admin.com",
                    Senha = hashedPassword,
                    PerfilId = 1,
                    Idade = 30,
                    Sexo = "Masculino",
                    Nacionalidade = "Brasileiro",
                    EstadoCivil = "Solteiro",
                    DataCriacao = dataFixa,
                    DataAtualizacao = dataFixa
                }
            );
        }
    }
}
