using AspLab04.Mvc.Options;
using AspLab04.Mvc.Repositories;
using AspLab04.Mvc.ViewModels;
using Microsoft.Extensions.Options;

namespace AspLab04.Mvc.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly AppSettings _settings;

    public BookService(IBookRepository bookRepository, IOptions<AppSettings> options)
    {
        _bookRepository = bookRepository;
        _settings = options.Value;
    }

    public async Task<List<BookListItemViewModel>> GetBookListAsync()
    {
        var books = await _bookRepository.GetAllReadOnlyAsync();
        return books.Select(b => new BookListItemViewModel
        {
            Id = b.Id,
            BookCode = b.BookCode,
            Title = b.Title,
            Author = b.Author,
            Price = b.Price,
            Quantity = b.Quantity,
            MinStock = b.MinStock,
            GenreName = b.Genre?.Name ?? "N/A",
            IsLowStock = b.Quantity > 0 && b.Quantity <= Math.Max(b.MinStock, _settings.LowStockThreshold)
        }).ToList();
    }

    public async Task<BookListItemViewModel?> GetBookDetailAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) return null;
        return new BookListItemViewModel
        {
            Id = book.Id,
            BookCode = book.BookCode,
            Title = book.Title,
            Author = book.Author,
            Price = book.Price,
            Quantity = book.Quantity,
            GenreName = book.Genre?.Name ?? "N/A",
            IsLowStock = book.Quantity <= _settings.LowStockThreshold
        };
    }

    public async Task<BookFilterViewModel> FilterBooksAsync(int? genreId, decimal? minPrice, decimal? maxPrice)
    {
        var books = await _bookRepository.FilterAsync(genreId, minPrice, maxPrice);
        var allBooks = await _bookRepository.GetAllReadOnlyAsync();
        var genres = allBooks.GroupBy(b => new { b.GenreId, Name = b.Genre?.Name ?? "N/A" })
            .Select(g => new GenreListItemViewModel { Id = g.Key.GenreId, Name = g.Key.Name, BookCount = g.Count() })
            .ToList();

        return new BookFilterViewModel
        {
            GenreId = genreId,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Books = books.Select(b => new BookListItemViewModel
            {
                Id = b.Id,
                BookCode = b.BookCode,
                Title = b.Title,
                Author = b.Author,
                Price = b.Price,
                Quantity = b.Quantity,
                GenreName = b.Genre?.Name ?? "N/A",
                IsLowStock = b.Quantity <= _settings.LowStockThreshold
            }).ToList(),
            Genres = genres
        };
    }
}