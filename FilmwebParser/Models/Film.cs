using System;

namespace FilmwebParser.Models
{
    public class Film
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string Link { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Cover { get; set; }
    }
}
