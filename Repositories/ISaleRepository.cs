using AspLab04.Mvc.Models;

namespace AspLab04.Mvc.Repositories;

public interface ISaleRepository
{
    Task<List<Sale>> GetAllReadOnlyAsync();
    Task<Sale?> GetByIdAsync(int id);
    Task AddAsync(Sale sale);
    Task SaveChangesAsync();
}