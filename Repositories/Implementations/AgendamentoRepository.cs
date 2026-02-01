using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MinhaApi.Repositories.Implementations;

public class AgendamentoRepository : IAgendamentoRepository
{
    private readonly AppDbContext _context;

    public AgendamentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Agendamento> Listar()
    {
        return _context.Agendamentos
            .Include(a => a.Usuario)
            .Include(a => a.Servico)
            .Include(a => a.Profissional)
            .AsNoTracking()
            .ToList();
    }

    public Agendamento? ObterPorIdComIncludes(Guid id)
    {
        return _context.Agendamentos
            .Include(a => a.Usuario)
            .Include(a => a.Servico)
            .Include(a => a.Profissional)
            .FirstOrDefault(a => a.Id == id);
    }

    public async Task AddAsync(Agendamento agendamento)
    {
        _context.Agendamentos.Add(agendamento);
        await _context.SaveChangesAsync();
    }

    public void Add(Agendamento agendamento)
    {
        _context.Agendamentos.Add(agendamento);
        _context.SaveChanges();
    }

    public void Remove(Guid id)
    {
        var ag = _context.Agendamentos.Find(id);
        if (ag == null) return;
        _context.Agendamentos.Remove(ag);
        _context.SaveChanges();
    }

    public void Update(Agendamento agendamento)
    {
        _context.Agendamentos.Update(agendamento);
        _context.SaveChanges();
    }
}

