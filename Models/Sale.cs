namespace AspLab04.Mvc.Models;

public class Sale
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}