using System.Collections.Generic;

namespace FilmwebParser.Models
{
    public interface IFilmRepository
    {
        IEnumerable<Film> GetAllFilms();
    }
}