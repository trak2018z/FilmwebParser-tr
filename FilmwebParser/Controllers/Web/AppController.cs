using FilmwebParser.Services;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmwebParser.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private ISettingsService _settingsService;

        public AppController(IMailService mailService, ISettingsService settingsService)
        {
            _mailService = mailService;
            _settingsService = settingsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Films()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                string sendResult = _mailService.SendMail(model.Name, model.Email, model.Subject, model.Message);
                ModelState.Clear();
                ViewBag.UserMessage = sendResult;
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Settings(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                string changeResult = _settingsService.ChangeAvatar(User.Identity, model.Avatar);
                ModelState.Clear();
                ViewBag.UserMessage = changeResult;
            }
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        [Authorize]
        public IActionResult Settings()
        {
            return View();
        }
    }
}
