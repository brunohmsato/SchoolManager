using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.Interfaces;

namespace SchoolManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlunosController(IAlunoService service) : ControllerBase
{
    private readonly IAlunoService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.ObterTodosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var aluno = await _service.ObterPorIdAsync(id);
        if (aluno is null) return NotFound();
        return Ok(aluno);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AlunoCreateDto aluno)
    {
        try
        {
            var created = await _service.CriarAsync(aluno);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AlunoCreateDto aluno)
    {
        try
        {
            await _service.AtualizarAsync(id, aluno);
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