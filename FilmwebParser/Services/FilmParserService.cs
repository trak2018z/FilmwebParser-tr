using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
                HtmlNode polishTitle = document.DocumentNode.SelectSingleNode("//h1[contains(@class, 'filmTitle')]/a");
                result.Title = HtmlEntity.DeEntitize(polishTitle.ChildNodes[0].InnerHtml);
            }
            catch { }
            try
            {
                HtmlNode originalTitle = document.DocumentNode.SelectSingleNode("//h2[contains(@class, 'cap')]");
                result.OriginalTitle = HtmlEntity.DeEntitize(originalTitle.ChildNodes[0].InnerHtml);
            }
            catch { }
            try
            {
                HtmlNode cover = document.DocumentNode.SelectSingleNode("//meta[contains(@property, 'og:image')]");
                result.Cover = cover.Attributes["content"].Value;
            }
            catch { }
            var table = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'filmInfo')]/table");
            try
            {
                string htmlWithDirectors = table.SelectNodes("tr/td")[0].InnerHtml;
                List<string> directors = new List<string>();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlWithDirectors);
                foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//li"))
                    directors.Add(HtmlEntity.DeEntitize(li.InnerText));
                result.Director = String.Join(", ", directors);
            }
            catch { }
            try
            {
                string htmlWithScreenplays = table.SelectNodes("tr/td")[1].InnerHtml;
                List<string> screenplays = new List<string>();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlWithScreenplays);
                foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//li"))
                    screenplays.Add(HtmlEntity.DeEntitize(li.InnerText));
                result.Screenplay = String.Join(", ", screenplays);
            }
            catch { }
            try
            {
                string htmlWithGenres = table.SelectNodes("tr/td")[3].InnerHtml;
                List<string> genres = new List<string>();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlWithGenres);
                foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//li"))
                    genres.Add(HtmlEntity.DeEntitize(li.InnerText));
                result.Genre = String.Join(", ", genres);
            }
            catch { }
            try
            {
                string htmlWithCountries = table.SelectNodes("tr/td")[4].InnerHtml;
                List<string> countries = new List<string>();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlWithCountries);
                foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//li"))
                    countries.Add(HtmlEntity.DeEntitize(li.InnerText));
                result.Country = String.Join(", ", countries);
            }
            catch { }
            try
            {
                string htmlWithDates = table.SelectNodes("tr/td")[5].InnerHtml;
                List<string> dates = new List<string>();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlWithDates);
                foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//a//span"))
                    dates.Add(HtmlEntity.DeEntitize(li.InnerText.TrimStart().TrimEnd()));
                if (dates.Count == 2)
                {
                    dates[0] = dates[0] + " (Polska)";
                    dates[1] = dates[1] + " (świat)";
                    result.ReleaseDate = String.Join(", ", dates);
                }
                else
                {
                    dates[0] = dates[0] + " (świat)";
                    result.ReleaseDate = String.Join(", ", dates);
                }
            }
            catch { }
            try
            {
                string cast = string.Empty;
                var table2 = document.DocumentNode.SelectSingleNode("//table[contains(@class, 'filmCast')]");
                foreach (HtmlNode row in table2.SelectNodes("tr/td[position() = 4]"))
                    cast += (HtmlEntity.DeEntitize(row.InnerText.TrimStart().TrimEnd().Replace("   ", " - ") + Environment.NewLine));
                result.Cast = cast;
            }
            catch { }
            try
            {
                HtmlDocument doc = web.Load(link + "/descs");
                HtmlNode description = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'text')]");
                result.Description = HtmlEntity.DeEntitize(description.InnerText);
            }
            catch { }
            result.Success = true;
            result.Message = "SUKCES";
            return result;
        }
    }
}
