using System.ComponentModel.DataAnnotations;

namespace SchoolManager.Domain.Entities;

public class Disciplina
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Nome { get; set; } = string.Empty;

    public ICollection<Nota> Notas { get; set; } = new List<Nota>();
}