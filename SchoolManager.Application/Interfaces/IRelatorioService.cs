using SchoolManager.Application.DTOs;
using SchoolManager.Domain.DTOs;

namespace SchoolManager.Application.Interfaces;

public interface IRelatorioService
{
    Task<IEnumerable<MediaAlunoDto>> ObterMediasPorAlunoAsync();

    Task<IEnumerable<MediaDisciplinaDto>> ObterMediasPorDisciplinaAsync();

    Task<IEnumerable<RankingAlunoDto>> ObterRankingGeralAsync();
}