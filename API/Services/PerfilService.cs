using System;
using API.Models;
using API.Models.Request;
using API.Repositories;

namespace API.Services;

public class PerfilService : IPerfilService
{
    private readonly IPerfilRepository _perfilRepository;

    public PerfilService(IPerfilRepository perfilRepository)
    {
        _perfilRepository = perfilRepository;
    }

    public async Task<Perfil?> GetByIdAsync(int id)
    {
        return await _perfilRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Perfil>> GetAllAsync()
    {
        return await _perfilRepository.GetAllAsync();
    }

    public async Task<Perfil> CreateAsync(CreatePerfilRequest perfil)
    {
        var newPerfil = new Perfil
        {
            Nome = perfil.Nome,
            Descricao = perfil.Descricao,
            Permissoes = perfil.Permissoes,
            DataCriacao = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        return await _perfilRepository.CreateAsync(newPerfil);
    }

    public async Task UpdateAsync(UpdatePerfilRequest perfil)
    {
        var perfilExistente = await _perfilRepository.GetByIdAsync(perfil.Id);

        if (perfilExistente == null)
        {
            throw new KeyNotFoundException("Perfil não encontrado");
        }

        perfilExistente.Nome = perfil.Nome;
        perfilExistente.Descricao = perfil.Descricao;
        perfilExistente.Permissoes = perfil.Permissoes;
        perfilExistente.DataAtualizacao = DateTime.UtcNow;
        
        await _perfilRepository.UpdateAsync(perfilExistente);
    }

    public async Task DeleteAsync(int id)
    {
        await _perfilRepository.DeleteAsync(id);
    }
}
