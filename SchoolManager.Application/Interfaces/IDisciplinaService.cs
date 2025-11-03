using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.DTOs.Response;

namespace SchoolManager.Application.Interfaces;

public interface IDisciplinaService
{
    Task<IEnumerable<DisciplinaResponseDto>> ObterTodasAsync();

    Task<DisciplinaResponseDto?> ObterPorIdAsync(int id);

    Task<DisciplinaResponseDto> CriarAsync(DisciplinaCreateDto dto);

    Task AtualizarAsync(int id, DisciplinaCreateDto dto);

    Task RemoverAsync(int id);
}