using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.Interfaces;

namespace SchoolManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotasController(INotaService service) : ControllerBase
{
    private readonly INotaService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.ObterTodasAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NotaCreateDto nota)
    {
        try
        {
            var created = await _service.CriarAsync(nota);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
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