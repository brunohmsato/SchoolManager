using FluentValidation;
using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.DTOs.Response;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.Validators;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Interfaces;

namespace SchoolManager.Application.Services;

public class DisciplinaService(IDisciplinaRepository repository) : IDisciplinaService
{
    private readonly IDisciplinaRepository _repository = repository;
    private readonly DisciplinaValidator _validator = new();

    public async Task<IEnumerable<DisciplinaResponseDto>> ObterTodasAsync()
    {
        var disciplinas = await _repository.GetAllAsync();
        return disciplinas.Select(d => new DisciplinaResponseDto
        {
            Id = d.Id,
            Nome = d.Nome
        });
    }

    public async Task<DisciplinaResponseDto?> ObterPorIdAsync(int id)
    {
        var disciplina = await _repository.GetByIdAsync(id);
        if (disciplina is null) return null;

        return new DisciplinaResponseDto
        {
            Id = disciplina.Id,
            Nome = disciplina.Nome
        };
    }

    public async Task<DisciplinaResponseDto> CriarAsync(DisciplinaCreateDto dto)
    {
        var disciplina = new Disciplina
        {
            Nome = dto.Nome
        };

        var result = _validator.Validate(disciplina);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        await _repository.AddAsync(disciplina);

        return new DisciplinaResponseDto
        {
            Id = disciplina.Id,
            Nome = disciplina.Nome
        };
    }

    public async Task AtualizarAsync(int id, DisciplinaCreateDto dto)
    {
        var disciplina = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Disciplina não encontrada.");
        disciplina.Nome = dto.Nome;

        var result = _validator.Validate(disciplina);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        await _repository.UpdateAsync(disciplina);
    }

    public async Task RemoverAsync(int id)
    {
        var disciplina = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Disciplina não encontrada.");
        await _repository.DeleteAsync(disciplina);
    }
}