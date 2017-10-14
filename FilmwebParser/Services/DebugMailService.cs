using System.Diagnostics;

namespace FilmwebParser.Services
{
    public class DebugMailService : IMailService
    {
        public void SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Wysyłanie e-maila: do {to}, od {from}, temat {subject}.");
        }
    }
}
