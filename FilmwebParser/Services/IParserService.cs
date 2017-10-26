namespace FilmwebParser.Services
{
    public interface IParserService
    {
        ParseLinkResult ParseLink(string link);
    }
}
