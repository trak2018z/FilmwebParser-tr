using System.ComponentModel.DataAnnotations;

namespace FilmwebParser.ViewModels
{
    public class SettingsViewModel
    {
        [Required, RegularExpression(@"^.+\.(jpg|png)$", ErrorMessage = "Niepoprawny link")]
        public string Avatar { get; set; }
    }
}
