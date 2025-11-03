using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Infrastructure.Data;

namespace SchoolManager.Infrastructure.Repositories;

public class NotaRepository(SchoolContext context) : INotaRepository
{
    private readonly SchoolContext _context = context;

    public async Task<IEnumerable<Nota>> GetAllAsync()
        => await _context.Notas
            .Include(n => n.Aluno)
            .Include(n => n.Disciplina)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Nota?> GetByIdAsync(int id)
        => await _context.Notas
            .Include(n => n.Aluno)
            .Include(n => n.Disciplina)
            .FirstOrDefaultAsync(n => n.Id == id);

    public async Task AddAsync(Nota nota)
    {
        await _context.Notas.AddAsync(nota);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Nota nota)
    {
        _context.Notas.Update(nota);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Nota nota)
    {
        _context.Notas.Remove(nota);
        await _context.SaveChangesAsync();
    }
}