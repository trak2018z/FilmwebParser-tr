using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmwebParser.Models
{
    public interface IFilmRepository
    {
        IEnumerable<Film> GetAllFilms();
        void AddFilm(Film film);
        Task<bool> SaveChangesAsync();
    }
}