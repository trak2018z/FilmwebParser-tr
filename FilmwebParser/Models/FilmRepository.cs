using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmwebParser.Models
{
    public class FilmRepository : IFilmRepository
    {
        private FilmContext _context;
        private ILogger<FilmRepository> _logger;

        public FilmRepository(FilmContext context, ILogger<FilmRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddFilm(Film film)
        {
            _context.Add(film);
        }

        public IEnumerable<Film> GetAllFilms()
        {
            _logger.LogInformation("Getting all films from the database");
            return _context.Films.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
