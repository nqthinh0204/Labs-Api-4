using AspLab04.Mvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookstoreMvc.Controllers;

public class HomeController : Controller
{
    private readonly AppSettings _settings;

    public HomeController(IOptions<AppSettings> options)
    {
        _settings = options.Value;
    }

    public IActionResult Index()
    {
        ViewData["AppName"] = _settings.AppName;
        ViewData["SupportEmail"] = _settings.SupportEmail;
        return View();
    }
}