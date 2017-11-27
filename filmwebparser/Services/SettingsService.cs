using FilmwebParser.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace FilmwebParser.Services
{
    public class SettingsService : ISettingsService
    {
        private UserManager<FilmUser> _userInManager;

        public SettingsService(UserManager<FilmUser> userInManager)
        {
            _userInManager = userInManager;
        }

        public string ChangeAvatar(IIdentity identity, string link)
        {
            try
            {
                var user = _userInManager.FindByNameAsync(identity.Name);
                user.Wait();
                var userIdentity = ((ClaimsIdentity)identity);
                var removeClaim = _userInManager.RemoveClaimAsync(user.Result, userIdentity.FindFirst("Avatar"));
                removeClaim.Wait();
                var addClaim = _userInManager.AddClaimAsync(user.Result, new Claim("Avatar", link));
                addClaim.Wait();
                return "Avatar zmieniono poprawnie. Zmiany będą widoczne po ponownym zalogowaniu";
            }
            catch (Exception ex)
            {
                return "Wystąpił błąd podczas zmieniania avataru: " + ex.Message;
            }
        }
    }
}

