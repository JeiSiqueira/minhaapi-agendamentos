using MinhaApi.DTOs.Profissional;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;
using MinhaApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MinhaApi.Services.Implementations;

public class ProfissionalService : IProfissionalService
{
    private readonly IProfissionalRepository _repository;

    public ProfissionalService(IProfissionalRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<ProfissionalResponseDto> Listar()
    {
        return _repository.Listar().Select(p => new ProfissionalResponseDto
        {
            Id = p.Id,
            Nome = p.Nome
        }).ToList();
    }

    public ProfissionalResponseDto? ObterPorId(int id)
    {
        var p = _repository.ObterPorId(id);
        if (p == null) return null;
        return new ProfissionalResponseDto
        {
            Id = p.Id,
            Nome = p.Nome
        };
    }

    public ProfissionalResponseDto Criar(ProfissionalResponseDto dto)
    {
        var p = new Profissional
        {
            Nome = dto.Nome,
            IsDeleted = false
        };

        _repository.AddAsync(p).GetAwaiter().GetResult();

        return new ProfissionalResponseDto
        {
            Id = p.Id,
            Nome = p.Nome
        };
    }

    public bool Remover(int id)
    {
        var p = _repository.ObterPorId(id);
        if (p == null) return false;
        _repository.RemoveSoft(id);
        return true;
    }

    public ProfissionalResponseDto? Atualizar(ProfissionalUpdateDto dto)
    {
        var p = _repository.ObterPorId(dto.Id);
        if (p == null) return null;

        p.Nome = dto.Nome;

        _repository.Update(p);

        return new ProfissionalResponseDto
        {
            Id = p.Id,
            Nome = p.Nome
        };
    }
}
