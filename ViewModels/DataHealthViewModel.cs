namespace AspLab04.Mvc.ViewModels;
public class DataHealthViewModel
{
    public bool DatabaseConnected { get; set; }
    public bool MigrationApplied { get; set; }
    public int GenreCount { get; set; }
    public int BookCount { get; set; }
    public int SaleCount { get; set; }
    public List<BookListItemViewModel> TrackingBooks { get; set; } = new();
    public List<BookListItemViewModel> NoTrackingBooks { get; set; } = new();
    public string AppName { get; set; } = string.Empty;
    public string SupportEmail { get; set; } = string.Empty;
    public int LowStockThreshold { get; set; }
}