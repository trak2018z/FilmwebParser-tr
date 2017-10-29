using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FilmwebParser.Models
{
    public class FilmContextSeedData
    {
        private FilmContext _context;
        private UserManager<FilmUser> _userManager;

        public FilmContextSeedData(FilmContext context, UserManager<FilmUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            //foreach (var entity in _context.Films)
            //    _context.Films.Remove(entity);
            //await _context.SaveChangesAsync();

            if (await _userManager.FindByEmailAsync("damian@gmail.com") == null)
            {
                var user = new FilmUser()
                {
                    UserName = "Damian",
                    Email = "damian@gmail.com"
                };
                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }
            if (!_context.Films.Any())
            {
                var sample1 = new Film()
                {
                    DateAdded = DateTime.UtcNow,
                    Link = "http://www.filmweb.pl/film/Cobra-1986-4625",
                    UserName = "Damian",
                    Title = "Cobra",
                    Year = 1986,
                    Cover = "http://1.fwcdn.pl/po/46/25/4625/7464974.3.jpg"
                };
                _context.Films.Add(sample1);

                var sample2 = new Film()
                {
                    DateAdded = DateTime.UtcNow,
                    Link = "http://www.filmweb.pl/film/Obecno%C5%9B%C4%87-2013-627100",
                    UserName = "Damian",
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
