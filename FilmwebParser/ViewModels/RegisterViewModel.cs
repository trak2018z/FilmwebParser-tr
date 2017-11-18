using System.ComponentModel.DataAnnotations;

namespace FilmwebParser.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
