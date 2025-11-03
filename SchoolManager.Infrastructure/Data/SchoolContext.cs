using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Entities;
using SchoolManager.Infrastructure.Converters;

namespace SchoolManager.Infrastructure.Data;

public class SchoolContext(DbContextOptions<SchoolContext> options) : DbContext(options)
{
    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Disciplina> Disciplinas => Set<Disciplina>();
    public DbSet<Nota> Notas => Set<Nota>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Aluno>()
            .Property(a => a.DataNascimento)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>();

        modelBuilder.Entity<Nota>()
        .HasOne(n => n.Aluno)
        .WithMany(a => a.Notas)
        .HasForeignKey(n => n.AlunoId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Nota>()
            .HasOne(n => n.Disciplina)
            .WithMany(d => d.Notas)
            .HasForeignKey(n => n.DisciplinaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}