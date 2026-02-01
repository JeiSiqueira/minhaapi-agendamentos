using MinhaApi.Data;
using MinhaApi.DTOs.Agendamento;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;
using MinhaApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MinhaApi.Services.Implementations
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _repository;
        private readonly AppDbContext _context;

        public AgendamentoService(IAgendamentoRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public IEnumerable<AgendamentoResponseDto> Listar()
        {
            var lista = _repository.Listar();

            return lista.Select(a => new AgendamentoResponseDto
            {
                Id = a.Id,
                Data = a.Data,
                Cliente = a.Usuario != null ? a.Usuario.Nome : string.Empty,
                Servico = a.Servico != null ? a.Servico.Nome : string.Empty,
                Profissional = a.Profissional != null ? a.Profissional.Nome : string.Empty
            }).ToList();
        }

        public AgendamentoResponseDto Criar(AgendamentoCreateDto dto)
        {
            // Validate foreign keys exist to avoid DB FK violations
            var usuarioExists = _context.Usuarios.Any(u => u.Id == dto.UsuarioId);
            if (!usuarioExists)
                throw new KeyNotFoundException("Usuario not found");

            var servicoExists = _context.Servicos.Any(s => s.Id == dto.ServicoId);
            if (!servicoExists)
                throw new KeyNotFoundException("Servico not found");

            var profissionalExists = _context.Profissionais.Any(p => p.Id == dto.ProfissionalId);
            if (!profissionalExists)
                throw new KeyNotFoundException("Profissional not found");

            var agendamento = new Agendamento
            {
                Id = Guid.NewGuid(),
                UsuarioId = dto.UsuarioId,
                ServicoId = dto.ServicoId,
                ProfissionalId = dto.ProfissionalId,
                Data = dto.Data
            };

            _repository.Add(agendamento);

            var criado = _repository.ObterPorIdComIncludes(agendamento.Id);

            return new AgendamentoResponseDto
            {
                Id = agendamento.Id,
                Data = agendamento.Data,
                Cliente = criado?.Usuario?.Nome ?? string.Empty,
                Servico = criado?.Servico?.Nome ?? string.Empty,
                Profissional = criado?.Profissional?.Nome ?? string.Empty
            };
        }

        public AgendamentoResponseDto? ObterPorId(Guid id)
        {
            var ag = _repository.ObterPorIdComIncludes(id);
            if (ag == null) return null;

            return new AgendamentoResponseDto
            {
                Id = ag.Id,
                Data = ag.Data,
                Cliente = ag.Usuario?.Nome ?? string.Empty,
                Servico = ag.Servico?.Nome ?? string.Empty,
                Profissional = ag.Profissional?.Nome ?? string.Empty
            };
        }

        public bool Remover(Guid id)
        {
            var ag = _repository.ObterPorIdComIncludes(id);
            if (ag == null) return false;

            _repository.Remove(id);
            return true;
        }

        public AgendamentoResponseDto? Atualizar(AgendamentoUpdateDto dto)
        {
            var ag = _repository.ObterPorIdComIncludes(dto.Id);
            if (ag == null) return null;

            // validate fks
            if (!_context.Usuarios.Any(u => u.Id == dto.UsuarioId)) throw new KeyNotFoundException("Usuario not found");
            if (!_context.Servicos.Any(s => s.Id == dto.ServicoId)) throw new KeyNotFoundException("Servico not found");
            if (!_context.Profissionais.Any(p => p.Id == dto.ProfissionalId)) throw new KeyNotFoundException("Profissional not found");

            ag.UsuarioId = dto.UsuarioId;
            ag.ServicoId = dto.ServicoId;
            ag.ProfissionalId = dto.ProfissionalId;
            ag.Data = dto.Data;

            _repository.Update(ag);

            var atualizado = _repository.ObterPorIdComIncludes(ag.Id);

            return new AgendamentoResponseDto
            {
                Id = atualizado!.Id,
                Data = atualizado.Data,
                Cliente = atualizado.Usuario?.Nome ?? string.Empty,
                Servico = atualizado.Servico?.Nome ?? string.Empty,
                Profissional = atualizado.Profissional?.Nome ?? string.Empty
            };
        }
    }
}
