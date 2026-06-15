using AspLab04.Mvc.Services;
using AspLab04.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspLab04.Mvc.Controllers;

public class SalesController : Controller
{
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = await _saleService.GetCreateFormAsync();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SaleCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var form = await _saleService.GetCreateFormAsync();
            model.AvailableBooks = form.AvailableBooks;
            return View(model);
        }

        var (success, message) = await _saleService.CreateSaleAsync(model);
        if (!success)
        {
            ModelState.AddModelError("", message);
            var form = await _saleService.GetCreateFormAsync();
            model.AvailableBooks = form.AvailableBooks;
            return View(model);
        }

        TempData["SuccessMessage"] = message;
        return RedirectToAction(nameof(Success));
    }

    public IActionResult Success()
    {
        return View();
    }

    public async Task<IActionResult> History()
    {
        var history = await _saleService.GetSaleHistoryAsync();
        return View(history);
    }
}