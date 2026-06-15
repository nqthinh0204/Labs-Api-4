using System.ComponentModel.DataAnnotations;

namespace AspLab04.Mvc.ViewModels;

public class SaleCreateViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
    [MaxLength(150)]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn sách")]
    public int BookId { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "Số lượng phải từ 1 đến 100")]
    public int Quantity { get; set; } = 1;

    public List<BookListItemViewModel> AvailableBooks { get; set; } = new();
}