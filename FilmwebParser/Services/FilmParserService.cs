using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace FilmwebParser.Services
{
    public class FilmParserService : IParserService
    {
        public ParseLinkResult ParseLink(string link)
        {
            var result = new ParseLinkResult()
            {
                Success = false,
                Message = "BŁĄD"
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
            try
            {
                var table = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'filmInfo')]/table");
                foreach (var tableItem in table.ChildNodes)
                {
                    switch (tableItem.ChildNodes[0].InnerText)
                    {
                        case "reżyseria:":
                            result.Director = GetDataFromTable(tableItem.ChildNodes[1].LastChild.InnerHtml);
                            break;
                        case "scenariusz:":
                            result.Screenplay = GetDataFromTable(tableItem.ChildNodes[1].LastChild.InnerHtml);
                            break;
                        case "gatunek:":
                            result.Genre = GetDataFromTable(tableItem.ChildNodes[1].LastChild.InnerHtml);
                            break;
                        case "produkcja:":
                            result.Country = GetDataFromTable(tableItem.ChildNodes[1].LastChild.InnerHtml);
                            break;
                        case "premiera:":
                            var htmlWithDates = tableItem.ChildNodes[1].LastChild.InnerHtml;
                            List<string> dates = new List<string>();
                            HtmlDocument doc = new HtmlDocument();
                            doc.LoadHtml(htmlWithDates);
                            foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//span"))
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
                            break;
                    }
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

        private static string GetDataFromTable(string htmlWithData)
        {
            List<string> listWithData = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlWithData);
            foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//li"))
                listWithData.Add(HtmlEntity.DeEntitize(li.InnerText));
            return String.Join(", ", listWithData);
        }
    }
}
