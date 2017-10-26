using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

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
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(link);
            try
            {
                HtmlNode onlyYear = document.DocumentNode.SelectSingleNode("//span[contains(@class, 'halfSize')]");
                string rokFilmu = HtmlEntity.DeEntitize(onlyYear.ChildNodes[0].InnerHtml);
                result.Year = Int32.Parse(String.Join("", rokFilmu.Where(c => (c >= '0' && c <= '9') || c == '-')));
            }
            catch { }
            try
            {
                HtmlNode originalTitle = document.DocumentNode.SelectSingleNode("//h2[contains(@class, 'cap')]");
                result.Title = HtmlEntity.DeEntitize(originalTitle.ChildNodes[0].InnerHtml);
            }
            catch { }
            try
            {
                HtmlNode cover = document.DocumentNode.SelectSingleNode("//meta[contains(@property, 'og:image')]");
                result.Cover = cover.Attributes["content"].Value;
            }
            catch { }
            result.Success = true;
            result.Message = "SUKCES";
            return result;
        }
    }
}
