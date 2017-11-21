using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmwebParser.Models
{
    public interface IFilmRepository
    {
        IEnumerable<Film> GetFilmsByUsername(string name);
        Film GetFilmByTitle(string filmTitle);
        string AddFilm(Film film);
        Task<bool> SaveChangesAsync();
    }
}