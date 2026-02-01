using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MinhaApi.Repositories.Implementations;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        var u = await _context.Usuarios.FindAsync(id);
        return u != null && !u.IsDeleted ? u : null;
    }

    public async Task AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public void RemoveSoft(Guid id)
    {
        var u = _context.Usuarios.Find(id);
        if (u == null) return;
        u.IsDeleted = true;
        _context.SaveChanges();
    }

    public IEnumerable<Usuario> Listar()
    {
        return _context.Usuarios.Where(u => !u.IsDeleted).AsNoTracking().ToList();
    }
}