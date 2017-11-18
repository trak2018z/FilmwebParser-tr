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

        public void AddFilm(Film film)
        {
            _context.Add(film);
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
