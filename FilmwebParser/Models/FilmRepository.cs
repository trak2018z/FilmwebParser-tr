using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Film> GetAllFilms()
        {
            _logger.LogInformation("Getting all films from the database");
            return _context.Films.ToList();
        }
    }
}
