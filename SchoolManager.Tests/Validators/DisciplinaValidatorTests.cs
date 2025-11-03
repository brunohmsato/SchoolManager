using FluentAssertions;
using SchoolManager.Application.Validators;
using SchoolManager.Domain.Entities;

namespace SchoolManager.Tests.Validators;

public class DisciplinaValidatorTests
{
    private readonly DisciplinaValidator _validator = new();

    [Fact]
    public void Deve_Passar_Quando_NomeValido()
    {
        var disciplina = new Disciplina { Nome = "Matemática" };

        var result = _validator.Validate(disciplina);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Falhar_Quando_NomeVazio()
    {
        var disciplina = new Disciplina { Nome = "" };

        var result = _validator.Validate(disciplina);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "O nome da disciplina é obrigatório.");
    }

    [Fact]
    public void Deve_Falhar_Quando_NomeMuitoCurto()
    {
        var disciplina = new Disciplina { Nome = "A" };

        var result = _validator.Validate(disciplina);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("deve ser maior ou igual a 2 caracteres"));
    }
}