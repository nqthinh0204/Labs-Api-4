using AspLab04.Mvc.Models;

namespace AspLab04.Mvc.Repositories;

public interface IGenreRepository
{
    Task<List<Genre>> GetAllWithBooksReadOnlyAsync();
}