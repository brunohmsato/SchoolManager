namespace SchoolManager.Application.DTOs.Request;

public class NotaCreateDto
{
    public int AlunoId { get; set; }
    public int DisciplinaId { get; set; }
    public double Valor { get; set; }
}