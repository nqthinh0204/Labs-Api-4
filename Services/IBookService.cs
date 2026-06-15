using AspLab04.Mvc.ViewModels;

namespace AspLab04.Mvc.Services;

public interface IBookService
{
    Task<List<BookListItemViewModel>> GetBookListAsync();
    Task<BookListItemViewModel?> GetBookDetailAsync(int id);
    Task<BookFilterViewModel> FilterBooksAsync(int? genreId, decimal? minPrice, decimal? maxPrice);
}