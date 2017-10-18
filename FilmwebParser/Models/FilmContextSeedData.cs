using System;
using System.Collections.Generic;
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
                    Name = "Old Hits",
                    UserName = "", //TODO Add UserName
                    FilmDetails = new List<FilmDetail>()
                    {
                        new FilmDetail(){ Title="Cobra", Cover="http://1.fwcdn.pl/po/46/25/4625/7464974.3.jpg",Year=1986,Order=0},
                        new FilmDetail(){ Title="Rocky", Cover="http://1.fwcdn.pl/po/91/90/9190/7530897.6.jpg",Year=1976,Order=1},
                        new FilmDetail(){ Title="Scarface", Cover="http://1.fwcdn.pl/po/58/78/605878/7346684.6.jpg",Year=1983,Order=2}
                    }
                };
                _context.Films.Add(sample1);
                _context.FilmDetails.AddRange(sample1.FilmDetails);

                var sample2 = new Film()
                {
                    DateAdded = DateTime.UtcNow,
                    Name = "Horrors",
                    UserName = "", //TODO Add UserName
                    FilmDetails = new List<FilmDetail>()
                    {
                        new FilmDetail(){ Title="The Conjuring", Cover="http://1.fwcdn.pl/po/71/00/627100/7557683.3.jpg",Year=2013,Order=0},
                        new FilmDetail(){ Title="The Exorcist", Cover="http://1.fwcdn.pl/po/13/15/1315/7368591.3.jpg",Year=1973,Order=1},
                        new FilmDetail(){ Title="The Shining", Cover="http://1.fwcdn.pl/po/10/20/1020/7183753.3.jpg",Year=1980,Order=2}
                    }
                };
                _context.Films.Add(sample2);
                _context.FilmDetails.AddRange(sample2.FilmDetails);

                await _context.SaveChangesAsync();
            }
        }
    }
}
