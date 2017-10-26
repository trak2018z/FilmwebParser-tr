using Microsoft.Extensions.Logging;

namespace FilmwebParser.Services
{
    public class FilmParserService : IParserService
    {
        private ILogger<FilmParserService> _logger;

        public FilmParserService(ILogger<FilmParserService> logger)
        {
            _logger = logger;
        }

        public ParseLinkResult ParseLink(string link)
        {
            var result = new ParseLinkResult()
            {
                Success = false,
                Message = "Failed"
            };
            result.Title = "Commando";
            result.Year = 1984;
            result.Cover = "http://1.fwcdn.pl/po/46/48/4648/7039934.3.jpg";
            result.Success = true;
            result.Message = "SUKCES";
            return result;
        }
    }
}
