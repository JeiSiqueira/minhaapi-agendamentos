using MinhaApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaApi.Repositories.Interfaces;

public interface IServicoRepository
{
    IEnumerable<Servico> Listar();
    Servico? ObterPorId(int id);
    Task AddAsync(Servico servico);
    void RemoveSoft(int id);
    void Update(Servico servico);
}
