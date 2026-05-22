namespace AspLab02.Mvc.ViewModels;

public class BookListItemViewModel
{
    public int Id { get; set; }

    public string Isbn { get; set; } = "";

    public string Title { get; set; } = "";

    public string Category { get; set; } = "";

    public string Author { get; set; } = "";

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int MinStock { get; set; }

    public string PriceText => $"{Price:N0} VND";

    public decimal InventoryValue => Price * Quantity;

    public string InventoryValueText => $"{InventoryValue:N0} VND";

    public string StockStatus
    {
        get
        {
            if (Quantity <= 0)
            {
                return "Out of Stock";
            }

            if (Quantity <= MinStock)
            {
                return "Low Stock";
            }

            return "Available";
        }
    }

    public string StockStatusClass
    {
        get
        {
            if (Quantity <= 0)
            {
                return "badge badge-danger";
            }

            if (Quantity <= MinStock)
            {
                return "badge badge-warning";
            }

            return "badge badge-success";
        }
    }
}