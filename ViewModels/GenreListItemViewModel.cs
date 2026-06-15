namespace AspLab04.Mvc.ViewModels;
public class GenreListItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BookCount { get; set; }
    public List<BookListItemViewModel> Books { get; set; } = new();
}