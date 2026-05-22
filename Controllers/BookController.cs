using AspLab02.Mvc.Models;
using AspLab02.Mvc.Services;
using AspLab02.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspLab02.Mvc.Controllers;

public class BooksController : Controller
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    public IActionResult Index()
    {
        var books = _bookService.GetAll()
            .Select(ToListItemViewModel)
            .ToList();

        return View(books);
    }

    public IActionResult Detail(int id)
    {
        var book = _bookService.GetById(id);

        if (book == null)
        {
            return NotFound($"Cannot find book with id = {id}");
        }

        var viewModel = ToDetailViewModel(book);

        return View(viewModel);
    }

    public IActionResult Stats()
    {
        var stats = _bookService.GetStats();

        return View(stats);
    }

    public IActionResult Welcome()
    {
        return Content("Welcome to Mini Bookstore Catalog MVC");
    }

    public IActionResult BookJson()
    {
        var books = _bookService.GetAll()
            .Select(book => new
            {
                book.Id,
                book.Isbn,
                book.Title,
                book.Category,
                book.Author,
                book.Price,
                book.Quantity
            });

        return Json(books);
    }

    public IActionResult GoToList()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Force404()
    {
        return NotFound("This is a demo 404 response from Force404 action.");
    }

    private static BookListItemViewModel ToListItemViewModel(Book book)
    {
        return new BookListItemViewModel
        {
            Id = book.Id,
            Isbn = book.Isbn,
            Title = book.Title,
            Category = book.Category,
            Author = book.Author,
            Price = book.Price,
            Quantity = book.Quantity,
            MinStock = book.MinStock
        };
    }

    private static BookDetailViewModel ToDetailViewModel(Book book)
    {
        return new BookDetailViewModel
        {
            Id = book.Id,
            Isbn = book.Isbn,
            Title = book.Title,
            Category = book.Category,
            Author = book.Author,
            Price = book.Price,
            Quantity = book.Quantity,
            MinStock = book.MinStock,
            LastUpdatedAt = book.LastUpdatedAt
        };
    }
}