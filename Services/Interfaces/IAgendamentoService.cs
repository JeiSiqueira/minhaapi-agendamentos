using MinhaApi.DTOs.Agendamento;
using System.Collections.Generic;
using System;

namespace MinhaApi.Services.Interfaces
{
    public interface IAgendamentoService
    {
        IEnumerable<AgendamentoResponseDto> Listar();
        AgendamentoResponseDto Criar(AgendamentoCreateDto dto);
        AgendamentoResponseDto? ObterPorId(Guid id);
        bool Remover(Guid id);
        AgendamentoResponseDto? Atualizar(AgendamentoUpdateDto dto);
    }
}
