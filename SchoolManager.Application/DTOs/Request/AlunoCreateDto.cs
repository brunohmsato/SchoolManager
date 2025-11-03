namespace SchoolManager.Application.DTOs.Request;

public class AlunoCreateDto
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
}