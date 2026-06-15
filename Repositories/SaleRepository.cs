using AspLab04.Mvc.Data;
using AspLab04.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace AspLab04.Mvc.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Sale>> GetAllReadOnlyAsync()
        => _context.Sales.Include(s => s.SaleItems).ThenInclude(si => si.Book).AsNoTracking().ToListAsync();

    public Task<Sale?> GetByIdAsync(int id)
        => _context.Sales.Include(s => s.SaleItems).ThenInclude(si => si.Book).FirstOrDefaultAsync(s => s.Id == id);

    public async Task AddAsync(Sale sale)
        => await _context.Sales.AddAsync(sale);

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}