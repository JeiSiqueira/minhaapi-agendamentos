using MinhaApi.DTOs.Servico;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;
using MinhaApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MinhaApi.Services.Implementations;

public class ServicoService : IServicoService
{
    private readonly IServicoRepository _repository;

    public ServicoService(IServicoRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<ServicoResponseDto> Listar()
    {
        return _repository.Listar().Select(s => new ServicoResponseDto
        {
            Id = s.Id,
            Nome = s.Nome,
            Preco = s.Preco,
            DuracaoMinutos = s.DuracaoMinutos
        }).ToList();
    }

    public ServicoResponseDto? ObterPorId(int id)
    {
        var s = _repository.ObterPorId(id);
        if (s == null) return null;
        return new ServicoResponseDto
        {
            Id = s.Id,
            Nome = s.Nome,
            Preco = s.Preco,
            DuracaoMinutos = s.DuracaoMinutos
        };
    }

    public ServicoResponseDto Criar(ServicoResponseDto dto)
    {
        var s = new Servico
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            DuracaoMinutos = dto.DuracaoMinutos,
            IsDeleted = false
        };

        _repository.AddAsync(s).GetAwaiter().GetResult();

        return new ServicoResponseDto
        {
            Id = s.Id,
            Nome = s.Nome,
            Preco = s.Preco,
            DuracaoMinutos = s.DuracaoMinutos
        };
    }

    public bool Remover(int id)
    {
        var s = _repository.ObterPorId(id);
        if (s == null) return false;
        _repository.RemoveSoft(id);
        return true;
    }

    public ServicoResponseDto? Atualizar(ServicoUpdateDto dto)
    {
        var s = _repository.ObterPorId(dto.Id);
        if (s == null) return null;

        s.Nome = dto.Nome;
        s.Preco = dto.Preco;
        s.DuracaoMinutos = dto.DuracaoMinutos;

        _repository.Update(s);

        return new ServicoResponseDto
        {
            Id = s.Id,
            Nome = s.Nome,
            Preco = s.Preco,
            DuracaoMinutos = s.DuracaoMinutos
        };
    }
}
