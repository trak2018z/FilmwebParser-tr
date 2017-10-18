using System;
using System.Collections.Generic;

namespace FilmwebParser.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserName { get; set; }
        public ICollection<FilmDetail> FilmDetails { get; set; }
    }
}
