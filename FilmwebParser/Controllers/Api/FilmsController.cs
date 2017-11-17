using AutoMapper;
using FilmwebParser.Models;
using FilmwebParser.Services;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmwebParser.Controllers.Api
{
    [Route("api/films")]
    [Authorize]
    public class FilmsController : Controller
    {
        private IFilmRepository _repository;
        private ILogger<FilmsController> _logger;
        private IParserService _parserService;

        public FilmsController(IFilmRepository repository, ILogger<FilmsController> logger, IParserService parserService)
        {
            _repository = repository;
            _logger = logger;
            _parserService = parserService;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetFilmsByUsername(User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<FilmViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas pobierania filmów: {ex}");
                return BadRequest("Wystąpił błąd podczas pobierania filmów");
            }
        }

        [HttpGet("{title}")]
        public IActionResult Get(string title)
        {
            try
            {
                var results = _repository.GetFilmByTitle(title);
                return Ok(Mapper.Map<FilmViewModel>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas pobierania filmów: {ex}");
                return BadRequest("Wystąpił błąd podczas pobierania filmów");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]FilmViewModel theFilm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newFilm = Mapper.Map<Film>(theFilm);
                    var result = _parserService.ParseLink(newFilm.Link);
                    if (!result.Success)
                        _logger.LogError(result.Message);
                    else
                    {
                        newFilm.Title = result.Title;
                        newFilm.Year = result.Year;
                        newFilm.Cover = result.Cover;
                        newFilm.UserName = User.Identity.Name;
                        _repository.AddFilm(newFilm);
                        if (await _repository.SaveChangesAsync())
                            return Created($"api/films/{newFilm.Title}", Mapper.Map<FilmViewModel>(newFilm));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Błąd podczas zapisywania filmu: {0}", ex);
            }
            return BadRequest("Wystąpił błąd podczas zapisywania filmu");
        }
    }
}
