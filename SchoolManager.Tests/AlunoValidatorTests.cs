using FluentAssertions;
using SchoolManager.Application.Validators;
using SchoolManager.Domain.Entities;

namespace SchoolManager.Tests;

public class AlunoValidatorTests
{
    [Fact]
    public void Deve_Retornar_Erro_Quando_Nome_Vazio()
    {
        var validator = new AlunoValidator();
        var aluno = new Aluno { NomeCompleto = "" };

        var result = validator.Validate(aluno);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "NomeCompleto");
    }
}