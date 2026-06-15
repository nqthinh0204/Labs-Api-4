using AspLab04.Mvc.Data;
using AspLab04.Mvc.Options;
using AspLab04.Mvc.Repositories;
using AspLab04.Mvc.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AspLab04.Mvc.Controllers;

using Microsoft.AspNetCore.Mvc;

public class DataHealthController : Controller
{
    private readonly AppDbContext _context;
    private readonly IBookRepository _bookRepository;
    private readonly AppSettings _settings;

    public DataHealthController(AppDbContext context, IBookRepository bookRepository, IOptions<AppSettings> options)
    {
        _context = context;
        _bookRepository = bookRepository;
        _settings = options.Value;
    }

    public async Task<IActionResult> Index()
    {
        var model = new DataHealthViewModel
        {
            AppName = _settings.AppName,
            SupportEmail = _settings.SupportEmail,
            LowStockThreshold = _settings.LowStockThreshold
        };

        try
        {
            model.DatabaseConnected = await _context.Database.CanConnectAsync();
            model.MigrationApplied = !(await _context.Database.GetPendingMigrationsAsync()).Any();
            model.GenreCount = await _context.Genres.CountAsync();
            model.BookCount = await _context.Books.CountAsync();
            model.SaleCount = await _context.Sales.CountAsync();

            // Tracking query (có thể cập nhật sau)
            var trackingBooks = await _bookRepository.GetAllAsync();
            model.TrackingBooks = trackingBooks.Take(3).Select(b => new BookListItemViewModel
            {
                Id = b.Id, Title = b.Title, Quantity = b.Quantity, GenreName = b.Genre?.Name ?? ""
            }).ToList();

            // AsNoTracking query (chỉ đọc)
            var noTrackingBooks = await _bookRepository.GetAllReadOnlyAsync();
            model.NoTrackingBooks = noTrackingBooks.Take(3).Select(b => new BookListItemViewModel
            {
                Id = b.Id, Title = b.Title, Quantity = b.Quantity, GenreName = b.Genre?.Name ?? ""
            }).ToList();
        }
        catch
        {
            model.DatabaseConnected = false;
        }

        return View(model);
    }
}