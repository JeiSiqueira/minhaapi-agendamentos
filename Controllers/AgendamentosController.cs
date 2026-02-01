using Microsoft.AspNetCore.Mvc;
using MinhaApi.DTOs.Agendamento;
using MinhaApi.Services.Interfaces;
using System;

namespace MinhaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendamentosController : ControllerBase
{
    private readonly IAgendamentoService _service;

    public AgendamentosController(IAgendamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var agendamentos = _service.Listar();
        return Ok(agendamentos);
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(Guid id)
    {
        var ag = _service.ObterPorId(id);
        if (ag == null) return NotFound();
        return Ok(ag);
    }

    [HttpPost]
    public IActionResult Criar(AgendamentoCreateDto dto)
    {
        try
        {
            var agendamento = _service.Criar(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = agendamento.Id }, agendamento);
        }
        catch (KeyNotFoundException knf)
        {
            return NotFound(new { message = knf.Message });
        }
        catch (ArgumentException ae)
        {
            return BadRequest(new { message = ae.Message });
        }
        catch (Exception ex)
        {
            // log if logger available
            return Problem(detail: ex.Message);
        }
    }

    [HttpPut]
    public IActionResult Atualizar(AgendamentoUpdateDto dto)
    {
        try
        {
            var updated = _service.Atualizar(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        catch (KeyNotFoundException knf)
        {
            return NotFound(new { message = knf.Message });
        }
        catch (ArgumentException ae)
        {
            return BadRequest(new { message = ae.Message });
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Remover(Guid id)
    {
        var ok = _service.Remover(id);
        if (!ok) return NotFound();
        return NoContent();
    }
}
