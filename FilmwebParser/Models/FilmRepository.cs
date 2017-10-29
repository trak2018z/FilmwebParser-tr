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
            _logger.LogInformation("Pobieranie wszystkich filmów z bazy danych");
            return _context.Films.ToList();
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
