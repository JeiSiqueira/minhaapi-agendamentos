using Microsoft.AspNetCore.Mvc;
using MinhaApi.DTOs.Usuario;
using MinhaApi.Services.Interfaces;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuariosController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(UsuarioCreateDto dto)
    {
        var created = _service.Criar(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Valida as credenciais do usuário.
    /// </summary>
    /// <returns>200 com os dados do usuário, ou 401 quando as credenciais estiverem incorretas.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(UsuarioLoginDto dto)
    {
        var usuario = await _service.AutenticarAsync(dto);

        if (usuario == null)
        {
            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });
        }

        return Ok(usuario);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var u = await _service.ObterPorIdAsync(id);
        if (u == null) return NotFound();
        return Ok(u);
    }

    [HttpPut]
    public IActionResult Atualizar(UsuarioUpdateDto dto)
    {
        var updated = _service.Atualizar(dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Remover(Guid id)
    {
        var ok = _service.Remover(id);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var usuarios = _service.Listar();
        return Ok(usuarios);
    }
}
