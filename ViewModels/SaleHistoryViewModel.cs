namespace AspLab04.Mvc.ViewModels;
public class SaleHistoryViewModel
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public int ItemCount { get; set; }
    public List<SaleItemViewModel> Items { get; set; } = new();
}

public class SaleItemViewModel
{
    public string BookTitle { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal => Quantity * UnitPrice;
}