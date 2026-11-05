using MinhaApi.DTOs.Usuario;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MinhaApi.Services.Interfaces;

public interface IUsuarioService
{
    UsuarioResponseDto Criar(UsuarioCreateDto dto);
    Task<UsuarioResponseDto?> ObterPorIdAsync(Guid id);
    UsuarioResponseDto? Atualizar(UsuarioUpdateDto dto);
    bool Remover(Guid id);
    IEnumerable<UsuarioResponseDto> Listar();

    // Valida e-mail e senha. Retorna o usuário quando as credenciais
    // estiverem corretas e null quando não estiverem.
    Task<UsuarioResponseDto?> AutenticarAsync(UsuarioLoginDto dto);
}
