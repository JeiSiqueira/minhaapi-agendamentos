using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaApi.Repositories.Implementations;

public class ProfissionalRepository : IProfissionalRepository
{
    private readonly AppDbContext _context;

    public ProfissionalRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Profissional> Listar()
    {
        return _context.Profissionais.Where(p => !p.IsDeleted).AsNoTracking().ToList();
    }

    public Profissional? ObterPorId(int id)
    {
        var p = _context.Profissionais.Find(id);
        return p != null && !p.IsDeleted ? p : null;
    }

    public async Task AddAsync(Profissional profissional)
    {
        _context.Profissionais.Add(profissional);
        await _context.SaveChangesAsync();
    }

    public void RemoveSoft(int id)
    {
        var p = _context.Profissionais.Find(id);
        if (p == null) return;
        p.IsDeleted = true;
        _context.SaveChanges();
    }

    public void Update(Profissional profissional)
    {
        _context.Profissionais.Update(profissional);
        _context.SaveChanges();
    }
}
