using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaApi.Repositories.Implementations;

public class ServicoRepository : IServicoRepository
{
    private readonly AppDbContext _context;

    public ServicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Servico> Listar()
    {
        return _context.Servicos.Where(s => !s.IsDeleted).AsNoTracking().ToList();
    }

    public Servico? ObterPorId(int id)
    {
        var s = _context.Servicos.Find(id);
        return s != null && !s.IsDeleted ? s : null;
    }

    public async Task AddAsync(Servico servico)
    {
        _context.Servicos.Add(servico);
        await _context.SaveChangesAsync();
    }

    public void RemoveSoft(int id)
    {
        var s = _context.Servicos.Find(id);
        if (s == null) return;
        s.IsDeleted = true;
        _context.SaveChanges();
    }

    public void Update(Servico servico)
    {
        _context.Servicos.Update(servico);
        _context.SaveChanges();
    }
}
