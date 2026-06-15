using AspLab04.Mvc.Data;
using AspLab04.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace AspLab04.Mvc.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Genre>> GetAllWithBooksReadOnlyAsync()
        => _context.Genres.Include(g => g.Books).AsNoTracking().ToListAsync();
}