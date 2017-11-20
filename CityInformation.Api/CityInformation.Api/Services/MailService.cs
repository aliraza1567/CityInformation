using System.Diagnostics;

namespace CityInformation.Api.Services
{
    public class MailService: IMailService
    {
        private readonly string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private readonly string _mailFrom = Startup.Configuration["mailSettings:MailFromAddress"];

        public void Send(string subject, string message)
        {
            Debug.Write($"Mail From: {_mailFrom}, Mail To: {_mailTo}");
            Debug.Write($"Subject: {subject}");
            Debug.Write($"Message: {message}");
        }
    }
}
