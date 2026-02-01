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
            Senha = dto.Senha, // TODO: hashing
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
