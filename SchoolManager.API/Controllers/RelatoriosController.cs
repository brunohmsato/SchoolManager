using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Interfaces;

namespace SchoolManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RelatoriosController(IRelatorioService service) : ControllerBase
{
    private readonly IRelatorioService _service = service;

    [HttpGet("medias")]
    public async Task<IActionResult> GetMediasPorAluno()
    {
        var medias = await _service.ObterMediasPorAlunoAsync();
        return Ok(medias);
    }

    [HttpGet("medias-disciplinas")]
    public async Task<IActionResult> GetMediasPorDisciplina()
    {
        var medias = await _service.ObterMediasPorDisciplinaAsync();
        return Ok(medias);
    }

    [HttpGet("ranking")]
    public async Task<IActionResult> GetRanking()
    {
        var ranking = await _service.ObterRankingGeralAsync();
        return Ok(ranking);
    }
}