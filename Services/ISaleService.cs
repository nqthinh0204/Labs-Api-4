using AspLab04.Mvc.ViewModels;

namespace AspLab04.Mvc.Services;

public interface ISaleService
{
    Task<(bool Success, string Message)> CreateSaleAsync(SaleCreateViewModel model);
    Task<List<SaleHistoryViewModel>> GetSaleHistoryAsync();
    Task<SaleCreateViewModel> GetCreateFormAsync();
}