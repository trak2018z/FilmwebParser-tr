using AutoMapper;
using FilmwebParser.Models;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmwebParser.Controllers.Api
{
    [Route("api/films")]
    public class FilmsController : Controller
    {
        private IFilmRepository _repository;
        private ILogger<FilmsController> _logger;

        public FilmsController(IFilmRepository repository, ILogger<FilmsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllFilms();
                return Ok(Mapper.Map<IEnumerable<FilmViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all films: {ex}");
                return BadRequest("Error ocurred");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]FilmViewModel theFilm)
        {
            if (ModelState.IsValid)
            {
                var newFilm = Mapper.Map<Film>(theFilm);
                _repository.AddFilm(newFilm);
                if (await _repository.SaveChangesAsync())
                    return Created($"api/films/{theFilm.Name}", Mapper.Map<FilmViewModel>(newFilm));
            }
            return BadRequest("Failed to save the film");
        }
    }
}
