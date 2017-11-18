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
                    UserName = "Damian",
                    Link = "http://www.filmweb.pl/film/Cobra-1986-4625",
                    Title = "Cobra",
                    OriginalTitle = string.Empty,
                    Cover = "http://1.fwcdn.pl/po/46/25/4625/7464974.3.jpg",
                    Director = "George P. Cosmatos",
                    Screenplay = "Sylvester Stallone",
                    Genre = "Kryminał, Sensacyjny",
                    Country = "USA",
                    ReleaseDate = "23 maja 1986 (świat)",
                    Cast = "Sylvester Stallone - Porucznik Marion 'Cobra' Cobretti" + Environment.NewLine + "Brigitte Nielsen - Ingrid" + Environment.NewLine + "Reni Santoni - Sierżant Gonzales",
                    Description = "Cobra (Sylvester Stallone), znany z niekonwencjonalnych metod działania policjant grupy specjalnej, wpada na trop groźnego mordercy (Brian Thompson), członka niebezpiecznej sekty. Natychmiast rozpoczyna śledztwo w tej sprawie, podejmując się jednocześnie ochrony pięknej modelki (Brigitte Nielsen) - jedynego świadka rytualnego zabójstwa, która ma zeznawać w procesie przeciwko mordercom. Cobra, przekonany, że ma do czynienia z ludźmi, którzy nie zasługują na humanitarne traktowanie, nie stroni od bardzo brutalnych działań, znacznie wykraczających poza policyjny regulamin ..."
                };
                _context.Films.Add(sample1);

                var sample2 = new Film()
                {
                    DateAdded = DateTime.UtcNow,
                    UserName = "Damian",
                    Link = "http://www.filmweb.pl/film/Obecno%C5%9B%C4%87-2013-627100",
                    Title = "Obecność",
                    OriginalTitle = "The Conjuring",
                    Cover = "http://1.fwcdn.pl/po/71/00/627100/7557683.3.jpg",
                    Director = "James Wan",
                    Screenplay = "Chad Hayes, Carey Hayes",
                    Genre = "Horror",
                    Country = "USA",
                    ReleaseDate = "26 lipca 2013 (Polska), 27 czerwca 2013 (świat)",
                    Cast = "Vera Farmiga - Lorraine Warren" + Environment.NewLine + "Patrick Wilson - Ed Warren" + Environment.NewLine + "Lili Taylor - Carolyn Perron",
                    Description = "Lorraine Warren (Vera Farmiga) i Ed Warren (Patrick Wilson) to badacze zjawisk paranormalnych. Pewnego dnia zostają poproszeni o pomoc przez przerażoną rodzinę mieszkającą w starym domu na uboczu. Okazuje się, że domownicy są nawiedzani przez tajemniczą zjawę, która nie chce dać im spokoju. Para badaczy staje do walki z najstraszniejszym demonem, z jakim kiedykolwiek miała do czynienia."
                };
                _context.Films.Add(sample2);

                await _context.SaveChangesAsync();
            }
        }
    }
}
