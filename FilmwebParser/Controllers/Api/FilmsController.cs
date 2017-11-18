using AutoMapper;
using FilmwebParser.Models;
using FilmwebParser.Services;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private IParserService _parserService;

        public FilmsController(IFilmRepository repository, IParserService parserService)
        {
            _repository = repository;
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
                return BadRequest("Wystąpił błąd podczas pobierania listy filmów: " + ex.Message);
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
                return BadRequest("Wystąpił błąd podczas pobierania szczegółów filmu: " + ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]FilmViewModel theFilm)
        {
            string exception = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    var newFilm = Mapper.Map<Film>(theFilm);
                    var result = _parserService.ParseLink(newFilm.Link);
                    if (result.Success)
                    {
                        newFilm.UserName = User.Identity.Name;
                        newFilm.Title = result.Title;
                        newFilm.OriginalTitle = result.OriginalTitle;
                        newFilm.Cover = result.Cover;
                        newFilm.Director = result.Director;
                        newFilm.Screenplay = result.Screenplay;
                        newFilm.Genre = result.Genre;
                        newFilm.Country = result.Country;
                        newFilm.ReleaseDate = result.ReleaseDate;
                        newFilm.Cast = result.Cast;
                        newFilm.Description = result.Description;
                        _repository.AddFilm(newFilm);
                        if (await _repository.SaveChangesAsync())
                            return Created($"api/films/{newFilm.Title}", Mapper.Map<FilmViewModel>(newFilm));
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex.Message;
            }
            return BadRequest("Wystąpił błąd podczas zapisywania filmu: " + exception);
        }
    }
}
