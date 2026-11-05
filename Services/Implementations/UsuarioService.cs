using MinhaApi.Data;
using MinhaApi.DTOs.Usuario;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;
using MinhaApi.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MinhaApi.Services.Implementations;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;
    private readonly IUsuarioRepository _repository;

    public UsuarioService(AppDbContext context, IUsuarioRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public UsuarioResponseDto Criar(UsuarioCreateDto dto)
    {
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Email = dto.Email,
            // A senha nunca é armazenada em texto: guardamos apenas o hash BCrypt.
            // O "salt" é gerado automaticamente e fica embutido no próprio hash,
            // por isso dois usuários com a mesma senha têm hashes diferentes.
            Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            IsDeleted = false
        };

        _repository.AddAsync(usuario).GetAwaiter().GetResult();

        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }

    public async Task<UsuarioResponseDto?> AutenticarAsync(UsuarioLoginDto dto)
    {
        var usuario = await _repository.ObterPorEmailAsync(dto.Email);

        // Não é possível comparar senhas diretamente: o hash é diferente a cada
        // geração. O Verify refaz o hash da senha informada usando o salt que
        // está dentro do hash salvo e compara os resultados.
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha))
        {
            // Mesma resposta para e-mail inexistente e senha errada, para não
            // revelar quais e-mails estão cadastrados.
            return null;
        }

        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }

    public async Task<UsuarioResponseDto?> ObterPorIdAsync(Guid id)
    {
        var u = await _repository.ObterPorIdAsync(id);
        if (u == null) return null;

        return new UsuarioResponseDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email
        };
    }

    public UsuarioResponseDto? Atualizar(UsuarioUpdateDto dto)
    {
        var u = _context.Usuarios.Find(dto.Id);
        if (u == null || u.IsDeleted) return null;

        u.Nome = dto.Nome;
        u.Email = dto.Email;

        _context.Usuarios.Update(u);
        _context.SaveChanges();

        return new UsuarioResponseDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email
        };
    }

    public bool Remover(Guid id)
    {
        var u = _context.Usuarios.Find(id);
        if (u == null || u.IsDeleted) return false;

        _repository.RemoveSoft(id);
        return true;
    }

    public IEnumerable<UsuarioResponseDto> Listar()
    {
        return _repository.Listar().Select(u => new UsuarioResponseDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email
        }).ToList();
    }
}
