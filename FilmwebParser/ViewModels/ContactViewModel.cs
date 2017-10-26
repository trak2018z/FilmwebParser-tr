using System.ComponentModel.DataAnnotations;

namespace FilmwebParser.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [StringLength(4096, MinimumLength = 10)]
        public string Message { get; set; }
    }
}
