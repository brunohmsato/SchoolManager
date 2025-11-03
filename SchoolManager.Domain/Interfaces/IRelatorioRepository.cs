using SchoolManager.Application.DTOs;
using SchoolManager.Domain.DTOs;

namespace SchoolManager.Domain.Interfaces;

public interface IRelatorioRepository
{
    Task<IEnumerable<MediaAlunoDto>> ObterMediasPorAlunoAsync();

    Task<IEnumerable<MediaDisciplinaDto>> ObterMediasPorDisciplinaAsync();

    Task<IEnumerable<RankingAlunoDto>> ObterRankingGeralAsync();
}