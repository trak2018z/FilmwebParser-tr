using FilmwebParser.Models;
using FilmwebParser.Services;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FilmwebParser.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IFilmRepository _repository;

        public AppController(IMailService mailService, IConfigurationRoot config, IFilmRepository repository)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
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
            if (model.Email.Contains("gmail.com"))
                ModelState.AddModelError("", "Skrzynka pocztowa gmail.com nie jest obsługiwana!");
            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "FilmwebParser", model.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Wiadomość została wysłana!";
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
