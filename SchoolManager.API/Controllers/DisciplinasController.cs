using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.Interfaces;

namespace SchoolManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DisciplinasController(IDisciplinaService service) : ControllerBase
{
    private readonly IDisciplinaService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.ObterTodasAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var disciplina = await _service.ObterPorIdAsync(id);
        if (disciplina is null) return NotFound();
        return Ok(disciplina);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DisciplinaCreateDto disciplina)
    {
        try
        {
            var created = await _service.CriarAsync(disciplina);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DisciplinaCreateDto disciplina)
    {
        try
        {
            await _service.AtualizarAsync(id, disciplina);
            return NoContent();
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.RemoverAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}