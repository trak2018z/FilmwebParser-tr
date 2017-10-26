namespace FilmwebParser.Services
{
    public class ParseLinkResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Cover { get; set; }
    }
}