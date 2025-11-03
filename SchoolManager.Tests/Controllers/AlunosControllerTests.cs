using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolManager.API.Controllers;
using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.DTOs.Response;
using SchoolManager.Application.Interfaces;

namespace SchoolManager.Tests.Controllers;

public class AlunosControllerTests
{
    private readonly Mock<IAlunoService> _serviceMock;
    private readonly AlunosController _controller;

    public AlunosControllerTests()
    {
        _serviceMock = new Mock<IAlunoService>();
        _controller = new AlunosController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetAll_DeveRetornarOk_ComListaDeAlunos()
    {
        // Arrange
        var lista = new List<AlunoResponseDto>
        {
            new() { Id = 1, NomeCompleto = "João", Matricula = "A001", DataNascimento = new DateOnly(2000, 1, 1) },
            new() { Id = 2, NomeCompleto = "Maria", Matricula = "A002", DataNascimento = new DateOnly(2001, 5, 10) }
        };
        _serviceMock.Setup(s => s.ObterTodosAsync()).ReturnsAsync(lista);

        // Act
        var result = await _controller.GetAll() as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(lista);
    }

    [Fact]
    public async Task GetById_DeveRetornarOk_QuandoAlunoExiste()
    {
        // Arrange
        var aluno = new AlunoResponseDto { Id = 1, NomeCompleto = "João", Matricula = "A001", DataNascimento = new DateOnly(2000, 1, 1) };
        _serviceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync(aluno);

        // Act
        var result = await _controller.GetById(1) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(aluno);
    }

    [Fact]
    public async Task GetById_DeveRetornarNotFound_QuandoAlunoNaoExiste()
    {
        // Arrange
        _serviceMock.Setup(s => s.ObterPorIdAsync(99)).ReturnsAsync((AlunoResponseDto?)null);

        // Act
        var result = await _controller.GetById(99);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_DeveRetornarCreated_QuandoValido()
    {
        // Arrange
        var createDto = new AlunoCreateDto { NomeCompleto = "João", Matricula = "A001", DataNascimento = new DateOnly(2000, 1, 1) };
        var responseDto = new AlunoResponseDto { Id = 1, NomeCompleto = "João", Matricula = "A001", DataNascimento = createDto.DataNascimento };
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
    public async Task Create_DeveRetornarBadRequest_QuandoValidationFalhar()
    {
        // Arrange
        var dto = new AlunoCreateDto { NomeCompleto = "", Matricula = "", DataNascimento = new DateOnly(2025, 1, 1) };
        _serviceMock
            .Setup(s => s.CriarAsync(dto))
            .ThrowsAsync(new ValidationException("Erro de validação"));

        // Act
        var result = await _controller.Create(dto) as BadRequestObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Update_DeveRetornarNoContent_QuandoSucesso()
    {
        // Arrange
        var dto = new AlunoCreateDto { NomeCompleto = "Atualizado", Matricula = "A002", DataNascimento = new DateOnly(2001, 5, 10) };
        _serviceMock.Setup(s => s.AtualizarAsync(1, dto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Update(1, dto);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Update_DeveRetornarBadRequest_QuandoValidationFalhar()
    {
        // Arrange
        var dto = new AlunoCreateDto { NomeCompleto = "", Matricula = "", DataNascimento = new DateOnly(2025, 1, 1) };
        _serviceMock
            .Setup(s => s.AtualizarAsync(1, dto))
            .ThrowsAsync(new ValidationException("Erro de validação"));

        // Act
        var result = await _controller.Update(1, dto) as BadRequestObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(400);
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
    public async Task Delete_DeveRetornarNotFound_QuandoAlunoNaoExiste()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.RemoverAsync(99))
            .ThrowsAsync(new KeyNotFoundException("Aluno não encontrado."));

        // Act
        var result = await _controller.Delete(99) as NotFoundObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(404);
        result.Value.Should().Be("Aluno não encontrado.");
    }
}