using FilmwebParser.Models;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmwebParser.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<FilmUser> _signInManager;
        private UserManager<FilmUser> _userManager;

        public AuthController(SignInManager<FilmUser> signInManager, UserManager<FilmUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
