using AspLab04.Mvc.Models;

namespace AspLab04.Mvc.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<List<Book>> GetAllReadOnlyAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<List<Book>> FilterAsync(int? genreId, decimal? minPrice, decimal? maxPrice);
    Task AddAsync(Book book);
    Task SaveChangesAsync();
}