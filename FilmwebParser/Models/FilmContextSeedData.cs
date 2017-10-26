using System;
using System.Linq;
using System.Threading.Tasks;

namespace FilmwebParser.Models
{
    public class FilmContextSeedData
    {
        private FilmContext _context;

        public FilmContextSeedData(FilmContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.Films.Any())
            {
                var sample1 = new Film()
                {
                    DateAdded = DateTime.UtcNow,
                    Link = "http://www.filmweb.pl/film/Cobra-1986-4625",
                    UserName = "", //TODO Add UserName
                    Title = "Cobra",
                    Year = 1986,
                    Cover = "http://1.fwcdn.pl/po/46/25/4625/7464974.3.jpg"
                };
                _context.Films.Add(sample1);

                var sample2 = new Film()
                {
                    DateAdded = DateTime.UtcNow,
                    Link = "http://www.filmweb.pl/film/Obecno%C5%9B%C4%87-2013-627100",
                    UserName = "", //TODO Add UserName
                    Title = "The Conjuring",
                    Year = 2013,
                    Cover = "http://1.fwcdn.pl/po/71/00/627100/7557683.3.jpg"
                };
                _context.Films.Add(sample2);

                await _context.SaveChangesAsync();
            }
        }
    }
}
