using FluentValidation;
using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.DTOs.Response;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.Validators;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Interfaces;

namespace SchoolManager.Application.Services;

public class AlunoService(IAlunoRepository repository) : IAlunoService
{
    private readonly IAlunoRepository _repository = repository;
    private readonly AlunoValidator _validator = new();

    public async Task<IEnumerable<AlunoResponseDto>> ObterTodosAsync()
    {
        var alunos = await _repository.GetAllAsync();
        return alunos.Select(d => new AlunoResponseDto
        {
            Id = d.Id,
            NomeCompleto = d.NomeCompleto,
            Matricula = d.Matricula,
            DataNascimento = d.DataNascimento
        });
    }

    public async Task<AlunoResponseDto?> ObterPorIdAsync(int id)
    {
        var aluno = await _repository.GetByIdAsync(id);
        if (aluno is null) return null;

        return new AlunoResponseDto
        {
            Id = aluno.Id,
            NomeCompleto = aluno.NomeCompleto,
            Matricula = aluno.Matricula,
            DataNascimento = aluno.DataNascimento
        };
    }

    public async Task<AlunoResponseDto> CriarAsync(AlunoCreateDto dto)
    {
        var aluno = new Aluno
        {
            NomeCompleto = dto.NomeCompleto,
            Matricula = dto.Matricula,
            DataNascimento = dto.DataNascimento
        };

        var result = _validator.Validate(aluno);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        await _repository.AddAsync(aluno);
        return new AlunoResponseDto
        {
            Id = aluno.Id,
            NomeCompleto = aluno.NomeCompleto,
            Matricula = aluno.Matricula,
            DataNascimento = aluno.DataNascimento
        };
    }

    public async Task AtualizarAsync(int id, AlunoCreateDto dto)
    {
        var aluno = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Disciplina não encontrada.");

        aluno.NomeCompleto = dto.NomeCompleto;
        aluno.Matricula = dto.Matricula;
        aluno.DataNascimento = dto.DataNascimento;

        var result = _validator.Validate(aluno);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        await _repository.UpdateAsync(aluno);
    }

    public async Task RemoverAsync(int id)
    {
        var aluno = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Aluno não encontrado.");
        await _repository.DeleteAsync(aluno);
    }
}