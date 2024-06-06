using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Visitor_Security_Clearance_System.Services
{
    public class EmailSender 
    {
        private readonly string _apiKey;

        public EmailSender()
        {
            _apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        }
        public async Task SendEmail(string subject,string fromEmail,string toEmail,string username,string message )
        {
           
            var client = new SendGridClient(_apiKey);
            var from = new SendGrid.Helpers.Mail.EmailAddress(fromEmail, "Visitory Security Clearance System");
            var to = new SendGrid.Helpers.Mail.EmailAddress(toEmail,username);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var iResponse = await client.SendEmailAsync(msg);
        }
    }
}
