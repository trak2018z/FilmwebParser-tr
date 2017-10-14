using FilmwebParser.Services;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FilmwebParser.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;

        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
        }

        public IActionResult Index()
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
    }
}
