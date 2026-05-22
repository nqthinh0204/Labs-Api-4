namespace AspLab02.Mvc.ViewModels;

public class BookDetailViewModel
{
    public int Id { get; set; }

    public string Isbn { get; set; } = "";

    public string Title { get; set; } = "";

    public string Category { get; set; } = "";

    public string Author { get; set; } = "";

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int MinStock { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public string PriceText => $"{Price:N0} VND";

    public decimal InventoryValue => Price * Quantity;

    public string InventoryValueText => $"{InventoryValue:N0} VND";

    public string LastUpdatedText => LastUpdatedAt.ToString("dd/MM/yyyy HH:mm");

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

    public string RestockSuggestion
    {
        get
        {
            if (Quantity <= 0)
            {
                return "Need to restock immediately because this book is out of stock.";
            }

            if (Quantity <= MinStock)
            {
                return $"Need more copies. Current quantity is {Quantity}, minimum stock is {MinStock}.";
            }

            return "Inventory level is stable.";
        }
    }
}