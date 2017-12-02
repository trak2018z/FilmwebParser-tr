using FilmwebParser.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
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
                if (IsImageUrl(link))
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
                else
                    return "Podany link nie zawiera obrazka";
            }
            catch (Exception ex)
            {
                return "Wystąpił błąd podczas zmieniania avataru: " + ex.Message;
            }
        }

        private bool IsImageUrl(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "HEAD";
            var resp = req.GetResponseAsync();
            resp.Wait();
            if (resp.Result.ContentType.StartsWith("image/"))
                return true;
            else
                return false;
        }
    }
}