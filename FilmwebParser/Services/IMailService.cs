namespace FilmwebParser.Services
{
    public interface IMailService
    {
        string SendMail(string name, string email, string subject, string message);
    }
}
