using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FilmwebParser.Models
{
    public class FilmUser : IdentityUser
    {
        public static explicit operator FilmUser(ClaimsPrincipal v)
        {
            throw new NotImplementedException();
        }
    }
}
