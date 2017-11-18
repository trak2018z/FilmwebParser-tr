using System;
using System.ComponentModel.DataAnnotations;

namespace FilmwebParser.ViewModels
{
    public class FilmViewModel
    {
        [Required]
        public string Link { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
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
