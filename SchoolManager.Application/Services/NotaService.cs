using FluentValidation;
using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.DTOs.Response;
using SchoolManager.Application.Interfaces;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Interfaces;

namespace SchoolManager.Application.Services;

public class NotaService(
    INotaRepository repository,
    IAlunoRepository alunoRepository,
    IDisciplinaRepository disciplinaRepository) : INotaService
{
    private readonly INotaRepository _repository = repository;
    private readonly IAlunoRepository _alunoRepository = alunoRepository;
    private readonly IDisciplinaRepository _disciplinaRepository = disciplinaRepository;

    public async Task<IEnumerable<NotaResponseDto>> ObterTodasAsync()
    {
        var notas = await _repository.GetAllAsync();
        return notas.Select(d => new NotaResponseDto
        {
            Id = d.Id,
            AlunoId = d.AlunoId,
            NomeAluno = d.Aluno.NomeCompleto,
            DisciplinaId = d.DisciplinaId,
            NomeDisciplina = d.Disciplina.Nome,
            Valor = d.Valor
        });
    }

    public async Task<NotaResponseDto> CriarAsync(NotaCreateDto dto)
    {
        if (dto.Valor < 0 || dto.Valor > 10)
            throw new ValidationException("A nota deve estar entre 0 e 10.");

        var aluno = await _alunoRepository.GetByIdAsync(dto.AlunoId)
            ?? throw new KeyNotFoundException("Aluno não encontrado.");

        var disciplina = await _disciplinaRepository.GetByIdAsync(dto.DisciplinaId)
            ?? throw new KeyNotFoundException("Disciplina não encontrada.");

        var nota = new Nota
        {
            AlunoId = dto.AlunoId,
            DisciplinaId = dto.DisciplinaId,
            Valor = dto.Valor
        };

        await _repository.AddAsync(nota);

        return new NotaResponseDto
        {
            Id = nota.Id,
            AlunoId = nota.AlunoId,
            NomeAluno = aluno.NomeCompleto,
            DisciplinaId = nota.DisciplinaId,
            NomeDisciplina = disciplina.Nome,
            Valor = nota.Valor
        };
    }

    public async Task RemoverAsync(int id)
    {
        var nota = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Nota não encontrada.");
        await _repository.DeleteAsync(nota);
    }
}