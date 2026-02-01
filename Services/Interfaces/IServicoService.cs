using MinhaApi.DTOs.Servico;
using System.Collections.Generic;

namespace MinhaApi.Services.Interfaces;

public interface IServicoService
{
    IEnumerable<ServicoResponseDto> Listar();
    ServicoResponseDto? ObterPorId(int id);
    ServicoResponseDto Criar(ServicoResponseDto dto);
    bool Remover(int id);
    ServicoResponseDto? Atualizar(ServicoUpdateDto dto);
}
