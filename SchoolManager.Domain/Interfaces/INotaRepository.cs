using SchoolManager.Domain.Entities;

namespace SchoolManager.Domain.Interfaces;

public interface INotaRepository
{
    Task<IEnumerable<Nota>> GetAllAsync();

    Task<Nota?> GetByIdAsync(int id);

    Task AddAsync(Nota nota);

    Task UpdateAsync(Nota nota);

    Task DeleteAsync(Nota nota);
}