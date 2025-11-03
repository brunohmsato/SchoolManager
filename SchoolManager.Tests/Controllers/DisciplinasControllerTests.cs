using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolManager.API.Controllers;
using SchoolManager.Application.DTOs.Request;
using SchoolManager.Application.DTOs.Response;
using SchoolManager.Application.Interfaces;

namespace SchoolManager.Tests.Controllers;

public class DisciplinasControllerTests
{
    private readonly Mock<IDisciplinaService> _serviceMock;
    private readonly DisciplinasController _controller;

    public DisciplinasControllerTests()
    {
        _serviceMock = new Mock<IDisciplinaService>();
        _controller = new DisciplinasController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetAll_DeveRetornarOk_ComListaDeDisciplinas()
    {
        // Arrange
        var lista = new List<DisciplinaResponseDto>
        {
            new() { Id = 1, Nome = "Matemática" },
            new() { Id = 2, Nome = "História" }
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
    public async Task GetById_DeveRetornarOk_QuandoDisciplinaExiste()
    {
        // Arrange
        var disciplina = new DisciplinaResponseDto { Id = 1, Nome = "Matemática" };
        _serviceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync(disciplina);

        // Act
        var result = await _controller.GetById(1) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(disciplina);
    }

    [Fact]
    public async Task GetById_DeveRetornarNotFound_QuandoDisciplinaNaoExiste()
    {
        // Arrange
        _serviceMock.Setup(s => s.ObterPorIdAsync(99)).ReturnsAsync((DisciplinaResponseDto?)null);

        // Act
        var result = await _controller.GetById(99);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_DeveRetornarCreated_QuandoValido()
    {
        // Arrange
        var createDto = new DisciplinaCreateDto { Nome = "Matemática" };
        var responseDto = new DisciplinaResponseDto { Id = 1, Nome = "Matemática" };
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
        var dto = new DisciplinaCreateDto { Nome = "" };
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
        var dto = new DisciplinaCreateDto { Nome = "Atualizada" };
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
        var dto = new DisciplinaCreateDto { Nome = "" };
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
    public async Task Delete_DeveRetornarNotFound_QuandoDisciplinaNaoExiste()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.RemoverAsync(99))
            .ThrowsAsync(new KeyNotFoundException("Disciplina não encontrada."));

        // Act
        var result = await _controller.Delete(99) as NotFoundObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(404);
        result.Value.Should().Be("Disciplina não encontrada.");
    }
}