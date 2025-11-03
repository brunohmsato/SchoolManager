using FluentValidation;
using SchoolManager.Domain.Entities;

namespace SchoolManager.Application.Validators;

public class DisciplinaValidator : AbstractValidator<Disciplina>
{
    public DisciplinaValidator()
    {
        RuleFor(d => d.Nome)
            .NotEmpty().WithMessage("O nome da disciplina é obrigatório.")
            .MinimumLength(2);
    }
}