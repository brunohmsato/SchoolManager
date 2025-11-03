using FluentAssertions;
using SchoolManager.Application.Validators;
using SchoolManager.Domain.Entities;

namespace SchoolManager.Tests.Validators;

public class AlunoValidatorTests
{
    private readonly AlunoValidator _validator = new();

    [Fact]
    public void Deve_Passar_Quando_DadosValidos()
    {
        var aluno = new Aluno
        {
            NomeCompleto = "João da Silva",
            Matricula = "A001",
            DataNascimento = new DateOnly(2000, 5, 10)
        };

        var result = _validator.Validate(aluno);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Falhar_Quando_NomeVazio()
    {
        var aluno = new Aluno
        {
            NomeCompleto = "",
            Matricula = "A001",
            DataNascimento = new DateOnly(2000, 5, 10)
        };

        var result = _validator.Validate(aluno);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "O nome é obrigatório.");
    }

    [Fact]
    public void Deve_Falhar_Quando_NomeCurto()
    {
        var aluno = new Aluno
        {
            NomeCompleto = "Jo",
            Matricula = "A001",
            DataNascimento = new DateOnly(2000, 5, 10)
        };

        var result = _validator.Validate(aluno);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "O nome deve ter pelo menos 3 caracteres.");
    }

    [Fact]
    public void Deve_Falhar_Quando_MatriculaVazia()
    {
        var aluno = new Aluno
        {
            NomeCompleto = "João da Silva",
            Matricula = "",
            DataNascimento = new DateOnly(2000, 5, 10)
        };

        var result = _validator.Validate(aluno);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "A matrícula é obrigatória.");
    }

    [Fact]
    public void Deve_Falhar_Quando_DataNascimentoFutura()
    {
        var aluno = new Aluno
        {
            NomeCompleto = "João da Silva",
            Matricula = "A001",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today.AddDays(1))
        };

        var result = _validator.Validate(aluno);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "Data de nascimento inválida.");
    }
}