using Microsoft.AspNetCore.Mvc;
using MinhaApi.DTOs.Servico;
using MinhaApi.Services.Interfaces;

namespace MinhaApi.Controllers;

[ApiController]
[Route("api/servicos")]
public class ServicosController : ControllerBase
{
    private readonly IServicoService _service;

    public ServicosController(IServicoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Listar() => Ok(_service.Listar());

    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var s = _service.ObterPorId(id);
        if (s == null) return NotFound();
        return Ok(s);
    }

    [HttpPost]
    public IActionResult Criar(ServicoResponseDto dto)
    {
        var created = _service.Criar(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = created.Id }, created);
    }

    [HttpPut]
    public IActionResult Atualizar(ServicoUpdateDto dto)
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
