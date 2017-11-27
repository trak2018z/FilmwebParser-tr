using System.Security.Principal;

namespace FilmwebParser.Services
{
    public interface ISettingsService
    {
        string ChangeAvatar(IIdentity identity, string link);
    }
}
