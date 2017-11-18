using System;

namespace FilmwebParser.Models
{
    public class Film
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserName { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Cover { get; set; }
        public string Director { get; set; }
        public string Screenplay { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public string ReleaseDate { get; set; }
        public string Cast { get; set; }
        public string Description { get; set; }
    }
}
