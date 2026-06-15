using AspLab04.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspLab04.Mvc.Controllers;

public class GenresController : Controller
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _genreService.GetGenreListAsync();
        return View(genres);
    }
}