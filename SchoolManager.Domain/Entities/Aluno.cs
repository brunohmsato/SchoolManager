using System.ComponentModel.DataAnnotations;

namespace SchoolManager.Domain.Entities;

public class Aluno
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string NomeCompleto { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Matricula { get; set; } = string.Empty;

    public DateOnly DataNascimento { get; set; }

    public ICollection<Nota> Notas { get; set; } = new List<Nota>();
}