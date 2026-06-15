namespace AspLab04.Mvc.Models;

public class Book
{
    public int Id { get; set; }
    public string BookCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int MinStock { get; set; } = 3;
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}