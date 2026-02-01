using Microsoft.AspNetCore.Mvc;
using MinhaApi.DTOs.Profissional;
using MinhaApi.Services.Interfaces;

namespace MinhaApi.Controllers;

[ApiController]
[Route("api/profissionais")]
public class ProfissionaisController : ControllerBase
{
    private readonly IProfissionalService _service;

    public ProfissionaisController(IProfissionalService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Listar() => Ok(_service.Listar());

    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var p = _service.ObterPorId(id);
        if (p == null) return NotFound();
        return Ok(p);
    }

    [HttpPost]
    public IActionResult Criar(ProfissionalResponseDto dto)
    {
        var created = _service.Criar(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = created.Id }, created);
    }

    [HttpPut]
    public IActionResult Atualizar(ProfissionalUpdateDto dto)
    {
        var updated = _service.Atualizar(dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Remover(int id)
    {
        var ok = _service.Remover(id);
        if (!ok) return NotFound();
        return NoContent();
    }
}
