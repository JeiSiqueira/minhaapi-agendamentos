using MinhaApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaApi.Repositories.Interfaces;

public interface IProfissionalRepository
{
    IEnumerable<Profissional> Listar();
    Profissional? ObterPorId(int id);
    Task AddAsync(Profissional profissional);
    void RemoveSoft(int id);
    void Update(Profissional profissional);
}
