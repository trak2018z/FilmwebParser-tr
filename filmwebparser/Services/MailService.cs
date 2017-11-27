using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace FilmwebParser.Services
{
    public class MailService : IMailService
    {
        private IConfigurationRoot _config;

        public MailService(IConfigurationRoot config)
        {
            _config = config;
        }

        public string SendMail(string name, string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_config["MailSettings:NameFrom"], _config["MailSettings:EmailFrom"]));
                emailMessage.To.Add(new MailboxAddress(_config["MailSettings:NameTo"], _config["MailSettings:EmailTo"]));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("plain")
                {
                    Text = "Nadawca: " + name + " (" + email + ")" + Environment.NewLine + message
                };
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(_config["MailSettings:Host"], Int32.Parse(_config["MailSettings:Port"]), true);
                    client.Authenticate(_config["MailSettings:EmailFrom"], _config["MailSettings:Password"]);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
                return "Wiadomość została wysłana";
            }
            catch (Exception ex)
            {
                return "Wystąpił błąd podczas wysyłania wiadomości: " + ex.Message;
            }
        }
    }
}

