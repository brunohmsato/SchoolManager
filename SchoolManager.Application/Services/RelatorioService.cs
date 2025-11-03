using SchoolManager.Application.DTOs;
using SchoolManager.Application.Interfaces;
using SchoolManager.Domain.DTOs;
using SchoolManager.Domain.Interfaces;

namespace SchoolManager.Application.Services;

public class RelatorioService(IRelatorioRepository repository) : IRelatorioService
{
    private readonly IRelatorioRepository _repository = repository;

    public async Task<IEnumerable<MediaAlunoDto>> ObterMediasPorAlunoAsync()
        => await _repository.ObterMediasPorAlunoAsync();

    public async Task<IEnumerable<MediaDisciplinaDto>> ObterMediasPorDisciplinaAsync()
        => await _repository.ObterMediasPorDisciplinaAsync();

    public async Task<IEnumerable<RankingAlunoDto>> ObterRankingGeralAsync()
        => await _repository.ObterRankingGeralAsync();
}