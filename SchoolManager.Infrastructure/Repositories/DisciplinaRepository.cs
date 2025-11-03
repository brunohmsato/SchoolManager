using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Infrastructure.Data;

namespace SchoolManager.Infrastructure.Repositories;

public class DisciplinaRepository(SchoolContext context) : IDisciplinaRepository
{
    private readonly SchoolContext _context = context;

    public async Task<IEnumerable<Disciplina>> GetAllAsync()
        => await _context.Disciplinas.AsNoTracking().ToListAsync();

    public async Task<Disciplina?> GetByIdAsync(int id)
        => await _context.Disciplinas.FindAsync(id);

    public async Task AddAsync(Disciplina disciplina)
    {
        await _context.Disciplinas.AddAsync(disciplina);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Disciplina disciplina)
    {
        _context.Disciplinas.Update(disciplina);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Disciplina disciplina)
    {
        _context.Disciplinas.Remove(disciplina);
        await _context.SaveChangesAsync();
    }
}