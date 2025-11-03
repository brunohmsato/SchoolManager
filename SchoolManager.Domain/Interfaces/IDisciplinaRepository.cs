using SchoolManager.Domain.Entities;

namespace SchoolManager.Domain.Interfaces;

public interface IDisciplinaRepository
{
    Task<IEnumerable<Disciplina>> GetAllAsync();

    Task<Disciplina?> GetByIdAsync(int id);

    Task AddAsync(Disciplina disciplina);

    Task UpdateAsync(Disciplina disciplina);

    Task DeleteAsync(Disciplina disciplina);
}