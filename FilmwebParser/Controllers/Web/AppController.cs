﻿using FilmwebParser.Models;
using FilmwebParser.Services;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace FilmwebParser.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IFilmRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService, IConfigurationRoot config, IFilmRepository repository, ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var data = _repository.GetAllFilms();
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get films in Index page: {ex.Message}");
                return Redirect("/error");
            }
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
