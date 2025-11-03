using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolManager.API.Controllers;
using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.DTOs.Response;
using SchoolManager.Application.Interfaces;

namespace SchoolManager.Tests.Controllers;

public class NotasControllerTests
{
    private readonly Mock<INotaService> _serviceMock;
    private readonly NotasController _controller;

    public NotasControllerTests()
    {
        _serviceMock = new Mock<INotaService>();
        _controller = new NotasController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetAll_DeveRetornarOk_ComListaDeNotas()
    {
        // Arrange
        var lista = new List<NotaResponseDto>
        {
            new() { Id = 1, AlunoId = 1, NomeAluno = "João", DisciplinaId = 1, NomeDisciplina = "Matemática", Valor = 8.5 },
            new() { Id = 2, AlunoId = 2, NomeAluno = "Maria", DisciplinaId = 2, NomeDisciplina = "História", Valor = 9.0 }
        };
        _serviceMock.Setup(s => s.ObterTodasAsync()).ReturnsAsync(lista);

        // Act
        var result = await _controller.GetAll() as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(lista);
    }

    [Fact]
    public async Task Create_DeveRetornarCreated_QuandoValido()
    {
        // Arrange
        var createDto = new NotaCreateDto { AlunoId = 1, DisciplinaId = 1, Valor = 8.5 };
        var responseDto = new NotaResponseDto
        {
            Id = 1,
            AlunoId = 1,
            NomeAluno = "João",
            DisciplinaId = 1,
            NomeDisciplina = "Matemática",
            Valor = 8.5
        };
        _serviceMock.Setup(s => s.CriarAsync(createDto)).ReturnsAsync(responseDto);

        // Act
        var result = await _controller.Create(createDto) as CreatedAtActionResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(201);
        result.Value.Should().BeEquivalentTo(responseDto);
        result.RouteValues!["id"].Should().Be(1);
    }

    [Fact]
    public async Task Create_DeveRetornarBadRequest_QuandoNotaInvalida()
    {
        // Arrange
        var dto = new NotaCreateDto { AlunoId = 1, DisciplinaId = 1, Valor = 11.0 };
        _serviceMock
            .Setup(s => s.CriarAsync(dto))
            .ThrowsAsync(new ValidationException("A nota deve estar entre 0 e 10."));

        // Act
        var result = await _controller.Create(dto) as BadRequestObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Create_DeveRetornarNotFound_QuandoAlunoOuDisciplinaNaoExistir()
    {
        // Arrange
        var dto = new NotaCreateDto { AlunoId = 99, DisciplinaId = 1, Valor = 7.5 };
        _serviceMock
            .Setup(s => s.CriarAsync(dto))
            .ThrowsAsync(new KeyNotFoundException("Aluno não encontrado."));

        // Act
        var result = await _controller.Create(dto) as NotFoundObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(404);
        result.Value.Should().Be("Aluno não encontrado.");
    }

    [Fact]
    public async Task Delete_DeveRetornarNoContent_QuandoSucesso()
    {
        // Arrange
        _serviceMock.Setup(s => s.RemoverAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_DeveRetornarNotFound_QuandoNotaNaoExiste()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.RemoverAsync(99))
            .ThrowsAsync(new KeyNotFoundException("Nota não encontrada."));

        // Act
        var result = await _controller.Delete(99) as NotFoundObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(404);
        result.Value.Should().Be("Nota não encontrada.");
    }
}