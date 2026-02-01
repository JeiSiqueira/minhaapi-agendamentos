using MinhaApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaApi.Repositories.Interfaces;

public interface IAgendamentoRepository
{
    IEnumerable<Agendamento> Listar();
    Agendamento? ObterPorIdComIncludes(Guid id);
    Task AddAsync(Agendamento agendamento);
    void Add(Agendamento agendamento);
    void Remove(Guid id);
    void Update(Agendamento agendamento);
}
