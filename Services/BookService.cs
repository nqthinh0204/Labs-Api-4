using AspLab02.Mvc.Models;
using AspLab02.Mvc.ViewModels;

namespace AspLab02.Mvc.Services;

public class BookService
{
    private readonly List<Book> _books = new()
    {
        new Book
        {
            Id = 1,
            Isbn = "BK-001",
            Title = "Clean Code",
            Category = "Programming",
            Author = "Robert C. Martin",
            Price = 350000,
            Quantity = 10,
            MinStock = 3,
            LastUpdatedAt = new DateTime(2025, 5, 9, 9, 12, 0)
        },

        new Book
        {
            Id = 2,
            Isbn = "BK-002",
            Title = "Atomic Habits",
            Category = "Self-help",
            Author = "James Clear",
            Price = 280000,
            Quantity = 2,
            MinStock = 5,
            LastUpdatedAt = new DateTime(2025, 5, 9, 9, 12, 0)
        },

        new Book
        {
            Id = 3,
            Isbn = "BK-003",
            Title = "Deep Work",
            Category = "Productivity",
            Author = "Cal Newport",
            Price = 300000,
            Quantity = 0,
            MinStock = 4,
            LastUpdatedAt = new DateTime(2025, 5, 9, 9, 12, 0)
        },

        new Book
        {
            Id = 4,
            Isbn = "BK-004",
            Title = "The Pragmatic Programmer",
            Category = "Programming",
            Author = "Andrew Hunt",
            Price = 420000,
            Quantity = 8,
            MinStock = 3,
            LastUpdatedAt = new DateTime(2025, 5, 9, 9, 12, 0)
        },

        new Book
        {
            Id = 5,
            Isbn = "BK-005",
            Title = "Harry Potter",
            Category = "Fantasy",
            Author = "J.K. Rowling",
            Price = 500000,
            Quantity = 1,
            MinStock = 4,
            LastUpdatedAt = new DateTime(2025, 5, 9, 9, 12, 0)
        }
    };

    public List<Book> GetAll()
    {
        return _books;
    }

    public Book? GetById(int id)
    {
        return _books.FirstOrDefault(book => book.Id == id);
    }

    public BookStatsViewModel GetStats()
    {
        var totalBooks = _books.Count;

        var totalQuantity = _books.Sum(book => book.Quantity);

        var totalInventoryValue = _books.Sum(book =>
            book.Price * book.Quantity);

        var outOfStockCount = _books.Count(book =>
            book.Quantity <= 0);

        var lowStockCount = _books.Count(book =>
            book.Quantity > 0 && book.Quantity <= book.MinStock);

        return new BookStatsViewModel
        {
            TotalBooks = totalBooks,
            TotalQuantity = totalQuantity,
            TotalInventoryValue = totalInventoryValue,
            OutOfStockCount = outOfStockCount,
            LowStockCount = lowStockCount
        };
    }
}