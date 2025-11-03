using FluentValidation;
using SchoolManager.Domain.Entities;

namespace SchoolManager.Application.Validators;

public class AlunoValidator : AbstractValidator<Aluno>
{
    public AlunoValidator()
    {
        RuleFor(a => a.NomeCompleto)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");

        RuleFor(a => a.Matricula)
            .NotEmpty().WithMessage("A matrícula é obrigatória.");

        RuleFor(a => a.DataNascimento)
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Data de nascimento inválida.");
    }
}