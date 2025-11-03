using System.ComponentModel.DataAnnotations;

namespace SchoolManager.Domain.Entities;

public class Nota
{
    public int Id { get; set; }

    [Required]
    public int AlunoId { get; set; }

    [Required]
    public int DisciplinaId { get; set; }

    [Range(0, 10)]
    public double Valor { get; set; }

    public virtual Aluno Aluno { get; set; }
    public virtual Disciplina Disciplina { get; set; }
}