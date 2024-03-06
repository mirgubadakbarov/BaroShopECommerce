using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Web.Services.Abstract;
namespace Web.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(string to, string subject, string body, string from = null)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(from ?? _configuration.GetSection("Smtp:FromAddress").Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };


            //Send email

            var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_configuration.GetSection("Smtp:Server").Value,
               int.Parse(_configuration.GetSection("Smtp:Port").Value)
               , SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("Smtp:FromAddress").Value,
                _configuration.GetSection("Smtp:Password").Value);

            smtp.Send(email);
            smtp.Disconnect(true);






        }
    }
}
