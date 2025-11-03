namespace SchoolManager.Application.DTOs.Response;

public class NotaResponseDto
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public string NomeAluno { get; set; } = string.Empty;
    public int DisciplinaId { get; set; }
    public string NomeDisciplina { get; set; } = string.Empty;
    public double Valor { get; set; }
}