using MinhaApi.DTOs.Profissional;
using System.Collections.Generic;

namespace MinhaApi.Services.Interfaces;

public interface IProfissionalService
{
    IEnumerable<ProfissionalResponseDto> Listar();
    ProfissionalResponseDto? ObterPorId(int id);
    ProfissionalResponseDto Criar(ProfissionalResponseDto dto);
    bool Remover(int id);
    ProfissionalResponseDto? Atualizar(ProfissionalUpdateDto dto);
}
