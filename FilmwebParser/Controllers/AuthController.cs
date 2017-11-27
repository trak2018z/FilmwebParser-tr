using FilmwebParser.Models;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FilmwebParser.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<FilmUser> _signInManager;
        private UserManager<FilmUser> _userManager;
        private IPasswordHasher<FilmUser> _hasher;
        private IConfigurationRoot _config;

        public AuthController(SignInManager<FilmUser> signInManager, UserManager<FilmUser> userManager, IPasswordHasher<FilmUser> hasher, IConfigurationRoot config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _hasher = hasher;
            _config = config;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Films", "App");
            return View();
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Films", "App");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
                if (signInResult.Succeeded)
                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("Films", "App");
                    else
                        return Redirect(returnUrl);
                else
                    ModelState.AddModelError("", "Wpisany login lub hasło nie są poprawne");
            }
            return View();
        }

        [HttpPost("Auth/Token")]
        public async Task<IActionResult> CreateToken([FromBody]LoginViewModel vm)
        {
            var user = await _userManager.FindByNameAsync(vm.Username);
            if (user != null)
            {
                if (_hasher.VerifyHashedPassword(user, user.PasswordHash, vm.Password) == PasswordVerificationResult.Success)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: _config["Tokens:Issuer"],
                        audience: _config["Tokens:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(15),
                        signingCredentials: creds
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }
            return BadRequest("Wystąpił błąd podczas tworzenia tokenu");
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
                await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new FilmUser { UserName = vm.Username, Email = vm.Email };
                var signUpResult = await _userManager.CreateAsync(user, vm.Password);
                if (signUpResult.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("Avatar", "http://funkyimg.com/i/2zK9A.png"));
                    await _signInManager.SignInAsync(user, false);
                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("Films", "App");
                    else
                        return Redirect(returnUrl);
                }
                else
                    foreach (var error in signUpResult.Errors)
                        ModelState.AddModelError("", error.Description);
            }
            return View();
        }
    }
}
