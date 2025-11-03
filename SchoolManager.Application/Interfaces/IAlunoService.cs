using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.DTOs.Response;

namespace SchoolManager.Application.Interfaces;

public interface IAlunoService
{
    Task<IEnumerable<AlunoResponseDto>> ObterTodosAsync();

    Task<AlunoResponseDto?> ObterPorIdAsync(int id);

    Task<AlunoResponseDto> CriarAsync(AlunoCreateDto dto);

    Task AtualizarAsync(int id, AlunoCreateDto dto);

    Task RemoverAsync(int id);
}