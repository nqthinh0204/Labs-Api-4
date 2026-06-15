namespace AspLab04.Mvc.ViewModels;

public class BookFilterViewModel
{
    public int? GenreId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public List<BookListItemViewModel> Books { get; set; } = new();
    public List<GenreListItemViewModel> Genres { get; set; } = new();
}