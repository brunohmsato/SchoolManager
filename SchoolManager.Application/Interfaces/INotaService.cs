using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.DTOs.Response;
using SchoolManager.Domain.Entities;

namespace SchoolManager.Application.Interfaces;

public interface INotaService
{
    Task<IEnumerable<NotaResponseDto>> ObterTodasAsync();

    Task<NotaResponseDto> CriarAsync(NotaCreateDto dto);

    Task RemoverAsync(int id);
}