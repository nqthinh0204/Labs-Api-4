using AspLab04.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspLab04.Mvc.Controllers;

public class BooksController : Controller
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetBookListAsync();
        return View(books);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var book = await _bookService.GetBookDetailAsync(id);
        if (book == null) return NotFound($"Không tìm thấy sách có id = {id}");
        return View(book);
    }

    public async Task<IActionResult> Filter(int? genreId, decimal? minPrice, decimal? maxPrice)
    {
        var model = await _bookService.FilterBooksAsync(genreId, minPrice, maxPrice);
        return View(model);
    }
}