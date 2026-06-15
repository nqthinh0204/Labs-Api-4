using AspLab04.Mvc.Data;
using AspLab04.Mvc.Models;
using AspLab04.Mvc.Repositories;
using AspLab04.Mvc.ViewModels;

namespace AspLab04.Mvc.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBookRepository _bookRepository;
    private readonly AppDbContext _context;

    public SaleService(ISaleRepository saleRepository, IBookRepository bookRepository, AppDbContext context)
    {
        _saleRepository = saleRepository;
        _bookRepository = bookRepository;
        _context = context;
    }

    public async Task<(bool Success, string Message)> CreateSaleAsync(SaleCreateViewModel model)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Dùng tracking để cập nhật Quantity
            var book = await _bookRepository.GetByIdAsync(model.BookId);
            if (book == null)
                return (false, "Không tìm thấy sách.");

            if (book.Quantity < model.Quantity)
                return (false, $"Sách \"{book.Title}\" chỉ còn {book.Quantity} bản, không đủ số lượng yêu cầu.");

            book.Quantity -= model.Quantity;

            var sale = new Sale
            {
                CustomerName = model.CustomerName,
                CreatedAt = DateTime.Now,
                TotalAmount = book.Price * model.Quantity
            };
            sale.SaleItems.Add(new SaleItem
            {
                BookId = book.Id,
                Quantity = model.Quantity,
                UnitPrice = book.Price
            });

            await _saleRepository.AddAsync(sale);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return (true, $"Đã tạo hóa đơn thành công! Tổng tiền: {sale.TotalAmount:N0} VNĐ");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi ghi dữ liệu, đã rollback. Chi tiết: {ex.Message}");
        }
    }

    public async Task<List<SaleHistoryViewModel>> GetSaleHistoryAsync()
    {
        var sales = await _saleRepository.GetAllReadOnlyAsync();
        return sales.Select(s => new SaleHistoryViewModel
        {
            Id = s.Id,
            CreatedAt = s.CreatedAt,
            CustomerName = s.CustomerName,
            TotalAmount = s.TotalAmount,
            ItemCount = s.SaleItems.Sum(si => si.Quantity),
            Items = s.SaleItems.Select(si => new SaleItemViewModel
            {
                BookTitle = si.Book?.Title ?? "N/A",
                Quantity = si.Quantity,
                UnitPrice = si.UnitPrice
            }).ToList()
        }).OrderByDescending(s => s.CreatedAt).ToList();
    }

    public async Task<SaleCreateViewModel> GetCreateFormAsync()
    {
        var books = await _bookRepository.GetAllReadOnlyAsync();
        return new SaleCreateViewModel
        {
            AvailableBooks = books.Where(b => b.Quantity > 0).Select(b => new BookListItemViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                Quantity = b.Quantity,
                GenreName = b.Genre?.Name ?? "N/A"
            }).ToList()
        };
    }
}