namespace SchoolManager.Application.DTOs.Response;

public class AlunoResponseDto
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
}