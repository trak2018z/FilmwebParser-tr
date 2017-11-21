using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmwebParser.Models
{
    public class FilmRepository : IFilmRepository
    {
        private FilmContext _context;

        public FilmRepository(FilmContext context)
        {
            _context = context;
        }

        public string AddFilm(Film film)
        {
            if (film.Title == null)
                return "W podanym linku nie wykryto filmu";
            else if (!_context.Films.Any(t => t.Title == film.Title && t.UserName == film.UserName))
            {
                _context.Add(film);
                return string.Empty;
            }
            else
                return "Podany film znajduje się już w bazie";
        }

        public Film GetFilmByTitle(string filmTitle)
        {
            return _context.Films
                .Where(t => t.Title == filmTitle)
                .FirstOrDefault();
        }

        public IEnumerable<Film> GetFilmsByUsername(string name)
        {
            return _context.Films.Where(t => t.UserName == name).ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
