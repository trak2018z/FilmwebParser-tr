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
        public int Year { get; set; }
        public string Cover { get; set; }
    }
}
