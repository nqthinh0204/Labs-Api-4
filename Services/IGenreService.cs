using AspLab04.Mvc.ViewModels;

namespace AspLab04.Mvc.Services;

public interface IGenreService
{
    Task<List<GenreListItemViewModel>> GetGenreListAsync();
}