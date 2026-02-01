using MinhaApi.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MinhaApi.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<Usuario?> ObterPorIdAsync(Guid id);
    Task AddAsync(Usuario usuario);
    void RemoveSoft(Guid id);
    IEnumerable<Usuario> Listar();
}
