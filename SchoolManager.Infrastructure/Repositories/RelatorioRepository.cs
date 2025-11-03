using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.DTOs;
using SchoolManager.Domain.DTOs;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Infrastructure.Data;

namespace SchoolManager.Infrastructure.Repositories;

public class RelatorioRepository(SchoolContext context) : IRelatorioRepository
{
    private readonly SchoolContext _context = context;

    public async Task<IEnumerable<MediaAlunoDto>> ObterMediasPorAlunoAsync()
    {
        var mediasQuery =
            from n in _context.Notas
            group n by n.AlunoId into g
            select new { AlunoId = g.Key, Media = g.Average(x => x.Valor) };

        var lista =
            await (from m in mediasQuery
                   join a in _context.Alunos on m.AlunoId equals a.Id
                   orderby a.NomeCompleto
                   select new MediaAlunoDto(m.AlunoId, a.NomeCompleto, m.Media))
                  .ToListAsync();

        return lista.Select(x => new MediaAlunoDto(x.AlunoId, x.Aluno, Math.Round(x.Media, 2))).ToList();
    }

    public async Task<IEnumerable<MediaDisciplinaDto>> ObterMediasPorDisciplinaAsync()
    {
        var mediasQuery =
            from n in _context.Notas
            group n by n.DisciplinaId into g
            select new { DisciplinaId = g.Key, Media = g.Average(x => x.Valor) };

        var lista =
            await (from m in mediasQuery
                   join d in _context.Disciplinas on m.DisciplinaId equals d.Id
                   orderby d.Nome
                   select new MediaDisciplinaDto(m.DisciplinaId, d.Nome, m.Media))
                  .ToListAsync();

        return lista.Select(x => new MediaDisciplinaDto(x.DisciplinaId, x.Disciplina, Math.Round(x.Media, 2))).ToList();
    }

    public async Task<IEnumerable<RankingAlunoDto>> ObterRankingGeralAsync()
    {
        var mediasQuery =
            from n in _context.Notas
            group n by n.AlunoId into g
            select new { AlunoId = g.Key, Media = g.Average(x => x.Valor) };

        var medias =
            await (from m in mediasQuery
                   join a in _context.Alunos on m.AlunoId equals a.Id
                   select new { m.AlunoId, a.NomeCompleto, m.Media })
                  .ToListAsync();

        var ordenado = medias
            .Select(x => new { x.AlunoId, x.NomeCompleto, Media = Math.Round(x.Media, 2) })
            .OrderByDescending(x => x.Media)
            .ToList();

        return ordenado
            .Select((x, i) => new RankingAlunoDto(x.AlunoId, x.NomeCompleto, x.Media, i + 1))
            .ToList();
    }

}