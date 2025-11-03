using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Infrastructure.Data;

namespace SchoolManager.Infrastructure.Repositories;

public class AlunoRepository(SchoolContext context) : IAlunoRepository
{
    private readonly SchoolContext _context = context;

    public async Task<IEnumerable<Aluno>> GetAllAsync()
        => await _context.Alunos.AsNoTracking().ToListAsync();

    public async Task<Aluno?> GetByIdAsync(int id)
        => await _context.Alunos.FindAsync(id);

    public async Task AddAsync(Aluno aluno)
    {
        await _context.Alunos.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aluno aluno)
    {
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Aluno aluno)
    {
        _context.Alunos.Remove(aluno);
        await _context.SaveChangesAsync();
    }
}